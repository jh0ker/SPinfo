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
        private const string FirstOptionText = "First option";
        private const string SecondOptionText = "Second option";

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            long willson;
            string cooldown;
            string hotspots;
            string pivmt;
            string pivmten;
            //string pivtn;
            //string pivtnen;
            string pivbd;
            string pivbden;
            string pivlv;
            string pivlven;
            pivlven = "246&cp12,cp40,cp67,cp94,cp122,cp149,cp176,cp203,cp231,cp258,cp284,cp310,cp336,cp361,cp387,cp413,cp439,cp465,cp491,cp517,cp542,cp568,cp594,cp620,cp646,cp672,cp698,cp723,cp749,cp775,cp788,cp801,cp814,cp827,cp840,cp12,cp39,cp67,cp94,cp121,cp148,cp175,cp203,cp230,cp257,cp283,cp308,cp334,cp360,cp386,cp411,cp437,cp463,cp488,cp514,cp540,cp566,cp591,cp617,cp643,cp669,cp694,cp720,cp746,cp772,cp784,cp797,cp810,cp823,cp836,cp12,cp39,cp67,cp94,cp121,cp148,cp175,cp203,cp230,cp257,cp283,cp308,cp334,cp360,cp386,cp411,cp437,cp463,cp489,cp514,cp540,cp566,cp592,cp617,cp643,cp669,cp695,cp720,cp746,cp772,cp785,cp798,cp810,cp823,cp836";
            pivlv = "246&wp12,wp40,wp67,wp94,wp122,wp149,wp176,wp203,wp231,wp258,wp284,wp310,wp336,wp361,wp387,wp413,wp439,wp465,wp491,wp517,wp542,wp568,wp594,wp620,wp646,wp672,wp698,wp723,wp749,wp775,wp788,wp801,wp814,wp827,wp840,wp12,wp39,wp67,wp94,wp121,wp148,wp175,wp203,wp230,wp257,wp283,wp308,wp334,wp360,wp386,wp411,wp437,wp463,wp488,wp514,wp540,wp566,wp591,wp617,wp643,wp669,wp694,wp720,wp746,wp772,wp784,wp797,wp810,wp823,wp836,wp12,wp39,wp67,wp94,wp121,wp148,wp175,wp203,wp230,wp257,wp283,wp308,wp334,wp360,wp386,wp411,wp437,wp463,wp489,wp514,wp540,wp566,wp592,wp617,wp643,wp669,wp695,wp720,wp746,wp772,wp785,wp798,wp810,wp823,wp836";
            pivbden = "374&cp11,cp37,cp62,cp88,cp113,cp139,cp164,cp190,cp215,cp241,cp265,cp289,cp313,cp337,cp361,cp385,cp409,cp434,cp458,cp482,cp506,cp530,cp554,cp578,cp602,cp626,cp651,cp675,cp699,cp723,cp735,cp747,cp759,cp771,cp783,cp11,cp37,cp62,cp88,cp113,cp138,cp164,cp189,cp215,cp240,cp264,cp288,cp312,cp336,cp360,cp384,cp408,cp432,cp456,cp480,cp504,cp528,cp552,cp576,cp600,cp624,cp649,cp673,cp697,cp721,cp733,cp745,cp757,cp769,cp781,cp11,cp37,cp62,cp87,cp113,cp138,cp163,cp189,cp214,cp239,cp263,cp287,cp311,cp335,cp359,cp383,cp407,cp431,cp455,cp479,cp503,cp527,cp551,cp575,cp599,cp623,cp647,cp671,cp695,cp719,cp731,cp743,cp755,cp767,cp779";
            pivbd = "374&wp11,wp37,wp62,wp88,wp113,wp139,wp164,wp190,wp215,wp241,wp265,wp289,wp313,wp337,wp361,wp385,wp409,wp434,wp458,wp482,wp506,wp530,wp554,wp578,wp602,wp626,wp651,wp675,wp699,wp723,wp735,wp747,wp759,wp771,wp783,wp11,wp37,wp62,wp88,wp113,wp138,wp164,wp189,wp215,wp240,wp264,wp288,wp312,wp336,wp360,wp384,wp408,wp432,wp456,wp480,wp504,wp528,wp552,wp576,wp600,wp624,wp649,wp673,wp697,wp721,wp733,wp745,wp757,wp769,wp781,wp11,wp37,wp62,wp87,wp113,wp138,wp163,wp189,wp214,wp239,wp263,wp287,wp311,wp335,wp359,wp383,wp407,wp431,wp455,wp479,wp503,wp527,wp551,wp575,wp599,wp623,wp647,wp671,wp695,wp719,wp731,wp743,wp755,wp767,wp779";
            //pivtn = "248&wp51,wp162,wp273,wp384,wp495,wp605,wp716,wp827,wp938,wp1048,wp1153,wp1258,wp1363,wp1468,wp1573,wp1677,wp1782,wp1887,wp1992,wp2097,wp2202,wp2307,wp2411,wp2516,wp2621,wp2726,wp2831,wp2936,wp3041,wp3146,wp3198,wp3250,wp3303,wp3355,wp3408,wp51,wp162,wp272,wp383,wp493,wp604,wp715,wp825,wp936,wp1046,wp1151,wp1255,wp1360,wp1465,wp1569,wp1674,wp1778,wp1883,wp1988,wp2092,wp2197,wp2301,wp2406,wp2511,wp2615,wp2720,wp2825,wp2929,wp3034,wp3139,wp3191,wp3243,wp3296,wp3348,wp3400,wp51,wp162,wp272,wp383,wp493,wp604,wp714,wp825,wp936,wp1046,wp1151,wp1255,wp1360,wp1464,wp1569,wp1674,wp1778,wp1883,wp1987,wp2092,wp2196,wp2301,wp2406,wp2510,wp2615,wp2720,wp2824,wp2929,wp3034,wp3138,wp3191,wp3243,wp3295,wp3347,wp3400";
            //pivtnen = "248&cp51,cp162,cp273,cp384,cp495,cp605,cp716,cp827,cp938,cp1048,cp1153,cp1258,cp1363,cp1468,cp1573,cp1677,cp1782,cp1887,cp1992,cp2097,cp2202,cp2307,cp2411,cp2516,cp2621,cp2726,cp2831,cp2936,cp3041,cp3146,cp3198,cp3250,cp3303,cp3355,cp3408,cp51,cp162,cp272,cp383,cp493,cp604,cp715,cp825,cp936,cp1046,cp1151,cp1255,cp1360,cp1465,cp1569,cp1674,cp1778,cp1883,cp1988,cp2092,cp2197,cp2301,cp2406,cp2511,cp2615,cp2720,cp2825,cp2929,cp3034,cp3139,cp3191,cp3243,cp3296,cp3348,cp3400,cp51,cp162,cp272,cp383,cp493,cp604,cp714,cp825,cp936,cp1046,cp1151,cp1255,cp1360,cp1464,cp1569,cp1674,cp1778,cp1883,cp1987,cp2092,cp2196,cp2301,cp2406,cp2510,cp2615,cp2720,cp2824,cp2929,cp3034,cp3138,cp3191,cp3243,cp3295,cp3347,cp3400";
            pivmten = "150&cp56,cp176,cp296,cp416,cp537,cp657,cp777,cp897,cp1018,cp1138,cp1251,cp1365,cp1479,cp1593,cp1706,cp1820,cp1934,cp2048,cp2161,cp2275,cp2389,cp2503,cp2617,cp2730,cp2844,cp2958,cp3072,cp3186,cp3300,cp3413,cp3470,cp3527,cp3584,cp3641,cp3698,cp56,cp176,cp296,cp415,cp535,cp655,cp775,cp895,cp1015,cp1135,cp1248,cp1362,cp1475,cp1589,cp1702,cp1816,cp1929,cp2042,cp2156,cp2269,cp2383,cp2496,cp2610,cp2723,cp2837,cp2951,cp3064,cp3178,cp3291,cp3405,cp3461,cp3518,cp3575,cp3632,cp3688,cp56,cp176,cp296,cp415,cp535,cp655,cp775,cp895,cp1015,cp1135,cp1248,cp1362,cp1475,cp1589,cp1702,cp1816,cp1929,cp2043,cp2156,cp2270,cp2383,cp2497,cp2610,cp2724,cp2837,cp2951,cp3064,cp3178,cp3292,cp3405,cp3462,cp3519,cp3575,cp3632,cp3689";
            
            pivmt = "150&wp56,wp176,wp296,wp416,wp537,wp657,wp777,wp897,wp1018,wp1138,wp1251,wp1365,wp1479,wp1593,wp1706,wp1820,wp1934,wp2048,wp2161,wp2275,wp2389,wp2503,wp2617,wp2730,wp2844,wp2958,wp3072,wp3186,wp3300,wp3413,wp3470,wp3527,wp3584,wp3641,wp3698,wp56,wp176,wp296,wp415,wp535,wp655,wp775,wp895,wp1015,wp1135,wp1248,wp1362,wp1475,wp1589,wp1702,wp1816,wp1929,wp2042,wp2156,wp2269,wp2383,wp2496,wp2610,wp2723,wp2837,wp2951,wp3064,wp3178,wp3291,wp3405,wp3461,wp3518,wp3575,wp3632,wp3688,wp56,wp176,wp296,wp415,wp535,wp655,wp775,wp895,wp1015,wp1135,wp1248,wp1362,wp1475,wp1589,wp1702,wp1816,wp1929,wp2043,wp2156,wp2270,wp2383,wp2497,wp2610,wp2724,wp2837,wp2951,wp3064,wp3178,wp3292,wp3405,wp3462,wp3519,wp3575,wp3632,wp3689";
            cooldown = " \n 1km <1min \n 2km 1min \n 3km <2min \n 4.6km 2min \n 5km 2min \n 7km 5min \n 9km <7min \n 10km 7min \n 12km 8min \n 18km 10min \n 26km 15min \n 42km 19min \n 65km <22min \n 76km <25min \n 81km 25min \n 100km <35min \n 220km 40min \n 250km 45min \n 350km 51min \n 460km 58min \n 500km 60min \n 565km 67min \n 700km 75min \n 716km 78min \n 830km  <86min \n 1000km 90min \n 1500km+  2hr";
            hotspots = "\n *Top Notch XP Grinding Spots* \n \n 1: Copenhagen, Denmark / 55.662278, 12.56246 \n 2: German Mall, Germany / 51.48986, 6.87533 \n 3: Paris Mall, Paris, France / 48.890717, 2.23783 \n \n *Coordinates List* \n \n 1: Chicago Lake Front / 42.0411,-87.7102 ( 60 Pokestops  \n 2: New York / 40.75512,-73.9836 ( 7 Pokestops  \n 3: Japan / 35.6961,139.8144 ( 9 Pokestops  \n 4: Mall of Scandanavia, Sweden / 59.370392, 18.002844 ( 6 Pokestops  \n 5: Disneyland, Florida / 33.8119,-117.919 \n 6: Victory Memorial Park, Minneapolis / 45.0301,-93.3195 \n 7: Central Park, New York / 40.7803,-73.963 \n 8: Santa Monica, California / 34.0076,-118.499 \n 9: Liberty Park / 40.7442,-111.874 \n 10: Paris / 48.8615, 2.289 \n 11: Union Square / 37.7879,-122.407 \n 12: Sidney / -33.8610,151.212 \n 13: Austin Texas / 30.2742,-97.740 \n 14: Olympia / 47.0477,-122.9041 ( 4 Pokestops  \n 16: NgurahRai Airport / -8.7467,115.166 \n 17: Times square, New York / 40.7589,-73.985 \n 18: Big Ben, London / 51.5010,-0.124 \n 19: Tokyo Disneyland / 35.6312,139.880 \n 20: Waikiki Aquarium / 21.2658,-157.821 \n 21: Rigos Loop / 21.2983,-157.860 \n 22: Jimmys Loop, USA 32.7758,-96.796 \n 23: Toms Loop, Ann Arbor / 42.2808,-83.743 \n 24: San Francisco Pier, California / 39 37.8095,-122.410 \n 25: Alameda, Mexico / 19.4362,-99.144 \n 26: Sydney Botanical / -33.8643,151.215 \n 27: Bidadari, Indonesia / -6.0356,106.746 \n 28: Long Beach, California / 33.7700,-118.193 \n 29: Den Haag, Netherland / 52.0689,4.220 \n 30: Korea / 37.5113,127.0980 ( 11 Pokestops  \n 31: Germany Mall / 51.491104,6.88055 \n 32: Copenhagen, Denmark Mall / 55.662518, 12.56217 \n 33: (Park) National Mall, Washington DC / 38.888515, -77.024124";
            willson = 481465695;

            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
                /*if (e.Message.Chat.Id == willson) {
                    await botClient.SendTextMessageAsync(
                 chatId: e.Message.Chat,

                 text: "~",
                   parseMode: ParseMode.Markdown
                 );
                }
                else
                {*/
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
                    else if (e.Message.Text == "/ivmt")
                    {
                        await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "Perfekte IV für _Mewtu_ ready2copypasta \n \n _INFO:_ \n Beinhaltet 98% mit 15 ATK \n Falls du dein Spiel auf Englisch hast, benutze bitte */ivmten*",
                        parseMode: ParseMode.Markdown
                     ); await botClient.SendTextMessageAsync(
                         chatId: e.Message.Chat,
                         text: pivmt,
                         parseMode: ParseMode.Markdown
                      );
                    }
                    else if (e.Message.Text == "/ivmten")
                    {
                        await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "Perfekte IV für _Mewtu_ ready2copypasta \n \n _INFO:_ \n Beinhaltet 98% mit 15 ATK \n Englische Version",
                        parseMode: ParseMode.Markdown
                     ); await botClient.SendTextMessageAsync(
                         chatId: e.Message.Chat,
                         text: pivmten,
                         parseMode: ParseMode.Markdown
                      );
                    }
                    /*
                    else if (e.Message.Text == "/ivdp")
                    {
                        await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "Perfekte IV für _Despotar_ ready2copypasta \n \n _INFO:_ \n Beinhaltet 98% mit 15 ATK \n Falls du dein Spiel auf Englisch hast, benutze bitte /ivdpen*",
                        parseMode: ParseMode.Markdown
                     ); await botClient.SendTextMessageAsync(
                         chatId: e.Message.Chat,
                         text: pivtn,
                         parseMode: ParseMode.Markdown
                      );
                    }
                    else if (e.Message.Text == "/ivdpen")
                    {
                        await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "Perfekte IV für _Despotar_ ready2copypasta \n \n _INFO:_ \n Beinhaltet 98% mit 15 ATK \n Englische Version",
                        parseMode: ParseMode.Markdown
                     ); await botClient.SendTextMessageAsync(
                         chatId: e.Message.Chat,
                         text: pivtnen,
                         parseMode: ParseMode.Markdown
                      );
                    }*/
                else if (e.Message.Text == "/ivth")
                {
                    await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Perfekte IV für _Tanhel_ ready2copypasta \n \n _INFO:_ \n Beinhaltet 98% mit 15 ATK \n Falls du dein Spiel auf Englisch hast, benutze bitte */ivthen*",
                    parseMode: ParseMode.Markdown
                 ); await botClient.SendTextMessageAsync(
                     chatId: e.Message.Chat,
                     text: pivbd,
                     parseMode: ParseMode.Markdown
                  );
                }
                else if (e.Message.Text == "/ivthen")
                {
                    await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Perfekte IV für _Tanhel_ ready2copypasta \n \n _INFO:_ \n Beinhaltet 98% mit 15 ATK \n Englische Version",
                    parseMode: ParseMode.Markdown
                 ); await botClient.SendTextMessageAsync(
                     chatId: e.Message.Chat,
                     text: pivbden,
                     parseMode: ParseMode.Markdown
                  );
                }
                else if (e.Message.Text == "/ivlv")
                {
                    await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Perfekte IV für _Tanhel_ ready2copypasta \n \n _INFO:_ \n Beinhaltet 98% mit 15 ATK \n  Falls du dein Spiel auf Englisch hast, benutze bitte */ivlven*",
                    parseMode: ParseMode.Markdown
                 ); await botClient.SendTextMessageAsync(
                     chatId: e.Message.Chat,
                     text: pivlv,
                     parseMode: ParseMode.Markdown
                  );
                }
                else if (e.Message.Text == "/ivlven")
                {
                    await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Perfekte IV für _Larvitar_ ready2copypasta \n \n _INFO:_ \n Beinhaltet 98% mit 15 ATK \n Englische Version",
                    parseMode: ParseMode.Markdown
                 ); await botClient.SendTextMessageAsync(
                     chatId: e.Message.Chat,
                     text: pivlven,
                     parseMode: ParseMode.Markdown
                  );
                }

                else
                    {
                        await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,

                      text: "Befehle: \n */cd*: Cooldown Tabelle \n */rb*: Raidboss Info \n */rwd*: Raid Belohnungen \n */hs*: Spoof Hotspots \n */ivmt* Perfekte IV für Mewtu \n */ivlv* Perfekte IV für Larvitar \n */ivth* Perfekte IV für Tanhel",
                        parseMode: ParseMode.Markdown
                      );
                    //}
                }
            }
        }
    }
}