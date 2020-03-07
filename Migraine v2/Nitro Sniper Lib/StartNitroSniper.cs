using Discord;

using Discord.WebSocket;
using Migraine_v2.SelfbotClasses;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Migraine_v2.Nitro_Sniper_Lib {
    internal class StartNitroSniper {
        [Obsolete]
        public static void Init() {
            new Thread(delegate () {
                new StartNitroSniper().Startthismuthafucka().GetAwaiter();
            }).Start();
        }

        [Obsolete]
        public async Task Startthismuthafucka() {
            _Client1 = new DiscordSocketClient();
            await _Client1.LoginAsync(TokenType.User, Token, true);
            await _Client1.StartAsync();
            _Client1.Ready += this._Client_Ready;
            _Client1.MessageReceived += _client_MessageReceived;
            if (Globals.NitroSniperRunning)
                await Task.Delay(-1);
        }

        private async Task _Client_Ready() {
            bool firstTime = StartBot.FirstTime;
            if (firstTime)
                ConsoleLog.Log(string.Format("[Console] Nitro Sniper Started\n[Console] Welcome {0}", _Client1.CurrentUser));
                FirstTime = false;
        }
        private static Task _client_MessageReceived(SocketMessage nitro) {
            if (nitro.Content.Contains("https://discord.gift/")) {
                string code = nitro.Content.Split(new[] { "https://discord.gift/" }, StringSplitOptions.None)[1].Split(' ')[0];

                var Cookie = new CookieContainer();
                try {
                    string header = "";
                    var req = (HttpWebRequest)WebRequest.Create($"https://discordapp.com/api/v6/entitlements/gift-codes/{code}/redeem");
                    req.Accept = "application/json, text/javascript, */*; q=0.01";
                    var post = header;
                    var data = Encoding.ASCII.GetBytes(post);
                    req.Headers["authorization"] = Token;
                    req.Method = "POST";
                    req.ContentType = "application/json";
                    req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.75 Safari/537.36";
                    req.ContentLength = data.Length;

                    using (var stream = req.GetRequestStream()) { stream.Write(data, 0, data.Length); }
                    var response = (HttpWebResponse)req.GetResponse();
                    string responsestring = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    if ((int)response.StatusCode == 200)
                        Globals.ValidNitro++;
                        ConsoleLog.Log("[Console] Successfully redeemed a code");
                }
                catch (WebException e) {
                    var shitreq = (HttpWebResponse)e.Response;
                    string respstring = new StreamReader(shitreq.GetResponseStream()).ReadToEnd();
                    if ((int)shitreq.StatusCode == 404) {
                        Globals.InvalidNitro++;
                        ConsoleLog.Log("[Console] Failed to redeem a code");
                    }
                    else if (((int)shitreq.StatusCode == 400) || ((int)shitreq.StatusCode == 429))
                        Globals.InvalidNitro++;
                    ConsoleLog.Log("[Console] Expired code");
                } catch {
                }
            }
            return Task.CompletedTask;
        }

        public static bool FirstTime = true;
        public static DiscordSocketClient _Client1;
        public static string Token;
    }
}
