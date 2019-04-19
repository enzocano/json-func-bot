using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Bot.Builder.Integration.AspNet.Functions;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Builder;

namespace StaticFunctionBot
{
    public static class MessageTrigger
    {
        [FunctionName("messages")]
        public static Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            ICredentialProvider credentialProvider = ConfigurationCredentialProvider.FromExecutionContext(context);
            IBotFrameworkFunctionsAdapter adapter = new BotFrameworkFunctionsAdapter(credentialProvider, logger: log);
            IBot bot = new StaticBot();

            return adapter.ProcessAsync(req, bot);
        }
    }
}
