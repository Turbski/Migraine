using Discord;
using Discord.Net;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Migraine_v2.SelfbotClasses {
    public class DiscordUpdateFromGui {
        //Status Timer
        private static Timer StatusTimer;
        public static bool TimerRunning;
        public static int j = 0;
        public static int y = 0;
        internal static Task StartTimer() {
            TimerRunning = true;
            StatusTimer = new Timer() {
                Interval = 5000,
                AutoReset = true,
                Enabled = true
            };
            StatusTimer.Elapsed += StatusTimer_Elapsed;
            return Task.CompletedTask;
        }

        private static async void StatusTimer_Elapsed(object sender, ElapsedEventArgs e) {
            if (Globals.LoopStatus) {
                if (j >= Globals.Status.Length)
                    j = 0;
                try {
                    string Type = Globals.Status[j].Split(' ')[0].ToLower();
                    string status = Globals.Status[j].Replace(Type, "");
                }
                catch { }
                j++;
            }
            else {
                StatusTimer.Stop();
                TimerRunning = false;
            }
        }
        public static async void UpdateStatus(string status, string Type) {
            try {
                if (Type == "playing")
                    await StartBot._Client.SetGameAsync(status, null, ActivityType.Playing);
                else if (Type == "listening")
                    await StartBot._Client.SetGameAsync(status, null, ActivityType.Listening);
                else if (Type == "watching")
                    await StartBot._Client.SetGameAsync(status, null, ActivityType.Watching);
                else if (Type == "streaming")
                    await StartBot._Client.SetGameAsync(status, "https://www.twitch.tv/tfue", ActivityType.Streaming);
                else
                    await StartBot._Client.SetGameAsync(status, null, ActivityType.Watching);
            } catch { 
            } return;
        }
        public static async void UpdateActivity(UserStatus i) => await StartBot._Client.SetStatusAsync(i);
    }
}
