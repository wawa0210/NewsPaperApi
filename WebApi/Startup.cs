using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CommonLib.Extensions;
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
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;
//using zipkin4net;
//using zipkin4net.Middleware;
//using zipkin4net.Tracers.Zipkin;
//using zipkin4net.Transport.Http;

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
            //替换控制器所有者
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

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

            //注入配置信息
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            //添加jwt认证
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
                            ClockSkew = TimeSpan.FromSeconds(StaticConfiguration["jwt:expireseconds"].ToInt32(1800)),
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


            var applicationName ="node1";
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //var lifetime = app.ApplicationServices.GetService<IApplicationLifetime>();
            //lifetime.ApplicationStarted.Register(() => {
            //    TraceManager.SamplingRate = 1.0f;
            //    var logger = new TracingLogger(loggerFactory, "zipkin4net");
            //    var httpSender = new HttpZipkinSender("http://localhost:9411", "application/json");
            //    var tracer = new ZipkinTracer(httpSender, new JSONSpanSerializer());
            //    TraceManager.RegisterTracer(tracer);
            //    TraceManager.Start(logger);
            //});
            //lifetime.ApplicationStopped.Register(() => TraceManager.Stop());
            //app.UseTracing(applicationName);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("any");
            //MapperInit.InitMapping();
            app.UseExceptionless("riuCGjWnRDEXcvLASaeRHVdYE9OxHyFtb9SBXPvU");
            app.UseMiddleware<ExceptionHandlerMiddleWare>();
            //app.UseExceptionHandler(appBuilder =>
            //{
            //    appBuilder.Use(async (context, next) =>
            //    {
            //        await new ExceptionMiddleware().HandleExceptionAsync(context, next);
            //    });
            //}
            //);
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
