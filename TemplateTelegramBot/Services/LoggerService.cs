using System.Text;

class LoggerService
{
    private readonly Exception? _ex;

    private readonly string _messageLog;

    private readonly string _discordWebhookURL;

    public LoggerService(string messageLog, string discordWebhookURL, Exception? exception = null)
    {
        this._messageLog = messageLog;
        this._discordWebhookURL = discordWebhookURL;
        this._ex = exception;
    }

    public LoggerService(Exception exception, string discordWebhookURL)
        : this(string.Empty, discordWebhookURL, exception) { }

    public async Task Execute()
    {
        var contentMessage = _ex != null
            ? $"Exception: [{_ex.Message}] Stack Trace: [{_ex.StackTrace}]"
            : $"Message Log: [{_messageLog}]";

        var message = new { content = contentMessage };
        var jsonMessage = System.Text.Json.JsonSerializer.Serialize(message);

        using var client = new HttpClient();
        var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(_discordWebhookURL, content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to send log. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
            }
            else
            {
                Console.WriteLine($"Log sent: {contentMessage}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending log: {ex.Message}");
        }
    }
}
