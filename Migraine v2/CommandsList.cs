using System.Collections.Generic;
namespace Migraine_v2 {
    public class CommandsList {
        public static string[] Get() {
            var Commands = new List<string>() {
                "stats:View bot stats",
                "ping:Check client latency",
                "spam:Spam Channel (spam Hi <amount>)",
                "embed:Embed a Message <message>",
                "poll:Create a Poll <message>",
                "quote:Quote a Message (quote <messageid>)",
                "cquote:Copy quote from different channel(quote <messageid>)",
                "ban:Ban a User <user>",
                "unban:Unban a User <user>",
                "kick:Kick a User <user>",
                "userinfo:Get info of User <user>",
                "purge:Purge <int> Messages",
                "massdm:Dm all Members (Risky On Main Account)",
                "gp:Makes the person your pinging clueless",
                "clearc:Clears chat with invisible characters",
                "ascii:Changes your text to ascii format <message>",
                "bomb:Bombs chat",
                "gay:Gay Calculator <user>",
                "spfp:Returns Server picture",
                "sstats:Returns Server statistics",
                "playing:Changes your status to playing with your message <message>",
                "streaming:Changes your status to streaming with your message <message>",
                "tcolor:Changes your text to color <color(red, cyan, green, orange)> <message>",
                "av:Returns mentioned users avatar <user>",
                "donate:Donate to the developer if you feel like supporting this tool.",
                "pay btc:Returns your bitcoin address",
                "dog:Returns a dog picture",
                "8ball:8balled nigga <question>",
                "coinflip:Flips coins",
                "hentai:Returns a Hentai Image",
                "hug:Gives selected person a hug <user>",
                "invis:Makes your name and profile picture blank",
                "migraine:Edits your message to show how good migraine is",
                "bye:Hits off like a dream <ip> <port> <time> <= 3600 max time per concurrent",
                "btcprice:Shows Bitcoin price",
                "oops:Pings all members in server",
                "lenny:Return a sexy lenny",
                "hack:hacks a person <user>",
                "cat:Returns a Cat Picture",
                "911:911 Attack",
                "ball:Bans all users in Guild",
                "dchan:Deletes all channels in Guild",
                "cchan:Creates over 10+ channels in guild <name of chan>",
                "loopinsult:Send a random insult every 15 seconds."
            }; return Commands.ToArray();
        }
    }
}
