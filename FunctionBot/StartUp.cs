using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(FunctionBot.StartUp))]
namespace FunctionBot
{
    using FunctionBot.Adapters;
    using FunctionBot.Bots;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Hosting;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Integration.AspNet.Functions;
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Extensions.DependencyInjection;
    using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

    public class StartUp : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddDependencyInjection(SimpleWire);
        }

        private void SimpleWire(IServiceCollection services)
        {
            services.AddSingleton<ICredentialProvider>(new SimpleCredentialProvider("0eaea65c-10a8-4077-8ce6-ef48049b6571", "fxtVGJV6387=iolcQCG4$?!"));
            services.AddSingleton<IBotFrameworkFunctionsAdapter, SimpleAdapter>();
            services.AddSingleton<IBot, SimpleBot>();
        }
    }
}
