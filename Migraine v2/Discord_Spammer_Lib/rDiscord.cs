﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace Migraine_v2.Discord_Spammer_Lib
{
    public class EmbedProperties
    {
        private string type = "rich";

        private string description { get; set; }

        private string color { get; set; }

        private string title { get; set; }

        private EmbedImageProperty image { get; set; }

        public EmbedProperties(string title, string description, string color = "5880085", string url = "https://cdn.shibe.online/shibes/9807584ea07f2719e55f53a6e2e7581f2ffa05da.jpg")
        {
            this.title = title;
            this.color = color;
            this.description = description;
            this.image = new EmbedImageProperty(url);
        }
    }

    public class EmbedImageProperty
    {
        private string url { get; set; }

        public EmbedImageProperty(string url)
        {
            this.url = url;
        }
    }
    public static class rDiscord
    {
        public static string IfValidAccount(HttpClient Client)
        {
            string result;
            try
            {
                bool invalid = Client.GetAsync("https://discordapp.com/activity").Result.StatusCode == HttpStatusCode.Forbidden;
                if (invalid)
                    result = "invalid";
                else
                {
                    HttpStatusCode statusCode = rDiscord.JoinServer(Client, "BAWBAWBAW").StatusCode;
                    bool invalid1 = statusCode == HttpStatusCode.Forbidden || statusCode == HttpStatusCode.Unauthorized;
                    if (invalid1)
                        result = "invalid";
                    else
                    {
                        bool ratelimit = statusCode.ToString() == "429";
                        if (ratelimit)
                            result = "throttled";
                        else
                            result = "valid";
                    }
                }
            }
            catch { result = "503"; }
            return result;
        }

        public static bool IsInServer(HttpClient Client, string GuildID)
        {
            HttpResponseMessage result = Client.GetAsync("https://discordapp.com/api/v6/guilds/" + GuildID).Result;
            return result.StatusCode != HttpStatusCode.Forbidden;
        }

        public static bool IsValidInvite(HttpClient Client, string Invite)
        {
            string result = Client.GetStringAsync("https://discordapp.com/api/invites/" + Invite).Result;
            return !result.ToLower().Contains("this invite may be expired");
        }

        public static int RandomMessage(HttpClient Client, string channelID)
        {
            var msg = new Random();
            var randommessage = new string[]
            {
                "I am a traveler",
                "I do NOT work in an office",
                "Good question - I am still trying to figure that out!",
                "I know what I want - Confidence",
                "I am always helping out - Kindness",
                "I am a bit of a bright spark - Intelligence",
                "I am usually the one to help them fix things",
                "I like to make my friends laugh"
            };
            var req = new HttpRequestMessage
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri("https://discordapp.com/api/v6/channels/" + channelID + "/messages"),
                Content = new StringContent("{\"content\":\"" + $"{ randommessage[msg.Next(0, randommessage.Length)] }" + "\",\"nonce\":\"\",\"tts\":\"false\"}", Encoding.UTF8, "application/json")
            };
            Client.SendAsync(req);
            int result = 0;
            return result;
        }
        public static bool SpotifyInviteSpam(HttpClient Client)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri("https://discordapp.com/api/v6/hypesquad/online"),
                Content = new StringContent("{\"house_id\":1}", Encoding.UTF8, "application/json")
            };
            Task<HttpResponseMessage> task = Client.SendAsync(request);
            return task.Result.StatusCode == HttpStatusCode.OK;
        }
        public static bool SpawnEmbed(HttpClient client, ulong channelID, string title, string text, string color = "5880085", string img = null)
        {
            var Color = color == "5880085" ? color : ParseColor(color);

            var response = client.PostAsync($"https://discordapp.com/api/channels/{channelID}/messages", new StringContent(JsonConvert.SerializeObject(new EmbedProperties(title, text, Color, img)), Encoding.UTF8, "application/json"));

            return response.Result.StatusCode == HttpStatusCode.OK;
        }
        public static string ParseColor(string color)
        {
            switch (color.ToLower())
            {
                default:
                    return "1266338";
                case "blue":
                    return "1266338";
                case "red":
                    return "13632027";
                case "green":
                    return "5880085";
                case "yellow":
                    return "16312092";
                case "cyan":
                    return "5301186";
                case "white":
                    return "16777215";
                case "black":
                    return "1";
            }
        }
        public static string[] GetMembers(HttpClient Client, string ServerID)
        {
            string get = Client.GetStringAsync("https://discordapp.com/api/v6/guilds/" + ServerID + "/members?limit=1000").Result;
            List<string> list = new List<string>();
            while (get.Contains("\"id\": \""))
            {
                string get2 = get.Substring(get.IndexOf("\"id\": \"") + 7);
                string[] array = get2.Split(new char[] {
                    '"'
                });
                list.Add(array[0]);
                get = string.Join("\"", array);
            }
            return list.ToArray();
        }

        public static dynamic GetCurrentUser(HttpClient Client) => JsonConvert.DeserializeObject<object>(Client.GetStringAsync("https://discordapp.com/api/v6/users/@me").Result);
        public static HttpResponseMessage JoinServer(HttpClient Client, string Invite) => Client.PostAsync("https://discordapp.com/api/v6/invites/" + Invite, new StringContent("", Encoding.UTF8, "application/json")).Result;
        public static async void LeaveServer(HttpClient Client, string ServerID)
        {
            try
            {
                var req = new HttpRequestMessage
                {
                    Method = new HttpMethod("DELETE"),
                    RequestUri = new Uri($"https://discordapp.com/api/v6/users/@me/guilds/{ServerID}")
                };
                await Client.SendAsync(req);
                req = null;
                Thread.Sleep(-1);

            }
            catch { }
        }
        public static async void AddFriend(HttpClient Client, string User)
        {
            try
            {
                var req = new HttpRequestMessage
                {
                    Method = new HttpMethod("PUT"),
                    RequestUri = new Uri("https://discordapp.com/api/v6/users/@me/relationships/" + User),
                    Content = new StringContent("{}", Encoding.UTF8, "application/json")
                };
                await Client.SendAsync(req);
                req = null;
            } catch
            {
            }
        }
        public static async void AuditLogSpam(HttpClient Client, string ServerID)
        {
            try
            {
                HttpRequestMessage req = new HttpRequestMessage
                {
                    Method = new HttpMethod("PUT"),
                    RequestUri = new Uri("https://discordapp.com/api/v6/channels/" + ServerID + "/invites"),
                    Content = new StringContent("{\"max_age\":\"0\",\"max_uses\":\"0\",\"temporary\":false}", Encoding.UTF8, "application/json")
                };
                await Client.SendAsync(req);
                req = null;
            }
            catch
            {
            }
        }
        public static async void ChangeAV(HttpClient Client, List<byte> image)
        {
            using (var img = System.Drawing.Image.FromFile(image.ToString()))
            {
                using (MemoryStream mem = new MemoryStream())
                {
                    img.Save(mem, img.RawFormat);
                    byte[] convert = mem.ToArray();

                    string fattwin = Convert.ToBase64String(convert);
                }
            }
            try
            {
                HttpRequestMessage req = new HttpRequestMessage
                {
                    Method = new HttpMethod("PATCH"),
                    RequestUri = new Uri("https://discordapp.com/api/v6/users/@me"),
                    Content = new StringContent("{\"avatar\":\"data:image/png;base64," + image + "\"}")
                };
                await Client.SendAsync(req);
                req = null;
            }
            catch {
            }
        }

        public static async void JoinVC(HttpClient Client, string voiceChannelID, string guildID)
        {
            try
            {
                HttpRequestMessage req = new HttpRequestMessage
                {
                    Method = new HttpMethod("POST"),
                    RequestUri = new Uri("https://discordapp.com/api/v6/science"),
                    Content = new StringContent("{\"events\":[{\"type\":\"join_voice_channel\",\"properties\":{\"client_track_timestamp\":1580834270995,\"channel_id\":\"" + voiceChannelID + "\",\"channel_type\":2,\"guild_id\":\"" + guildID + "\",\"rtc_connection_id\":\"e7f43d0a-64ee-4649-b3ec-464c08a906d2\",\"game_name\":\"\",\"game_platform\":null,\"custom_status_count\":0,\"voice_state_count\":0,\"video_stream_count\":0,\"video_enabled\":false,\"client_performance_cpu\":27.387610881623594,\"client_performance_memory\":227128,\"accessibility_support_enabled\":false,\"client_uuid\":\"SAACpIIR6AjjGHmVrhsQEXABAAALAAAA\",\"client_send_timestamp\":1580834271024}}]}", Encoding.UTF8, "application/json")
                };
                await Client.SendAsync(req);
                req = null;
            } catch {
            }
        }

        public static async void Status(CommandContext Context, string dis) => await (Context.Client as DiscordSocketClient).SetGameAsync(dis, "https://twitch.tv/ninja", ActivityType.Streaming);

        public static async void RemoveFriend(HttpClient Client, string User)
        {
            try
            {
                HttpRequestMessage req = new HttpRequestMessage
                {
                    Method = new HttpMethod("DELETE"),
                    RequestUri = new Uri("https://discordapp.com/api/v6/users/@me/relationships/" + User),
                    Content = new StringContent("{}", Encoding.UTF8, "application/json")
                };
                await Client.SendAsync(req);
                req = null;
            }
            catch { }
        }

        public static async void SetName(HttpClient client, string serverID, string nick)
        {
            HttpRequestMessage req = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri("https://discordapp.com/api/v6/guilds/" + serverID + "/members/@me/nick"),
                Content = new StringContent("{\"nick\":\"" + nick + "\"}", Encoding.UTF8, "application/json")
            };
            await client.SendAsync(req);
        }
        public static async void ResetNick(HttpClient client, string serverID)
        {
            HttpRequestMessage req = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri("https://discordapp.com/api/v6/guilds/" + serverID + "/members/@me/nick"),
                Content = new StringContent("{\"nick\":\"" + "" + "\"}", Encoding.UTF8, "application/json")
            };
            await client.SendAsync(req);
        }

        public static async void SetStatusTokenIdle(HttpClient client)
        {
            HttpRequestMessage req = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri("https://discordapp.com/api/v6/users/@me/settings"),
                Content = new StringContent("{\"status\":\"" + "idle" + "\"}", Encoding.UTF8, "application/json")
            };
            await client.SendAsync(req);
        }
        public static async void SetStatusTokenOnline(HttpClient client)
        {
            HttpRequestMessage req = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri("https://discordapp.com/api/v6/users/@me/settings"),
                Content = new StringContent("{\"status\":\"online\"}", Encoding.UTF8, "application/json")
            };
            await client.SendAsync(req);
        }
        public static async void SetStatusTokendnd(HttpClient client)
        {
            var req = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri("https://discordapp.com/api/v6/users/@me/settings"),
                Content = new StringContent("{\"status\":\"" + "dnd" + "\"}", Encoding.UTF8, "application/json")
            };
            await client.SendAsync(req);
        }
        public static async void SetStatusTokenOffline(HttpClient client)
        {
            var req = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri("https://discordapp.com/api/v6/users/@me/settings"),
                Content = new StringContent("{\"status\":\"" + "invisible" + "\"}", Encoding.UTF8, "application/json")
            };
            await client.SendAsync(req);
        }
        public static bool SetStatus(HttpClient client, string Status)
        {
            var request = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri("https://discordapp.com/api/v6/users/@me/settings"),
                Content = new StringContent("{\"status\":\"online\"}", Encoding.UTF8, "application/json")
            };
            Task<HttpResponseMessage> task = client.SendAsync(request);
            return task.Result.StatusCode == HttpStatusCode.OK;
        }

        public static bool Typing(HttpClient client, string ChannelID)
        {
            HttpRequestMessage req = new HttpRequestMessage
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri("https://discordapp.com/api/v6/channels/" + ChannelID + "/typing")
            };
            Task<HttpResponseMessage> task = client.SendAsync(req);
            return task.Result.StatusCode == HttpStatusCode.OK;
        }
        public static bool React(HttpClient client, string ChannelID, string MessageID, string emoji)
        {
            HttpRequestMessage req = new HttpRequestMessage
            {
                Method = new HttpMethod("PUT"),
                RequestUri = new Uri("https://discordapp.com/api/v6/channels/" + ChannelID + "/messages/" + MessageID + "/reactions/" + emoji + "/%40me")
            };
            Task<HttpResponseMessage> task = client.SendAsync(req);
            return task.Result.StatusCode == HttpStatusCode.OK;
        }

        public static bool CustomPUTInviteFucker(HttpClient Client, string Invite)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = new HttpMethod("PUT"),
                RequestUri = new Uri("https://discordapp.com/api/v6/invite/" + Invite),
            };
            Task<HttpResponseMessage> task = Client.SendAsync(request);
            return task.Result.StatusCode == HttpStatusCode.OK;
        }
        public static async void AuditSpammer(HttpClient client, string ChannelID)
        {
            HttpRequestMessage req = new HttpRequestMessage
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri("https://discordapp.com/api/v6/channels/" + ChannelID + "/invites"),
                Content = new StringContent("{\"max_age\":1800,\"max_uses\":1,\"temporary\":true}", Encoding.UTF8, "application/json")
            };
            await client.SendAsync(req);
        }
        public static int SendChannelMessage(HttpClient Client, string ChannelID, string Message)
        {
            HttpResponseMessage post = Client.PostAsync("https://discordapp.com/api/v6/channels/" + ChannelID + "/messages", new StringContent("{\"content\":\"" + Message + "\",\"nonce\":\"\",\"tts\":\"false\"}", Encoding.UTF8, "application/json")).Result; bool done = post == null;
            int result2;
            if (done)
                result2 = 0;
            else
            {
                string a = post.StatusCode.ToString();
                result2 = ((a == "429") ? 0 : 1);
            }
            return result2;
        }
        public static int SendMultiChannelMessage(HttpClient Client, string ChannelID, string ChannelID2, string Message)
        {
            HttpResponseMessage result = Client.PostAsync("https://discordapp.com/api/v6/channels/" + ChannelID + "/messages", new StringContent("{\"content\":\"" + Message + "\",\"nonce\":\"\",\"tts\":\"false\"}", Encoding.UTF8, "application/json")).Result; bool done = result == null;
            int result2;
            if (done)
                result2 = 0;
            else
            {
                string a = result.StatusCode.ToString();
                result2 = ((a == "429") ? 0 : 1);
            }
            HttpResponseMessage result1 = Client.PostAsync("https://discordapp.com/api/v6/channels/" + ChannelID2 + "/messages", new StringContent("{\"content\":\"" + Message + "\",\"nonce\":\"\",\"tts\":\"false\"}", Encoding.UTF8, "application/json")).Result; bool done1 = result1 == null;
            int result22;
            if (done) { result22 = 0; }
            else
            {
                string a = result.StatusCode.ToString();
                result22 = ((a == "429") ? 0 : 1);
            }
            return result22;
        }
        public static int SendMassPingMessage(HttpClient Client, string ChannelID, string users, string Message)
        {
            HttpResponseMessage result = Client.PostAsync("https://discordapp.com/api/v6/channels/" + ChannelID + "/messages", new StringContent("{\"content\":\"" + users + " | " + Message + "\",\"nonce\":\"\",\"tts\":\"false\"}", Encoding.UTF8, "application/json")).Result; bool flag = result == null;
            int result2;
            if (flag)
                result2 = 0;
            else
            {
                string a = result.StatusCode.ToString();
                result2 = ((a == "429") ? 0 : 1);
            }
            return result2;
        }
        //public static int SendDmMessage(HttpClient Client, string ChannelID, string Message)
        //{
        //    HttpResponseMessage result = Client.PostAsync("https://discordapp.com/api/v6/channels/" + ChannelID + "/messages", new StringContent("{\"content\":\"" + Message + "\",\"nonce\":\"\",\"tts\":\"false\"}", Encoding.UTF8, "application/json")).Result; bool flag = result == null;
        //    int result2;
        //    if (flag)
        //        result2 = 0;
        //    else
        //    {
        //        string a = result.StatusCode.ToString();
        //        result2 = ((a == "429") ? 0 : 1);
        //    }
        //    return result2;
        //}
        //public static async bool DMSPam(HttpClient Client, string userid)
        //{
        //    HttpRequestMessage req = new HttpRequestMessage
        //    {
        //        //id: 680225977917374504
        //        Method = new HttpMethod("POST"),
        //        RequestUri = new Uri("https://discordapp.com/api/v6/users/" + userid + "/channels"),
        //        Content = new StringContent("{\"recipients\":""460892852889845780""\"}"), Encoding.UTF8, "application/json")
        //    };
    }
}