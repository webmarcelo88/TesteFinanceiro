﻿using AutoMapper;
using Financeiro.ClientServices.ClientServices;
using Financeiro.ClientServices.Interfaces;
using Financeiro.Util.Configuracao;
using Financeiro.Util.ConfiguracaoAutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Financeiro.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private MapperConfiguration MapperConfiguration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperModelToService());
                cfg.AddProfile(new AutoMapperServiceToModel());
            });
            services.AddSingleton(sp => MapperConfiguration.CreateMapper());


            services.AddOptions();

            services.Configure<CustomConfiguration>(Configuration.GetSection("CustomConfiguration"));

            services.AddScoped<IBalancoClientService, BalancoClientServices>();
            services.AddScoped<ILancamentoFinanceiroClientServices, LancamentoFinanceiroClientService>();

            HttpClient httpClient = new HttpClient() { };

            services.AddSingleton<HttpClient>(httpClient);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Financeiro}/{action=Index}/{id?}");
            });
        }
    }
}
