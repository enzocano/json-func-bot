using JsonBot.Sample.Dialogs;
using JsonBot.Sample.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace JsonBot.Sample.Bots
{
    public class OnboardingBot : ActivityHandler
    {
        private readonly DialogSet dialogSet;
        private readonly IStatePropertyAccessor<UserProfile> userProfileAccessor;

        public OnboardingBot(
            UserState userState,
            ConversationState conversationState,
            UserProfileDialog rootDialog)
        {
            userProfileAccessor = userState.CreateProperty<UserProfile>(nameof(UserProfile));
            var dialogState = conversationState.CreateProperty<DialogState>(nameof(OnboardingBot));
            dialogSet = new DialogSet(dialogState);
            dialogSet.Add(rootDialog);
        }

        protected override async Task OnConversationUpdateActivityAsync(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var userProfile = await userProfileAccessor.GetAsync(turnContext, () => new UserProfile(), cancellationToken);
            if (!string.IsNullOrEmpty(userProfile.Name))
            {
                await turnContext.SendActivityAsync(MessageFactory.Text($"Welcome back {userProfile.Name}!"), cancellationToken);
            }
            else
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Hello stranger!"), cancellationToken);
            }

            await base.OnConversationUpdateActivityAsync(turnContext, cancellationToken);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var dialogContext = await dialogSet.CreateContextAsync(turnContext, cancellationToken);

            var results = await dialogContext.ContinueDialogAsync(cancellationToken);

            if (results.Status == DialogTurnStatus.Empty)
            {
                await dialogContext.BeginDialogAsync(nameof(UserProfileDialog));
            }
        }
    }
}
