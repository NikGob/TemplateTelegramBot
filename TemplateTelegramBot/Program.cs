using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Requests;
using System.Diagnostics;
using Telegram.Bot.Types.Payments;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using TemplateTelegramBot;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

namespace TemplateTelegramBot
{
    internal class Program
    {
        private static IConfiguration configuration;

        private static HttpClient? _client;

        private static long adminID;

        static string botToken;






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
            HostTelegram Hosttelegram = new HostTelegram(botToken, adminID);
            Hosttelegram.Start();
            Hosttelegram.OnMessage += OnMessage;
            await Host.CreateDefaultBuilder(args)
                     .ConfigureServices((hostContext, services) =>
                     {
                         // Конфигурация зависимостей, если нужно
                         services.AddHostedService<Worker>();
                     })
                     .RunConsoleAsync();


        }
        static async void OnMessage(ITelegramBotClient client, Update update)
        {
            var username = update.Message?.From?.Username != null ? $"@{update.Message.From.Username}" : update.Message?.From?.FirstName;
            var userId = update.Message?.From?.Id;
            var messageText = update.Message?.Text ?? "[no text]";

            Console.WriteLine($"{username} ({userId}) sent message: {messageText}");


            switch (update.Type)
            {
                case UpdateType.Message:
                    switch (update.Message?.Text)
                    {
                        case var s when string.Equals(s, "", StringComparison.OrdinalIgnoreCase):

                            break;

                        default:
                            
                            break;

                    }


                    break;
            }






        }


    }

}

