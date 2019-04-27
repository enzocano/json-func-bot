namespace FunctionBot
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Bot.Builder.Integration.AspNet.Functions;
    using Microsoft.Bot.Builder;
    using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

    public static class MessageTrigger
    {
        [FunctionName("messages")]
        public static Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Inject] IBotFrameworkFunctionsAdapter adapter,
            [Inject] IBot bot)
        {
            return adapter.ProcessAsync(req, bot);
        }
    }
}
