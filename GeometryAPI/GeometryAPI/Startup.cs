using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GeometryAPI.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace GeometryAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<DbPostgreContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("PostgreConnection")));

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Первая версия API",

                    Contact = new Contact
                    {
                        Name = "Нажми для миграции",
                        Url = "/api/values"
                    }
                });
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(options =>
          {

              options.Cookie.Name = "GeometryCookie";
              options.ExpireTimeSpan = TimeSpan.FromDays(2);
              options.SlidingExpiration = false;
              options.Events = new CookieAuthenticationEvents
              {
                  OnRedirectToLogin = context =>
                  {
                      context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                      return Task.CompletedTask;
                  },
                  OnRedirectToAccessDenied = context =>
                  {
                      context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                      return Task.CompletedTask;
                  }
              };

          });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;

            });

            app.UseCors(builder =>
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                    .AllowAnyHeader()
                        .AllowCredentials()
                        );

            app.UseAuthentication();

            app.UseSwagger();

            app.UseMvc();
        }
    }
}
