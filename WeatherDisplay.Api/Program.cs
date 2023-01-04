using System.Gpio.Devices;
using System.Gpio.Devices.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NLog;
using NLog.Extensions.Logging;
using RaspberryPi.Extensions;
using UnitsNet.Serialization.JsonNet;
using WeatherDisplay.Api.Services;
using WeatherDisplay.Api.Services.Configuration;
using WeatherDisplay.Api.Services.Security;
using WeatherDisplay.Api.Updater.Services;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model;
using WeatherDisplay.Pages.MeteoSwiss;
using WeatherDisplay.Pages.OpenWeatherMap;
using WeatherDisplay.Pages.Wiewarm;

namespace WeatherDisplay.Api
{
    internal static class Program
    {
        private const string UserSpecificAppSettingsFileName = "appsettings.User.json";

        private static void Main(string[] args)
        {
            Console.WriteLine(
                $"WeatherStation version {typeof(Program).Assembly.GetName().Version} {Environment.NewLine}" +
                $"Copyright(C) superdev GmbH. All rights reserved.{Environment.NewLine}");

            var privateKeyFile = "localhost.pfx";
            var publicKeyFile = "localhost.crt";
            var httpsEndpoint = IPAddress.Any;

            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseKestrel(o =>
            {
                o.ConfigureHttpsDefaults(httpsOptions =>
                {
                    var (Private, Public) = CreateSelfSignedCertificate(privateKeyFile, publicKeyFile, httpsEndpoint);

                    try
                    {
                        httpsOptions.ServerCertificate = Private;
                    }
                    catch (CryptographicException)
                    {
                        Console.Error.WriteLine("Error importing certificate.");
                    }

                    httpsOptions.SslProtocols = SslProtocols.Tls12;
                    Console.WriteLine("Using certificate with hash: " + httpsOptions.ServerCertificate.GetCertHashString());
                });
            });

            builder.Host.UseSystemd();
            builder.Host.UseWindowsService();

            // ====== Setup logging ======
            builder.Logging.ClearProviders();
            builder.Logging.AddDebug();
            builder.Logging.AddNLog();

            LogManager.AutoShutdown = false;

            // ====== Setup configuration ======
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile(UserSpecificAppSettingsFileName, optional: true, reloadOnChange: true);

            // ====== Setup services ======
            var services = builder.Services;
            services.AddEndpointsApiExplorer();
            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.Converters.Add(new UnitsNetIQuantityJsonConverter());
                opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                opt.SerializerSettings.DateFormatHandling= DateFormatHandling.IsoDateFormat;
                opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherDisplay API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddRaspberryPi();

            // ====== Auto update ======
            var autoUpdateOptions = new AutoUpdateOptions();
            builder.Configuration.GetSection("AutoUpdateOptions").Bind(autoUpdateOptions);
            services.AddSingleton(autoUpdateOptions);

            var githubVersionCheckerOptions = new GithubVersionCheckerOptions();
            builder.Configuration.GetSection("AutoUpdateOptions").GetSection("RemoteVersionChecker").Bind(githubVersionCheckerOptions);
            services.AddSingleton(githubVersionCheckerOptions);

            services.AddSingleton<ILocalVersionChecker, ProductVersionChecker>();
            services.AddSingleton<IRemoteVersionChecker, GithubVersionChecker>();
            services.AddSingleton<IAutoUpdateService, AutoUpdateService>();

            // ====== Hardware access ======
            services.AddGpioDevices();

            services.AddSingleton<IWeatherDisplayServiceConfigurator, WeatherDisplayServiceConfigurator>();

            // ====== Weather services ======
            services.AddWeatherDisplay(builder.Configuration);
            services.ConfigureWritable<AppSettings>(builder.Configuration.GetSection("AppSettings"), UserSpecificAppSettingsFileName);
            services.ConfigureWritable<OpenWeatherMapPageOptions>(builder.Configuration.GetSection("OpenWeatherMapPageOptions"), UserSpecificAppSettingsFileName);
            services.ConfigureWritable<TemperatureDiagramPageOptions>(builder.Configuration.GetSection("TemperatureDiagramPageOptions"), UserSpecificAppSettingsFileName);
            services.ConfigureWritable<MeteoSwissWeatherPageOptions>(builder.Configuration.GetSection("MeteoSwissWeatherPageOptions"), UserSpecificAppSettingsFileName);
            services.ConfigureWritable<WaterTemperaturePageOptions>(builder.Configuration.GetSection("WaterTemperaturePageOptions"), UserSpecificAppSettingsFileName);

            services.AddHostedService<AutoStartupBackgroundService>();

            // ====== Authentification & authorization ======
            services.AddScoped<IUserService, UserService>();

            var identityConfiguration = new IdentityConfiguration();
            var identitySection = builder.Configuration.GetSection("Identity");
            identitySection.Bind(identityConfiguration);
            services.AddSingleton<IIdentityConfiguration>(identityConfiguration);

            services.AddAuthorization(o => o.AddPolicy("RequireAuthenticatedUserPolicy", builder => builder.RequireAuthenticatedUser()));

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = identityConfiguration.JwtIssuer,
                        ValidateAudience = true,
                        ValidAudience = identityConfiguration.JwtIssuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identityConfiguration.JwtKey)),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });

            // ====== Configure services ======
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization("RequireAuthenticatedUserPolicy");
            });

            app.UseSwagger();
            app.UseSwaggerUI(o => o.InjectStylesheet("/swagger-ui/SwaggerStyle.css"));

            app.UseStaticFiles();

            app.Run();
        }

        private static (X509Certificate2 Private, X509Certificate2 Public) CreateSelfSignedCertificate(string privateKeyFile, string publicKeyFile, IPAddress httpsEndpoint)
        {
            var now = DateTime.Now;

            X509Certificate2 privateKeyCertificate;
            if (File.Exists(privateKeyFile))
            {
                privateKeyCertificate = new X509Certificate2(privateKeyFile);
                if (privateKeyCertificate.NotAfter.AddYears(-1) < now)
                {
                    privateKeyCertificate = null;
                }
            }
            else
            {
                privateKeyCertificate = null;
            }

            X509Certificate2 publicKeyCertificate;
            if (File.Exists(publicKeyFile))
            {
                publicKeyCertificate = new X509Certificate2(publicKeyFile);
                if (publicKeyCertificate.NotAfter.AddYears(-1) < now)
                {
                    publicKeyCertificate = null;
                }
            }
            else
            {
                publicKeyCertificate = null;
            }

            if (privateKeyCertificate == null || publicKeyCertificate == null)
            {
                Console.WriteLine("Creating certificate...");

                var certificate = Certificates.CreateSelfSignedCertificate(httpsEndpoint, "CN=WeatherDisplay");
                File.WriteAllBytes(privateKeyFile, certificate.Export(X509ContentType.Pfx));
                File.WriteAllBytes(publicKeyFile, certificate.Export(X509ContentType.Cert));

                privateKeyCertificate = new X509Certificate2(privateKeyFile);
                publicKeyCertificate = new X509Certificate2(publicKeyFile);
            }

            return (privateKeyCertificate, publicKeyCertificate);
        }
    }
}