using JsonBot.Sample.Integration;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using System;
using System.Threading.Tasks;

namespace JsonBot.Sample.Adapters
{
    public class OnboardingAdapter : BotFrameworkFunctionsAdapter
    {
        public OnboardingAdapter(
            ICredentialProvider credentialProvider,
            IChannelProvider channelProvider,
            BotStateSet botStateSet)
            : base(credentialProvider, channelProvider)
        {
            OnTurnError = TurnErrorHandler;
            Use(new ShowTypingMiddleware());
            Use(new AutoSaveStateMiddleware(botStateSet));
        }

        private async Task TurnErrorHandler(ITurnContext turnContext, Exception exception)
        {
            var card = new BasicCard()
            {
                Title = "Oh, snap!",
                Subtitle = exception.Message,
                Text = exception.StackTrace
            };

            var attachment = new Attachment()
            {
                Content = card
            };

            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment));
        }
    }
}
