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
        Console.WriteLine("Bot started.");
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

        var username = update.Message?.From?.Username != null ? $"@{update.Message.From.Username}" : update.Message?.From?.FirstName;
        var userId = update.Message?.From?.Id;
        var messageText = update.Message?.Text ?? "[no text]";

        Console.WriteLine($"{username} ({userId}) sent message: {messageText}");
    }
}
