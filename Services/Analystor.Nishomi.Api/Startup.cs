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
using SimpleInjector;
using SimpleInjector.Lifestyles;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this._container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            this._container.RegisterFrameworkDependencies(services,this.Configuration);
            this._container.RegisterApplicationDependencies(services, this.Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<NishomiDbContextProvider, NishomiDbContextProvider>();
            services.AddDbContext<NishomiDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("NishomiDbContext")));
            services.AddControllers();
            //services.AddSingleton<ICategory, CategoryService>();
            //services.AddSingleton<IProduct, ProductService>();
            //services.AddSingleton<ICustomerRequest, CustomerService>();
            //services.AddSingleton<IMailService, MailService>();
            services.AddCors(options =>
            {
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());
            });
            // Mail Service
            //services.AddScoped<SmtpClient>((serviceProvider) =>
            //{
            //    var config = serviceProvider.GetRequiredService<IConfiguration>();
            //    return new SmtpClient()
            //    {
            //        Host = config.GetValue<string>("Email:Smtp:Host"),
            //        Port = config.GetValue<int>("Email:Smtp:Port"),
            //        Credentials = new NetworkCredential(
            //                config.GetValue<string>("Email:Smtp:Username"),
            //                config.GetValue<string>("Email:Smtp:Password"))
            //    };
            //});
            //services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSimpleInjector(this._container, options =>
            {
                options.AutoCrossWireFrameworkComponents = false;
            });

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
