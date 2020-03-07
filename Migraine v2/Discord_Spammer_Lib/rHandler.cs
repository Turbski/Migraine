using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Migraine_v2.Discord_Spammer_Lib {
    public class rHandler {
        public CommandService _cmds;
        private DiscordSocketClient _client;
        public async Task Install(DiscordSocketClient c) {
            this._client = c;
            this._cmds = new CommandService();
            await this._cmds.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            this._client.MessageReceived += this.HandleCommand;
        }

        public async Task HandleCommand(SocketMessage s) {
            //bool flag = !Globals.SelfbotRunning;
            //if (flag) { Thread.CurrentThread.Abort(); }
            SocketUserMessage msg = s as SocketUserMessage;
            bool nonemess = msg == null;
            if (!nonemess) {
                SocketCommandContext context = new SocketCommandContext(this._client, msg);
                int argPos = 0;
                //string prefix = Globals.Prefix;
                Globals.RecentGuildID = context.Channel.Id;
                bool prefox = msg.HasStringPrefix(null, ref argPos, StringComparison.Ordinal) && msg.Author.Id == this._client.CurrentUser.Id;
                if (prefox) {
                    IResult result2 = await this._cmds.ExecuteAsync(context, argPos, null, MultiMatchHandling.Exception);
                    IResult result = result2;
                    result2 = null;
                }
            }
        }
    }
}
