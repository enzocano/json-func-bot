using JsonBot.Sample.Integration;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace JsonBot.Sample.Adapters
{
    public class SimpleAdapter : BotFrameworkFunctionsAdapter
    {
        public SimpleAdapter(
            ICredentialProvider credentialProvider,
            IChannelProvider channelProvider,
            ILogger logger = null)
            : base(credentialProvider, channelProvider, logger)
        {
            OnTurnError = TurnErrorHandler;
        }

        private async Task TurnErrorHandler(ITurnContext turnContext, Exception exception)
        {
            await turnContext.SendActivityAsync(MessageFactory.Text($"Oh, snap! {exception.Message}"));
        }
    }
}
