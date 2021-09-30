using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using BotTelega.Models;

namespace BotTelega
{
    class Program
    {
        static async Task Main(string[] args)
        {
            BotTelegram bot = new BotTelegram("2012624409:AAG0IDvme3uIDEY6ebxvViLaK-U1E2aep8U", 0);
            Console.WriteLine("Bot listening");
            await bot.StartBot();
        }
    }
}
