using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JsonBot.Sample.Bots
{
    public class SimpleBot : ActivityHandler
    {
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            await turnContext.SendActivityAsync("Hi! I'm a simple bot that annoys you echoing your messages!!");
            await turnContext.SendActivityAsync("Like this....");
            await Task.Delay(500);
            await turnContext.SendActivityAsync("_this..._");
            await Task.Delay(1000);
            await turnContext.SendActivityAsync("_this......_");
            await base.OnMembersAddedAsync(membersAdded, turnContext, cancellationToken);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            await turnContext.SendActivityAsync($"echo: _{turnContext.Activity.Text}_");
            await base.OnMessageActivityAsync(turnContext, cancellationToken);
        }
    }
}
