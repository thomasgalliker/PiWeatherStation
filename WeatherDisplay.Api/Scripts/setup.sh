#!/bin/bash

# Sources:
# https://gist.github.com/damoclark/ab3d700aafa140efb97e510650d9b1be
# https://github.com/pi-top/pi-top-4-.NET-SDK/blob/417ea28fdd47480a6c9ef6835e292259a03e7aa0/setup.sh
# https://github.com/thnk2wn/rasp-cat-siren/blob/main/pi-setup/setup.sh

echo "PiWeatherStation Setup Script [Version 1.0.0]"
echo "(c) superdev gmbh. All rights reserved."
echo ""

# Toggle 'debug' variable (true/false) in order to receive more verbose log output
debug=true

while [ $# -gt 0 ]; do
  case "$1" in
    -h|--host)
      host="$2"
      ;;
    -tz|--timezone)
      timezone="$2"
      ;;
    -k|--keyboard)
      keyboard="$2"
      ;;
    *)
      echo "Invalid argument: $1"
      exit 1
  esac
  shift
  shift
done

if [ -z "$host" ]
then
      echo "Missing argument --host"
	  exit 1
fi

if [ -z "$timezone" ]
then
      timezone="Europe/Zurich"
fi

if [ -z "$keyboard" ]
then
      keyboard="us"
fi


if [ "$debug" == 'true' ]
then
	echo "Debug Arguments:"
	echo "host: ${host}"
	echo "timezone: ${timezone}"
	echo "keyboard: ${keyboard}"
	echo ""
fi

#exit 1

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
do_hostname ${host}
do_change_timezone ${timezone}
do_change_locale LANG=en_US.UTF-8
# Don't add any raspi-config configuration options after 'END' line below & don't remove 'END' line
END

echo "Updating packages"
sudo apt-get update && sudo apt-get -y upgrade
echo ""

echo "Installing libgdiplus"
sudo apt-get install -y libgdiplus
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

echo "Restarting..."
sudo reboot