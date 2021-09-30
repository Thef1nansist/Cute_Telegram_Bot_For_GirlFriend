using BotTelega.Interfaces;
using BotTelega.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotTelega.Commands
{
    public class GetActionCommand : IGetActionCommand
    {
        private Message _message;
        private TelegramBotClient _botClient;
        private Context _ctx;

        public GetActionCommand(Message message, TelegramBotClient botClient, Context ctx)
        {
            _message = message;
            _botClient = botClient;
            _ctx = ctx;
        }
        public async Task TextCommand()
        {
            Random rnd = new Random();
            switch (_message.Text)
            {
                case "/getcute":
                    {
                        var index = rnd.Next(0, _ctx.Cute.Count());
                        Model modelText = _ctx.Cute.ToArray()[index];
                        await _botClient.SendTextMessageAsync(_message.Chat.Id, $"{modelText.text}");

                        WriteLog("GetCute",
                            modelText.id,
                            _message.Chat.Username,
                            (int)_message.Chat.Id,
                            _message.Date.ToLocalTime());
                        break;
                    }
                case "/getimage":
                    {
                        var index = rnd.Next(0, _ctx.imageModels.Count());
                        ImageModel imageModel = _ctx.imageModels.ToArray()[index];

                        await _botClient.SendPhotoAsync(_message.Chat.Id, $"{imageModel.imageref}");
                        WriteLog("GetImage", imageModel.id,
                            _message.Chat.Username,
                            (int)_message.Chat.Id,
                            _message.Date.ToLocalTime());
                        break;
                    }
                case "/getnewimgcute":
                    {
                        var index = rnd.Next(0, _ctx.userPicture.Count());
                        UsersImageModel imageModel = _ctx.userPicture.ToArray()[index];

                        Stream stream = new MemoryStream(imageModel.userPicture);
                        await _botClient.SendPhotoAsync(_message.Chat.Id, stream);

                        stream.Dispose();
                        WriteLog("GetNewImgCute", imageModel.id,
                            _message.Chat.Username,
                            (int)_message.Chat.Id,
                            _message.Date.ToLocalTime());
                        break;
                    }
                case "/getaudio":
                    {
                        await _botClient.SendTextMessageAsync(_message.Chat.Id, "Идёт загрузка аудио");
                        var index = rnd.Next(0, _ctx.audioModels.Count());
                        AudioModel audio = _ctx.audioModels.ToArray()[index];

                        Stream stream = new MemoryStream(audio.audioUser);
                        await _botClient.SendAudioAsync(_message.Chat.Id, stream);

                        stream.Dispose();
                        WriteLog("GetAudio",
                            audio.id,
                            _message.Chat.Username,
                            (int)_message.Chat.Id,
                            _message.Date.ToLocalTime());
                        break;
                    }
                default:
                    break;
            }
        }
        private static void WriteLog(
            string action,
            int modelId,
            string userName,
            int chatId,
            DateTime date)
        {
            Logger.Information($"Time:{date};" +
                $" Action:{action}" +
                $" id-{modelId}" +
                $" User:{userName};" +
                $" ChatId:{chatId};"
                , $"{userName}");

        }
    }
}
