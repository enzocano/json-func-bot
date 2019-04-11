using JsonBot.Sample;
using JsonBot.Sample.Adapters;
using JsonBot.Sample.Bots;
using JsonBot.Sample.Dialogs;
using JsonBot.Sample.Integration;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: WebJobsStartup(typeof(StartUp))]
namespace JsonBot.Sample
{
    public class StartUp : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddLogging();
            builder.Services.AddTransient<ICredentialProvider, ConfigurationCredentialProvider>();
            builder.Services.AddTransient<CosmosDbStorageOptions, MyCosmosDbStorageOptions>();
            builder.Services.AddTransient<IBotFrameworkFunctionsAdapter, OnboardingAdapter>();
            builder.Services.AddTransient<IStorage>(sp => new CosmosDbStorage(sp.GetService<CosmosDbStorageOptions>()));
            builder.Services.AddTransient<UserState>();
            builder.Services.AddTransient<ConversationState>();
            builder.Services.AddTransient(sp => new BotStateSet(sp.GetService<UserState>(), sp.GetService<ConversationState>()));
            builder.Services.AddTransient<UserProfileDialog>();
            builder.Services.AddTransient<IBot, OnboardingBot>();
        }
    }

    internal class MyCosmosDbStorageOptions : CosmosDbStorageOptions
    {
        public MyCosmosDbStorageOptions(IConfiguration configuration)
        {
            CosmosDBEndpoint = new Uri(configuration.GetSection("CosmosDBEndpoint")?.Value);
            AuthKey = configuration.GetSection("AuthKey")?.Value;
            CollectionId = "botstate-collection";
            DatabaseId = "botstate-db";
        }
    }
}
