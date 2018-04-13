using System;
using Masato.OAuth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.HttpOverrides;

namespace Masato
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
            if (String.IsNullOrWhiteSpace(Configuration["anilist:clientId"]))
                throw new InvalidOperationException("Client ID and Client Secret must be configured for Anilist.");

            services.AddMvc();
            services.AddSingleton<Data>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(o => o.LoginPath = new PathString("/login"))
              .AddAnilist(o =>
                {
                    o.ClientId = Configuration["anilist:clientId"];
                    o.ClientSecret = Configuration["anilist:clientSecret"];
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseAuthentication()
                .UseMvc();
        }
    }
}
