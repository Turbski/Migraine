using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Figgle;
namespace Migraine_v2.Discord_Spammer_Lib {
    public class rCommands {
        public async Task embed(CommandContext Context, string message) {
            await Context.Message.DeleteAsync();
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("");
            embed.WithDescription(message);
            embed.WithColor(Globals.EmbedHexColor);
            await Context.Channel.SendMessageAsync("", false, embed.Build()); }
        //public static void ASCII(string message) { await context.Channel.SendMessageAsync("```" + FiggleFonts.Standard.Render(message) + "```"); }
        public async Task Streaming(CommandContext Context, string message) => await (Context.Client as DiscordSocketClient).SetGameAsync(message, "https://twitch.tv/ninja", ActivityType.Streaming);
        public async Task Playing(CommandContext Context, string message) => await (Context.Client as DiscordSocketClient).SetGameAsync(message, null, ActivityType.Playing);
        public async Task Watching(CommandContext Context, string message) => await (Context.Client as DiscordSocketClient).SetGameAsync(message, null, ActivityType.Watching);
        public async Task Listening(CommandContext Context, string message) => await (Context.Client as DiscordSocketClient).SetGameAsync(message, null, ActivityType.Listening);
    }
}
