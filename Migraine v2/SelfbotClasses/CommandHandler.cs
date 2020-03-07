using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Migraine_v2.SelfbotClasses {
    public class CommandHandler {
        public CommandService _cmds;
        private DiscordSocketClient _client;
        public async Task Install(DiscordSocketClient c) {
            this._client = c;
            this._cmds = new CommandService();
            await this._cmds.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            this._client.MessageReceived += this.HandleCommand;
        }
        public async Task HandleCommand(SocketMessage s) {
            bool notrunning = !Globals.SelfbotRunning;
            if (notrunning) { Thread.CurrentThread.Abort(); }
            SocketUserMessage msg = s as SocketUserMessage;
            bool msgnull = msg == null;
            if (!msgnull) {
                SocketCommandContext context = new SocketCommandContext(this._client, msg);
                int argPos = 0;
                string prefix = Globals.Prefix;
                Globals.RecentGuildID = context.Channel.Id;
                bool haspref = msg.HasStringPrefix(prefix, ref argPos, StringComparison.Ordinal) && msg.Author.Id == this._client.CurrentUser.Id;
                if (haspref) {
                    var result2 = await this._cmds.ExecuteAsync(context, argPos, null, MultiMatchHandling.Exception);
                    IResult result = result2;
                    result2 = null;
                    ConsoleLog.Log(string.Format("[Console] Executed CMD: {0}", context));
                    ConsoleLog.Log(string.Format("[Error] {0}", result2));
                    if (!result.IsSuccess) { ConsoleLog.Log(result.ToString());}
                    result = null;
                }
                
            }
        }
    }
}
