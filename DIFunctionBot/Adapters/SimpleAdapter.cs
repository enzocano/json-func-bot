namespace DIFunctionBot.Adapters
{
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Integration.AspNet.Functions;
    using Microsoft.Bot.Connector.Authentication;
    using System;
    using System.Threading.Tasks;

    public class SimpleAdapter: BotFrameworkFunctionsAdapter
    {
        public SimpleAdapter(ICredentialProvider credentialProvider)
            : base(credentialProvider)
        {
            OnTurnError = TurnErrorHandler;
        }

        private async Task TurnErrorHandler(ITurnContext turnContext, Exception exception)
        {
            await turnContext.SendActivityAsync(MessageFactory.Text($"Oh, snap! {exception.Message}"));
        }
    }
}
