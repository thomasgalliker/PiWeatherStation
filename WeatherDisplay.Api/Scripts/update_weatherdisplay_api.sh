#!/bin/bash

# Sources:
# https://gist.github.com/damoclark/ab3d700aafa140efb97e510650d9b1be
# https://github.com/pi-top/pi-top-4-.NET-SDK/blob/417ea28fdd47480a6c9ef6835e292259a03e7aa0/setup.sh
# https://github.com/thnk2wn/rasp-cat-siren/blob/main/pi-setup/setup.sh
# https://github.com/AdamZWinter/cnode/blob/6aa26d5ffd0b95c68d8814f63e300dafd687e138/scripts/setup_mon.sh

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
      -k|--keyboard) assert_argument "$1" "$opt"; keyboard="$1"; shift;;
      -t|--timezone) assert_argument "$1" "$opt"; timezone="$1"; shift;;
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

workingDirectory="/home/pi/WeatherDisplay.Api"
executable="WeatherDisplay.Api"
serviceName="weatherdisplay.api.service"
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

if [ -z "$timezone" ]
then
      timezone="Europe/Zurich"
fi

if [ -z "$keyboard" ]
then
      keyboard="us"
fi

serviceFilePath="$systemDir"/"$serviceName"

if [ "$debug"="true" ]; then
    echo "
=====================================================
Debug Variables
=====================================================
preRelease: $preRelease
systemDir: $systemDir
workingDirectory: $workingDirectory
executable: $executable
serviceName: $serviceName
serviceFilePath: $serviceFilePath
downloadFile: $downloadFile
host: $host
timezone: $timezone
keyboard: $keyboard
reboot: $reboot
=====================================================
" >&2
fi

#exit 1

if [ ! -d $workingDirectory ]
then
     echo "Creating directory $workingDirectory"
     mkdir $workingDirectory
fi

cd $workingDirectory

echo "Setting up raspberry pi@${host}.local"

echo "Running automated raspi-config tasks"
grep -E -v -e '^\s*#' -e '^\s*$' <<END | \
sed -e 's/$//' -e 's/^\s*/\/usr\/bin\/raspi-config nonint /' | bash -x -
#
# Drop this file in SD card root. After booting run: sudo /boot/setup.sh
# --- Begin raspi-config non-interactive config option specification ---
# Hardware Configuration
do_boot_wait 0            # Turn on waiting for network before booting
do_boot_splash 1          # Disable the splash screen
do_overscan 1             # Enable overscan
do_camera 1               # Enable the camera
do_ssh 0                  # Enable remote ssh login
# System Configuration
do_configure_keyboard ${keyboard}
do_change_timezone ${timezone}
do_change_locale LANG=en_US.UTF-8
# Don't add any raspi-config configuration options after 'END' line below & don't remove 'END' line
END
echo ""

echo "Updating packages"
sudo apt-get update && sudo apt-get -y upgrade
echo ""

echo "Installing packages"
sudo apt-get install -y libgdiplus
sudo apt-get install -y hostapd
sudo apt-get install -y dnsmasq

sudo systemctl stop hostapd
sudo systemctl stop dnsmasq
echo ""

echo "Installing dotnet"
sudo curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel Current
echo ""

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

echo "Updating timezone"
sudo timedatectl set-timezone ${timezone}
echo ""

if [ -f "$downloadFile" ] ; then
    rm "$downloadFile"
fi

if($preRelease = true) then
  downloadUrl=$(curl -s https://api.github.com/repos/thomasgalliker/PiWeatherStation/releases | grep browser_download_url | cut -d '"' -f 4 | head -n 1)
else
  downloadUrl=$(curl -s https://api.github.com/repos/thomasgalliker/PiWeatherStation/releases/latest | grep browser_download_url | cut -d '"' -f 4)
fi

echo "Download from url $downloadUrl..."
curl -L --output "$downloadFile" --progress-bar $downloadUrl

serviceStatus="$(systemctl is-active $serviceName)"
if [ "${serviceStatus}" = "active" ]; then
    echo "Stopping $serviceName..."
    sudo systemctl stop $serviceName
fi

echo "Updating weatherdisplay service..."
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
    echo "Starting $serviceName..."
    sudo systemctl daemon-reload
    sudo systemctl enable $serviceName
    sudo systemctl start $serviceName
fi

sudo hostnamectl set-hostname $host

echo "
=====================================================
Installation is completed
=====================================================

pi@${host}.local will be ready after the reboot...

=====================================================
" >&2

if [ "$reboot"="true" ]; then
    echo "Rebooting..."
    sudo reboot
else
    echo "Run 'sudo reboot' to reboot manually"
fi
