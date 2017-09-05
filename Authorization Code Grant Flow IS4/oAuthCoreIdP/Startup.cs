using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using IdentityServer4;
using IdentityModel;

namespace oAuthCoreIdP
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //Grab key for verifying JWT signature
            //In prod, we'd get this from the certificate store or similar
            var certPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "SscSign.pfx");
            var cert = new X509Certificate2(certPath); 

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddDeveloperIdentityServer(options =>
                {
                    options.IssuerUri = "SomeSecureCompany";
                })
                .AddInMemoryScopes(Scopes.Get())
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryUsers(Users.Get())
                .SetSigningCredential(cert);

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();
            app.UseStaticFiles();


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "external",
                AutomaticAuthenticate = false,
                AutomaticChallenge = false
            });

            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "Google",
                DisplayName = "Google",
                //SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                SignInScheme = "external",
                ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com",
                ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo"
            });

            app.UseFacebookAuthentication(new FacebookOptions
            {
                AuthenticationScheme="Facebook",
                DisplayName="Facebook",
                SignInScheme="external",
                AppId = "1979654995605301",
                AppSecret= "84434289758c4b05ec4e43e41057edae"
            });

            app.UseMicrosoftAccountAuthentication(new MicrosoftAccountOptions
            {
                AuthenticationScheme="Microsoft",
                DisplayName="Microsoft",
                SignInScheme="external",
                ClientId= "84cfbc40-8dfa-478b-9c9e-25b0ea3c8a7a",
                ClientSecret= "OefRqDOqLgkJq7i5pbQpKoH"
            });

            app.UseTwitterAuthentication(new TwitterOptions
            {
                AuthenticationScheme = "Twitter",
                DisplayName = "Twitter",
                SignInScheme = "external",
                ConsumerKey = "9G9IbXR7lDbG3d3Yeh2A7OMH2",
                ConsumerSecret = "a7L1aM2DzQaPD5UkxjL9uBPh1lF4Ot88eBqcMIw3ttZ3AEyDV2"
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
