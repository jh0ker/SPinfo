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
            botClient = new TelegramBotClient("XXXURIDHEREXXX");

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
            int cp = 0;
            int pokedex = 0;
            int baseAttack = 0;
            int baseDefense = 0;
            int baseStamina = 0;
            //[0,x] = BaseAttack
            //[1,x] = BaseDefense
            //[2,x] = BaseStamina
            //[3,x] = Candys to evolve
            var regex = new Regex(@"^[0-4]+[0-9]+[0-9]$");
            var coords = new Regex(@"^(\+|-)?(?:90(?:(?:\.0{1,6})?)|(?:[0-9]|[1-8][0-9])(?:(?:\.[0-9]{1,6})?)).(\+|-)?(?:180(?:(?:\.0{1,6})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\.[0-9]{1,6})?))$");
            double[] cpm = new double[81] { 0, 0, 0.094, 0.135137432, 0.16639787, 0.192650919, 0.21573247, 0.236572661, 0.25572005, 0.273530381, 0.29024988, 0.306057377, 0.3210876, 0.335445036, 0.34921268, 0.362457751, 0.37523559, 0.387592406, 0.39956728, 0.411193551, 0.42250001, 0.432926419, 0.44310755, 0.4530599578, 0.46279839, 0.472336083, 0.48168495, 0.4908558, 0.49985844, 0.508701765, 0.51739395, 0.525942511, 0.53435433, 0.542635767, 0.55079269, 0.558830576, 0.56675452, 0.574569153, 0.58227891, 0.589887917, 0.59740001, 0.604818814, 0.61215729, 0.619399365, 0.62656713, 0.633644533, 0.64065295, 0.647576426, 0.65443563, 0.661214806, 0.667934, 0.674577537, 0.68116492, 0.687680648, 0.69414365, 0.700538673, 0.70688421, 0.713164996, 0.71939909, 0.725571552, 0.7317, 0.734741009, 0.73776948, 0.740785574, 0.74378943, 0.746781211, 0.74976104, 0.752729087, 0.75568551, 0.758630378, 0.76156384, 0.764486065, 0.76739717, 0.770297266, 0.7731865, 0.776064962, 0.77893275, 0.781790055, 0.78463697, 0.787473578, 0.79030001 };
            int[,] baseStats = new int[4, 494] { { 0, 118, 151, 198, 116, 158, 223, 94, 126, 171, 55, 45, 167, 63, 46, 169, 85, 117, 166, 103, 161, 112, 182, 110, 167, 112, 193, 126, 182, 86, 117, 180, 105, 137, 204, 107, 178, 96, 169, 80, 156, 83, 161, 131, 153, 202, 121, 165, 100, 179, 109, 167, 92, 150, 122, 191, 148, 207, 136, 227, 101, 130, 182, 195, 232, 271, 137, 177, 234, 139, 172, 207, 97, 166, 132, 164, 211, 170, 207, 109, 177, 165, 223, 124, 158, 218, 85, 139, 135, 190, 116, 186, 186, 223, 261, 85, 89, 144, 181, 240, 109, 173, 107, 233, 90, 144, 224, 193, 108, 119, 174, 140, 222, 60, 183, 181, 129, 187, 123, 175, 137, 210, 192, 218, 223, 198, 206, 238, 198, 29, 237, 165, 91, 104, 205, 232, 246, 153, 155, 207, 148, 220, 221, 190, 192, 253, 251, 119, 163, 263, 300, 210, 92, 122, 168, 116, 158, 223, 117, 150, 205, 79, 148, 67, 145, 72, 107, 105, 161, 194, 106, 146, 77, 75, 69, 67, 139, 134, 192, 114, 145, 211, 169, 37, 112, 167, 174, 67, 91, 118, 136, 55, 185, 154, 75, 152, 261, 126, 175, 177, 167, 136, 60, 182, 108, 161, 131, 143, 148, 137, 212, 184, 236, 17, 234, 189, 142, 236, 118, 139, 90, 181, 118, 127, 197, 128, 148, 148, 152, 224, 194, 107, 214, 198, 192, 40, 64, 173, 153, 135, 151, 157, 129, 241, 235, 180, 115, 155, 251, 193, 239, 210, 124, 172, 223, 130, 163, 240, 126, 156, 208, 96, 171, 58, 142, 75, 60, 189, 60, 98, 71, 112, 173, 71, 134, 200, 106, 185, 106, 175, 79, 117, 237, 93, 192, 74, 241, 104, 159, 290, 80, 196, 153, 92, 134, 179, 99, 209, 36, 82, 84, 132, 141, 155, 121, 158, 198, 78, 121, 123, 215, 167, 147, 143, 143, 186, 80, 140, 171, 243, 136, 175, 119, 194, 151, 125, 171, 116, 162, 134, 205, 156, 221, 76, 141, 222, 196, 178, 178, 93, 151, 141, 224, 77, 140, 105, 152, 176, 222, 29, 192, 139, 161, 138, 218, 70, 124, 136, 175, 246, 41, 95, 162, 95, 137, 182, 133, 197, 211, 162, 81, 134, 172, 277, 96, 138, 257, 179, 179, 143, 228, 268, 270, 270, 284, 210, 345, 119, 157, 202, 113, 158, 222, 112, 150, 210, 101, 142, 234, 80, 162, 45, 160, 117, 159, 232, 91, 243, 218, 295, 76, 94, 53, 141, 185, 59, 149, 94, 132, 221, 108, 170, 103, 169, 205, 117, 180, 130, 156, 211, 243, 109, 172, 114, 121, 184, 43, 161, 124, 125, 25, 183, 169, 124, 172, 261, 137, 127, 236, 124, 201, 93, 180, 116, 211, 187, 96, 142, 105, 115, 178, 243, 238, 161, 241, 207, 249, 247, 225, 231, 216, 238, 185, 247, 264, 237, 135, 180, 171, 204, 156, 212, 270, 275, 280, 251, 287, 187, 152, 162, 210, 285, 261, 238 }, { 0, 118, 151, 198, 96, 129, 176, 122, 155, 210, 62, 94, 151, 55, 86, 150, 76, 108, 157, 70, 144, 61, 135, 102, 158, 101, 165, 145, 202, 94, 126, 174, 76, 112, 157, 116, 171, 122, 204, 44, 93, 76, 153, 116, 139, 170, 99, 146, 102, 150, 88, 147, 81, 139, 96, 163, 87, 144, 96, 166, 82, 130, 187, 103, 138, 194, 88, 130, 162, 64, 95, 138, 182, 237, 163, 196, 229, 132, 167, 109, 194, 128, 182, 118, 88, 145, 128, 184, 90, 184, 168, 323, 70, 112, 156, 288, 158, 215, 156, 214, 114, 179, 140, 158, 165, 200, 211, 212, 137, 164, 221, 157, 206, 176, 205, 165, 125, 182, 115, 154, 112, 184, 233, 170, 182, 173, 169, 197, 197, 102, 197, 180, 91, 121, 177, 201, 204, 139, 174, 227, 162, 203, 164, 190, 249, 188, 184, 94, 138, 201, 182, 210, 122, 155, 202, 96, 129, 176, 116, 151, 197, 77, 130, 101, 179, 142, 209, 73, 128, 178, 106, 146, 63, 91, 34, 116, 191, 89, 146, 82, 112, 172, 189, 93, 152, 198, 192, 101, 127, 197, 112, 55, 148, 94, 75, 152, 194, 250, 87, 194, 167, 91, 106, 133, 146, 242, 131, 204, 333, 89, 137, 148, 191, 396, 189, 157, 93, 144, 71, 209, 74, 147, 156, 69, 141, 90, 260, 260, 93, 159, 194, 107, 214, 183, 132, 88, 64, 214, 116, 110, 108, 211, 229, 210, 176, 235, 93, 133, 212, 323, 274, 210, 104, 130, 180, 92, 115, 141, 93, 133, 175, 63, 137, 80, 128, 61, 91, 98, 91, 172, 86, 128, 191, 86, 78, 121, 61, 130, 61, 189, 63, 100, 220, 97, 161, 110, 153, 104, 159, 183, 153, 114, 80, 42, 81, 142, 54, 114, 71, 236, 84, 132, 141, 155, 168, 240, 314, 107, 152, 78, 127, 147, 167, 171, 171, 148, 99, 159, 39, 83, 68, 87, 82, 139, 234, 145, 211, 116, 78, 99, 168, 74, 115, 139, 208, 124, 118, 163, 163, 83, 142, 113, 156, 131, 236, 154, 198, 100, 183, 102, 242, 139, 212, 66, 127, 162, 234, 165, 174, 120, 86, 95, 162, 90, 132, 176, 149, 194, 194, 234, 134, 107, 179, 168, 141, 185, 247, 356, 356, 285, 268, 228, 251, 251, 170, 210, 115, 110, 143, 188, 86, 105, 151, 102, 139, 186, 58, 94, 140, 73, 119, 74, 100, 64, 95, 156, 109, 185, 71, 109, 195, 286, 83, 180, 98, 83, 190, 172, 67, 114, 92, 153, 105, 143, 143, 80, 102, 105, 194, 187, 103, 82, 133, 94, 90, 132, 154, 213, 133, 142, 77, 91, 199, 84, 125, 193, 117, 78, 144, 118, 191, 151, 202, 76, 133, 136, 116, 170, 179, 105, 158, 171, 205, 181, 190, 184, 163, 172, 217, 156, 219, 205, 222, 146, 150, 195, 275, 254, 150, 219, 270, 212, 151, 211, 215, 213, 210, 225, 258, 162, 210, 198, 166, 238}, { 0, 90, 120, 160, 78, 116, 156, 88, 118, 158, 90, 100, 120, 80, 90, 130, 80, 126, 166, 60, 110, 80, 130, 70, 120, 70, 120, 100, 150, 110, 140, 180, 92, 122, 162, 140, 190, 76, 146, 230, 280, 80, 150, 90, 120, 150, 70, 120, 120, 140, 20, 70, 80, 130, 100, 160, 80, 130, 110, 180, 80, 130, 180, 50, 80, 110, 140, 160, 180, 100, 130, 160, 80, 160, 80, 110, 160, 100, 130, 180, 190, 50, 100, 104, 70, 120, 130, 180, 160, 210, 60, 100, 60, 90, 120, 70, 120, 170, 60, 110, 80, 120, 120, 190, 100, 120, 100, 100, 180, 80, 130, 160, 210, 500, 130, 210, 60, 110, 90, 160, 60, 120, 80, 140, 130, 130, 130, 130, 150, 40, 190, 260, 96, 110, 260, 130, 130, 130, 70, 140, 60, 120, 160, 320, 180, 180, 180, 82, 122, 182, 193, 200, 90, 120, 160, 78, 116, 156, 100, 130, 170, 70, 170, 120, 200, 80, 110, 80, 140, 170, 150, 250, 40, 100, 180, 70, 110, 80, 130, 110, 140, 180, 150, 140, 200, 140, 180, 70, 110, 150, 110, 60, 150, 130, 110, 190, 130, 190, 120, 190, 120, 96, 380, 140, 100, 150, 200, 130, 150, 120, 180, 130, 140, 40, 160, 110, 120, 180, 80, 100, 100, 200, 110, 70, 150, 90, 130, 130, 90, 150, 150, 180, 180, 170, 146, 110, 70, 100, 90, 90, 90, 190, 510, 180, 230, 200, 100, 140, 200, 212, 193, 200, 80, 100, 140, 90, 120, 160, 100, 140, 200, 70, 140, 76, 156, 90, 100, 120, 100, 120, 80, 120, 160, 80, 140, 180, 80, 120, 80, 120, 56, 76, 136, 80, 140, 120, 120, 120, 160, 273, 62, 122, 2, 128, 168, 208, 144, 288, 100, 60, 100, 140, 100, 100, 100, 120, 140, 60, 120, 80, 140, 120, 120, 130, 130, 100, 140, 200, 90, 140, 260, 340, 120, 140, 140, 120, 160, 120, 90, 100, 160, 100, 140, 90, 150, 146, 146, 180, 180, 100, 220, 86, 126, 80, 120, 132, 172, 90, 150, 40, 190, 140, 120, 88, 128, 40, 80, 198, 150, 130, 190, 100, 160, 140, 180, 220, 70, 110, 110, 200, 86, 90, 130, 190, 80, 120, 160, 160, 160, 160, 160, 160, 182, 182, 191, 200, 100, 146, 181, 216, 127, 162, 183, 142, 162, 197, 120, 146, 198, 153, 188, 114, 184, 128, 155, 190, 120, 155, 167, 219, 102, 155, 120, 155, 172, 102, 172, 155, 146, 198, 128, 172, 183, 244, 181, 207, 312, 146, 163, 155, 225, 135, 174, 128, 160, 230, 149, 167, 137, 85, 225, 183, 137, 151, 169, 239, 286, 120, 172, 169, 239, 120, 172, 134, 195, 179, 135, 170, 128, 155, 207, 172, 172, 242, 251, 225, 181, 181, 198, 200, 163, 163, 181, 242, 198, 169, 155, 128, 172, 137, 181, 190, 181, 205, 189, 209, 221, 284, 260, 190, 225, 172, 225, 237} , {0, 25, 100, -1, 25, 100, -1, 25, 100, -1, 12, 50, -1, 12, 50, -1, 12, 50, -1, 25, -1, 50, -1, 50, -1, 50, -1, 50, -1, 25, 100, -1, 25, 100, -1, 50, -1, 50, -1, 50, -1, 50, -1, 25, 100, -1, 50, -1, 50, -1, 50, -1, 50, -1, 50, -1, 50, -1, 50, -1, 25, 100, -1, 25, 100, -1, 25, 100, -1, 25, 100, -1, 50, -1, 25, 100, -1, 50, -1, 50, -1, 50, -1, -1, 50, -1, 50, -1, 50, -1, 50, -1, 25, 100, -1, -1, 50, -1, 50, -1, 50, -1, 50, -1, 50, -1, -1, -1, -1, 50, -1, 50, -1, -1, -1, -1, 50, -1, 50, -1, 50, -1, -1, -1, -1, -1, -1, -1, -1, 400, -1, -1, -1, 25, -1, -1, -1, -1, 50, -1, 50, -1, -1, -1, -1, -1, -1, 25, 100, -1, -1, -1, 25, 100, -1, 25, 100, -1, 25, 100, -1, 25, -1, 50, -1, 25, -1, 50, -1, -1, 50, -1, 25, 25, 25, 50, -1, 50, -1, 25, 100, -1, -1, 25, -1, -1, -1, 25, 100, -1, -1, 50, -1, -1, 50, -1, -1, -1, -1, -1, -1, -1, -1, -1, 50, -1, -1, -1, -1, 50, -1, -1, -1, -1, -1, -1, 50, -1, 50, -1, 50, -1, -1, 50, -1, -1, -1, -1, 50, -1, -1, 50, -1, -1, -1, -1, 25, -1, 25, 25, 25, -1, -1, -1, -1, -1, 25, 100, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25} };
            //[0,x] = english name
            //[1,x] = german name
            string[,] pkmname = new string[2, 494] {{nicetryhomie, Bulbasaur, Ivysaur, Venusaur, Charmander, Charmeleon, Charizard, Squirtle, Wartortle, Blastoise, Caterpie, Metapod, Butterfree, Weedle, Kakuna, Beedrill, Pidgey, Pidgeotto, Pidgeot, Rattata, Raticate, Spearow, Fearow, Ekans, Arbok, Pikachu, Raichu, Sandshrew, Sandslash, Nidoran♀, Nidorina, Nidoqueen, Nidoran♂, Nidorino, Nidoking, Clefairy, Clefable, Vulpix, Ninetales, Jigglypuff, Wigglytuff, Zubat, Golbat, Oddish, Gloom, Vileplume, Paras, Parasect, Venonat, Venomoth, Diglett, Dugtrio, Meowth, Persian, Psyduck, Golduck, Mankey, Primeape, Growlithe, Arcanine, Poliwag, Poliwhirl, Poliwrath, Abra, Kadabra, Alakazam, Machop, Machoke, Machamp, Bellsprout, Weepinbell, Victreebel, Tentacool, Tentacruel, Geodude, Graveler, Golem, Ponyta, Rapidash, Slowpoke, Slowbro, Magnemite, Magneton, Farfetch, Doduo, Dodrio, Seel, Dewgong, Grimer, Muk, Shellder, Cloyster, Gastly, Haunter, Gengar, Onix, Drowzee, Hypno, Krabby, Kingler, Voltorb, Electrode, Exeggcute, Exeggutor, Cubone, Marowak, Hitmonlee, Hitmonchan, Lickitung, Koffing, Weezing, Rhyhorn, Rhydon, Chansey, Tangela, Kangaskhan, Horsea, Seadra, Goldeen, Seaking, Staryu, Starmie, Mr, Mime, Scyther, Jynx, Electabuzz, Magmar, Pinsir, Tauros, Magikarp, Gyarados, Lapras, Ditto, Eevee, Vaporeon, Jolteon, Flareon, Porygon, Omanyte, Omastar, Kabuto, Kabutops, Aerodactyl, Snorlax, Articuno, Zapdos, Moltres, Dratini, Dragonair, Dragonite, Mewtwo, Mew, Chikorita, Bayleef, Meganium, Cyndaquil, Quilava, Typhlosion, Totodile, Croconaw, Feraligatr, Sentret, Furret, Hoothoot, Noctowl, Ledyba, Ledian, Spinarak, Ariados, Crobat, Chinchou, Lanturn, Pichu, Cleffa, Igglybuff, Togepi, Togetic, Natu, Xatu, Mareep, Flaaffy, Ampharos, Bellossom, Marill, Azumarill, Sudowoodo, Politoed, Hoppip, Skiploom, Jumpluff, Aipom, Sunkern, Sunflora, Yanma, Wooper, Quagsire, Espeon, Umbreon, Murkrow, Slowking, Misdreavus, Unown, Wobbuffet, Girafarig, Pineco, Forretress, Dunsparce, Gligar, Steelix, Snubbull, Granbull, Qwilfish, Scizor, Shuckle, Heracross, Sneasel, Teddiursa, Ursaring, Slugma, Magcargo, Swinub, Piloswine, Corsola, Remoraid, Octillery, Delibird, Mantine, Skarmory, Houndour, Houndoom, Kingdra, Phanpy, Donphan, Porygon2, Stantler, Smeargle, Tyrogue, Hitmontop, Smoochum, Elekid, Magby, Miltank, Blissey, Raikou, Entei, Suicune, Larvitar, Pupitar, Tyranitar, Lugia, Ho oh, Celebi, Treecko, Grovyle, Sceptile, Torchic, Combusken, Blaziken, Mudkip, Marshtomp, Swampert, Poochyena, Mightyena, Zigzagoon, Linoone, Wurmple, Silcoon, Beautifly, Cascoon, Dustox, Lotad, Lombre, Ludicolo, Seedot, Nuzleaf, Shiftry, Taillow, Swellow, Wingull, Pelipper, Ralts, Kirlia, Gardevoir, Surskit, Masquerain, Shroomish, Breloom, Slakoth, Vigoroth, Slaking, Nincada, Ninjask, Shedinja, Whismur, Loudred, Exploud, Makuhita, Hariyama, Azurill, Nosepass, Skitty, Delcatty, Sableye, Mawile, Aron, Lairon, Aggron, Meditite, Medicham, Electrike, Manectric, Plusle, Minun, Volbeat, Illumise, Roselia, Gulpin, Swalot, Carvanha, Sharpedo, Wailmer, Wailord, Numel, Camerupt, Torkoal, Spoink, Grumpig, Spinda, Trapinch, Vibrava, Flygon, Cacnea, Cacturne, Swablu, Altaria, Zangoose, Seviper, Lunatone, Solrock, Barboach, Whiscash, Corphish, Crawdaunt, Baltoy, Claydol, Lileep, Cradily, Anorith, Armaldo, Feebas, Milotic, Castform, Kecleon, Shuppet, Banette, Duskull, Dusclops, Tropius, Chimecho, Absol, Wynaut, Snorunt, Glalie, Spheal, Sealeo, Walrein, Clamperl, Huntail, Gorebyss, Relicanth, Luvdisc, Bagon, Shelgon, Salamence, Beldum, Metang, Metagross, Regirock, Regice, Registeel, Latias, Latios, Kyogre, Groudon, Rayquaza, Jirachi, Deoxys, Turtwig, Grotle, Torterra, Chimchar, Monferno, Infernape, Piplup, Prinplup, Empoleon, Starly, Staravia, Staraptor, Bidoof, Bibarel, Kricketot, Kricketune, Shinx, Luxio, Luxray, Budew, Roserade, Cranidos, Rampardos, Shieldon, Bastiodon, Burmy, Wormadam, Mothim, Combee, Vespiquen, Pachirisu, Buizel, Floatzel, Cherubi, Cherrim, Shellos, Gastrodon, Ambipom, Drifloon, Drifblim, Buneary, Lopunny, Mismagius, Honchkrow, Glameow, Purugly, Chingling, Stunky, Skuntank, Bronzor, Bronzong, Bonsly, Mime_jr, Happiny, Chatot, Spiritomb, Gible, Gabite, Garchomp, Munchlax, Riolu, Lucario, Hippopotas, Hippowdon, Skorupi, Drapion, Croagunk, Toxicroak, Carnivine, Finneon, Lumineon, Mantyke, Snover, Abomasnow, Weavile, Magnezone, Lickilicky, Rhyperior, Tangrowth, Electivire, Magmortar, Togekiss, Yanmega, Leafeon, Glaceon, Gliscor, Mamoswine, Porygon_z, Gallade, Probopass, Dusknoir, Froslass, Rotom, Uxie, Mesprit, Azelf, Dialga, Palkia, Heatran, Regigigas, Giratina, Cresselia, Phione, Manaphy, Darkrai, Shaymin, Arceus }, {Bisasam, Bisaknosp, Bisaflor, Glumanda, Glutexo, Glurak, Schiggy, Schillok, Turtok, Raupy, Safcon, Smettbo, Hornliu, Kokuna, Bibor, Taubsi, Tauboga, Tauboss, Rattfratz, Rattikarl, Habitak, Ibitak, Rettan, Arbok, Pikachu, Raichu, Sandan, Sandamer, Nidoran♀, Nidorina, Nidoqueen, Nidoran♂, Nidorino, Nidoking, Piepi, Pixi, Vulpix, Vulnona, Pummeluff, Knuddeluff, Zubat, Golbat, Myrapla, Duflor, Giflor, Paras, Parasek, Bluzuk, Omot, Digda, Digdri, Mauzi, Snobilikat, Enton, Entoron, Menki, Rasaff, Fukano, Arkani, Quapsel, Quaputzi, Quappo, Abra, Kadabra, Simsala, Machollo, Maschock, Machomei, Knofensa, Ultrigaria, Sarzenia, Tentacha, Tentoxa, Kleinstein, Georok, Geowaz, Ponita, Gallopa, Flegmon, Lahmus, Magnetilo, Magneton, Porenta, Dodu, Dodri, Jurob, Jugong, Sleima, Sleimok, Muschas, Austos, Nebulak, Alpollo, Gengar, Onix, Traumato, Hypno, Krabby, Kingler, Voltobal, Lektrobal, Owei, Kokowei, Tragosso, Knogga, Kicklee, Nockchan, Schlurp, Smogon, Smogmog, Rihorn, Rizeros, Chaneira, Tangela, Kangama, Seeper, Seemon, Goldini, Golking, Sterndu, Starmie, Pantimos, Sichlor, Rossana, Elektek, Magmar, Pinsir, Tauros, Karpador, Garados, Lapras, Ditto, Evoli, Aquana, Blitza, Flamara, Porygon, Amonitas, Amoroso, Kabuto, Kabutops, Aerodactyl, Relaxo, Arktos, Zapdos, Lavados, Dratini, Dragonir, Dragoran, Mewtu, Mew, Endivie, Lorblatt, Meganie, Feurigel, Igelavar, Tornupto, Karnimani, Tyracroc, Impergator, Wiesor, Wiesenior, Hoothoot, Noctuh, Ledyba, Ledian, Webarak, Ariados, Iksbat, Lampi, Lanturn, Pichu, Pii, Fluffeluff, Togepi, Togetic, Natu, Xatu, Voltilamm, Waaty, Ampharos, Blubella, Marill, Azumarill, Mogelbaum, Quaxo, Hoppspross, Hubelupf, Papungha, Griffel, Sonnkern, Sonnflora, Yanma, Felino, Morlord, Psiana, Nachtara, Kramurx, Laschoking, Traunfugil, Icognito, Woingenau, Girafarig, Tannza, Forstellka, Dummisel, Skorgla, Stahlos, Snubbull, Granbull, Baldorfish, Scherox, Pottrott, Skaraborn, Sniebel, Teddiursa, Ursaring, Schneckmag, Magcargo, Quiekel, Keifel, Corasonn, Remoraid, Octillery, Botogel, Mantax, Panzaeron, Hunduster, Hundemon, Seedraking, Phanpy, Donphan, Porygon2, Damhirplex, Farbeagle, Rabauz, Kapoera, Kussilla, Elekid, Magby, Miltank, Heiteira, Raikou, Entei, Suicune, Larvitar, Pupitar, Despotar, Lugia, Ho-oh, Celebi, Geckarbor, Reptain, Gewaldro, Flemmli, Jungglut, Lohgock, Hydropi, Moorabbel, Sumpex, Fiffyen, Magnayen, Zigzachs, Geradaks, Waumpel, Schaloko, Papinella, Panekon, Pudox, Loturzel, Lombrero, Kappalores, Samurzel, Blanas, Tengulist, Schwalbini, Schwalboss, Wingull, Pelipper, Trasla, Kirlia, Guardevoir, Gehweiher, Maskeregen, Knilz, Kapilz, Bummelz, Muntier, Letarking, Nincada, Ninjask, Ninjatom, Flurmel, Krakeelo, Krawumms, Makuhita, Hariyama, Azurill, Nasgnet, Eneco, Enekoro, Zobiris, Flunkifer, Stollunior, Stollrak, Stolloss, Meditie, Meditalis, Frizelbliz, Voltenso, Plusle, Minun, Volbeat, Illumise, Roselia, Schluppuck, Schlukwech, Kanivanha, Tohaido, Wailmer, Wailord, Camaub, Camerupt, Qurtel, Spoink, Groink, Pandir, Knacklion, Vibrava, Libelldra, Tuska, Noktuska, Wablu, Altaria, Sengo, Vipitis, Lunastein, Sonnfel, Schmerbe, Welsar, Krebscorps, Krebutack, Puppance, Lepumentas, Liliep, Wielie, Anorith, Armaldo, Barschwa, Milotic, Formeo, Kecleon, Shuppet, Banette, Zwirrlicht, Zwirrklop, Tropius, Palimpalim, Absol, Isso, Schneppke, Firnontor, Seemops, Seejong, Walraisa, Perlu, Aalabyss, Saganabyss, Relicanth, Liebiskus, Kindwurm, Draschel, Brutalanda, Tanhel, Metang, Metagross, Regirock, Regice, Registeel, Latias, Latios, Kyogre, Groudon, Rayquaza, Jirachi, Deoxys, Chelast, Chelcarain, Chelterrar, Panflam, Panpyro, Panferno, Plinfa, Pliprin, Impoleon, Staralili, Staravia, Staraptor, Bidiza, Bidifas, Zirpurze, Zirpeise, Sheinux, Luxio, Luxtra, Knospi, Roserade, Koknodon, Rameidon, Schilterus, Bollterus, Burmy, Burmadame, Moterpel, Wadribie, Honweisel, Pachirisu, Bamelin, Bojelin, Kikugi, Kinoso, Schalellos, Gastrodon, Ambidiffel, Driftlon, Drifzepeli, Haspiror, Schlapor, Traunmagil, Kramshef, Charmian, Shnurgarst, Klingplim, Skunkapuh, Skuntank, Bronzel, Bronzong, Mobai, Pantimimi, Wonneira, Plaudagei, Kryppuk, Kaumalat, Knarksel, Knakrack, Mampfaxo, Riolu, Lucario, Hippopotas, Hippoterus, Pionskora, Piondragi, Glibunkel, Toxiquak, Venuflibis, Finneon, Lumineon, Mantirps, Shnebedeck, Rexblisar, Snibunna, Magnezone, Schlurplek, Rihornior, Tangoloss, Elevoltek, Magbrant, Togekiss, Yanmega, Folipurba, Glaziola, Skorgro, Mamutel, Porygon-Z, Galagladi, Voluminas, Zwirrfinst, Frosdedje, Rotom, Selfe, Vesprit, Tobutz, Dialga, Palkia, Heatran, Regigigas, Giratina, Cresselia, Phione, Manaphy, Darkrai, Shaymin, Arceus}}
            string output;
            string outputwb;
            string msg;
            string hotspots = "\n *Top Notch XP Grinding Spots* \n \n 1: Copenhagen, Denmark / 55.662278, 12.56246 \n 2: German Mall, Germany / 51.48986, 6.87533 \n 3: Paris Mall, Paris, France / 48.890717, 2.23783 \n \n *Coordinates List* \n \n 1: Chicago Lake Front / 42.0411,-87.7102 ( 60 Pokestops  \n 2: New York / 40.75512,-73.9836 ( 7 Pokestops  \n 3: Japan / 35.6961,139.8144 ( 9 Pokestops  \n 4: Mall of Scandanavia, Sweden / 59.370392, 18.002844 ( 6 Pokestops  \n 5: Disneyland, Florida / 33.8119,-117.919 \n 6: Victory Memorial Park, Minneapolis / 45.0301,-93.3195 \n 7: Central Park, New York / 40.7803,-73.963 \n 8: Santa Monica, California / 34.0076,-118.499 \n 9: Liberty Park / 40.7442,-111.874 \n 10: Paris / 48.8615, 2.289 \n 11: Union Square / 37.7879,-122.407 \n 12: Sidney / -33.8610,151.212 \n 13: Austin Texas / 30.2742,-97.740 \n 14: Olympia / 47.0477,-122.9041 ( 4 Pokestops  \n 16: NgurahRai Airport / -8.7467,115.166 \n 17: Times square, New York / 40.7589,-73.985 \n 18: Big Ben, London / 51.5010,-0.124 \n 19: Tokyo Disneyland / 35.6312,139.880 \n 20: Waikiki Aquarium / 21.2658,-157.821 \n 21: Rigos Loop / 21.2983,-157.860 \n 22: Jimmys Loop, USA 32.7758,-96.796 \n 23: Toms Loop, Ann Arbor / 42.2808,-83.743 \n 24: San Francisco Pier, California / 39 37.8095,-122.410 \n 25: Alameda, Mexico / 19.4362,-99.144 \n 26: Sydney Botanical / -33.8643,151.215 \n 27: Bidadari, Indonesia / -6.0356,106.746 \n 28: Long Beach, California / 33.7700,-118.193 \n 29: Den Haag, Netherland / 52.0689,4.220 \n 30: Korea / 37.5113,127.0980 ( 11 Pokestops  \n 31: Germany Mall / 51.491104,6.88055 \n 32: Copenhagen, Denmark Mall / 55.662518, 12.56217 \n 33: (Park) National Mall, Washington DC / 38.888515, -77.024124"; ;
            string cooldown = cooldown = " \n 1km <1min \n 2km 1min \n 3km <2min \n 4.6km 2min \n 5km 2min \n 7km 5min \n 9km <7min \n 10km 7min \n 12km 8min \n 18km 10min \n 26km 15min \n 42km 19min \n 65km <22min \n 76km <25min \n 81km 25min \n 100km <35min \n 220km 40min \n 250km 45min \n 350km 51min \n 460km 58min \n 500km 60min \n 565km 67min \n 700km 75min \n 716km 78min \n 830km  <86min \n 1000km 90min \n 1500km+  2hr";

            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
                msg = e.Message.Text;
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
                
                else if (regex.IsMatch(e.Message.Text))
                {
                    string searchstring;
                    pokedex = Convert.ToInt32(msg);
                    baseAttack = baseStats[0, pokedex];
                    baseDefense = baseStats[1, pokedex];
                    baseStamina = baseStats[2, pokedex];
                    cp = Convert.ToInt16(Math.Floor(((((baseStats[0, pokedex] + 15) * cpm[40]) * Math.Sqrt((baseStats[1, pokedex] + 15) * cpm[40])) * Math.Sqrt((baseStats[2, pokedex] + 15) * cpm[40])) / 10));
                    output = Convert.ToString(cp);
                    cp = Convert.ToInt16(Math.Floor(((((baseStats[0, pokedex] + 15) * cpm[50]) * Math.Sqrt((baseStats[1, pokedex] + 15) * cpm[50])) * Math.Sqrt((baseStats[2, pokedex] + 15) * cpm[50])) / 10));
                    outputwb = Convert.ToString(cp);
                    await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: "Du hast nach" + pkmname[1, pokedex] "gefragt",
                      parseMode: ParseMode.Markdown
                    );
                    await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: "Die 100IV WP *ohne* Wetterboost in einem Raid ist:\n" + output + "\n Die 100IV WP *mit* Wetterboost in einem Raid ist:\n" + output,
                      parseMode: ParseMode.Markdown
                    );
                    searchstring = Convert.ToString(pokedex) + "&";
                    for (int i = 1; i < 81; i++)
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
                    await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: pkmname[1, pokedex] + "braucht" + baseStats[3, pokedex] + "Candys zum Entwickeln.",
                      parseMode: ParseMode.Markdown
                    );
                }
                else if(e.Message.Chat.Id == xxxxxxxxx && coords.IsMatch(e.Message.Text))
                    {
                    await botClient.SendTextMessageAsync(
                      chatId: xxxxxxxxx,
                      text: e.Message.Text,
                      parseMode: ParseMode.Markdown
                    );
                    await botClient.SendTextMessageAsync(
                      chatId: xxxxxxxxx,
                      text: e.Message.Text,
                      parseMode: ParseMode.Markdown
                    ); 
                }
                else
                 {
                   await botClient.SendTextMessageAsync(
                     chatId: e.Message.Chat,
                     text: "Befehle: \n */cd*: Cooldown Tabelle \n */rb*: Raidboss Info \n */rwd*: Raid Belohnungen \n */hs*: Spoof Hotspots \n */ivth* Perfekte IV für Tanhel \n Pokedex nummer gibt die 100% WP in einem Raid aus",
                     parseMode: ParseMode.Markdown
                   );
                    
                }
            }
        }
    }
}
