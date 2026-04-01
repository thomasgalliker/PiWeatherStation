using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using NLog;
using NLog.Extensions.Logging;
using WeatherDisplay.Api.Properties;
using WeatherDisplay.Api.Serialization;
using WeatherDisplay.Api.Services;
using Superdev.AspNetCore.Options;
using WeatherDisplay.Api.Services.Security;
using WeatherDisplay.Api.Updater.Services;
using WeatherDisplay.Model;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Pages.MeteoSwiss;
using WeatherDisplay.Pages.OpenWeatherMap;
using WeatherDisplay.Pages.Wiewarm;

namespace WeatherDisplay.Api
{
    internal static class Program
    {
        internal const string UserSpecificAppSettingsFileName = "appsettings.User.json";

        private static void Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyVersion = assembly.GetName().Version;
            var buildTime = assembly.GetBuildTime();

            Console.WriteLine(
                $"WeatherStation version {assemblyVersion} {(buildTime != null ? $"[{buildTime.Value:u}]{Environment.NewLine}" : "")}" +
                $"Copyright(C) superdev GmbH. All rights reserved.{Environment.NewLine}");

            var privateKeyFile = "localhost.pfx";
            var publicKeyFile = "localhost.crt";
            var httpsEndpoint = IPAddress.Any;

            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseKestrel(o =>
            {
                o.UseSystemd();
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
            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.Converters.Add(new UnitsNetIQuantityJsonConverter());
                opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            var swaggerVersion = $"v{assemblyVersion.Major}";
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc(swaggerVersion, new OpenApiInfo { Title = "WeatherDisplay API", Version = $"{assemblyVersion}" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Paste only the JWT access token here. Swagger UI adds the 'Bearer ' prefix automatically.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                option.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document, null)] = new List<string>()
                });
                var xmlDocumentationFilePath = Path.Combine(AppContext.BaseDirectory, "WeatherDisplay.Api.xml");
                option.IncludeXmlComments(xmlDocumentationFilePath);
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
            var identityConfigSection = builder.Configuration.GetSection(IdentityOptions.SectionName);
            services.ConfigureWritable<IdentityOptions>(identityConfigSection);
            services.Configure<UserServiceOptions>(builder.Configuration.GetSection(UserServiceOptions.SectionName));
            services.AddScoped<IUserService, UserService>();

            services.AddAuthorization(o => o.AddPolicy("RequireAuthenticatedUserPolicy", b => b.RequireAuthenticatedUser()));

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    var identityOptions = identityConfigSection.Get<IdentityOptions>();
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = identityOptions.JwtIssuer,
                        ValidateAudience = true,
                        ValidAudience = identityOptions.JwtIssuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identityOptions.JwtKey)),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });

            // ====== Configure services ======
            var app = builder.Build();

            var writableOptions = app.Services.GetRequiredService<IWritableOptions<IdentityOptions>>();
            writableOptions.UpdateAsync(o =>
            {
                if (o.JwtKey == "___SOME_RANDOM_KEY_DO_NOT_SHARE___")
                {
                    o.JwtKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                }
                //if (string.IsNullOrEmpty(o.JwtIssuer))
                //{
                //    o.JwtIssuer = "WeatherDisplayApi";
                //}
            });


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
            app.MapControllers().RequireAuthorization("RequireAuthenticatedUserPolicy");

            // ===== Use Swagger ======
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint($"/swagger/{swaggerVersion}/swagger.json", swaggerVersion);
                o.RoutePrefix = "swagger";
                o.InjectStylesheet("/swagger-ui/SwaggerStyle.css");
            });

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
