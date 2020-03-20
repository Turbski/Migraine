using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Migraine_v2.API;
using Migraine_v2.CustomSettings;

namespace Migraine_v2.SelfbotClasses {
    public class CommandHandler {
        public CommandService _cmds;
        private DiscordSocketClient _client;
        public async Task Install(DiscordSocketClient c) {
            this._client = c;
            this._cmds = new CommandService();
            //Utils.Service = _cmds;
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
                    if (Configuration._Config.CustomCommands.TryGetValue(context.Message.Content.ToLower(), out string value))
                    {
                        await context.Message.DeleteAsync();
                        ConsoleLog.Log(string.Format("[Console] Executed Custom CMD: {0}", value));
                        await context.Channel.SendMessageAsync(value);
                    }
                    else
                    {
                        var result2 = await this._cmds.ExecuteAsync(context, argPos, null, MultiMatchHandling.Exception);
                        IResult result = result2;
                        result2 = null;
                        ConsoleLog.Log(string.Format("[Console] Executed CMD: {0}", context));
                        if (!result.IsSuccess) { ConsoleLog.Log(result.ToString()); }
                    }
                }
                
            }
        }
    }
}
