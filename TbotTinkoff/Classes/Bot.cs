using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TbotTinkoff.Classes
{
    public class Bot
    {
        private TelegramBotClient _bot;
        private CancellationTokenSource _token;

        public Bot(string token)
        {
            _bot = new TelegramBotClient(token);
            _token = new CancellationTokenSource();
        }
        public void Start()
        {
            _bot.StartReceiving(HaldeUpdateAsync, HaldeErrorAsync, new Telegram.Bot.Polling.ReceiverOptions()
            {
                ThrowPendingUpdates=true
            },cancellationToken:_token.Token);
        }
        public void Stop() 
        {
            _token.Cancel();
        }
        public async Task GetInfo()
        {
            var botInfo= await _bot.GetMeAsync(cancellationToken: _token.Token);
            Console.WriteLine($"Бот {botInfo.Username} запущен");
        }
        private async Task HaldeUpdateAsync(ITelegramBotClient bot, Update update,CancellationToken cancellationToken)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                await _bot.SendTextMessageAsync(update.Message.Chat.Id,update.Message.Text,cancellationToken:cancellationToken
                    );
            }
        }
        private Task HaldeErrorAsync(ITelegramBotClient bot,Exception exception,  CancellationToken cancellationToken)
        {
            Console.WriteLine("Error: \n"+exception);
            Environment.Exit(1);
            return null;

        }
    }
}
