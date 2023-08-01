#!/bin/bash
# set -exv

#set -o errexit
#set -o pipefail
#set -o nounset

# Logging
DEFAULT='\033[0;39m'
WHITE='\033[0;02m'
GREEN='\033[1;32m'
RED='\033[1;31m'

logDebug() {
    echo -e "${DEFAULT}${1}${DEFAULT}"
}

logSuccess() {
    echo -e "${GREEN}${1}${DEFAULT}"
}

logError() {
    echo -e "${RED}${1}${DEFAULT}"
}

showHelp() {
    cat 1>&2 <<EOF
USAGE:
    # Configures the Raspberry Pi with the latest stable version of PiWeatherStation
    sudo bash setup_weatherdisplay.sh
    
    # Configures the Raspberry Pi with the latest pre-release version of PiWeatherStation
    sudo bash setup_weatherdisplay.sh --pre

PARAMETERS:
    -h, --host          Sets the hostname of the system.
    -t, --timezone      Sets the timezone.
    -l, --locale        Sets the locale.
    -k, --keyboard      Sets the keyboard locale.
    -s, --systemDir     Sets the path to the system deamon directory (default: /etc/systemd/system).

FLAGS:
    -p, --pre           Downloads the latest pre-release of PiWeatherStation.
    -d, --debug         Prints verbose debug log messages.
    -n, --no-reboot     Does not reboot the system when the script ends.
    -h, --help          Show this help.

EOF
    exit 0
}

logDebug "
=====================================================
PiWeatherStation Setup Script [Version 1.0.0]
(c) superdev gmbh. All rights reserved.
=====================================================
" >&2

# Toggle 'debug' variable (true/false) in order to receive more verbose log output
debug=false
preRelease=false
reboot=true

usage_error () {
    logError >&2 "$(basename $0):  $1"; exit 2;
}

assert_argument () {
    test "$1" != "$EOL" || usage_error "$2 requires an argument";
}

# One loop, nothing more.
if [ "$#" != 0 ]; then
  EOL=$(printf '\1\3\3\7')
  set -- "$@" "$EOL"
  while [ "$1" != "$EOL" ]; do
    opt="$1"; shift
    case "$opt" in

      -h|--host) assert_argument "$1" "$opt"; host="$1"; shift;;
      -t|--timezone) assert_argument "$1" "$opt"; timezone="$1"; shift;;
      -l|--locale) assert_argument "$1" "$opt"; locale="$1"; shift;;
      -k|--keyboard) assert_argument "$1" "$opt"; keyboard="$1"; shift;;
      -p|--pre) preRelease=true;;
      -v|--debug) debug=true;;
      -n|--no-reboot) reboot=false;;
      -?|--help) showHelp; shift;;
      -s|--systemDir) assert_argument "$1" "$opt"; systemDir="$1"; shift;;

      # Arguments processing. You may remove any unneeded line after the 1st.
      -|''|[!-]*) set -- "$@" "$opt";;                                          # positional argument, rotate to the end
      --*=*)      set -- "${opt%%=*}" "${opt#*=}" "$@";;                        # convert '--name=arg' to '--name' 'arg'
      -[!-]?*)    set -- $(echo "${opt#-}" | sed 's/\(.\)/ -\1/g') "$@";;       # convert '-abc' to '-a' '-b' '-c'
      --)         while [ "$1" != "$EOL" ]; do set -- "$@" "$1"; shift; done;;  # process remaining arguments as positional
      -*)         usage_error "unknown option: '$opt'";;                        # catch misspelled options
      *)          usage_error "this should NEVER happen ($opt)";;               # sanity test for previous patterns

    esac
  done
  shift  # $EOL
fi

if [ $(id -u) != 0 ]; then
    logError "You need to be root to run this script! Please run 'sudo bash $0'"
    exit 1
fi

dotnetDirectory="/home/pi/.dotnet"
bootConfig="/boot/config.txt"
workingDirectory="/home/pi/WeatherDisplay.Api"
executable="WeatherDisplay.Api"
serviceName="weatherdisplay.api"
downloadFile="$workingDirectory/WeatherDisplay.Api.zip"

if ! test -v systemDir; then
    systemDir="/etc/systemd/system"
fi

serialNumber=$( cat /proc/cpuinfo | grep Serial | cut -d ' ' -f 2 )

if ! test -v host; then

    host="raspi$(echo $serialNumber)"
fi

# Generate wifi SSID and pre-shared key
# - The SSID should be constant therefore we use the serial number as part of it.
# - The PSK is a random number with a length of 8 characters. Some characters are explicitly filtered to avoid confusion (like O with 0).
ap_ssid="PiWeatherDisplay_$(echo $serialNumber | tail -c 7 | tr '[:lower:]' '[:upper:]')"
ap_psk=$(< /dev/urandom tr -dc A-Z-a-z-0-9_$ | tr -d oO0 | head -c 8)
ap_wifi_mode="g"
ap_country_code="CH"
ap_ip="192.168.10.1"
ap_ip_begin=$(echo "${ap_ip}" | sed -e 's/\.[0-9]\{1,3\}$//g')

serviceFilePath="$systemDir"/"$serviceName.service"

if [ "$debug" = "true" ]; then
    echo "
=====================================================
Debug Variables
=====================================================
preRelease: $preRelease
systemDir: $systemDir
workingDirectory: $workingDirectory
dotnetDirectory: $dotnetDirectory
bootConfig: $bootConfig
executable: $executable
serviceName: $serviceName
serviceFilePath: $serviceFilePath
downloadFile: $downloadFile
serialNumber: $serialNumber
host: $host
ap_ssid: $ap_ssid
ap_psk: $ap_psk
ap_ip: $ap_ip
ap_ip_begin: $ap_ip_begin
timezone: $timezone
locale: $locale
keyboard: $keyboard
reboot: $reboot
=====================================================
" >&2
fi

#exit 1

if [ ! -d $workingDirectory ]; then
    logDebug "Creating directory $workingDirectory"
    echo ""
    mkdir $workingDirectory
fi

cd $workingDirectory

logSuccess "Setting up raspberry pi@${host}..."
sudo raspi-config nonint do_boot_wait 0                     # Turn on waiting for network before booting
sudo raspi-config nonint do_boot_splash 0                   # Disable the splash screen
sudo raspi-config nonint do_spi 0                           # Enable SPI support
sudo raspi-config nonint do_i2c 0                           # Enable I2C support
sudo raspi-config nonint do_ssh 0                           # Enable SSH support
sudo raspi-config nonint do_camera 0                        # Disable camera

sudo bash -c "sed -i \"s/^\s*hdmi_force_hotplug=/#hdmi_force_hotplug=/\" $bootConfig"
sudo bash -c "sed -i \"s/^\s*camera_auto_detect=/#camera_auto_detect=/\" $bootConfig"
sudo bash -c "sed -i \"s/^\s*display_auto_detect=/#display_auto_detect=/\" $bootConfig"
sudo bash -c "sed -i \"s/^\s*dtoverlay=vc4-kms-v3d/#dtoverlay=vc4-kms-v3d/\" $bootConfig"
sudo bash -c "sed -i \"s/^\s*dtparam=audio=on/dtparam=audio=off/\" $bootConfig"

dtoverlayToBeAdded="dtoverlay=spi0-1cs,cs0_pin=28"
cnt=$(grep -c $dtoverlayToBeAdded $bootConfig)
if [ $cnt -eq 0 ]; then
    sudo bash -c "cat >> $bootConfig <<EOF
# WeatherDisplay.Api config section:
$dtoverlayToBeAdded
dtoverlay=disable-bt
EOF"
fi
echo ""

logSuccess "Updating packages..."
sudo apt-get update && sudo apt-get -y upgrade
echo ""

logSuccess "Installing packages..."
if [[ $(dpkg -l | grep -c libgdiplus) == 0 ]]; then
    logDebug "Installing libgdiplus..."
    sudo apt-get install -y libgdiplus
fi

if [[ $(dpkg -l | grep -c dhcpcd) == 0 ]]; then
    logDebug "Installing dhcpcd..."
    sudo apt-get install -y dhcpcd
fi

if [[ $(dpkg -l | grep -c hostapd) == 0 ]]; then
    logDebug "Installing hostapd..."
    sudo apt-get install -y hostapd
fi

if [[ $(dpkg -l | grep -c dnsmasq) == 0 ]]; then
    logDebug "Installing dnsmasq..."
    sudo apt-get install -y dnsmasq
fi
echo ""


logSuccess "Setting up access point..."

# Exclude ap0 from `/etc/dhcpcd.conf`
sudo bash -c 'cat >> /etc/dhcpcd.conf' << EOF
# This sets a static address for ap@wlan0 and disables wpa_supplicant for this interface
interface ap@wlan0
    static ip_address=${ap_ip}/24
    ipv4only
    nohook wpa_supplicant
EOF

# Populate `/etc/dnsmasq.conf`
logDebug "Populate /etc/dnsmasq.conf"
sudo bash -c 'cat > /etc/dnsmasq.conf' << EOF
interface=lo,ap@wlan0
no-dhcp-interface=lo,wlan0
bind-dynamic
server=1.1.1.1
domain-needed
bogus-priv
dhcp-range=${ap_ip_begin}.50,${ap_ip_begin}.150,240h
dhcp-option=3,${ap_ip}
EOF

# Populate hostapd.conf
logDebug "Populate /etc/hostapd/hostapd.conf"
sudo bash -c 'cat > /etc/hostapd/hostapd.conf' << EOF
ctrl_interface=/var/run/hostapd
ctrl_interface_group=0
interface=ap@wlan0
driver=nl80211
ieee80211n=1
ssid=${ap_ssid}
hw_mode=${ap_wifi_mode}
channel=11
wmm_enabled=1
macaddr_acl=0
auth_algs=1
wpa=2
$([ $ap_psk ] && echo "wpa_passphrase=${ap_psk}")
wpa_key_mgmt=WPA-PSK
wpa_pairwise=TKIP
rsn_pairwise=CCMP
EOF

sudo chmod 600 /etc/hostapd/hostapd.conf

sudo bash -c 'SYSTEMD_EDITOR=tee systemctl edit --force --full accesspoint@.service' << EOF
[Unit]
Description=IEEE 802.11 ap@%i AP on %i with hostapd
Wants=wpa_supplicant@%i.service
[Service]
Type=forking
PIDFile=/run/hostapd.pid
Restart=on-failure
RestartSec=2
Environment=DAEMON_CONF=/etc/hostapd/hostapd.conf
EnvironmentFile=-/etc/default/hostapd
ExecStartPre=/sbin/iw dev %i interface add ap@%i type __ap
ExecStart=/usr/sbin/hostapd -i ap@%i -P /run/hostapd.pid -B /etc/hostapd/hostapd.conf
ExecStopPost=-/sbin/iw dev ap@%i del
[Install]
WantedBy=sys-subsystem-net-devices-%i.device
EOF

# wpa_supplicant is no longer used, as the agent is hooked by dhcpcd
sudo systemctl disable wpa_supplicant.service

logDebug "enable dnsmasq.service / disable hostapd.service"
systemctl unmask dnsmasq.service
systemctl enable dnsmasq.service
sudo systemctl stop hostapd # if the default hostapd service was active before
sudo systemctl disable hostapd # if the default hostapd service was enabled before
sudo systemctl enable accesspoint@wlan0.service
sudo rfkill unblock wlan
sudo systemctl daemon-reload

logDebug "Create access point config"
sudo bash -c "cat > $workingDirectory/accesspoint@wlan0.json" << EOF
{
  "AccessPoint": {
    "SSID": "$ap_ssid",
    "PSK": "$ap_psk"
  }
}
EOF

logDebug "Create log folder"
mkdir -p /var/log/ap_sta_wifi
touch /var/log/ap_sta_wifi/ap0_mgnt.log
touch /var/log/ap_sta_wifi/on_boot.log

logDebug "Turn power management off for wlan0"
grep 'iw dev wlan0 set power_save off' /etc/rc.local || sudo sed -i 's:^exit 0:iw dev wlan0 set power_save off\n\nexit 0:' /etc/rc.local
echo ""


if [ -d $dotnetDirectory ]; then
    logSuccess "Updating dotnet..."
else
    logSuccess "Installing dotnet..."
fi

curl -sSL https://dot.net/v1/dotnet-install.sh | sudo bash /dev/stdin --version latest --channel 6.0 --install-dir $dotnetDirectory
echo ""

logDebug "Updating dotnet environment variables"
if ! grep -q ".NET Core SDK tools" "/home/pi/.bashrc"; then
    cat << \EOF >> "/home/pi/.bashrc"
# .NET Core SDK tools
export PATH=${PATH}:/home/pi/.dotnet
export PATH=${PATH}:/home/pi/.dotnet/tools
export DOTNET_ROOT=/home/pi/.dotnet
EOF
fi

export PATH=${PATH}:/home/pi/.dotnet
export PATH=${PATH}:/home/pi/.dotnet/tools
export DOTNET_ROOT=/home/pi/.dotnet

sudo -s source /etc/bash.bashrc
echo ""

logSuccess "Downloading WeatherDisplay.Api..."
if [ -f "$downloadFile" ] ; then
    rm "$downloadFile"
fi

if [ "$preRelease" = "true" ]; then
  downloadUrl=$(curl -s https://api.github.com/repos/thomasgalliker/PiWeatherStation/releases | grep browser_download_url | cut -d '"' -f 4 | head -n 1)
else
  downloadUrl=$(curl -s https://api.github.com/repos/thomasgalliker/PiWeatherStation/releases/latest | grep browser_download_url | cut -d '"' -f 4)
fi

echo "$downloadUrl"
curl -L --output "$downloadFile" --progress-bar $downloadUrl
echo ""

serviceStatus="$(systemctl is-active $serviceName)"
if [ "${serviceStatus}" = "active" ]; then
    logDebug "Stopping $serviceName..."
    sudo systemctl stop $serviceName
fi

logDebug "Installing WeatherDisplay.Api..."
unzip -q -o "$downloadFile" -d $workingDirectory
rm "$downloadFile"

sudo chown pi -R $workingDirectory
sudo chmod +x "$workingDirectory/$executable"

if [ ! -f "$serviceFilePath" ] ; then
    logDebug "Creating service $serviceName..."
else
    logDebug "Updating service $serviceName..."
fi

cat > "$serviceFilePath" <<EOF
[Unit]
Description=WeatherDisplay.Api
After=network-online.target firewalld.service
Wants=network-online.target

[Service]
Type=simple
WorkingDirectory=$workingDirectory
ExecStart=sudo $dotnetDirectory/dotnet $workingDirectory/$executable.dll
ExecStop=/bin/kill \$MAINPID
KillSignal=SIGTERM
KillMode=process
SyslogIdentifier=$executable
TimeoutStartSec=60
TimeoutStopSec=20

User=pi
Group=pi

Restart=no

Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false
Environment=DOTNET_ROOT=/home/pi/.dotnet

[Install]
WantedBy=multi-user.target
EOF

if [ "${serviceStatus}" != "active" ]; then
    logDebug "Starting service $serviceName..."
    sudo systemctl daemon-reload
    sudo systemctl enable $serviceName
    #sudo systemctl start $serviceName
fi

if [ ! -z "$timezone" ]; then
    logDebug "Updating timezone $timezone..."
    sudo raspi-config nonint do_change_timezone $timezone
fi

if [ ! -z "$locale" ]; then
    logDebug "Updating locale $locale..."
    sudo raspi-config nonint do_change_locale $locale
fi

if [ ! -z "$keyboard" ]; then
    logDebug "Updating keyboard layout $keyboard..."
    sudo raspi-config nonint do_configure_keyboard $keyboard
fi

logDebug "Updating hostname..."
currentHostname=`cat /etc/hostname | tr -d " \t\n\r"`
sudo raspi-config nonint do_hostname $host
echo $host > /etc/hostname
sed -i "s/127.0.1.1.*$currentHostname/127.0.1.1\t$host/g" /etc/hosts

logSuccess "
=====================================================
Installation is completed
=====================================================

Hostname:       ${host}
Wifi SSID:      ${ap_ssid}
Wifi password:  ${ap_psk}
Wifi AP IP:     ${ap_ip}

" >&2

if [ "$reboot" = "true" ]; then
    echo "Rebooting now..."
    sudo reboot
else
    echo "Run 'sudo reboot' to reboot manually."
fi
echo "====================================================="

exit 0

# Links / Sources:
# https://gist.github.com/damoclark/ab3d700aafa140efb97e510650d9b1be
# https://github.com/pi-top/pi-top-4-.NET-SDK/blob/417ea28fdd47480a6c9ef6835e292259a03e7aa0/setup.sh
# https://github.com/thnk2wn/rasp-cat-siren/blob/main/pi-setup/setup.sh
# https://github.com/AdamZWinter/cnode/blob/6aa26d5ffd0b95c68d8814f63e300dafd687e138/scripts/setup_mon.sh
# https://raspberrypi.stackexchange.com/questions/28907/how-could-one-automate-the-raspbian-raspi-config-setup
