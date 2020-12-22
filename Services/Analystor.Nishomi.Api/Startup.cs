using Analystor.Nishomi.Api.Config;
using Analystor.Nishomi.Core;
using Analystor.Nishomi.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Analystor.Nishomi.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// The container
        /// </summary>
        private Container _container = new Container();

        public IConfiguration Configuration { get; }

        //public ILoggerFactory loggerFactory { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.RegisterFrameworkDependencies(this.Configuration);
            //services.RegisterApplicationDependencies(this.Configuration);
            //this._container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            this._container.RegisterFrameworkDependencies(services,this.Configuration);
            this._container.RegisterApplicationDependencies(services, this.Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<NishomiDbContextProvider, NishomiDbContextProvider>();
            services.AddDbContext<NishomiDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("NishomiDbContext")));
            //services.AddSingleton<Microsoft.AspNetCore.Hosting.IHostingEnvironment, Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
            //services.AddControllers();
            //services.AddLogging();
            ////services.AddSingleton<ICategory, CategoryService>();
            ////services.AddSingleton<IProduct, ProductService>();
            ////services.AddSingleton<ICustomerRequest, CustomerService>();
            services.AddCors(options =>
            {
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());
            });
            // Mail Service
            services.AddScoped<SmtpClient>((serviceProvider) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                return new SmtpClient()
                {
                    Host = config.GetValue<string>("Email:Smtp:Host"),
                    Port = config.GetValue<int>("Email:Smtp:Port"),
                    Credentials = new NetworkCredential(
                            config.GetValue<string>("Email:Smtp:Username"),
                            config.GetValue<string>("Email:Smtp:Password"))
                };
            });
            //services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseHsts();
            }

            app.UseCors("CorsPolicy");

            app.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseStaticFiles();

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, CommonConstants.ResourcesFolder)),
            //    RequestPath = new PathString(CommonConstants.ResourcesProviderUrl)
            //});

            //app.UseResponseWrapper();

            app.UseApiExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
