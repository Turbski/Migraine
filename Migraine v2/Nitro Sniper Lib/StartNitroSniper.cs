using Discord;

using Discord.WebSocket;
using Migraine_v2.SelfbotClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Migraine_v2.Nitro_Sniper_Lib {
    internal class StartNitroSniper {

        public static List<string> AttemptedCodes = new List<string>();

        public static void Init() {
            new Thread(delegate () {
                new StartNitroSniper().Startthismuthafucka().GetAwaiter();
            }).Start();
        }
        public async Task Startthismuthafucka() {
            _Client = new DiscordSocketClient();
            await _Client.LoginAsync((TokenType)0, Token, true);
            await _Client.StartAsync();
            _Client.Ready += this._Client_Ready;
            _Client.MessageReceived += _client_MessageReceived;
            if (Globals.NitroSniperRunning)
                await Task.Delay(-1);
        }

        private async Task _Client_Ready() {
            if (FirstTime)
                ConsoleLog.Log(string.Format("[Console] Nitro Sniper Started\n[Console] Welcome {0}", _Client.CurrentUser));
                FirstTime = false;
        }
        private static Task _client_MessageReceived(SocketMessage nitro) {
            if (nitro.Content.Contains("https://discord.gift/")) {
                string code = nitro.Content.Split(new[] { "https://discord.gift/" }, StringSplitOptions.None)[1].Split(' ')[0];
                if (!AttemptedCodes.Contains(code))
                {
                    try
                    {
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Add("Authorization", Token);

                        var response = client.PostAsync($"https://discordapp.com/api/v6/entitlements/gift-codes/{code}/redeem", new StringContent("{\"channel_id\":null,\"payment_source_id\":null}", Encoding.UTF8, "application/json"));

                        if ((int)response.Result.StatusCode == 200)
                        {
                            Globals.ValidNitro++;
                            ConsoleLog.Log("[Console] Successfully redeemed a code");
                        }
                        else
                        {
                            Globals.InvalidNitro++;
                            ConsoleLog.Log("[Console] Invalid Nitro Code.");
                        }

                        AttemptedCodes.Add(code);
                    }
                    catch (Exception e)
                    {
                        ConsoleLog.Log("Error: " + e.Message.ToString());
                    }
                }
                else
                {
                    ConsoleLog.Log("[Console] Attempted Code, ignored.");
                }
                
            }
            return Task.CompletedTask;
        }
        public static bool FirstTime = true;
        public static DiscordSocketClient _Client;
        public static string Token;
    }
}
