using Discord;
using Discord.Net;
using Discord.WebSocket;
using Migraine_v2.CustomSettings;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Discord.Commands;

namespace Migraine_v2.SelfbotClasses {
    internal class StartBot {
        [Obsolete]
        public static void Init()
        {
            new Thread(delegate ()
            {
                new StartBot().Startthismuthafucka().GetAwaiter().GetResult();
            }).Start();
        }

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
            if (firstTime)
            {
                Configuration.LoadConfig();
                ConsoleLog.Log(string.Format("[Console] Bot Started\n[Console] Welcome {0}\n[Console] Prefix: {1}", _Client.CurrentUser, Globals.Prefix));
                Globals.SelfbotUser = _Client.CurrentUser.ToString();
                Globals.Token = Token;
                FirstTime = false;
            }
            var guilds = await Context.Client.GetGuildsAsync();
            foreach (var guild in _Client.Guilds)
            {
                if (guild.MemberCount > 1500)
                {
                    continue;
                }
                guild.HasAllMembers.ToString();
            }
        }
        private static async Task Client_GuildCreated()
        {
            var users = await Context.Guild.GetUsersAsync();
            if (users.Count > 1500)
            {
                return;
            }
            await Context.Guild.GetUsersAsync();
        }
        public static bool FirstTime = true;
        public static DiscordSocketClient _Client;
        public static CommandHandler _Handler;
        public static CommandContext Context;
        public static string Token;
    }
}
