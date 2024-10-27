using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Configuration;
using TemplateTelegramBot.Commands;

namespace TemplateTelegramBot.Bootstrap
{
    internal class Program
    {
        private static IConfiguration? configuration;

        private static HttpClient? _client;

        private static long adminID;

        private static string? botToken;

        private static string? discordWebhookURL;

        private static async Task Main(string[] args)
        {
            configuration = new ConfigurationBuilder()
             .AddJsonFile("Bootstrap/appsettings.json", optional: true)
             .Build();

            botToken = configuration.GetValue<string>("BotToken");
            adminID = configuration.GetValue<long>("AdminID");
            discordWebhookURL = configuration.GetValue<string>("discordWebhookURL");

            _client = new HttpClient
            {
                BaseAddress = new Uri($"https://api.telegram.org/bot{botToken}/")
            };
            HostTelegram Hosttelegram = new HostTelegram(botToken!, adminID);
            Hosttelegram.Start();
            Hosttelegram.OnMessage += OnMessage;
            await Host.CreateDefaultBuilder(args)
                     .ConfigureServices((hostContext, services) =>
                     {
                         services.AddHostedService<Worker>();
                     })
                     .RunConsoleAsync();


        }
        static async void OnMessage(ITelegramBotClient client, Update update)
        {

            switch (update.Type)
            {
                case UpdateType.Message:

                    var messageUpdateHandler = new UpdateTypeMessage(client, update, adminID, discordWebhookURL!);
                    await messageUpdateHandler.Execute();

                    break;

                case UpdateType.EditedMessage:

                    break;

                case UpdateType.ChannelPost:

                    var channelPostHandler = new UpdateTypeChannelPost(client, update, adminID, discordWebhookURL!);
                    await channelPostHandler.Execute();

                    break;

                case UpdateType.EditedChannelPost:

                    break;

                case UpdateType.CallbackQuery:

                    break;

                case UpdateType.InlineQuery:

                    break;

                case UpdateType.ChosenInlineResult:

                    break;

                case UpdateType.PreCheckoutQuery:

                    break;

                default:

                    break;
            }
        }
    }
}

