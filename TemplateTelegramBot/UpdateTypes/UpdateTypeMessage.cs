using Telegram.Bot;
using Telegram.Bot.Types;

namespace TemplateTelegramBot.Commands
{
    public class UpdateTypeMessage
    {
        private readonly ITelegramBotClient _client;

        private readonly Update _update;

        private readonly long _adminId;

        private readonly string? _discordWebhookURL;

        public UpdateTypeMessage(ITelegramBotClient client, Update update, long adminId, string discordWebhookURL)
        {
            this._client = client;
            this._adminId = adminId;
            this._update = update;
            this._discordWebhookURL = discordWebhookURL;
        }

        public async Task Execute()
        {
            if (_update.Message != null)
            {
                switch (_update.Message)
                {
                    case { Text: not null }:
                        await HandleTextMessage();
                        break;

                    case { Photo: { Length: > 0 } }:
                        await HandlePhotoMessage();
                        break;

                    case { Video: not null }:
                        await HandleVideoMessage();
                        break;

                    case { VideoNote: not null }:
                        await HandleVideoNoteMessage();
                        break;

                    case { Audio: not null }:
                        await HandleAudioMessage();
                        break;

                    case { Voice: not null }:
                        await HandleVoiceMessage();
                        break;

                    case { Document: not null }:
                        await HandleDocumentMessage();
                        break;

                    case { Sticker: not null }:
                        await HandleStickerMessage();
                        break;

                    case { Contact: not null }:
                        await HandleContactMessage();
                        break;

                    case { Location: not null }:
                        await HandleLocationMessage();
                        break;

                    case { Venue: not null }:
                        await HandleVenueMessage();
                        break;

                    case { Poll: not null }:
                        await HandlePollMessage();
                        break;

                    case { Dice: not null }:
                        await HandleDiceMessage();
                        break;

                    case { SuccessfulPayment: not null }:
                        await HandlePaymentMessage();
                        break;

                    default:
                        await HandleUnknownMessage();
                        break;
                }
            }
        }

        private async Task HandleTextMessage()
        {
            await ExecuteWithLogging(async () =>
            {
                var textMessageLogger = new LoggerService($"Text message: {_update.Message!.Text} - from bot messages", _discordWebhookURL!);
                await textMessageLogger.Execute();
            });
        }

        private async Task HandlePhotoMessage()
        {
            await ExecuteWithLogging(async () =>
            {
                var captionText = _update.Message!.Caption is not null ? $" with caption: [{_update.Message.Caption}]" : string.Empty;
                var photoMessageLogger = new LoggerService($"Photo received{captionText} - from bot messages", _discordWebhookURL!);
                await photoMessageLogger.Execute();
            });
        }

        private async Task HandleVideoMessage()
        {
            await ExecuteWithLogging(async () =>
            {
                var captionText = _update.Message!.Caption is not null ? $" with caption: [{_update.Message.Caption}]" : string.Empty;
                var videoMessageLogger = new LoggerService($"Video received{captionText} - from bot messages", _discordWebhookURL!);
                await videoMessageLogger.Execute();
            });
        }

        private async Task HandleVideoNoteMessage()
        {
            await ExecuteWithLogging(async () =>
            {
                var captionText = _update.Message!.Caption is not null ? $" with caption: [{_update.Message.Caption}]" : string.Empty;
                var videoNoteMessageLogger = new LoggerService($"Video Note received{captionText} - from bot messages", _discordWebhookURL!);
                await videoNoteMessageLogger.Execute();
            });
        }

        private async Task HandleAudioMessage()
        {
            await ExecuteWithLogging(async () =>
            {
                var captionText = _update.Message!.Caption is not null ? $" with caption: [{_update.Message.Caption}]" : string.Empty;
                var audioMessageLogger = new LoggerService($"Audio received{captionText} - from bot messages", _discordWebhookURL!);
                await audioMessageLogger.Execute();
            });
        }

        private async Task HandleVoiceMessage()
        {
            await ExecuteWithLogging(async () =>
            {
                var captionText = _update.Message!.Caption is not null ? $" with caption: [{_update.Message.Caption}]" : string.Empty;
                var voiceMessageLogger = new LoggerService($"Voice message received{captionText} - from bot messages", _discordWebhookURL!);
                await voiceMessageLogger.Execute();
            });
        }

        private async Task HandleDocumentMessage()
        {
            await ExecuteWithLogging(async () =>
            {
                var documentName = _update.Message!.Document?.FileName ?? "without name";
                var captionText = _update.Message.Caption is not null ? $" with caption: [{_update.Message.Caption}]" : string.Empty;
                var documentMessageLogger = new LoggerService($"Document received: {documentName}{captionText} - from bot messages", _discordWebhookURL!);
                await documentMessageLogger.Execute();
            });
        }

        private async Task HandleStickerMessage()
        {
            await ExecuteWithLogging(async () =>
            {
                var stickerMessageLogger = new LoggerService("Sticker received - from bot messages", _discordWebhookURL!);
                await stickerMessageLogger.Execute();
            });
        }

        private async Task HandleContactMessage()
        {
            await ExecuteWithLogging(async () =>
            {
                var contactMessageLogger = new LoggerService("Contact received - from bot messages", _discordWebhookURL!);
                await contactMessageLogger.Execute();
            });
        }

        private async Task HandleLocationMessage()
        {
            await ExecuteWithLogging(async () =>
            {
                var locationMessageLogger = new LoggerService("Location received - from bot messages", _discordWebhookURL!);
                await locationMessageLogger.Execute();
            });
        }

        private async Task HandleVenueMessage()
        {
            await ExecuteWithLogging(async () =>
            {
                var captionText = _update.Message!.Caption is not null ? $" with caption: [{_update.Message.Caption}]" : string.Empty;
                var venueMessageLogger = new LoggerService($"Venue received{captionText} - from bot messages", _discordWebhookURL!);
                await venueMessageLogger.Execute();
            });
        }

        private async Task HandlePollMessage()
        {
            await ExecuteWithLogging(async () =>
            {
                var pollMessageLogger = new LoggerService("Poll received - from bot messages", _discordWebhookURL!);
                await pollMessageLogger.Execute();
            });
        }

        private async Task HandleDiceMessage()
        {
            await ExecuteWithLogging(async () =>
            {
                var diceMessageLogger = new LoggerService("Dice roll received - from bot messages", _discordWebhookURL!);
                await diceMessageLogger.Execute();
            });
        }

        private async Task HandlePaymentMessage()
        {
            await ExecuteWithLogging(async () =>
            {
                var paymentMessageLogger = new LoggerService("Payment received successfully - from bot messages", _discordWebhookURL!);
                await paymentMessageLogger.Execute();
            });
        }

        private async Task HandleUnknownMessage()
        {
            await ExecuteWithLogging(async () =>
            {
                var captionText = _update.Message!.Caption is not null ? $" with caption: [{_update.Message.Caption}]" : string.Empty;
                var defaultMessageLogger = new LoggerService($"Unknown message type received{captionText} - from bot messages", _discordWebhookURL!);
                await defaultMessageLogger.Execute();
            });
        }

        private async Task ExecuteWithLogging(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                var loggerWithExceptionOnly = new LoggerService($"Error: {ex} - from bot messages", _discordWebhookURL!);
                await loggerWithExceptionOnly.Execute();
            }
        }
    }
}
