using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Migraine_v2.Discord_Spammer_Lib;

namespace Migraine_v2.Discord_Spammer_Lib
{
    class rCommands : ModuleBase
    {
        public async Task Streaming([Remainder] string message) => await (Context.Client as DiscordSocketClient).SetGameAsync(message, "https://twitch.tv/ninja", ActivityType.Streaming);

        public async Task Playing([Remainder] string message)
        {
            await (Context.Client as DiscordShardedClient).SetGameAsync(message, null, ActivityType.Playing);
        }
    }
}
