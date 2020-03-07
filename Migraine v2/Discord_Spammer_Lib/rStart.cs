using Discord;

using Discord.WebSocket;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Migraine_v2.SelfbotClasses
{
    internal class rStart
    {
        [Obsolete]
        public static void Init()
        {
            new Thread(delegate () {
                new rStart().Startthismuthafucka().GetAwaiter().GetResult();
            }).Start();
        }
        [Obsolete]
        public async Task Startthismuthafucka()
        {
            rStart._Client = new DiscordSocketClient();
            await rStart._Client.LoginAsync(TokenType.User, rStart.Token, true);
            await rStart._Client.StartAsync();
            rStart._Handler = new CommandHandler();
            await rStart._Handler.Install(rStart._Client);
            await Task.Delay(-1);
        }
        public static bool FirstTime = true;
        public static DiscordSocketClient _Client;
        public static CommandHandler _Handler;
        public static string Token;
    }
}
