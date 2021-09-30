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

                        Logger.Information($"Time:{_message.Date.ToLocalTime()};" +
                                           $" Action: GetCute" +
                                           $" id-{modelText.id}" +
                                           $" User:{_message.Chat.Username};" +
                                           $" ChatId:{_message.Chat.Id};"
                                           , $"{_message.Chat.Username}");
                        break;
                    }
                case "/getimage":
                    {
                        var index = rnd.Next(0, _ctx.imageModels.Count());
                        ImageModel imageModel = _ctx.imageModels.ToArray()[index];

                        await _botClient.SendPhotoAsync(_message.Chat.Id, $"{imageModel.imageref}");
                        Logger.Information($"Time:{_message.Date.ToLocalTime()};" +
                                        $" Action: GetImage" +
                                        $" id-{imageModel.id}" +
                                        $" User:{_message.Chat.Username};" +
                                        $" ChatId:{_message.Chat.Id};"
                                        , $"{_message.Chat.Username}");
                        break;
                    }
                case "/getnewimgcute":
                    {
                        var index = rnd.Next(0, _ctx.userPicture.Count());
                        UsersImageModel imageModel = _ctx.userPicture.ToArray()[index];

                        Stream stream = new MemoryStream(imageModel.userPicture);
                        await _botClient.SendPhotoAsync(_message.Chat.Id, stream);

                        stream.Dispose();
                        Logger.Information($"Time:{_message.Date.ToLocalTime()};" +
                                                        $" Action: GetImage" +
                                                        $" id-{imageModel.id}" +
                                                        $" User:{_message.Chat.Username};" +
                                                        $" ChatId:{_message.Chat.Id};"
                                                        , $"{_message.Chat.Username}");
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
                        Logger.Information($"Time:{_message.Date.ToLocalTime()};" +
                                                                        $" Action: GetImage" +
                                                                        $" id-{audio.id}" +
                                                                        $" User:{_message.Chat.Username};" +
                                                                        $" ChatId:{_message.Chat.Id};"
                                                                        , $"{_message.Chat.Username}");
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
