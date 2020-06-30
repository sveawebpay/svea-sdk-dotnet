using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Svea.WebPay.SDK.Extensions;

using Sample.AspNetCore.Data;
using Sample.AspNetCore.Extensions;
using Sample.AspNetCore.Models;

namespace Sample.AspNetCore
{
    using Newtonsoft.Json;

    using Svea.WebPay.SDK.Extensions;

    using System;

    using JsonSerializer = System.Text.Json.JsonSerializer;

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
            var sveaApiUrlsSettings = Configuration.GetSection("SveaApiUrls");
            services.Configure<SveaApiUrls>(sveaApiUrlsSettings);
            var sveaApiUrls = sveaApiUrlsSettings.Get<SveaApiUrls>();

            var credentialsSettings = Configuration.GetSection("Credentials");
            services.Configure<Credentials>(sveaApiUrlsSettings);
            var credentials = credentialsSettings.Get<Credentials>();

            var merchantSettingsSettings = Configuration.GetSection("MerchantSettings");
            services.Configure<MerchantSettings>(sveaApiUrlsSettings);
            var merchantSettings = merchantSettingsSettings.Get<MerchantSettings>();

            services.AddTransient(s => merchantSettings);
            services.AddDbContext<StoreDbContext>(options => options.UseInMemoryDatabase("Products"));
            services.AddControllersWithViews();
            services.AddControllers();
            services.AddDistributedMemoryCache();

            services.Configure<MerchantSettings>(Configuration.GetSection("MerchantSettings"));
            services.AddScoped(provider => SessionCart.GetCart(provider));
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            Console.WriteLine("Credentials");
            Console.WriteLine(JsonSerializer.Serialize(credentialsSettings));
            Console.WriteLine(JsonSerializer.Serialize(credentials));

            Console.WriteLine("MerchantSettings");
            Console.WriteLine(JsonSerializer.Serialize(merchantSettingsSettings));
            Console.WriteLine(JsonSerializer.Serialize(merchantSettings));

            Console.WriteLine("SveaApiUrls");
            Console.WriteLine(JsonSerializer.Serialize(sveaApiUrlsSettings));
            Console.WriteLine(JsonSerializer.Serialize(sveaApiUrls));

            services.AddSveaClient(sveaApiUrls.CheckoutApiUri, sveaApiUrls.PaymentAdminApiUri, credentials.MerchantId, credentials.Secret);
            services.AddSession();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "products",
                    "{controller=Products}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
