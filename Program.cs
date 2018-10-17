using System;
using System.Threading;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Awesome
{
    class Program
    {
        static ITelegramBotClient botClient;

        static void Main()
        {
            botClient = new TelegramBotClient("763774086:AAFB4DTaKbU-l-txbwplQ1_SUFtd854ck6A");

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            string cooldown;
            string cd = "cd";
            string gm;
            cooldown = " \n 1km <1min \n 2km 1min \n 3km <2min \n 4.6km 2min \n 5km 2min \n 7km 5min \n 9km <7min \n 10km 7min \n 12km 8min \n 18km 10min \n 26km 15min \n 42km 19min \n 65km <22min \n 76km <25min \n 81km 25min \n 100km <35min \n 220km 40min \n 250km 45min \n 350km 51min \n 460km 58min \n 500km 60min \n 565km 67min \n 700km 75min \n 716km 78min \n 830km  <86min \n 1000km 90min \n 1500km+  2hr";

            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
                gm = e.Message.Text;
                if (gm == "cd")
                {
                  await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  
                  text: "As u wanted: The cooldown Chart:" + cooldown,
                    parseMode: ParseMode.Markdown
                  );
                }
                else if (e.Message.Text == "rb")
                    await botClient.SendPhotoAsync(
                    chatId: e.Message.Chat,
                    photo: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
                    caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
                    parseMode: ParseMode.Html
                );
            }
           

        }
    }
}