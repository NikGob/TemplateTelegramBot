using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

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

        public async void Execute()
        {
            if (_update.Message?.Text != null)
            {
                switch (_update.Message.Text)
                {
                    case var s when string.Equals(s, "", StringComparison.OrdinalIgnoreCase):

                        break;

                    default:
                       await _client.SendTextMessageAsync(_update.Message.Chat.Id, _update.Message.Text);
                        break;
                }
            }
        }
    }
}