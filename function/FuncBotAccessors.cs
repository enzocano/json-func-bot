namespace JsonBot.Sample
{
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Dialogs;

    public class TestBotAccessors
    {
        public IStatePropertyAccessor<DialogState> ConversationDialogState { get; set; }

        public ConversationState ConversationState { get; set; }

        public UserState UserState { get; set; }
    }
}
