using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace api.leads
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(
                    (hostingContext, config) =>
                    {
                        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                        {
                            var azSvcProvider = new AzureServiceTokenProvider();
                            var authCallBack = new KeyVaultClient.AuthenticationCallback(azSvcProvider.KeyVaultTokenCallback);
                            var kvClient = new KeyVaultClient(authCallBack);

                            config.AddAzureKeyVault("https://dev-api-kv.vault.azure.net/", kvClient, new DefaultKeyVaultSecretManager());
                        }
                        else
                        {
                            // Add Key Vault Mount.
                            if (Directory.Exists("/kvmnt"))
                            {
                                config.AddKeyPerFile("/kvmnt", optional: false);
                            }
                        }
                        config.AddJsonFile("appsettings.json", false, true);
                        config.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("HYC_ENV")}.json", optional: false, reloadOnChange: true);
                        config.AddEnvironmentVariables().Build();
                    }
                )
                .UseStartup<Startup>();
    }
}
