namespace DIFunctionBot
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Bot.Builder.Integration.AspNet.Functions;
    using Microsoft.Bot.Builder;

    public class MessageTrigger
    {
        private readonly IBotFrameworkFunctionsAdapter adapter;
        private readonly IBot bot;

        public MessageTrigger(IBotFrameworkFunctionsAdapter adapter, IBot bot)
        {
            this.adapter = adapter;
            this.bot = bot;
        }

        [FunctionName("messages")]
        public Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            return adapter.ProcessAsync(req, bot);
        }
    }
}
