namespace JsonBot.Sample
{
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;
    using System.Threading;
    using System.Threading.Tasks;

    public class FuncBot : IBot
    {
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                await turnContext.SendActivityAsync($"echo: {turnContext.Activity.Text}");
            }
        }
    }
}
