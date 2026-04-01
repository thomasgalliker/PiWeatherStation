#!/bin/bash
# Setup script for PiWeatherStation
# Author: Thomas Galliker

# set -exv

# Error management
#set -o errexit
#set -o pipefail
#set -o nounset

# Logging
DEFAULT='\033[0;39m'
WHITE='\033[0;02m'
GREEN='\033[1;32m'
RED='\033[1;31m'

export DEBIAN_FRONTEND=noninteractive
export APT_LISTCHANGES_FRONTEND=none

logDebug() {
    echo -e "${DEFAULT}${1}${DEFAULT}"
}

logSuccess() {
    echo -e "${GREEN}${1}${DEFAULT}"
}

logError() {
    echo -e "${RED}${1}${DEFAULT}"
}

if [ -z "${BASH_VERSION:-}" ]; then
    logError "This script must be run with bash, not sh."
    logError "Use: sudo bash $0 [options]"
    exit 1
fi

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
    -f, --framework     Sets the target dotnet runtime version (default: net10.0).

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
PiWeatherStation Setup
(c) superdev gmbh. All rights reserved.
=====================================================
" >&2

# Toggle 'debug' variable (true/false) in order to receive more verbose log output
debug=false
preRelease=false
reboot=true
targetFramework="net10.0"

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
      -f|--framework) assert_argument "$1" "$opt"; targetFramework="$1"; shift;;
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

installUser="${SUDO_USER:-$(logname 2>/dev/null)}"
if [ -z "$installUser" ] || [ "$installUser" = "root" ]; then
    installUser=$(getent passwd 1000 | cut -d: -f1)
fi

if [ -z "$installUser" ]; then
    logError "Could not determine the non-root user that should own the installation."
    exit 1
fi

installHome=$(getent passwd "$installUser" | cut -d: -f6)
if [ -z "$installHome" ]; then
    logError "Could not determine the home directory of user '$installUser'."
    exit 1
fi

dotnetDirectory="$installHome/.dotnet"
bootConfig="/boot/firmware/config.txt"

workingDirectory="$installHome/WeatherDisplay.Api"
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
ap_psk=$(< /dev/urandom tr -dc A-Z-a-z-0-9_$ | tr -d oO0lI1 | head -c 8)
ap_wifi_mode="g"
ap_country_code="CH"
ap_ip="192.168.10.1"
ap_ip_begin=$(echo "${ap_ip}" | sed -e 's/\.[0-9]\{1,3\}$//g')
dotnetChannel=$(echo "$targetFramework" | sed 's/^net//')

serviceFilePath="$systemDir"/"$serviceName.service"

set_config_var() {
    awk -v key="$1" -v value="$2" '
        $0 ~ "^[#[:space:]]*" key "=" {
            print key "=" value
            made_change=1
            next
        }
        { print }
        END {
            if (!made_change) {
                print key "=" value
            }
        }
    ' "$3" > "$3.tmp" && mv "$3.tmp" "$3"
}

append_if_missing() {
    pattern="$1"
    line="$2"
    file="$3"

    if ! grep -qF "$pattern" "$file" 2>/dev/null; then
        printf '%s\n' "$line" >> "$file"
    fi
}

ensure_rc_local_power_save_off() {
    if [ ! -f /etc/rc.local ]; then
        cat > /etc/rc.local <<'EOF'
#!/bin/sh -e

exit 0
EOF
        chmod +x /etc/rc.local
    fi

    if ! grep -q 'iw dev wlan0 set power_save off' /etc/rc.local; then
        sed -i 's:^exit 0:iw dev wlan0 set power_save off\n\nexit 0:' /etc/rc.local
    fi
}

if [ "$debug" = "true" ]; then
    echo "
=====================================================
Debug Variables
=====================================================
preRelease: $preRelease
systemDir: $systemDir
workingDirectory: $workingDirectory
dotnetDirectory: $dotnetDirectory
installUser: $installUser
installHome: $installHome
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
targetFramework: $targetFramework
dotnetChannel: $dotnetChannel
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
logDebug "Disabling cloud-init..."
mkdir -p /etc/cloud
touch /etc/cloud/cloud-init.disabled

logDebug "Updating hostname..."
currentHostname=`cat /etc/hostname | tr -d " \t\n\r"`
echo "$currentHostname -> $host"
hostnamectl set-hostname "$host"
echo $host > /etc/hostname
sed -i -E 's/(127\.0\.1\.1\s+)[^ ]+/\1'"$host"'/g' /etc/hosts

append_if_missing 'dtparam=spi=on' 'dtparam=spi=on' "$bootConfig"
append_if_missing 'dtparam=i2c_arm=on' 'dtparam=i2c_arm=on' "$bootConfig"
set_config_var camera_auto_detect 0 "$bootConfig"
systemctl enable ssh >/dev/null 2>&1 || true
systemctl start ssh >/dev/null 2>&1 || true

bash -c "sed -i \"s/^\s*hdmi_force_hotplug=/#hdmi_force_hotplug=/\" $bootConfig"
bash -c "sed -i \"s/^\s*camera_auto_detect=/#camera_auto_detect=/\" $bootConfig"
bash -c "sed -i \"s/^\s*display_auto_detect=/#display_auto_detect=/\" $bootConfig"
bash -c "sed -i \"s/^\s*dtoverlay=vc4-kms-v3d/#dtoverlay=vc4-kms-v3d/\" $bootConfig"
bash -c "sed -i \"s/^\s*dtparam=audio=on/dtparam=audio=off/\" $bootConfig"

dtoverlayToBeAdded="dtoverlay=spi0-1cs,cs0_pin=28"
cnt=$(grep -c $dtoverlayToBeAdded $bootConfig)
if [ $cnt -eq 0 ]; then
    bash -c "cat >> $bootConfig <<EOF
# WeatherDisplay.Api config section:
$dtoverlayToBeAdded
dtoverlay=disable-bt
EOF"
fi
echo ""

logSuccess "Updating software..."
apt-get update && apt-get -y upgrade
echo ""

install_package() {
if [[ "$(dpkg -s ${1} 2> /dev/null | grep -cow '^Status: install ok installed$')" -eq '0' ]]
then
    logSuccess "Installing package ${1}..."
    apt-get -y install "${1}"
else
    logSuccess "Installing package ${1} --> already installed"
fi
}

install_package "libgdiplus"
install_package "dhcpcd"
install_package "hostapd"
install_package "dnsmasq"
echo ""

systemctl enable dhcpcd >/dev/null 2>&1 || true

logSuccess "Setting up access point..."

logDebug "Configuring NetworkManager to ignore ap@wlan0..."
mkdir -p /etc/NetworkManager/conf.d
cat > /etc/NetworkManager/conf.d/99-weatherdisplay-ap-unmanaged.conf <<'EOF'
[keyfile]
unmanaged-devices=interface-name:ap@wlan0;interface-name:p2p-dev-ap@wlan0
EOF
logDebug "NetworkManager unmanaged-device config written. It will apply cleanly after reboot."

# Exclude ap0 from `/etc/dhcpcd.conf`
bash -c 'cat >> /etc/dhcpcd.conf' << EOF
# This sets a static address for ap@wlan0 and disables wpa_supplicant for this interface
interface ap@wlan0
    static ip_address=${ap_ip}/24
    ipv4only
    nohook wpa_supplicant
EOF

# Update `/etc/dnsmasq.conf`
logDebug "Updating /etc/dnsmasq.conf..."
bash -c 'cat > /etc/dnsmasq.conf' << EOF
interface=lo,ap@wlan0
no-dhcp-interface=lo,wlan0
bind-dynamic
server=1.1.1.1
domain-needed
bogus-priv
dhcp-range=${ap_ip_begin}.50,${ap_ip_begin}.150,240h
dhcp-option=3,${ap_ip}
EOF

# Update hostapd.conf
logDebug "Updating /etc/hostapd/hostapd.conf..."
bash -c 'cat > /etc/hostapd/hostapd.conf' << EOF
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

chmod 600 /etc/hostapd/hostapd.conf

# Create accesspoint service
logDebug "Creating accesspoint service..."
cat > "$systemDir/accesspoint@.service" << EOF
[Unit]
Description=IEEE 802.11 ap@%i AP on %i with hostapd
Wants=wpa_supplicant@%i.service
After=network-online.target
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
WantedBy=multi-user.target
EOF

# wpa_supplicant is no longer used, as the agent is hooked by dhcpcd
systemctl disable wpa_supplicant.service

logDebug "enable dnsmasq.service / disable hostapd.service"
systemctl unmask dnsmasq.service
systemctl enable dnsmasq.service
systemctl stop hostapd     # if the default hostapd service was active before
systemctl disable hostapd  # if the default hostapd service was enabled before
systemctl daemon-reload
systemctl enable accesspoint@wlan0.service
rfkill unblock wlan
systemctl start accesspoint@wlan0.service || true

bash -c "cat > $workingDirectory/accesspoint@wlan0.json" << EOF
{
  "AccessPoint": {
    "SSID": "$ap_ssid",
    "PSK": "$ap_psk"
  }
}
EOF

if id "$installUser" >/dev/null 2>&1; then
    logDebug "Updating password for user $installUser..."
    echo "$installUser:$ap_psk" | chpasswd
else
    logError "User '$installUser' was not found. Skipping password update."
fi

logDebug "Create log folder for wifi access point"
mkdir -p /var/log/ap_sta_wifi
touch /var/log/ap_sta_wifi/ap0_mgnt.log
touch /var/log/ap_sta_wifi/on_boot.log

logDebug "Turn power management off for wlan0"
ensure_rc_local_power_save_off
echo ""


if [ -d $dotnetDirectory ]; then
    logSuccess "Updating dotnet runtime..."
else
    logSuccess "Installing dotnet runtime..."
fi

curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --runtime aspnetcore --version latest --channel "$dotnetChannel" --install-dir $dotnetDirectory
echo ""

logDebug "Updating dotnet environment variables"
if ! grep -q ".NET runtime" "$installHome/.bashrc"; then
    cat << \EOF >> "$installHome/.bashrc"
# .NET runtime
export PATH=${PATH}:$HOME/.dotnet
export DOTNET_ROOT=$HOME/.dotnet
EOF
fi

export PATH=${PATH}:$dotnetDirectory
export DOTNET_ROOT=$dotnetDirectory
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
    systemctl stop $serviceName
fi

logDebug "Installing WeatherDisplay.Api..."
unzip -q -o "$downloadFile" -d $workingDirectory
rm "$downloadFile"

chown "$installUser" -R $workingDirectory
chmod +x "$workingDirectory/$executable"

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
ExecStart=$dotnetDirectory/dotnet $workingDirectory/$executable.dll
ExecStop=/bin/kill \$MAINPID
KillSignal=SIGTERM
KillMode=process
SyslogIdentifier=$executable
TimeoutStartSec=60
TimeoutStopSec=20

User=$installUser
Group=$installUser

Restart=no

Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false
Environment=DOTNET_ROOT=$dotnetDirectory

[Install]
WantedBy=multi-user.target
EOF

if [ "${serviceStatus}" != "active" ]; then
    logDebug "Starting service $serviceName..."
    systemctl daemon-reload
    systemctl enable $serviceName
    #sudo systemctl start $serviceName
fi

if [ ! -z "$timezone" ]; then
    logDebug "Updating timezone $timezone..."
    timedatectl set-timezone "$timezone"
fi

if [ ! -z "$locale" ]; then
    logDebug "Updating locale $locale..."
    sed -i "s/^# *$locale UTF-8/$locale UTF-8/" /etc/locale.gen
    locale-gen "$locale"
    update-locale LANG="$locale"
fi

if [ ! -z "$keyboard" ]; then
    logDebug "Updating keyboard layout $keyboard..."
    sed -i "s/^XKBLAYOUT=.*/XKBLAYOUT=\\\"$keyboard\\\"/" /etc/default/keyboard
    setupcon -k --force >/dev/null 2>&1 || true
fi

logSuccess "
=====================================================
Installation is completed
=====================================================

Hostname:       ${host}
User:           ${installUser}
Password:       ${ap_psk}
Wifi SSID:      ${ap_ssid}
Wifi PSK:       ${ap_psk}
Wifi IP:        ${ap_ip}

" >&2

if [ "$reboot" = "true" ]; then
    echo "Rebooting now..."
    reboot
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
