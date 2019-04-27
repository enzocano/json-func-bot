namespace DIFunctionBot.Bots
{
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SimpleBot: ActivityHandler
    {
        protected override Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            return turnContext.SendActivityAsync(MessageFactory.Text($"Welcome {membersAdded.FirstOrDefault().Name}!\nYou are using Azure functions with DI!!"));
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            await turnContext.SendActivityAsync(MessageFactory.Text($"echo: _{turnContext.Activity.Text}_"));
            await turnContext.SendActivityAsync(MessageFactory.Text($"from <F> with \U0001f497"));
        }
    }
}
