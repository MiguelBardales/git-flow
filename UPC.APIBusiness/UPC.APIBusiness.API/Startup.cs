using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBContext;
using DBEntity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using static System.Net.Mime.MediaTypeNames;

namespace API
{
    /// <summary>
    /// /
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // TODO: Configuration Section
            services.Configure<DBEntity.ConfigSettings>(Configuration.GetSection("ConfigSettings"));
            DBEntity.ConfigSettings config = Configuration.GetSection("ConfigSettings").Get<DBEntity.ConfigSettings>();
            AppSettingsProvider.config = config;

            //services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            //{
            //    builder.WithOrigins("https://codigoprivado.com/",
            //                            "https://servicio.codigoprivado.com")
            //           .AllowAnyMethod()
            //           .AllowAnyHeader()
            //          ;
            //}));



            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularOrigins",
                builder =>
                {
                    //builder.WithOrigins("http://localhost", "https://codigoprivado.com")
                    //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost", "https://codigoprivado.com")                    
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();                    
                });
            });


            services.AddAuthentication("Bearer")
              .AddJwtBearer("Bearer", options =>
              {
                  options.Authority = AppSettingsProvider.config.UrlBaseIdentityServer;
                  options.RequireHttpsMetadata = false;
                  options.RefreshOnIssuerKeyNotFound = true;
                  options.Audience = "API-APP-CP";
              });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    name: AppSettingsProvider.config.Version,
                    new OpenApiInfo
                    {
                        Title = AppSettingsProvider.config.ApplicationName,
                        Version = AppSettingsProvider.config.Version,
                        Contact = new OpenApiContact()
                        {
                            Name = AppSettingsProvider.config.OrganizationName,
                            Url = new System.Uri("https://servicio.codigoprivado.com/"),
                            Email = "miguel_mynet@hotmail.com",
                        },
                        Description = AppSettingsProvider.config.ApplicationDescription,

                        License = new OpenApiLicense() { Name = AppSettingsProvider.config.OrganizationName, Url = new System.Uri("https://servicio.codigoprivado.com/") },
                        TermsOfService = new System.Uri("https://servicio.codigoprivado.com/")
                    });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Esta api usa Authorization  basada en JWT.-  
                      Ingrese 'Bearer' [space] y luego el token de autenticación.
                      Ejemplo: 'Bearer HJNX4354X...'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });
            });


            //TODO: Registrar las interfaces para Inyección de Dependencias

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();

            // ----------------------------------------------------------------
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{AppSettingsProvider.config.Version}/swagger.json", $"{AppSettingsProvider.config.ApplicationName}");
            });
       

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseCors("AllowAngularOrigins");
            app.UseAuthorization();

            //app.UseCors("MyPolicy");
            //app.UseCors("AllowAngularOrigins");
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
