namespace Microsoft.Bot.Builder.Integration.AspNet.Functions
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class BotFrameworkFunctionsAdapter : BotFrameworkAdapter, IBotFrameworkFunctionsAdapter
    {
        public BotFrameworkFunctionsAdapter(ICredentialProvider credentialProvider = null, IChannelProvider channelProvider = null, ILogger logger = null)
            : base(credentialProvider ?? new SimpleCredentialProvider(), channelProvider, null, null, null, logger)
        {
        }

        public async Task<IActionResult> ProcessAsync(HttpRequest httpRequest, IBot bot, CancellationToken cancellationToken = default)
        {
            if (httpRequest == null)
            {
                throw new ArgumentNullException(nameof(httpRequest));
            }

            if (bot == null)
            {
                throw new ArgumentNullException(nameof(bot));
            }

            // deserialize the incoming Activity
            var activity = HttpHelper.ReadRequest(httpRequest);

            // grab the auth header from the inbound http request
            var authHeader = httpRequest.Headers["Authorization"];

            try
            {
                // process the inbound activity with the bot
                var invokeResponse = await ProcessActivityAsync(authHeader, activity, bot.OnTurnAsync, cancellationToken).ConfigureAwait(false);

                // write the response, potentially serializing the InvokeResponse
                return HttpHelper.GenerateResponse(invokeResponse);
            }
            catch (UnauthorizedAccessException)
            {
                return new UnauthorizedResult();
            }
        }
    }
}
