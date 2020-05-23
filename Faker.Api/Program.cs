using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Faker.Data;
using Faker.Data.Models;

namespace Faker.Api
{

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemory")
            .Options;

            using (var context = new AppDbContext(options))
            {
                context.Initialize();
            }

            await Host.CreateDefaultBuilder(args)
                              .ConfigureWebHostDefaults(builder =>
                               {
                                   builder.UseStartup<Startup>();
                               })
                              .ConfigureServices(services =>
                               {
                                   services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);
                                   services.AddDbContext<AppDbContext>((options, context) => context.UseInMemoryDatabase("InMemory"));
                                   services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());
                               })
                              //   .ConfigureLogging((context, logging) =>
                              //   {
                              //       logging.AddConfiguration(context.Configuration);
                              //       logging.AddConsole();
                              //   })
                              .RunConsoleAsync(config => config.SuppressStatusMessages = true);

        }
    }
}

