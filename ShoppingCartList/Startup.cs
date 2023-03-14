using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCartList;
using System;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ShoppingCartList;

public class Startup : FunctionsStartup
{
    private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
        .SetBasePath(Environment.CurrentDirectory)
        .AddJsonFile("local.settings.json", optional: true)
        .Build();


    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddSingleton(s =>
        {
            var connectionString = Configuration["Values:CosmosDBConnection"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Please specify a valid CosmosDbConnections in local.settings.json file");
            }

            return new CosmosClientBuilder(connectionString)
            .Build();
        });
    }
}
