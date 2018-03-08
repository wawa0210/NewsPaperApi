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

namespace WebApi
{
    public class Startup
    {

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var builder = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
             .AddEnvironmentVariables();
            Configuration = builder.Build();
            StaticConfiguration = configuration;
        }

        public IConfiguration StaticConfiguration { get; }

        public IConfigurationRoot Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
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

            //注入配置信息
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));


            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = StaticConfiguration["jwt:issure"],
                            ValidAudience = StaticConfiguration["jwt:audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(StaticConfiguration["jwt:securitykey"]))
                        };
                    });

            ApplicationContainer = AutofacBuilder.Builder(services);
            services.AddMvc();
            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(ApplicationContainer);

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<ExceptionHandlerMiddleWare>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("any");
            MapperInit.InitMapping();
            app.UseExceptionless("riuCGjWnRDEXcvLASaeRHVdYE9OxHyFtb9SBXPvU");
            app.UseAuthentication();
            app.UseMvc();
            app.UseStaticFiles();
        }
    }
}
