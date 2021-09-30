using BotTelega.Models;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Linq;
using Telegram.Bot.Types;

namespace BotTelega.Commands
{
   public class SetAudioCommand
    {
        public static async Task AudioCommand(Message message, TelegramBotClient botClient)
        {
            Context _context = new Context();
            var id = message.Audio.FileId;
            var file = await botClient.GetFileAsync(id);
            string fileName = Path.GetFileName(file.FilePath);

            FileStream fs = new(fileName, FileMode.Create);
            await botClient.DownloadFileAsync(file.FilePath, fs);
            fs.Close();

            FileStream fS = new(fileName, FileMode.Open, FileAccess.Read);
            byte[] b = new byte[fS.Length];
            fS.Read(b, 0, (int)fS.Length);
            _context.audioModels.Add(new AudioModel
            {
                audioUser = b
                
            });
            await _context.SaveChangesAsync();

            fS.Close();
            _context.Dispose();
            System.IO.File.Delete(fileName);

            Logger.Information($"Time:{message.Date.ToLocalTime()};" +
                $" Action: Save Audio" +
                $" User:{message.Chat.Username};" +
                $" ChatId:{message.Chat.Id};"
                , $"{message.Chat.Username}");
        }
    }
}
