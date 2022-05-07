using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WeatherDisplay.Api.Services;
using WeatherDisplay.Extensions;
using NLog.Extensions.Logging;
using NLog;

var builder = WebApplication.CreateBuilder(args);
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
builder.Services.AddSwaggerGen(option =>
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

// ====== Weather services ======
services.AddWeatherDisplay(builder.Configuration);
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
app.UseSwaggerUI();

app.Run();
