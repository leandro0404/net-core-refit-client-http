using App.Demo.Client.Interfaces;
using App.Demo.Client.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Refit;
using System;
using System.IO;
using System.Threading.Tasks;

namespace App.Demo.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetService<IPostService>();
            var posts = await service.Get();
            var postById = await service.GetById(1);
            var created = await service.Created(new Entities.Post() { Id = 10, Title = "dsadsa" });
            var update = await service.Update(1, new Entities.Post() { Id = 1, Title = "dsadsa" });
            service.Remove(1).Wait();

            Print(posts, postById);
        }

        #region Config and Print
        public static void Print(object posts, object postById)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("LIST POSTS \n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(JsonConvert.SerializeObject(posts, Formatting.Indented));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n POST BY ID \n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(JsonConvert.SerializeObject(postById, Formatting.Indented));
            Console.ReadKey();
        }

        private const string AppSettingsUrl = "Configuration:URL_API";
        private static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            //Service
            services.AddTransient<IPostService, PostService>();
            // build configuration
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("app-settings.json", false)
            .Build();
            services.AddSingleton(configuration);
            //Refit injection http client
            services.AddRefitClient<IPostService>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration.GetValue<string>(AppSettingsUrl));
                //c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("scheme and token");
            });

            return services;
        }
        #endregion
    }
}
