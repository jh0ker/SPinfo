# Pokemon Go Info Telegram Bot

The bot is working!
Pokemon Go Telegram Bot which calculate the perfect wild IVs and the perfect RAID IVs for any Raid bosses.
Generation 4 is COMPLETLY added.

**This bot requires dotnetCore (min. version 3.1).**

## Instructions:

1. Get a token from the Telegram Bot Father (t.me/u/BotFather)
2. Place your token as the value for `TOKEN` in the code.
3. Open a command line/powershell in the main directory.
4. Run `dotnet restore`
5. Run `dotnet run` to start the bot.

### Use moderation service

1. Place your username (without @) as the value for `MODERATOR_USERNAME` in the code.
2. The Moderator can send now messages to all Users by texting the BOT, who have subscribed by using the folowing Syntax:
3. Send normal message, the end must be a valid coordinate `(000.000000,000.000000)`

## TODO

Calculate the distance between 2 geo locations.

Formula:

`distance = 6378.388 \* Math.Acos(Math.Sin(lat1) \* Math.Sin(lat2) + Math.Cos(lat1) \* Math.Cos(lat2) \* Math.Cos(lon2 - lon1));`
