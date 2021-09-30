using BotTelega.Commands;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace BotTelega
{
    public class BotTelegram
    {
        private readonly string _token;
        private int _offset;
        public BotTelegram(string token, int offset)
        {
            _token = token;
            _offset = offset;
        }

        public async Task StartBot()
        {
            try
            {
                var botClient = new TelegramBotClient(_token);
                var me = await botClient.GetMeAsync();
                var admin = "Progerser";
                await botClient.SetWebhookAsync("");

                while (true)
                {
                    try
                    {
                        Context context = new();
                        var updates = await botClient.GetUpdatesAsync(_offset);

                        foreach (var update in updates)
                        {
                            var message = update.Message;
                            if (message == null)
                            {
                                _offset = update.Id + 1;
                                break;
                            }



                            switch (message.Type)
                            {
                                case MessageType.Text:
                                    {
                                        GetActionCommand getAction = new(message, botClient, context);
                                        await getAction.TextCommand();
                                        break;
                                    }
                                case MessageType.Audio:
                                    {
                                        if (admin.Equals(message.Chat.Username))
                                        {
                                            SetAudioCommand setAudio = new(message, botClient, context);
                                            await setAudio.AudioCommand();
                                        }
                                        else
                                            await botClient.SendTextMessageAsync(message.Chat.Id, "You haven't acsses for added audio");
                                        break;
                                    }
                                case MessageType.Photo:
                                    {
                                        if (admin.Equals(message.Chat.Username))
                                        {
                                            SetPhotoCommand setPhoto = new(message, botClient, context);
                                            await setPhoto.PhotoCommand();
                                        }

                                        else
                                            await botClient.SendTextMessageAsync(message.Chat.Id, "You haven't acsses for added photo");
                                        break;
                                    }
                                default:
                                    break;
                            }

                            _offset = update.Id + 1;
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Server Destroed Inside");
                        Console.WriteLine(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Server Destroed Outside");
                Console.WriteLine(ex);
            }
        }
    }
}
