namespace JsonBot.Sample
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Bot.Builder.Integration.AspNet.Core;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.Bot.Connector.Authentication;

    public static class MessageTrigger
    {
        [FunctionName("messages")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var adapter = new BotFrameworkHttpAdapter(new SimpleCredentialProvider("<--app-id-->", @"<--app-password-->"));

            var res = new DefaultHttpResponse(new DefaultHttpContext());

            var bot = new FuncBot();

            await adapter.ProcessAsync(req, res, bot);

            return res.StatusCode == 200
                ? (ActionResult)new OkResult()
                : new BadRequestObjectResult(res.Body);
        }
    }
}
