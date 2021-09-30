using BotTelega.Models;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Linq;
using Telegram.Bot.Types;
using BotTelega.Interfaces;

namespace BotTelega.Commands
{
    public class SetAudioCommand : ISetAudioCommand
    {
        private readonly Message _message;
        private readonly TelegramBotClient _botClient;
        private readonly Context _ctx;

        public SetAudioCommand(Message message, TelegramBotClient botClient, Context ctx)
        {
            _message = message;
            _botClient = botClient;
            _ctx = ctx;
        }

        public async Task AudioCommand()
        {
            var id = _message.Audio.FileId;
            var file = await _botClient.GetFileAsync(id);
            string fileName = Path.GetFileName(file.FilePath);

            FileStream fs = new(fileName, FileMode.Create);
            await _botClient.DownloadFileAsync(file.FilePath, fs);
            fs.Close();

            FileStream fS = new(fileName, FileMode.Open, FileAccess.Read);
            byte[] b = new byte[fS.Length];
            fS.Read(b, 0, (int)fS.Length);
            _ctx.audioModels.Add(new AudioModel
            {
                audioUser = b

            });
            await _ctx.SaveChangesAsync();

            fS.Close();
            _ctx.Dispose();
            System.IO.File.Delete(fileName);

            Logger.Information($"Time:{_message.Date.ToLocalTime()};" +
                $" Action: Save Audio" +
                $" User:{_message.Chat.Username};" +
                $" ChatId:{_message.Chat.Id};"
                , $"{_message.Chat.Username}");
        }
    }
}
