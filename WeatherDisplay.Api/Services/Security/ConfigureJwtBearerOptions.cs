using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WeatherDisplay.Api.Services.Security
{
    /// <summary>
    /// Configures JWT bearer authentication from the configured identity options.
    /// </summary>
    public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly IOptionsMonitor<IdentityOptions> identityOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureJwtBearerOptions"/> class.
        /// </summary>
        /// <param name="identityOptions">The identity options used for token validation.</param>
        public ConfigureJwtBearerOptions(IOptionsMonitor<IdentityOptions> identityOptions)
        {
            this.identityOptions = identityOptions;
        }

        /// <inheritdoc />
        public void Configure(JwtBearerOptions options)
        {
            this.Configure(JwtBearerDefaults.AuthenticationScheme, options);
        }

        /// <inheritdoc />
        public void Configure(string name, JwtBearerOptions options)
        {
            if (name != JwtBearerDefaults.AuthenticationScheme)
            {
                return;
            }

            var identityOptions = this.identityOptions.CurrentValue;
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
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
        }
    }
}
