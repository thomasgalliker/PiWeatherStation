# PiWeatherStation
This is a demo project which uses a Raspberry Pi 4 to draw some basic weather information to a 7.5" Waveshare ePaper display. The code is based on .NET 6 and there are two runtime projects you can chose from: A console client (WeatherDisplay.ConsoleApp) and an ASP.NET Core Web API (WeatherDisplay.Api).

![Image of display](https://raw.githubusercontent.com/thomasgalliker/PiWeatherStation/master/Docs/DisplayPhoto1.jpg)

### Quick Setup

#### Prepare the Raspberry Pi
- Before we install any additional library, make sure the Raspberry OS as well as the installed libraries are on the latest stable releases.
`apt update` updates the package sources list to get the latest list of available packages in the repositories.
`apt upgrade` updates all the packages presently installed in our Linux system to their latest versions.
```
sudo apt update
sudo apt upgrade
```

- Install the GDI+ library. This library is later used to render images via SkiaSharp.
```
sudo apt install libgdiplus
```

- Reboot the system.
```
sudo reboot
```

#### Install .NET on Raspberry Pi
- Go to Microsoft's [dotnet download page](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) and download the appropriate version of .NET (ARM32 or ARM64 depending on your Raspberry OS).
```
wget https://download.visualstudio.microsoft.com/download/.../dotnet-sdk-6.0.200-linux-arm.tar.gz
```

- Extract the binaries and export the paths according to the instructions given on the download page:
```
mkdir -p $HOME/dotnet && tar zxf dotnet-sdk-6.0.200-linux-arm.tar.gz -C $HOME/dotnet
export DOTNET_ROOT=$HOME/dotnet
export PATH=$PATH:$HOME/dotnet
```
- You can edit your shell profile to permanently add the dotnet commands (even after a reboot):
```
sudo nano ~/.bashrc
```
- Edit the appropriate bash profile and add following lines to the end of the file. If `export PATH` already exists, extend it instead of creating a new export.
```
export DOTNET_ROOT=$HOME/dotnet
export PATH=$PATH:$HOME/dotnet
```

- Reboot the system.
```
sudo reboot
```

- Run `dotnet --info` to check if your .NET installation works as expected:
```
pi@raspberrypi:~ $ dotnet --info
.NET SDK (reflecting any global.json):
 Version:   6.0.200
 Commit:    4c30de7899

Runtime Environment:
 OS Name:     raspbian
 OS Version:  11
 OS Platform: Linux
 RID:         linux-arm
 Base Path:   /home/pi/dotnet/sdk/6.0.200/
 ...
```

#### Attach the Waveshare Display & enable GPIO
- Shutdown the the Raspberry using `sudo shutdown -h "now"`. Disconnect the Raspberry from all power sources.
- Attach the Waveshare Display Hat to the GPIO port of the Raspberry. Start the Raspberry up again.
- Edit the [config.txt](https://www.raspberrypi.com/documentation/computers/config_txt.html) in order to enable the SPI interface.
```
sudo nano /boot/config.txt
```

- Enable the SPI interface.
```
dtparam=spi=on
```
- Add this line if you get `IOException: Device or resource busy : '/sys/class/gpio/export'`. More info [here](https://github.com/eXoCooLd/Waveshare.EPaperDisplay/issues/17).
```
#dtoverlay=spi0-1cs,cs0_pin=28
```

- Reboot the system.
```
sudo reboot
```

#### Deploy WeatherDisplay.Api
- Prepare the project folder on the Raspberry Pi:
```
cd ~
mkdir WeatherDisplay.Api
```

- Build the project with `RELEASE` build configuration and copy the binaries to the Raspberry Pi using WinSCP (or any other file transfer tool).
There are other ways (like `dotnet publish`) to get a set of release-ready binaries.
- Take ownership of the folder and file.
```
sudo chown pi -R /home/pi/WeatherDisplay.Api
```
- Update permissions to allow execution of the executable file `WeatherDisplay.Api`.
```
sudo chmod +x /home/pi/WeatherDisplay.Api/WeatherDisplay.Api
```

- Test the ASP.NET Core web service by starting it manually. (Alternatively, you can also run the executable by calling: `./WeatherDisplay.Api`)
```
dotnet WeatherDisplay.Api.dll
```

#### Create background service for WeatherDisplay.Api
If everything works fine so far, we can setup the WeatherDisplay.Api as a service. This ensures that the display is regularly updated with latest information even if the ssh console is closed. Services are also automatically started if the system is rebooted.
- Navigate to /etc/systemd/system and create a new service definition:

```
cd /etc/systemd/system
```

```
sudo nano weatherdisplay.api.service
```

- Create a service definition which automatically starts the web API service when the operating system is started.
```
[Unit]
Description=WeatherDisplay.Api Service

# When this service should be started up
After=network-online.target firewalld.service

# Is dependent on this target
Wants=network-online.target

[Service]
Type=notify
WorkingDirectory=/home/pi/WeatherDisplay.Api
ExecStart=/home/pi/WeatherDisplay.Api/WeatherDisplay.Api
ExecStop=/bin/kill ${MAINPID}
KillSignal=SIGTERM
SyslogIdentifier=WeatherDisplay.Api

# Use your username to keep things simple, for production scenario's I recommend a dedicated user/group.
# If you pick a different user, make sure dotnet and all permissions are set correctly to run the app.
# To update permissions, use 'chown pi -R /home/pi/WeatherDisplay.Api' to take ownership of the folder and files,
#       Use 'chmod +x /home/pi/WeatherDisplay.Api/WeatherDisplay.Api' to allow execution of the executable file.
User=pi
Group=pi

Restart=always
RestartSec=5

# ASP.NET environment variable
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

# This environment variable is necessary when dotnet isn't loaded for the specified user.
# To figure out this value, run 'env | grep DOTNET_ROOT' when dotnet has been loaded into your shell.
Environment=DOTNET_ROOT=/home/pi/dotnet

[Install]
WantedBy=multi-user.target
```

Explanations for some of the configuration values:
| Attribute | Description |
|---|---|
| `WorkingDirectory` | Will set the current working directory. |
| `ExecStart` | Systemd will run this executable to start the service. |
| `ExecStop` | Defines the way the service is stopped when systemctl stop is called on this service. Together with KillSignal, this value is responsible for a graceful shutdown. |
| `KillSignal` | Is a very important value to determine how the ASP.NET Core web service is stopped. If the wrong value is used, the service is killed without gracefully shutting down it's services (e.g. BackgroundService, IHostedService, IDispose, etc). |
| `SyslogIdentifier` | Primary identifier of this service. This name is used to run systemctl start/stop operations as well as to read the service log (journalctl). |
| `Restart` | Ensure the service restarts after crashing. |
| `RestartSec` | Amount of time to wait before restarting the service. |

- Enable the service definition:
```
sudo systemctl enable weatherdisplay.api
```

- Start the service:
```
sudo systemctl daemon-reload
```
```
sudo systemctl start weatherdisplay.api
```

### Run PiWeatherStation
- Access the API using http://{ip-address-raspberry}:5000/swagger/index.html in order to start the Swagger UI. 
- Use the /login method to authenticate with the API.
- Call any other API method after successful login. 
### Troubleshooting & Maintenance
#### Update and restart the service
If anything in the service definition (weatherdisplay.api.service file) is changed, the service needs to be stopped and restarted.
The same procedure is necessary if we want to re-deploy the WeatherDisplay.Api binaries.

-  Stop the service to release any file locks or http listeners.
```
sudo systemctl stop weatherdisplay.api
```

- Rebuild the web API project and copy the output to the raspberry.
- Restart the weatherdisplay.api.service.
```
sudo systemctl daemon-reload
sudo systemctl start weatherdisplay.api
```

#### Service Operations
```
sudo systemctl start weatherdisplay.api
sudo systemctl stop weatherdisplay.api
sudo systemctl restart weatherdisplay.api
```

#### Check Service Log
```
journalctl -u weatherdisplay.api.service -f
```

#### Check Listening Network Ports
```
sudo netstat -tulpn | grep LISTEN
```

#### Call URL using cURL
```
curl -I -k https://localhost:5000/swagger/index.html

HTTP/2 200
content-type: text/html
date: Wed, 23 Feb 2022 16:35:52 GMT
server: Kestrel
accept-ranges: bytes
etag: "1d7c28cf574c2b4"
last-modified: Sat, 16 Oct 2021 12:54:30 GMT
content-length: 1460
```

### Links
#### Similar projects / Waveshare / IoT
- https://github.com/eXoCooLd/Waveshare.EPaperDisplay
- https://github.com/thecaptncode/IoTDisplay
- https://www.youtube.com/watch?v=t-rFj54BsDI
- https://github.com/Tharnas/EInkDisplayService
- https://github.com/bezysoftware/crypto-clock
- https://www.petecodes.co.uk/install-and-use-microsoft-dot-net-6-with-the-raspberry-pi/

#### Linux and ASP.NET Core related sources
- https://swimburger.net/blog/dotnet/how-to-run-a-dotnet-core-console-app-as-a-service-using-systemd-on-linux
- https://swimburger.net/blog/dotnet/how-to-run-aspnet-core-as-a-service-on-linux
- https://docs.microsoft.com/en-us/troubleshoot/developer/webapps/aspnetcore/practice-troubleshoot-linux/2-6-run-two-aspnetcore-applications-same-time
- https://procodeguide.com/programming/how-to-set-start-url-in-aspnet-core/
- https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/endpoints?view=aspnetcore-6.0
- https://richstokoe.com/2017/12/10/running-asp-net-core-raspbian-linux-raspberry-pi-https/
- https://github.com/alastairgould/dotnet-core-systemd/blob/7eb500a1f1ffe4e27278edb14ef85fb0a11bf8bf/webapplication.service

#### Other sources
- https://github.com/angelobreuer/OpenWeatherMap4NET
- https://github.com/Violetta-9/Weather/tree/master
- https://openweathermap.org/weather-conditions
