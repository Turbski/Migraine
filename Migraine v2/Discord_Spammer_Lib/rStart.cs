using Discord;
using Discord.Commands;
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
            _Client = new DiscordSocketClient();
            await _Client.LoginAsync(TokenType.User, Token, false);
            await _Client.StartAsync();
            _Handler = new CommandHandler();
            _Client.Ready += Client_Ready;
            await _Handler.Install(_Client);
            await Task.Delay(-1);
        }
        private async Task Client_Ready()
        {
            bool firstTime = FirstTime;
            if (firstTime)
            {
                Globals.ConnectedUsers = _Client.CurrentUser.ToString();
                ConsoleLog.Log(string.Format("[Console] Raid Client Started"));
                FirstTime = false;
            }
        }

        public static bool FirstTime = true;
        public static DiscordSocketClient _Client;
        public static CommandHandler _Handler;
        public static string Token;
    }
}
