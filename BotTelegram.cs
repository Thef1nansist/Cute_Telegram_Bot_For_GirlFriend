using BotTelega.Commands;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace BotTelega
{
    public class BotTelegram
    {
        private string _token { get; set; }
        private int _offset { get; set; }

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
            await botClient.SetWebhookAsync("");

            while (true)
            {
                try
                {
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
                                    await GetActionCommand.TextCommand(message, botClient);
                                    break;
                                }
                            case MessageType.Audio:
                                {
                                    if (message.Chat.Username == "Progerser")
                                        await SetAudioCommand.AudioCommand(message, botClient);
                                    else
                                        await botClient.SendTextMessageAsync(message.Chat.Id, "You haven't acsses for added audio");
                                    break;
                                }
                            case MessageType.Photo:
                                {
                                    if (message.Chat.Username == "Progerser")
                                        await SetPhotoCommand.PhotoCommand(message, botClient);
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
                    Console.WriteLine("Server Destroed");
                    Console.WriteLine(ex);
                }

            }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Server Destroed");
                Console.WriteLine(ex);
            }


        }
    }
}
