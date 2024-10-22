using Telegram.Bot;
using Telegram.Bot.Types;

namespace TemplateTelegramBot.Commands
{
    public class UpdateTypeMessageCommand
    {
        private ITelegramBotClient _client;

        private Update _update;

        public UpdateTypeMessageCommand(ITelegramBotClient client, Update update)
        {
            this._client = client;

            this._update = update;
        }

        public async Task Execute()
        {
            if (_update.Message != null)
            {
                switch (_update.Message)
                {
                    case { Text: not null }:

                        await _client.SendTextMessageAsync(_update.Message.Chat.Id, _update.Message.Text);

                        break;

                    case { Photo: { Length: > 0 } }:
                        
                        break;

                    case { Video: not null }:
                        
                        break;

                    case { Audio: not null }:
                        
                        break;

                    case { Document: not null }:

                        break;

                    case { SuccessfulPayment: not null }:

                        break;

                    default:
                        
                        break;
                }
            }

        }
    }
}
