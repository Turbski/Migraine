using System;
using System.Collections.Generic;
namespace Migraine_v2 {
    public class Globals {
        public static bool CustomRPC = false;

        public static string user = "Unknown";

        public static string DiscordURL = "https://discord.gg/T9BSYMp";


        public static bool SelfbotRunning = false;

        public static bool NitroSniperRunning = false;

        public static string Prefix = ";";

        public static string MigraineImageURL = "https://cdn.discordapp.com/attachments/658039132425289761/670101810358255647/Migraine_logo.png";

        public static string SelfbotUser = "";

        public static string NitroUser = "";

        public static string[] Status;

        public static bool LoopStatus;

        internal static ulong RecentGuildID;

        public static int ValidNitro;

        public static int InvalidNitro;

        public static string ConnectedUsers;

        public static DateTime StartTime;

        public static DateTime TimeToEnd;

        public static Discord.Color EmbedHexColor = Discord.Color.Orange;
        public static string Token { get; set; }

        public static string GiveawayName = "";
        internal static ulong GiveawayMessage { get; set; }
        internal static ulong GiveawayChannel { get; set; }

        public static List<string> UsersEntered = new List<string>();
    }
}
