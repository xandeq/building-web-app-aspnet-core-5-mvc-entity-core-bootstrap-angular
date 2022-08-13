using DutchTreat.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DutchTreat
{
    public class Program
    {
        /// <summary>
        /// Just like a console app it is self hosted. 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            //if (args.Length == 1 && args[0].ToLower() == "/seed")
            //{
            RunSeeding(host);
            //}
            //else
            //{
            host.Run();
            //}
        }

        private static void RunSeeding(IWebHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                seeder.Seed();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
          .ConfigureAppConfiguration(SetupConfiguration)
                .UseStartup<Startup>()
                .Build();

        /// <summary>
        /// To configure our project, we are going to go hardcore, to add configuration manually.
        /// We use the builder object and use the methods on it to add configuration files.
        /// In case of conflicts in values among different configuration files, the order of 
        /// precedence depends on the order in which you add it.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="builder"></param>
        private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            //remove default configuration objects so that we can customize this to the core
            builder.Sources.Clear();

            //asp.net used to use web.config but this was cumbersome.
            //aspnetcore however provides a more flexible configuration system
            //several different types of configuration options available
            builder.AddJsonFile("config.json", optional: false, reloadOnChange: true)
              //.AddXmlFile("config.xml", optional: true)
              .AddEnvironmentVariables();
        }
    }
}