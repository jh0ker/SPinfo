using System;
using System.Threading;
using System.Text.RegularExpressions;

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
            botClient = new TelegramBotClient("XXX");

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }
        private const string FirstOptionText = "First option";
        private const string SecondOptionText = "Second option";

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            bool wb;
            int cp = 0;
            int pokedex = 0;
            int baseAttack = 0;
            int baseDefense = 0;
            int baseStamina = 0;
            //[2,x] = BaseAttack
            //[3,x] = BaseDefense
            //[4,x] = BaseStamina
            var regex = new Regex(@"[0-3]+[0-9]+[0-9]");
            double[] cpm = new double[80] { 0, 0.094, 0.135137432, 0.16639787, 0.192650919, 0.21573247, 0.236572661, 0.25572005, 0.273530381, 0.29024988, 0.306057377, 0.3210876, 0.335445036, 0.34921268, 0.362457751, 0.37523559, 0.387592406, 0.39956728, 0.411193551, 0.42250001, 0.432926419, 0.44310755, 0.4530599578, 0.46279839, 0.472336083, 0.48168495, 0.4908558, 0.49985844, 0.508701765, 0.51739395, 0.525942511, 0.53435433, 0.542635767, 0.55079269, 0.558830576, 0.56675452, 0.574569153, 0.58227891, 0.589887917, 0.59740001, 0.604818814, 0.61215729, 0.619399365, 0.62656713, 0.633644533, 0.64065295, 0.647576426, 0.65443563, 0.661214806, 0.667934, 0.674577537, 0.68116492, 0.687680648, 0.69414365, 0.700538673, 0.70688421, 0.713164996, 0.71939909, 0.725571552, 0.7317, 0.734741009, 0.73776948, 0.740785574, 0.74378943, 0.746781211, 0.74976104, 0.752729087, 0.75568551, 0.758630378, 0.76156384, 0.764486065, 0.76739717, 0.770297266, 0.7731865, 0.776064962, 0.77893275, 0.781790055, 0.78463697, 0.787473578, 0.79030001 };
            int[,] baseStats = new int[3, 387] { { 0, 118, 151, 198, 116, 158, 223, 94, 126, 171, 55, 45, 167, 63, 46, 169, 85, 117, 166, 103, 161, 112, 182, 110, 167, 112, 193, 126, 182, 86, 117, 180, 105, 137, 204, 107, 178, 96, 169, 80, 156, 83, 161, 131, 153, 202, 121, 165, 100, 179, 109, 167, 92, 150, 122, 191, 148, 207, 136, 227, 101, 130, 182, 195, 232, 271, 137, 177, 234, 139, 172, 207, 97, 166, 132, 164, 211, 170, 207, 109, 177, 165, 223, 124, 158, 218, 85, 139, 135, 190, 116, 186, 186, 223, 261, 85, 89, 144, 181, 240, 109, 173, 107, 233, 90, 144, 224, 193, 108, 119, 174, 140, 222, 60, 183, 181, 129, 187, 123, 175, 137, 210, 192, 218, 223, 198, 206, 238, 198, 29, 237, 165, 91, 104, 205, 232, 246, 153, 155, 207, 148, 220, 221, 190, 192, 253, 251, 119, 163, 263, 300, 210, 92, 122, 168, 116, 158, 223, 117, 150, 205, 79, 148, 67, 145, 72, 107, 105, 161, 194, 106, 146, 77, 75, 69, 67, 139, 134, 192, 114, 145, 211, 169, 37, 112, 167, 174, 67, 91, 118, 136, 55, 185, 154, 75, 152, 261, 126, 175, 177, 167, 136, 60, 182, 108, 161, 131, 143, 148, 137, 212, 184, 236, 17, 234, 189, 142, 236, 118, 139, 90, 181, 118, 127, 197, 128, 148, 148, 152, 224, 194, 107, 214, 198, 192, 40, 64, 173, 153, 135, 151, 157, 129, 241, 235, 180, 115, 155, 251, 193, 263, 210, 124, 172, 223, 130, 163, 240, 126, 156, 208, 96, 171, 58, 142, 75, 60, 189, 60, 98, 71, 112, 173, 71, 134, 200, 106, 185, 106, 175, 79, 117, 237, 93, 192, 74, 241, 104, 159, 290, 80, 199, 153, 92, 134, 179, 99, 209, 36, 82, 84, 132, 141, 155, 121, 158, 198, 78, 121, 123, 215, 167, 147, 143, 143, 186, 80, 140, 171, 243, 136, 175, 119, 194, 151, 125, 171, 116, 162, 134, 205, 156, 221, 76, 141, 222, 196, 178, 178, 93, 151, 141, 224, 77, 140, 105, 152, 176, 222, 29, 192, 139, 161, 138, 218, 70, 124, 136, 175, 246, 41, 95, 162, 95, 137, 182, 133, 197, 211, 162, 81, 134, 172, 277, 96, 138, 257, 179, 179, 143, 228, 268, 297, 297, 312, 210, 345 }, { 0, 118, 151, 198, 96, 129, 176, 122, 155, 210, 62, 94, 151, 55, 86, 150, 76, 108, 157, 70, 144, 61, 135, 102, 158, 101, 165, 145, 202, 94, 126, 174, 76, 112, 157, 116, 171, 122, 204, 44, 93, 76, 153, 116, 139, 170, 99, 146, 102, 150, 88, 147, 81, 139, 96, 163, 87, 144, 96, 166, 82, 130, 187, 103, 138, 194, 88, 130, 162, 64, 95, 138, 182, 237, 163, 196, 229, 132, 167, 109, 194, 128, 182, 118, 88, 145, 128, 184, 90, 184, 168, 323, 70, 112, 156, 288, 158, 215, 156, 214, 114, 179, 140, 158, 165, 200, 211, 212, 137, 164, 221, 157, 206, 176, 205, 165, 125, 182, 115, 154, 112, 184, 233, 170, 182, 173, 169, 197, 197, 102, 197, 180, 91, 121, 177, 201, 204, 139, 174, 227, 162, 203, 164, 190, 249, 188, 184, 94, 138, 201, 182, 210, 122, 155, 202, 96, 129, 176, 116, 151, 197, 77, 130, 101, 179, 142, 209, 73, 128, 178, 106, 146, 63, 91, 34, 116, 191, 89, 146, 82, 112, 172, 189, 93, 152, 198, 192, 101, 127, 197, 112, 55, 148, 94, 75, 152, 194, 250, 87, 194, 167, 91, 106, 133, 146, 242, 131, 204, 333, 89, 137, 148, 191, 396, 189, 157, 93, 144, 71, 209, 74, 147, 156, 69, 141, 90, 260, 260, 93, 159, 194, 107, 214, 183, 132, 88, 64, 214, 116, 110, 108, 211, 229, 210, 176, 235, 93, 133, 212, 323, 301, 210, 104, 130, 180, 92, 115, 141, 93, 133, 175, 63, 137, 80, 128, 61, 91, 98, 91, 172, 86, 128, 191, 86, 78, 121, 61, 130, 61, 189, 63, 100, 220, 97, 161, 110, 153, 104, 159, 183, 153, 116, 80, 42, 81, 142, 54, 114, 71, 236, 84, 132, 141, 155, 168, 240, 314, 107, 152, 78, 127, 147, 167, 171, 171, 148, 99, 159, 39, 83, 68, 87, 82, 139, 234, 145, 211, 116, 78, 99, 168, 74, 115, 139, 208, 124, 118, 163, 163, 83, 142, 113, 156, 131, 236, 154, 198, 100, 183, 102, 242, 139, 212, 66, 127, 162, 234, 165, 174, 120, 86, 95, 162, 90, 132, 176, 149, 194, 194, 234, 134, 107, 179, 168, 141, 185, 248, 356, 356, 285, 268, 228, 276, 276, 187, 210, 115 }, { 0, 90, 120, 160, 78, 116, 156, 88, 118, 158, 90, 100, 120, 80, 90, 130, 80, 126, 166, 60, 110, 80, 130, 70, 120, 70, 120, 100, 150, 110, 140, 180, 92, 122, 162, 140, 190, 76, 146, 230, 280, 80, 150, 90, 120, 150, 70, 120, 120, 140, 20, 70, 80, 130, 100, 160, 80, 130, 110, 180, 80, 130, 180, 50, 80, 110, 140, 160, 180, 100, 130, 160, 80, 160, 80, 110, 160, 100, 130, 180, 190, 50, 100, 104, 70, 120, 130, 180, 160, 210, 60, 100, 60, 90, 120, 70, 120, 170, 60, 110, 80, 120, 120, 190, 100, 120, 100, 100, 180, 80, 130, 160, 210, 500, 130, 210, 60, 110, 90, 160, 60, 120, 80, 140, 130, 130, 130, 130, 150, 40, 190, 260, 96, 110, 260, 130, 130, 130, 70, 140, 60, 120, 160, 320, 180, 180, 180, 82, 122, 182, 193, 200, 90, 120, 160, 78, 116, 156, 100, 130, 170, 70, 170, 120, 200, 80, 110, 80, 140, 170, 150, 250, 40, 100, 180, 70, 110, 80, 130, 110, 140, 180, 150, 140, 200, 140, 180, 70, 110, 150, 110, 60, 150, 130, 110, 190, 130, 190, 120, 190, 120, 96, 380, 140, 100, 150, 200, 130, 150, 120, 180, 130, 140, 40, 160, 110, 120, 180, 80, 100, 100, 200, 110, 70, 150, 90, 130, 130, 90, 150, 150, 180, 180, 170, 146, 110, 70, 100, 90, 90, 90, 190, 510, 180, 230, 200, 100, 140, 200, 212, 212, 200, 80, 100, 140, 90, 120, 160, 100, 140, 200, 70, 140, 76, 156, 90, 100, 120, 100, 120, 80, 120, 160, 80, 140, 180, 80, 120, 80, 120, 56, 76, 136, 80, 140, 120, 120, 120, 160, 273, 62, 122, 2, 128, 168, 208, 144, 288, 100, 60, 100, 140, 100, 100, 100, 120, 140, 60, 120, 80, 140, 120, 120, 130, 130, 100, 140, 200, 90, 140, 260, 340, 120, 140, 140, 120, 160, 120, 90, 100, 160, 100, 140, 90, 150, 146, 146, 180, 180, 100, 220, 86, 126, 80, 120, 132, 172, 90, 150, 40, 190, 140, 120, 88, 128, 40, 80, 198, 150, 130, 190, 100, 160, 140, 180, 220, 70, 110, 110, 200, 86, 90, 130, 190, 80, 120, 160, 160, 160, 160, 160, 160, 200, 200, 210, 200, 100 } };
            string output;
            long willson;
            string msg;
            string hotspots = "\n *Top Notch XP Grinding Spots* \n \n 1: Copenhagen, Denmark / 55.662278, 12.56246 \n 2: German Mall, Germany / 51.48986, 6.87533 \n 3: Paris Mall, Paris, France / 48.890717, 2.23783 \n \n *Coordinates List* \n \n 1: Chicago Lake Front / 42.0411,-87.7102 ( 60 Pokestops  \n 2: New York / 40.75512,-73.9836 ( 7 Pokestops  \n 3: Japan / 35.6961,139.8144 ( 9 Pokestops  \n 4: Mall of Scandanavia, Sweden / 59.370392, 18.002844 ( 6 Pokestops  \n 5: Disneyland, Florida / 33.8119,-117.919 \n 6: Victory Memorial Park, Minneapolis / 45.0301,-93.3195 \n 7: Central Park, New York / 40.7803,-73.963 \n 8: Santa Monica, California / 34.0076,-118.499 \n 9: Liberty Park / 40.7442,-111.874 \n 10: Paris / 48.8615, 2.289 \n 11: Union Square / 37.7879,-122.407 \n 12: Sidney / -33.8610,151.212 \n 13: Austin Texas / 30.2742,-97.740 \n 14: Olympia / 47.0477,-122.9041 ( 4 Pokestops  \n 16: NgurahRai Airport / -8.7467,115.166 \n 17: Times square, New York / 40.7589,-73.985 \n 18: Big Ben, London / 51.5010,-0.124 \n 19: Tokyo Disneyland / 35.6312,139.880 \n 20: Waikiki Aquarium / 21.2658,-157.821 \n 21: Rigos Loop / 21.2983,-157.860 \n 22: Jimmys Loop, USA 32.7758,-96.796 \n 23: Toms Loop, Ann Arbor / 42.2808,-83.743 \n 24: San Francisco Pier, California / 39 37.8095,-122.410 \n 25: Alameda, Mexico / 19.4362,-99.144 \n 26: Sydney Botanical / -33.8643,151.215 \n 27: Bidadari, Indonesia / -6.0356,106.746 \n 28: Long Beach, California / 33.7700,-118.193 \n 29: Den Haag, Netherland / 52.0689,4.220 \n 30: Korea / 37.5113,127.0980 ( 11 Pokestops  \n 31: Germany Mall / 51.491104,6.88055 \n 32: Copenhagen, Denmark Mall / 55.662518, 12.56217 \n 33: (Park) National Mall, Washington DC / 38.888515, -77.024124";;
            string cooldown = cooldown = " \n 1km <1min \n 2km 1min \n 3km <2min \n 4.6km 2min \n 5km 2min \n 7km 5min \n 9km <7min \n 10km 7min \n 12km 8min \n 18km 10min \n 26km 15min \n 42km 19min \n 65km <22min \n 76km <25min \n 81km 25min \n 100km <35min \n 220km 40min \n 250km 45min \n 350km 51min \n 460km 58min \n 500km 60min \n 565km 67min \n 700km 75min \n 716km 78min \n 830km  <86min \n 1000km 90min \n 1500km+  2hr";

            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
                    if (e.Message.Text == "/cd")
                    {
                      await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "Cooldown Tabelle: \n" + cooldown,
                        parseMode: ParseMode.Markdown
                      );
                    }

                    else if (e.Message.Text == "/hs")
                    {
                      await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "Spoof Hotspots: \n" + hotspots,
                        parseMode: ParseMode.Markdown
                      );
                    }
                    else if (e.Message.Text == "/rb")
                    {
                      await botClient.SendPhotoAsync(
                        chatId: e.Message.Chat,
                        photo: "https://github.com/r4nd0wn/SPinfo/blob/master/misc/raid/Pokemon-GO-Raid-Bosses.jpg?raw=true",
                        caption: "<b>Raid Gegner und 100% WP</b>.",
                        parseMode: ParseMode.Html
                      );
                    }
                    else if (e.Message.Text == "/rwd")
                    {
                      await botClient.SendPhotoAsync(
                        chatId: e.Message.Chat,
                        photo: "https://github.com/r4nd0wn/SPinfo/blob/master/misc/raid/Pokemon-GO-Raid-Battle-Item-Rewards.png?raw=true",
                        caption: "<b>Raid Bundle Menge</b>.",
                        parseMode: ParseMode.Html
                      );
                      await botClient.SendPhotoAsync(
                        chatId: e.Message.Chat,
                        photo: "https://github.com/r4nd0wn/SPinfo/blob/master/misc/raid/Pokemon-GO-Raid-Boss-Item-Rewards.jpg?raw=true",
                        caption: "<b>Raid Bundle Prozent</b>.",
                        parseMode: ParseMode.Html
                      );
                    }
                else
                    {
                    msg = e.Message.Text;
                    if (regex.IsMatch(e.Message.Text))
                    {
                        string searchstring;
                        pokedex = Convert.ToInt32(msg);
                        baseAttack = baseStats[0, pokedex];
                        baseDefense = baseStats[1, pokedex];
                        baseStamina = baseStats[2, pokedex];
                        cp = Convert.ToInt16(Math.Floor(((((baseStats[0, pokedex] + 15) * cpm[40]) * Math.Sqrt((baseStats[1, pokedex] + 15) * cpm[40])) * Math.Sqrt((baseStats[2, pokedex] + 15) * cpm[40])) / 10));
                        output = Convert.ToString(cp);
                        await botClient.SendTextMessageAsync(
                          chatId: e.Message.Chat,
                          text: "Die 100IV WP in einem Raid ist ohne Wetterboost:",
                          parseMode: ParseMode.Markdown
                        );
                        await botClient.SendTextMessageAsync(
                          chatId: e.Message.Chat,
                          text: output,
                          parseMode: ParseMode.Markdown
                        );
                        searchstring = Convert.ToString(pokedex) + "&";
                        for (int i = 1; i < 80; i++)
                        {
                            cp = Convert.ToInt16(Math.Floor(((((baseAttack + 15) * cpm[i]) * (Math.Sqrt((baseDefense + 15) * cpm[i])) * Math.Sqrt((baseStamina + 15) * cpm[i])) / 10)));
                            searchstring = searchstring + ",wp" + Convert.ToString(cp);
                        }
                        await botClient.SendTextMessageAsync(
                          chatId: e.Message.Chat,
                          text: "Der Search String für wilde 100IV Pokemons ist: \n",
                          parseMode: ParseMode.Markdown
                          );
                        await botClient.SendTextMessageAsync(
                          chatId: e.Message.Chat,
                          text: searchstring,
                          parseMode: ParseMode.Markdown
                        );
                    }
                    else
                    {
                      await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "Befehle: \n */cd*: Cooldown Tabelle \n */rb*: Raidboss Info \n */rwd*: Raid Belohnungen \n */hs*: Spoof Hotspots \n */ivmt* Perfekte IV für Mewtu \n */ivlv* Perfekte IV für Larvitar \n */ivth* Perfekte IV für Tanhel \n Pokedex nummer gibt die 100% WP in einem Raid aus" ,
                        parseMode: ParseMode.Markdown
                      );
                    }
                }
            }
        }
    }
}
