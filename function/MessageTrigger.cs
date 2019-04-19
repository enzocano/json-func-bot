using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using JsonBot.Sample.Integration;
using Microsoft.Bot.Builder;

namespace JsonBot.Sample
{
    public class MessageTrigger
    {
        private readonly IBotFrameworkFunctionsAdapter _adapter;
        private readonly IBot _bot;

        public MessageTrigger(
            IBotFrameworkFunctionsAdapter adapter,
            IBot bot)
        {
            _adapter = adapter;
            _bot = bot;
        }

        [FunctionName("messages")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            return await _adapter.ProcessAsync(req, _bot);
        }
    }
}
