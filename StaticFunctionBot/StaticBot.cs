using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace StaticFunctionBot
{
    public class StaticBot: ActivityHandler
    {
        protected override Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            return turnContext.SendActivityAsync("Hello from <F>!");
        }
    }
}
