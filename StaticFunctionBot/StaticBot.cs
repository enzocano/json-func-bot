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
            var card = new ThumbnailCard()
            {
                Images = new CardImage[] { new CardImage("https://twitter.com/AzureFunctions/profile_image?size=bigger") },
                Title = "Hello from Azure Functions"
            }.ToAttachment();

            return turnContext.SendActivityAsync(MessageFactory.Attachment(card));
        }
    }
}
