#!/bin/bash

echo "
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
    echo >&2 "$(basename $0):  $1"; exit 2;
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
    echo "You need to be root to run this script! Please run 'sudo bash $0'"
    exit 1
fi

dotnetDirectory="/home/pi/.dotnet"
bootConfig="/boot/config.txt"
workingDirectory="/home/pi/WeatherDisplay.Api"
executable="WeatherDisplay.Api"
serviceName="weatherdisplay.api"
downloadFile="$workingDirectory/WeatherDisplay.Api.zip"

if [ -z "$systemDir" ]
then
    systemDir="/etc/systemd/system"
fi

if [ -z "$host" ]
then
    serialNumber=$( cat /proc/cpuinfo | grep Serial | cut -d ' ' -f 2 )
    host="raspi$serialNumber"
fi

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
host: $host
timezone: $timezone
locale: $locale
keyboard: $keyboard
reboot: $reboot
=====================================================
" >&2
fi

#exit 1

if [ ! -d $workingDirectory ]; then
     echo "Creating directory $workingDirectory"
     mkdir $workingDirectory
fi

cd $workingDirectory

echo "Setting up raspberry pi@${host}.local..."
sudo raspi-config nonint do_boot_wait 0                     # Turn on waiting for network before booting
sudo raspi-config nonint do_boot_splash 0                   # Disable the splash screen
sudo raspi-config nonint do_spi 0                           # Enable SPI support
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

echo "Updating packages..."
sudo apt-get update && sudo apt-get -y upgrade
echo ""

echo "Installing packages..."
sudo apt-get install -y libgdiplus
sudo apt-get install -y hostapd
sudo apt-get install -y dnsmasq

sudo systemctl stop hostapd
sudo systemctl unmask hostapd
sudo systemctl disable hostapd

sudo systemctl stop dnsmasq
sudo systemctl unmask dnsmasq
sudo systemctl disable dnsmasq
echo ""

if [ -d $dotnetDirectory ]; then
    echo "dotnet already exists"
else
    echo "Installing dotnet..."
    sudo curl -sSL https://dot.net/v1/dotnet-install.sh | sudo bash /dev/stdin --install-dir $dotnetDirectory --channel Current
    echo ""
fi

echo "Updating dotnet environment variables"
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

if [ -f "$downloadFile" ] ; then
    rm "$downloadFile"
fi

if [ "$preRelease" = "true" ]; then
  downloadUrl=$(curl -s https://api.github.com/repos/thomasgalliker/PiWeatherStation/releases | grep browser_download_url | cut -d '"' -f 4 | head -n 1)
else
  downloadUrl=$(curl -s https://api.github.com/repos/thomasgalliker/PiWeatherStation/releases/latest | grep browser_download_url | cut -d '"' -f 4)
fi

echo "Downloading WeatherDisplay.Api..."
echo "$downloadUrl"
curl -L --output "$downloadFile" --progress-bar $downloadUrl
echo ""

serviceStatus="$(systemctl is-active $serviceName)"
if [ "${serviceStatus}" = "active" ]; then
    echo "Stopping $serviceName..."
    sudo systemctl stop $serviceName
fi

echo "Installing WeatherDisplay.Api..."
unzip -q -o "$downloadFile" -d $workingDirectory
rm "$downloadFile"

sudo chown pi -R $workingDirectory
sudo chmod +x "$workingDirectory/$executable"

if [ ! -f "$downloadFile" ] ; then
    echo "Creating service $serviceName..."

    cat > "$serviceFilePath" <<EOF
[Unit]
Description=WeatherDisplay.Api
After=network-online.target firewalld.service
Wants=network-online.target

[Service]
Type=notify
WorkingDirectory=$workingDirectory
ExecStart=$workingDirectory/$executable
ExecStop=/bin/kill \$MAINPID
KillSignal=SIGTERM
KillMode=process
SyslogIdentifier=$executable
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
fi

if [ "${serviceStatus}" != "active" ]; then
    echo "Starting service $serviceName..."
    sudo systemctl daemon-reload
    sudo systemctl enable $serviceName
    #sudo systemctl start $serviceName
fi

if [ ! -z "$timezone" ]; then
    echo "Updating timezone $timezone..."
    sudo raspi-config nonint do_change_timezone $timezone
fi

if [ ! -z "$locale" ]; then
    echo "Updating locale $locale..."
    sudo raspi-config nonint do_change_locale $locale
fi

if [ ! -z "$keyboard" ]; then
    echo "Updating keyboard layout $keyboard..."
    sudo raspi-config nonint do_configure_keyboard $keyboard
fi

echo "Updating hostname..."
currentHostname=`cat /etc/hostname | tr -d " \t\n\r"`
sudo raspi-config nonint do_hostname $host
echo $host > /etc/hostname
sed -i "s/127.0.1.1.*$currentHostname/127.0.1.1\t$host/g" /etc/hosts

echo "
=====================================================
Installation is completed
=====================================================

pi@${host}.local will be ready
after the reboot.

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
