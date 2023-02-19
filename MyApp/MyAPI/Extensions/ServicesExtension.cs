using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyAPI.MyJWT.Contracts;
using MyAPI.MyJWT.Repositories;
using MyPersistence;
using MyPersistence.Contracts;
using MyPersistence.Repositories;
using System.Text;

namespace MyAPI.Extensions
{
    public static class ServicesExtension
    {
        public static void AddJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
                    };
                    
                    opt.Events = new JwtBearerEvents // Add an event that let us know if the JWT Access Token is expired
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true"); // Clue to helps us to call the refresh IAction method from the client-side.

                            return Task.CompletedTask;
                        }
                    };
                });
        }

        public static void AddOracle(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Oracle");

            services.AddDbContext<MyDbContext>(opt => opt.UseOracle(connectionString));
        }

        public static void AddJWTRepo(this IServiceCollection services)
            => services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();

        public static void AddRepositories(this IServiceCollection services)
            => services.AddScoped<IUserServiceRepository, UserServiceRepository>();
    }
}
