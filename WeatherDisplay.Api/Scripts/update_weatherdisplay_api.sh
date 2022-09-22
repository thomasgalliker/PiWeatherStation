#!/bin/bash

echo "PiWeatherStation Update Script [Version 1.0.0]"
echo "(c) superdev gmbh. All rights reserved."
echo ""

preRelease=false

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

      # Your options go here.
      -p|--pre) preRelease='true';;
      -n|--name) assert_argument "$1" "$opt"; name="$1"; shift;;

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

if [ ! -d $workingDirectory ]
then
     echo "Creating directory $workingDirectory"
     mkdir $workingDirectory
fi

cd $workingDirectory

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

sudo chown pi -R $workingDirectory
sudo chmod +x "$workingDirectory/$executable"

serviceStatus="$(systemctl is-active $serviceName)"
if [ "${serviceStatus}" = "active" ]; then
    echo "Stopping $serviceName..."
    sudo systemctl stop $serviceName
fi

echo "Updating weatherdisplay service..."
unzip -q -o "$downloadFile" -d $workingDirectory
rm "$downloadFile"


if [ "${serviceStatus}" = "active" ]; then
    echo "Starting $serviceName..."
    sudo systemctl daemon-reload
    sudo systemctl enable $serviceName
    sudo systemctl start $serviceName
else
    echo "Service $serviceName does not exist"
fi

echo "Successfully finished"
echo ""