using Discord;
using Discord.WebSocket;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Migraine_v2.SelfbotClasses {
    internal class StartBot {
        [Obsolete]
        public static void Init() =>  new Thread(delegate () {
                new StartBot().Startthismuthafucka().GetAwaiter().GetResult();
            }).Start();

        [Obsolete]
        public async Task Startthismuthafucka() {
            _Client = new DiscordSocketClient();
            await _Client.LoginAsync(TokenType.User, Token, true);
            await _Client.StartAsync();
            _Handler = new CommandHandler();
            await _Handler.Install(_Client);
            _Client.Ready += this.Client_Ready;
            if (Globals.SelfbotRunning)
                await Task.Delay(-1);
        }
        private async Task Client_Ready() {
            bool firstTime = FirstTime;
            if (firstTime) {
                ConsoleLog.Log(string.Format("[Console] Bot Started\n[Console] Welcome {0}\n[Console] Prefix: {1}", _Client.CurrentUser, Globals.Prefix));
                Globals.SelfbotUser = _Client.CurrentUser.ToString();
                
                FirstTime = false;
            }
        }
        public static bool FirstTime = true;
        public static DiscordSocketClient _Client;
        public static CommandHandler _Handler;
        public static string Token;
    }
}
