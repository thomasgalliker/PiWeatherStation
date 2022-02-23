# PiWeatherStation
Weather Station for RasperryPi and Waveshare ePaper Displays

#### Deploy WeatherDisplay.Api

`dotnet publish -r linux-arm`

#### Create Auto-Start Service for WeatherDisplay.Api
- Navigate to /etc/systemd/system and create a new service definition:

`cd /etc/systemd/system`

`sudo nano weatherdisplay.api.service`

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
# will set the Current Working Directory (CWD)
WorkingDirectory=/home/pi/WeatherDisplay.Api
# systemd will run this executable to start the service
ExecStart=/home/pi/WeatherDisplay.Api/WeatherDisplay.Api
# to query logs using journalctl, set a logical name here
SyslogIdentifier=WeatherDisplay.Api

# Use your username to keep things simple, for production scenario's I recommend a dedicated user/group.
# If you pick a different user, make sure dotnet and all permissions are set correctly to run the app.
# To update permissions, use 'chown pi -R /home/pi/WeatherDisplay.Api' to take ownership of the folder and files,
#       Use 'chmod +x /home/pi/WeatherDisplay.Api/WeatherDisplay.Api' to allow execution of the executable file.
User=pi
Group=pi

# ensure the service restarts after crashing
Restart=always
# amount of time to wait before restarting the service
RestartSec=5

# Copied from dotnet documentation at
# https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-3.1#code-try-7
KillSignal=SIGINT
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

# This environment variable is necessary when dotnet isn't loaded for the specified user.
# To figure out this value, run 'env | grep DOTNET_ROOT' when dotnet has been loaded into your shell.
Environment=DOTNET_ROOT=/home/pi/dotnet

[Install]
WantedBy=multi-user.target
```

- Enable the service definition:

`sudo systemctl enable weatherdisplay.api`

- Start the service:

`sudo systemctl daemon-reload`

`sudo systemctl start weatherdisplay.api`

#### Update and Restart Service
-  Stop the service to release any file locks or http listeners.

`sudo systemctl stop weatherdisplay.api`

- Rebuild the web API project and copy the output to the raspberry.

`sudo systemctl daemon-reload`

`sudo systemctl start weatherdisplay.api`

### Troubleshooting
#### Service Operations
`sudo systemctl start weatherdisplay.api`
`sudo systemctl stop weatherdisplay.api`
`sudo systemctl restart weatherdisplay.api`

#### Check Service Log
`journalctl -u weatherdisplay.api.service -f`

#### Check Listening Network Ports
`sudo netstat -tulpn | grep LISTEN`

#### Call URL using cURL
`curl -I -k https://localhost:5000/swagger/index.html`
```
HTTP/2 200
content-type: text/html
date: Wed, 23 Feb 2022 16:35:52 GMT
server: Kestrel
accept-ranges: bytes
etag: "1d7c28cf574c2b4"
last-modified: Sat, 16 Oct 2021 12:54:30 GMT
content-length: 1460
```


#### Links

https://swimburger.net/blog/dotnet/how-to-run-a-dotnet-core-console-app-as-a-service-using-systemd-on-linux

https://swimburger.net/blog/dotnet/how-to-run-aspnet-core-as-a-service-on-linux

https://docs.microsoft.com/en-us/troubleshoot/developer/webapps/aspnetcore/practice-troubleshoot-linux/2-6-run-two-aspnetcore-applications-same-time

https://procodeguide.com/programming/how-to-set-start-url-in-aspnet-core/

https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/endpoints?view=aspnetcore-6.0

https://richstokoe.com/2017/12/10/running-asp-net-core-raspbian-linux-raspberry-pi-https/

https://github.com/alastairgould/dotnet-core-systemd/blob/7eb500a1f1ffe4e27278edb14ef85fb0a11bf8bf/webapplication.service