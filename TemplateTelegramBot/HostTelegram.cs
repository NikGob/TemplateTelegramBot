using Telegram.Bot;
using Telegram.Bot.Types;

public class HostTelegram
{
    public Action<ITelegramBotClient, Update>? OnMessage;
    private TelegramBotClient _bot;
    private long _adminID;
    public HostTelegram(string token, long adminID)
    {
        _bot = new TelegramBotClient(token);
        _adminID = adminID;
    }

    public async void Start()
    {
        _bot.StartReceiving(UpdateHandler, ErrorHandler);
        Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}] @{_bot.GetMeAsync().Result.Username} started.");
        await _bot.SendTextMessageAsync(_adminID, "Bot started.");
    }

    private async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Console.WriteLine($"Error : + {exception.Message}");
        await Task.CompletedTask;
    }

    private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
    {
        OnMessage?.Invoke(client, update);
        await Task.CompletedTask;

        var fromUser = update.Message?.From ?? update.CallbackQuery?.From;
        var userName = fromUser?.Username ?? fromUser?.FirstName;

        string messageContent;

        if (update.Message != null)
        {
            if (update.Message.Text != null)
                messageContent = update.Message.Text;
            else if (update.Message.Photo != null)
                messageContent = update.Message.Caption != null ? $"{update.Message.Caption} | [Photo]" : "[Photo]";
            else if (update.Message.Document != null)
                messageContent = update.Message.Caption != null ? $"{update.Message.Caption} | [Document]" : "[Document]";
            else if (update.Message.Audio != null)
                messageContent = update.Message.Caption != null ? $"{update.Message.Caption} | [Audio]" : "[Audio]";
            else if (update.Message.Video != null)
                messageContent = update.Message.Caption != null ? $"{update.Message.Caption} | [Video]" : "[Video]";
            else if (update.Message.Voice != null)
                messageContent = "[Voice Message]";
            else if (update.Message.Sticker != null)
                messageContent = "[Sticker]";
            else if (update.Message.Location != null)
                messageContent = "[Location]";
            else if (update.Message.Contact != null)
                messageContent = "[Contact]";
            else
                messageContent = "[other]";
        }
        else
        {
            messageContent = "[CallbackQuery]";
        }

        Console.WriteLine($"[{DateTime.Now.ToShortTimeString()} | {update.Type}] {userName}: {messageContent}");

    }
}
