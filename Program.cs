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
            bool wb;
            int cp = 0;
            int pokedex = 0;
            int baseAttack = 300;
            int baseDefense = 182;
            int baseStamina = 193;
            //[2,x] = BaseAttack
            //[3,x] = BaseDefense
            //[4,x] = BaseStamina
            var regex = new Regex(@"[0-3]+[0-9]+[0-9]");
            double[] cpm = new double[80] { 0, 0.094, 0.135137432, 0.16639787, 0.192650919, 0.21573247, 0.236572661, 0.25572005, 0.273530381, 0.29024988, 0.306057377, 0.3210876, 0.335445036, 0.34921268, 0.362457751, 0.37523559, 0.387592406, 0.39956728, 0.411193551, 0.42250001, 0.432926419, 0.44310755, 0.4530599578, 0.46279839, 0.472336083, 0.48168495, 0.4908558, 0.49985844, 0.508701765, 0.51739395, 0.525942511, 0.53435433, 0.542635767, 0.55079269, 0.558830576, 0.56675452, 0.574569153, 0.58227891, 0.589887917, 0.59740001, 0.604818814, 0.61215729, 0.619399365, 0.62656713, 0.633644533, 0.64065295, 0.647576426, 0.65443563, 0.661214806, 0.667934, 0.674577537, 0.68116492, 0.687680648, 0.69414365, 0.700538673, 0.70688421, 0.713164996, 0.71939909, 0.725571552, 0.7317, 0.734741009, 0.73776948, 0.740785574, 0.74378943, 0.746781211, 0.74976104, 0.752729087, 0.75568551, 0.758630378, 0.76156384, 0.764486065, 0.76739717, 0.770297266, 0.7731865, 0.776064962, 0.77893275, 0.781790055, 0.78463697, 0.787473578, 0.79030001 };
            int[,] baseStats = new int[3, 387] { { 0, 118, 151, 198, 116, 158, 223, 94, 126, 171, 55, 45, 167, 63, 46, 169, 85, 117, 166, 103, 161, 112, 182, 110, 167, 112, 193, 126, 182, 86, 117, 180, 105, 137, 204, 107, 178, 96, 169, 80, 156, 83, 161, 131, 153, 202, 121, 165, 100, 179, 109, 167, 92, 150, 122, 191, 148, 207, 136, 227, 101, 130, 182, 195, 232, 271, 137, 177, 234, 139, 172, 207, 97, 166, 132, 164, 211, 170, 207, 109, 177, 165, 223, 124, 158, 218, 85, 139, 135, 190, 116, 186, 186, 223, 261, 85, 89, 144, 181, 240, 109, 173, 107, 233, 90, 144, 224, 193, 108, 119, 174, 140, 222, 60, 183, 181, 129, 187, 123, 175, 137, 210, 192, 218, 223, 198, 206, 238, 198, 29, 237, 165, 91, 104, 205, 232, 246, 153, 155, 207, 148, 220, 221, 190, 192, 253, 251, 119, 163, 263, 300, 210, 92, 122, 168, 116, 158, 223, 117, 150, 205, 79, 148, 67, 145, 72, 107, 105, 161, 194, 106, 146, 77, 75, 69, 67, 139, 134, 192, 114, 145, 211, 169, 37, 112, 167, 174, 67, 91, 118, 136, 55, 185, 154, 75, 152, 261, 126, 175, 177, 167, 136, 60, 182, 108, 161, 131, 143, 148, 137, 212, 184, 236, 17, 234, 189, 142, 236, 118, 139, 90, 181, 118, 127, 197, 128, 148, 148, 152, 224, 194, 107, 214, 198, 192, 40, 64, 173, 153, 135, 151, 157, 129, 241, 235, 180, 115, 155, 251, 193, 263, 210, 124, 172, 223, 130, 163, 240, 126, 156, 208, 96, 171, 58, 142, 75, 60, 189, 60, 98, 71, 112, 173, 71, 134, 200, 106, 185, 106, 175, 79, 117, 237, 93, 192, 74, 241, 104, 159, 290, 80, 199, 153, 92, 134, 179, 99, 209, 36, 82, 84, 132, 141, 155, 121, 158, 198, 78, 121, 123, 215, 167, 147, 143, 143, 186, 80, 140, 171, 243, 136, 175, 119, 194, 151, 125, 171, 116, 162, 134, 205, 156, 221, 76, 141, 222, 196, 178, 178, 93, 151, 141, 224, 77, 140, 105, 152, 176, 222, 29, 192, 139, 161, 138, 218, 70, 124, 136, 175, 246, 41, 95, 162, 95, 137, 182, 133, 197, 211, 162, 81, 134, 172, 277, 96, 138, 257, 179, 179, 143, 228, 268, 297, 297, 312, 210, 345 }, { 0, 118, 151, 198, 96, 129, 176, 122, 155, 210, 62, 94, 151, 55, 86, 150, 76, 108, 157, 70, 144, 61, 135, 102, 158, 101, 165, 145, 202, 94, 126, 174, 76, 112, 157, 116, 171, 122, 204, 44, 93, 76, 153, 116, 139, 170, 99, 146, 102, 150, 88, 147, 81, 139, 96, 163, 87, 144, 96, 166, 82, 130, 187, 103, 138, 194, 88, 130, 162, 64, 95, 138, 182, 237, 163, 196, 229, 132, 167, 109, 194, 128, 182, 118, 88, 145, 128, 184, 90, 184, 168, 323, 70, 112, 156, 288, 158, 215, 156, 214, 114, 179, 140, 158, 165, 200, 211, 212, 137, 164, 221, 157, 206, 176, 205, 165, 125, 182, 115, 154, 112, 184, 233, 170, 182, 173, 169, 197, 197, 102, 197, 180, 91, 121, 177, 201, 204, 139, 174, 227, 162, 203, 164, 190, 249, 188, 184, 94, 138, 201, 182, 210, 122, 155, 202, 96, 129, 176, 116, 151, 197, 77, 130, 101, 179, 142, 209, 73, 128, 178, 106, 146, 63, 91, 34, 116, 191, 89, 146, 82, 112, 172, 189, 93, 152, 198, 192, 101, 127, 197, 112, 55, 148, 94, 75, 152, 194, 250, 87, 194, 167, 91, 106, 133, 146, 242, 131, 204, 333, 89, 137, 148, 191, 396, 189, 157, 93, 144, 71, 209, 74, 147, 156, 69, 141, 90, 260, 260, 93, 159, 194, 107, 214, 183, 132, 88, 64, 214, 116, 110, 108, 211, 229, 210, 176, 235, 93, 133, 212, 323, 301, 210, 104, 130, 180, 92, 115, 141, 93, 133, 175, 63, 137, 80, 128, 61, 91, 98, 91, 172, 86, 128, 191, 86, 78, 121, 61, 130, 61, 189, 63, 100, 220, 97, 161, 110, 153, 104, 159, 183, 153, 116, 80, 42, 81, 142, 54, 114, 71, 236, 84, 132, 141, 155, 168, 240, 314, 107, 152, 78, 127, 147, 167, 171, 171, 148, 99, 159, 39, 83, 68, 87, 82, 139, 234, 145, 211, 116, 78, 99, 168, 74, 115, 139, 208, 124, 118, 163, 163, 83, 142, 113, 156, 131, 236, 154, 198, 100, 183, 102, 242, 139, 212, 66, 127, 162, 234, 165, 174, 120, 86, 95, 162, 90, 132, 176, 149, 194, 194, 234, 134, 107, 179, 168, 141, 185, 248, 356, 356, 285, 268, 228, 276, 276, 187, 210, 115 }, { 0, 90, 120, 160, 78, 116, 156, 88, 118, 158, 90, 100, 120, 80, 90, 130, 80, 126, 166, 60, 110, 80, 130, 70, 120, 70, 120, 100, 150, 110, 140, 180, 92, 122, 162, 140, 190, 76, 146, 230, 280, 80, 150, 90, 120, 150, 70, 120, 120, 140, 20, 70, 80, 130, 100, 160, 80, 130, 110, 180, 80, 130, 180, 50, 80, 110, 140, 160, 180, 100, 130, 160, 80, 160, 80, 110, 160, 100, 130, 180, 190, 50, 100, 104, 70, 120, 130, 180, 160, 210, 60, 100, 60, 90, 120, 70, 120, 170, 60, 110, 80, 120, 120, 190, 100, 120, 100, 100, 180, 80, 130, 160, 210, 500, 130, 210, 60, 110, 90, 160, 60, 120, 80, 140, 130, 130, 130, 130, 150, 40, 190, 260, 96, 110, 260, 130, 130, 130, 70, 140, 60, 120, 160, 320, 180, 180, 180, 82, 122, 182, 193, 200, 90, 120, 160, 78, 116, 156, 100, 130, 170, 70, 170, 120, 200, 80, 110, 80, 140, 170, 150, 250, 40, 100, 180, 70, 110, 80, 130, 110, 140, 180, 150, 140, 200, 140, 180, 70, 110, 150, 110, 60, 150, 130, 110, 190, 130, 190, 120, 190, 120, 96, 380, 140, 100, 150, 200, 130, 150, 120, 180, 130, 140, 40, 160, 110, 120, 180, 80, 100, 100, 200, 110, 70, 150, 90, 130, 130, 90, 150, 150, 180, 180, 170, 146, 110, 70, 100, 90, 90, 90, 190, 510, 180, 230, 200, 100, 140, 200, 212, 212, 200, 80, 100, 140, 90, 120, 160, 100, 140, 200, 70, 140, 76, 156, 90, 100, 120, 100, 120, 80, 120, 160, 80, 140, 180, 80, 120, 80, 120, 56, 76, 136, 80, 140, 120, 120, 120, 160, 273, 62, 122, 2, 128, 168, 208, 144, 288, 100, 60, 100, 140, 100, 100, 100, 120, 140, 60, 120, 80, 140, 120, 120, 130, 130, 100, 140, 200, 90, 140, 260, 340, 120, 140, 140, 120, 160, 120, 90, 100, 160, 100, 140, 90, 150, 146, 146, 180, 180, 100, 220, 86, 126, 80, 120, 132, 172, 90, 150, 40, 190, 140, 120, 88, 128, 40, 80, 198, 150, 130, 190, 100, 160, 140, 180, 220, 70, 110, 110, 200, 86, 90, 130, 190, 80, 120, 160, 160, 160, 160, 160, 160, 200, 200, 210, 200, 100 } };
            string output;
            long willson;
            string msg;
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
                        text: "Die 100IV WP in einem Raid ist:",
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
                    //}
                }
            }
        }
    }
}