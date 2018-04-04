using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmergencyEntity.Configuration;
using Exceptionless;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApi.Filter;
using WebApi.FrameWork;
using AutoMapper;
using WebApi.FrameWork.AutoMapper;

namespace WebApi
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var builder = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
             .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();

            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(AuthenticationFilter));
                options.Filters.Add(typeof(ValidationActionFilter));
            });
            services.AddMvc();

            //注入配置信息
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            ApplicationContainer = AutofacBuilder.Builder(services);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration.GetSection("jwt").GetSection("issure").Value,
                            ValidAudience = Configuration.GetSection("jwt").GetSection("audience").Value,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Configuration.GetSection("jwt").GetSection("securitykey").Value))
                        };
                    });

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(ApplicationContainer);

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("any");
            //MapperInit.InitMapping();
            app.UseExceptionless("riuCGjWnRDEXcvLASaeRHVdYE9OxHyFtb9SBXPvU");
            app.UseMiddleware<ExceptionHandlerMiddleWare>();
            app.UseAuthentication();
            app.UseMvc();
            app.UseStaticFiles();
        }
    }
}
