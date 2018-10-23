# Pokemon Go Info Telegram Bot

The bot is working!
Pokemon Go Telegram Bot which calculate the perfect wild IVs and the perfect RAID IVs for any Raid bosses.
Generation 4 is COMPLETLY added.

## Instructions:

1. Get a token from the Telegram Bot Father (t.me/u/BotFather)
2. place your token at the seperating XXX in the code
3. open cmd and change directory to the master.
4. run 'dotnet new console' in the master, if an error occurs, ignore it.
5. run 'dotnet add package Telegram.Bot'
6. run dotnet run to start the bot.

### Use moderation service

1. Place the User ID of the Moderation in the given space.
2. The Moderator can send now messages to all Users by texting the BOT, who have subscribed by using the folowing Syntax:
3. Send normal message, the end must be a valid coordinate (000.000000,000.000000)

## TODO
Calculate the distance between 2 geo locations.
Formular:
*distance = 6378.388 * Math.Acos(Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(lon2 - lon1));*
