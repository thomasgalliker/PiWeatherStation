using System.Gpio.Devices;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Extensions.Logging;
using RaspberryPi.Extensions;
using WeatherDisplay.Api.Services;
using WeatherDisplay.Api.Services.Configuration;
using WeatherDisplay.Api.Updater.Services;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model;

namespace WeatherDisplay.Api
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(
                $"WeatherStation version {typeof(Program).Assembly.GetName().Version} {Environment.NewLine}" +
                $"Copyright(C) superdev GmbH. All rights reserved.{Environment.NewLine}");

            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseKestrel();
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
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

            // ====== Setup services ======
            var services = builder.Services;
            services.AddEndpointsApiExplorer();
            services.AddControllers();
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
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            if (isWindows)
            {
                services.AddSingleton<IGpioController, GpioControllerSimulator>();
            }
            else
            {
                services.AddSingleton<IGpioController, GpioControllerWrapper>();
            }

            services.AddSingleton<IWeatherDisplayHardwareCoordinator, WeatherDisplayHardwareCoordinator>();
            services.AddSingleton<IWeatherDisplayServiceConfigurator, WeatherDisplayServiceConfigurator>();

            // ====== Weather services ======
            services.AddWeatherDisplay(builder.Configuration);
            services.ConfigureWritable<WeatherDisplay.Model.AppSettings>(builder.Configuration.GetSection("AppSettings"), "appsettings.Development.json");
            services.ConfigureWritable<OpenWeatherDisplayCompilationOptions>(builder.Configuration.GetSection("OpenWeatherDisplayCompilation"), "appsettings.Development.json");

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

            //app.UseHttpsRedirection();
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
    }
}