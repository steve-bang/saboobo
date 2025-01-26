
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace SaBooBo.Domain.Shared.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddDefaultAuthentication(this IHostApplicationBuilder builder)
    {
        var servivces = builder.Services;
        var configuration = builder.Configuration;

        var identitySection = configuration.GetSection("Identity");

        if (!identitySection.Exists())
        {
            return servivces;
        }

        servivces
            .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
            options =>
            {
                var identityUrl = identitySection.GetValue<string>("Issuer");
                var audience = identitySection.GetValue<string>("Audience");
                var secretKey = identitySection.GetValue<string>("Secret");

                if (string.IsNullOrEmpty(secretKey))
                    throw new NullReferenceException("Secret key is not provided.");



                options.Authority = identityUrl;
                options.Audience = audience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = identityUrl,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context => {
                        throw new UnauthorizedException();
                    } 
                };
            });

        servivces.AddAuthorization();


        return servivces;
    }
}