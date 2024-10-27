using Telegram.Bot;
using Telegram.Bot.Types;

namespace TemplateTelegramBot.Commands
{
    public class UpdateTypeChannelPost
    {
        private readonly ITelegramBotClient _client;

        private readonly Update _update;

        private readonly long _adminId;

        private readonly string? _discordWebhookURL;

        public UpdateTypeChannelPost(ITelegramBotClient client, Update update, long adminId, string discordWebhookURL)
        {
            _client = client;
            _adminId = adminId;
            _update = update;
            _discordWebhookURL = discordWebhookURL;
        }

        public async Task Execute()
        {
            if (_update.ChannelPost != null)
            {
                switch (_update.ChannelPost)
                {
                    case { Text: not null }:
                        await HandleTextChannelPost();
                        break;

                    case { Photo: { Length: > 0 } }:
                        await HandlePhotoChannelPost();
                        break;

                    case { Video: not null }:
                        await HandleVideoChannelPost();
                        break;

                    case { VideoNote: not null }:
                        await HandleVideoNoteMessage();
                        break;

                    case { Audio: not null }:
                        await HandleAudioChannelPost();
                        break;

                    case { Document: not null }:
                        await HandleDocumentChannelPost();
                        break;

                    case { Sticker: not null }:
                        await HandleStickerChannelPost();
                        break;

                    case { Location: not null }:
                        await HandleLocationChannelPost();
                        break;

                    case { Poll: not null }:
                        await HandlePollChannelPost();
                        break;

                    case { Dice: not null }:
                        await HandleDiceChannelPost();
                        break;

                    case { SuccessfulPayment: not null }:
                        await HandlePaymentChannelPost();
                        break;

                    default:
                        await HandleUnknownChannelPost();
                        break;
                }
            }
        }

        private async Task HandleTextChannelPost()
        {
            await ExecuteWithLogging(async () =>
            {
                var loggerText = new LoggerService($"Text channel post: {_update.ChannelPost!.Text} - from ChannelPost", _discordWebhookURL!);
                await loggerText.Execute();
            });
        }

        private async Task HandlePhotoChannelPost()
        {
            await ExecuteWithLogging(async () =>
            {
                var captionText = _update.ChannelPost!.Caption is not null ? $" with caption: [{_update.ChannelPost.Caption}]" : string.Empty;
                var loggerPhoto = new LoggerService($"Photo channel post received{captionText} - from ChannelPost", _discordWebhookURL!);
                await loggerPhoto.Execute();
            });
        }

        private async Task HandleVideoChannelPost()
        {
            await ExecuteWithLogging(async () =>
            {
                var captionText = _update.ChannelPost!.Caption is not null ? $" with caption: [{_update.ChannelPost.Caption}]" : string.Empty;
                var loggerVideo = new LoggerService($"Video channel post received{captionText} - from ChannelPost", _discordWebhookURL!);
                await loggerVideo.Execute();
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

        private async Task HandleAudioChannelPost()
        {
            await ExecuteWithLogging(async () =>
            {
                var captionText = _update.ChannelPost!.Caption is not null ? $" with caption: [{_update.ChannelPost.Caption}]" : string.Empty;
                var loggerAudio = new LoggerService($"Audio channel post received{captionText} - from ChannelPost", _discordWebhookURL!);
                await loggerAudio.Execute();
            });
        }

        private async Task HandleDocumentChannelPost()
        {
            await ExecuteWithLogging(async () =>
            {
                var documentName = _update.ChannelPost!.Document?.FileName ?? "without name";
                var captionText = _update.ChannelPost.Caption is not null ? $" with caption: [{_update.ChannelPost.Caption}]" : string.Empty;
                var loggerDocument = new LoggerService($"Document channel post received: {documentName}{captionText} - from ChannelPost", _discordWebhookURL!);
                await loggerDocument.Execute();
            });
        }

        private async Task HandleStickerChannelPost()
        {
            await ExecuteWithLogging(async () =>
            {
                var loggerSticker = new LoggerService("Sticker channel post received - from ChannelPost", _discordWebhookURL!);
                await loggerSticker.Execute();
            });
        }

        private async Task HandleLocationChannelPost()
        {
            await ExecuteWithLogging(async () =>
            {
                var loggerLocation = new LoggerService("Location channel post received - from ChannelPost", _discordWebhookURL!);
                await loggerLocation.Execute();
            });
        }

        private async Task HandlePollChannelPost()
        {
            await ExecuteWithLogging(async () =>
            {
                var loggerPoll = new LoggerService("Poll channel post received - from ChannelPost", _discordWebhookURL!);
                await loggerPoll.Execute();
            });
        }

        private async Task HandleDiceChannelPost()
        {
            await ExecuteWithLogging(async () =>
            {
                var loggerDice = new LoggerService("Dice roll channel post received - from ChannelPost", _discordWebhookURL!);
                await loggerDice.Execute();
            });
        }

        private async Task HandlePaymentChannelPost()
        {
            await ExecuteWithLogging(async () =>
            {
                var loggerPayment = new LoggerService("Payment channel post received successfully - from ChannelPost", _discordWebhookURL!);
                await loggerPayment.Execute();
            });
        }

        private async Task HandleUnknownChannelPost()
        {
            await ExecuteWithLogging(async () =>
            {
                var captionText = _update.ChannelPost!.Caption is not null ? $" with caption: [{_update.ChannelPost.Caption}]" : string.Empty;
                var defaultMessageLogger = new LoggerService($"Unknown channel post type received{captionText} - from ChannelPost", _discordWebhookURL!);
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
                var loggerWithExceptionOnly = new LoggerService($"Error: {ex} - from ChannelPost", _discordWebhookURL!);
                await loggerWithExceptionOnly.Execute();
            }
        }
    }
}
