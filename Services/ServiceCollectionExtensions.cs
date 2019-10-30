using EmpSubbieWebAPI.Data.Repositories;
using EmpSubbieWebAPI.Models;
using EmpSubbieWebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace EmpSubbieWebAPI.Middleware
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// App Configuration Settings
        /// </summary>
        public static IServiceCollection ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration);          
            //services.Configure<EmailConfig>(configuration.GetSection("Email"));

            return services;
        }

        /// <summary>
        /// Add Custom Services and Repositories
        /// </summary>       
        public static IServiceCollection AddCustomServicesAndRepositories(this IServiceCollection services)
        {
            // Add the Data Repository
            services.AddScoped<IDataRepository, DataRepository>();
            
            // Add any other required services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJWTService, JWTService>();          
            
            return services;
        }

        /// <summary>
        /// Add JWT Bearer Authentication
        /// </summary>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfigurationSection jwtConfig)
        {

            services.AddAuthentication(options => { options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
            })
                .AddJwtBearer(opts => {
                    opts.RequireHttpsMetadata = false;                    
                    opts.SaveToken = false;                    
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["SecretKey"].ToString())),
                        ValidateIssuer = true,
                        ValidIssuer = jwtConfig["ValidIssuer"].ToString(),
                        ValidateAudience = true,
                        ValidAudience = jwtConfig["ValidAudience"].ToString(),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}
