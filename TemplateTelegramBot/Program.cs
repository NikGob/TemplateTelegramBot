using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Configuration;
using TemplateTelegramBot.Commands;

namespace TemplateTelegramBot
{
    internal class Program
    {
        private static IConfiguration? configuration;

        private static HttpClient? _client;

        private static long adminID;

        static string? botToken;

        

        private static async Task Main(string[] args)
        {
            configuration = new ConfigurationBuilder()

                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            botToken = configuration.GetValue<string>("BotToken");
            adminID = configuration.GetValue<long>("AdminID");

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

                    UpdateTypeMessageCommand updateTypeMessage = new UpdateTypeMessageCommand(client, update);
                    await updateTypeMessage.Execute();

                    break;

                case UpdateType.EditedMessage:
                 
                    break;

                case UpdateType.ChannelPost:
                    
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

