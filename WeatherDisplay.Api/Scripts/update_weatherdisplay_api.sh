#!/bin/bash

echo "PiWeatherStation [Version 1.0.0]"
echo "(c) superdev gmbh. All rights reserved."
echo ""

preRelease=$1

downloadFile="WeatherDisplay.Api.zip"

if [ -f "$downloadFile" ] ; then
    rm "$downloadFile"
fi

if($preRelease = true) then
  echo "Download latest version (stable or preview)..."
  curl -L --output "$downloadFile" $(curl -s https://api.github.com/repos/thomasgalliker/PiWeatherStation/releases | grep browser_download_url | cut -d '"' -f 4 | head -n 1)
else
  echo "Download latest stable version..."
  curl -L --output "$downloadFile" $(curl -s https://api.github.com/repos/thomasgalliker/PiWeatherStation/releases/latest | grep browser_download_url | cut -d '"' -f 4)
fi

#targetDir="~/temp"
#if [[ -d $targetDir ]]; then
#    echo "$(targetDir) already  exists"
#else
#    echo "Creating $(targetDir)..."
#    mkdir $targetDir
#fi

echo "Stopping weatherdisplay service..."
sudo systemctl stop weatherdisplay.api.service

echo "Updating weatherdisplay service..."
unzip -q -o "$downloadFile" -d "../WeatherDisplay.Api"
rm "$downloadFile"

echo "Starting weatherdisplay service..."
sudo systemctl daemon-reload
sudo systemctl enable weatherdisplay.api.service
sudo systemctl start weatherdisplay.api.service

echo "Successfully finished"
