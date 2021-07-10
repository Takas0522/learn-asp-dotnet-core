using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAdWithB2C
{
    public static class MultipleAuth
    {
        public static void AddAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            // https://stackoverflow.com/questions/55852498/asp-net-core-authenticate-with-aad-and-b2c
            services.AddAuthentication()
                .AddJwtBearer("AAD", options =>
                {
                    options.MetadataAddress = configuration["AzureAd:Instance"] + configuration["AzureAd:TenantId"] +
                                              "/v2.0/.well-known/openid-configuration";
                    options.Authority = configuration["AzureAd:Instance"] + configuration["AzureAd:TenantId"];
                    options.Audience = configuration["AzureAd:ClientId"];
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidIssuer = $"https://sts.windows.net/{configuration["AzureAd:TenantId"]}/",
                        };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context => Task.CompletedTask,
                        OnChallenge = context => Task.CompletedTask,
                        OnAuthenticationFailed = (context) =>
                        {
                            Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("Validated: " + context.SecurityToken);
                            return Task.CompletedTask;
                        }
                    };
                })
                .AddJwtBearer("B2C", options =>
                {
                    options.Authority = configuration["AzureAdB2C:Instance"] + configuration["AzureAdB2C:Domain"] + "/" + configuration["AzureAdB2C:SignUpSignInPolicyId"] + "/v2.0";
                    options.Audience = configuration["AzureAdB2C:ClientId"];

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context => Task.CompletedTask,
                        OnChallenge = context => Task.CompletedTask,
                        OnAuthenticationFailed = (context) =>
                        {
                            Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("Validated: " + context.SecurityToken);
                            return Task.CompletedTask;
                        }
                    };
                });
            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy("AADUsers", new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .AddAuthenticationSchemes("AAD")
                        .Build());

                    options.AddPolicy("B2CUsers", new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .AddAuthenticationSchemes("B2C")
                        .Build());
                });
        }
    }
}
