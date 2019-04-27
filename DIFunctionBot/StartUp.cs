using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(DIFunctionBot.StartUp))]
namespace DIFunctionBot
{
    using DIFunctionBot.Adapters;
    using DIFunctionBot.Bots;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Hosting;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Integration.AspNet.Functions;
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Extensions.DependencyInjection;

    public class StartUp : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddSingleton<ICredentialProvider, ConfigurationCredentialProvider>();
            builder.Services.AddSingleton<IBotFrameworkFunctionsAdapter, SimpleAdapter>();
            builder.Services.AddSingleton<IBot, SimpleBot>();
        }
    }
}
