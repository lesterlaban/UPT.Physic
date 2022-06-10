using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using UPT.Physic.DataAccess;

namespace UPT.Physic.Extensions
{
	public static class ServiceExtensions
	{
        /// <summary>
        /// Method to configure the services used by the application, using dependency injection 
        /// </summary>
        /// <param name="services">collection of services</param>
        public static void ConfigureDataServices(this IServiceCollection services)
        {
            services.AddScoped<Context>();
            services.AddScoped<IRepository, Repository>();
        }

        /// <summary>
        /// Method to configure swagger service with Odata
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";
                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Foo {groupName}",
                    Version = groupName,
                    Description = "Foo API",
                    Contact = new OpenApiContact
                    {
                        Name = "Foo Company",
                        Email = string.Empty,
                        Url = new Uri("https://foo.com/"),
                    }
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                        },
                        new string[] { }
                    }
                });

            });
        }






    }
}
