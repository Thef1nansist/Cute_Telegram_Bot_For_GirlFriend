using BotTelega.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotTelega.Commands
{
    public static class GetActionCommand
    {
        static Random rnd = new Random();
        public static async Task TextCommand(Message message, TelegramBotClient botClient)
        {
            Context context = new Context();

            switch (message.Text)
            {
                case "/getcute":
                    {
                        var index = rnd.Next(0, context.Cute.Count());
                        Model modelText = context.Cute.ToArray()[index];
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"{modelText.text}");

                        WriteLog("GetCute",
                            modelText.id,
                            message.Chat.Username,
                            (int)message.Chat.Id,
                            message.Date.ToLocalTime());
                        break;
                    }
                case "/getimage":
                    {
                        var index = rnd.Next(0, context.imageModels.Count());
                        ImageModel imageModel = context.imageModels.ToArray()[index];

                        await botClient.SendPhotoAsync(message.Chat.Id, $"{imageModel.imageref}");
                        WriteLog("GetImage", imageModel.id,
                            message.Chat.Username,
                            (int)message.Chat.Id,
                            message.Date.ToLocalTime());
                        break;
                    }
                case "/getnewimgcute":
                    {
                        var index = rnd.Next(0, context.userPicture.Count());
                        UsersImageModel imageModel = context.userPicture.ToArray()[index];

                        Stream stream = new MemoryStream(imageModel.userPicture);
                        await botClient.SendPhotoAsync(message.Chat.Id, stream);

                        stream.Dispose();
                        WriteLog("GetNewImgCute", imageModel.id,
                            message.Chat.Username,
                            (int)message.Chat.Id,
                            message.Date.ToLocalTime());
                        break;
                    }
                case "/getaudio":
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Идёт загрузка аудио");
                        var index = rnd.Next(0, context.audioModels.Count());
                        AudioModel audio = context.audioModels.ToArray()[index];

                        Stream stream = new MemoryStream(audio.audioUser);
                            await botClient.SendAudioAsync(message.Chat.Id, stream);

                        stream.Dispose();
                        WriteLog("GetAudio",
                            audio.id,
                            message.Chat.Username,
                            (int)message.Chat.Id,
                            message.Date.ToLocalTime());
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
