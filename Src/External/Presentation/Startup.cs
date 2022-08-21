using Application.Services;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.EntityFrameworkCore.Context;
using Infrastructure.EntityFrameworkCore.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddNewtonsoftJson();

            // Registry the DbContext
            services.AddDbContext<RapidPayContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("Connection")));


            //Repositories Injection
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ICardsRepository, CardsRepository>();
            services.AddScoped<IRecordsRepository, RecordsRepository>();

            //Services Injections
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddScoped<ICardsServices, CardsServices>();
            services.AddScoped<IRecordsServices, RecordsServices>();

            //Jwt Configurations
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["JWT:KeySecret"])
                    )
                };
            });

            //Hosted Service
            services.AddHostedService<UFEHostedServices>();

            //Add Swagger
            AddSwagger(services);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Authorization by Jwt inside request's header",
                    Scheme = "ApiKeyScheme"
                });
                var key = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    },
                    In = ParameterLocation.Header
                };
                var requirement = new OpenApiSecurityRequirement
                {
                   { key, new List<string>() }
                };
                options.AddSecurityRequirement(requirement);


                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $" {groupName}",
                    Version = groupName,
                    Description = "TestRapiPay Documentation",
                    Contact = new OpenApiContact
                    {
                        Name = "Roy Roger Martinez Cano",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/RoyMartinez"),
                    }
                });
            });
        }
    }
}
