using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Figgle;
using Migraine_v2.API;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using Migraine_v2.CustomSettings;
using System.Text;
using WebSocketSharp;

namespace Migraine_v2.SelfbotClasses {
    public class Commands : ModuleBase
    {
        //====================work in progress=======================/
        [Command("dchan")]
        public async Task DeleteChannels()
        {
            await Context.Message.DeleteAsync();
            IReadOnlyCollection<IGuildChannel> get = await Context.Guild.GetChannelsAsync(CacheMode.AllowDownload, null);
            foreach (var channel in get)
                await channel.DeleteAsync();
        }
        [Command("goodbye")]
        public async Task Goodbye()
        {
            await Context.Message.DeleteAsync();
            IReadOnlyCollection<IGuildChannel> get = await Context.Guild.GetChannelsAsync(CacheMode.AllowDownload, null);
            foreach (var channel in get)
                await channel.DeleteAsync();
            var Emojis = Context.Guild.Emotes;
            foreach (var emoji in Emojis)
            {
                await Context.Guild.DeleteEmoteAsync(emoji);
            }
            var roles = Context.Guild.Roles;
            foreach (var role in roles)
            {
                await role.DeleteAsync();
            }
        }
        [Command("ccmds")]
        public async Task CustomCmds()
        {
            await Context.Message.DeleteAsync();

            EmbedBuilder build = new EmbedBuilder();
            build.WithTitle("Custom Commands");
            build.WithDescription("Here's a list of all your custom, user defined, commands");
            build.WithColor(Color.Orange);

            if (Configuration._Config.CustomCommands.Count > 0)
            {
                foreach (var key in Configuration._Config.CustomCommands.Keys)
                {
                    if (Configuration._Config.CustomCommands.TryGetValue(key, out string value))
                    {
                        build.WithFields().AddField(key, value, true);
                    }
                }
            }
            else
            {
                build.WithDescription($"You have no custom commands! You can use the command {Globals.Prefix}ccmd <cmdname> <response> to create one however.");
            }

            await ReplyAsync("", false, build.Build());
        }
        [Command("invisboi")]
        public async Task InvisBoi()
        {
            var success = 0;
            await Context.Message.DeleteAsync();

            for (int i = 0; i < 26; i++)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", Globals.Token);

                var response = await client.PutAsync("https://canary.discordapp.com/api/v6/users/@me/connections/skype/" + i, new StringContent("{\"friend_sync\":false}", Encoding.UTF8, "application/json"));

                if (response.StatusCode == (HttpStatusCode)200)
                {
                    //https://canary.discordapp.com/api/v6/users/@me/connections/skype
                    response = await client.PatchAsync($"https://canary.discordapp.com/api/v6/users/@me/connections/skype/{i}", new StringContent("{\"visibility\":1}", Encoding.UTF8, "application/json"));

                    if (response.StatusCode == (HttpStatusCode)200)
                    {
                        success++;
                    }
                }
            }

            if (success >= 25)
            {
                ConsoleLog.Log("[Console] Successfully created invisible connections.");
            }
        }
        [Command("ccmd")]
        public async Task CreateCustomCommand(string name, [Remainder] string response)
        {
            await Context.Message.DeleteAsync();

            EmbedBuilder build = new EmbedBuilder();
            build.WithTitle("Custom Commands");
            build.WithDescription("Successfully created your custom command.");
            build.WithColor(Color.Green);
            build.WithAuthor("Success!", "https://i.dlpng.com/static/png/6705921_preview.png");
            Configuration._Config.CustomCommands.Add(Globals.Prefix + name.ToLower(), response);
            Configuration.SaveConfig();

            await ReplyAsync("", false, build.Build());
        }
        [Command("dcmd")]
        public async Task DeleteCustomCommand(string name)
        {
            await Context.Message.DeleteAsync();

            EmbedBuilder build = new EmbedBuilder();
            build.WithTitle("Custom Commands");
            build.WithDescription("Successfully deleted your custom command.");
            build.WithColor(Color.Green);
            Configuration._Config.CustomCommands.Remove(Globals.Prefix + name.ToLower());
            Configuration.SaveConfig();
            build.WithAuthor("Success!", "https://i.dlpng.com/static/png/6705921_preview.png");

            await ReplyAsync("", false, build.Build());
        }
        [Command("loopinsult")]
        public async Task LoopInsult()
        {
            await Context.Message.DeleteAsync();
            var msg = new Random();
            var insults = new string[] {
                "ur a fag",
                "get a life u shitbag",
                "ur mom has balls bigger than you",
                "keep sucking dick faggot",
                "ur a pussy",
                "keep talking fag",
                "i bench ur mom tits in my mouth",
                "ok boomer",
                "u can't talk for shit, did u run out of jokes?",
                "go back to sucking your moms left tit",
                "are you gonna cry now? awwwwww",
                "bitch please, you built like a soggy noodle"
            };
            foreach (var message in insults)
            {
                await Context.Channel.SendMessageAsync($"{insults[msg.Next(0, insults.Length)]}");
                Thread.Sleep(15000);
            }
        }


        [Command("8ball")]
        public async Task ball([Remainder] string message)
        {
            await Context.Message.DeleteAsync();
            var answers = new string[] {
                "It is certain",
                "It is decidedly so",
                "Without a doubt",
                "Yes, definitely",
                "You may rely on it",
                "As I see it, yes",
                "Most likely",
                "Outlook good",
                "Yes",
                "Signs point to yes",
                "Reply hazy try again",
                "Ask again later",
                "Better not tell you now",
                "Cannot predict now",
                "Concentrate and ask again",
                "Don't count on it",
                "My reply is no",
                "My sources say no",
                "Outlook not so good",
                "Very doubtful"
            };

            if (string.IsNullOrWhiteSpace(message))
                return;
            Random rng = new Random();
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle($"Question: **{message}**");
            embed.WithDescription("Answer: `" + answers[rng.Next(0, answers.Length)] + "`");
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithFooter("in #" + Context.Channel.Name + " - Today at " + DateTime.Now.ToShortTimeString(), null);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("911")]
        public async Task nine11()
        {
            await Context.Message.DeleteAsync();
            var Message = await Context.Channel.SendMessageAsync(":firecracker::airplane:       :office:");
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":firecracker::airplane:     :office:";
            }, null);
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":firecracker::airplane:    :office:";
            }, null);
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":firecracker::airplane:   :office:";
            }, null);
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":firecracker::airplane:  :office:";
            }, null);
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":firecracker::airplane: :office:";
            }, null);
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":boom:";
            }, null);
            Thread.Sleep(1000);
            await Context.Message.DeleteAsync();
        }

        [Command("anime")]
        public async Task anime()
        {
            await Context.Message.DeleteAsync();

            var client = new WebClient();
            string json = client.DownloadString("https://api.computerfreaker.cf/v1/anime");
            var result = JsonConvert.DeserializeObject<API.API.RootObject>(json);

            var embed = new EmbedBuilder();
            embed.WithImageUrl(result.url.ToString());
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        [Command("shibe")]
        public async Task Shibe()
        {
            await Context.Message.DeleteAsync();
            var client = new WebClient();
            string json = client.DownloadString("http://shibe.online/api/shibes?count=1");
            var result = JsonConvert.DeserializeObject<API.API.RandomHug.RootObject>(json);
            var embed = new EmbedBuilder();
            embed.WithImageUrl(result.url.ToString());
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("annoy")]
        public async Task annoy([Remainder] string args)
        {
            await Context.Message.DeleteAsync();
            foreach (var message in args)
            {
                Thread.Sleep(16000);
                await Context.Channel.SendMessageAsync(args);
                await Context.Message.DeleteAsync();
            }
            await Context.Channel.SendMessageAsync(args);
        }

        [Command("ascii")]
        public async Task ascii([Remainder] string args)
        {
            await Context.Message.DeleteAsync();

            await Context.Channel.SendMessageAsync("```" + FiggleFonts.Standard.Render(args) + "```");
        }

        [Command("av")]
        public async Task av([Remainder] SocketUser user = null)
        {
            if (user == null)
                user = Context.Message.Author as SocketUser;

            List<string> RolesString = new List<string>();
            foreach (var role in Context.Guild.Roles)
                RolesString.Add(role.Mention);

            var embed = new EmbedBuilder();
            await Context.Message.DeleteAsync();
            embed.WithAuthor($"{user}'s Avatar");
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithImageUrl(user.GetAvatarUrl(0, 1024));
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("saveservers")]
        public async Task SaveAll()
        {
            await Context.Message.DeleteAsync();
            var getservers = await Context.Client.GetGuildsAsync(CacheMode.AllowDownload);
            bool misc = !Directory.Exists("MISC");
            if (misc)
                Directory.CreateDirectory("MISC");
            string saver = "MISC/Saved Servers/Saved Server-" + DateTime.Now.ToString("hh-mm tt" + "/");
            Directory.CreateDirectory(saver);
            string contents = string.Join("\n", getservers);
            File.AppendAllText(saver + "Saved Servers.txt", contents);
            var embed = new EmbedBuilder();
            embed.WithTitle("Success!");
            embed.WithAuthor(Context.User.Username, Context.User.GetAvatarUrl(ImageFormat.Auto));
            embed.WithDescription($"Successfully saved {getservers.Count.ToString()} servers to text file.");
            embed.WithColor(Globals.EmbedHexColor);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        //[Command("savebuyers")]
        //public async Task SaveBuyers()
        //{
        //    ulong buyer = 680623139599810591;
        //    await Context.Message.DeleteAsync();
        //    var getbuyers = await Context.Guild.GetUsersAsync();
        //    var getrole = Context.Guild.GetRole(buyer);

        //}

        [Command("ban")]
        public async Task ban(SocketUser userName = null, string reason = null)
        {
            SocketUser user = Context.User as SocketUser;
            bool flag = userName == null;
            if (flag)
                await Context.Channel.SendMessageAsync("Specify someone to ban.", false, null);
            else if (userName == user)
                await Context.Channel.SendMessageAsync("I'm not banning myself, try someone else.", false);
            try
            {
                await Context.Guild.AddBanAsync(userName, 5, reason);
                await Context.Channel.SendMessageAsync((reason == null) ? ("I Banned " + userName.Username) : ("I Banned " + userName.Username + " Because: " + reason), false, null);
                ConsoleLog.Log("Banned User " + userName.Username + " from " + Context.Guild.Name);
            }
            catch (Exception)
            {
                await Context.Channel.SendMessageAsync("Uhm, Just Kidding...", false, null);
            }
        }


        [Command("price btc")]
        public async Task BitcoinPrice()
        {
            await Context.Message.DeleteAsync();

            var args = new WebClient();
            {
                string price = args.DownloadString("https://bitpay.com/api/rates/usd");
                dynamic json = JsonConvert.DeserializeObject(price);
                var embed = new EmbedBuilder();
                embed.WithAuthor("Bitcoin Price", "https://bitcoin.org/img/icons/opengraph.png", "https://migraine.best/");
                embed.WithDescription("Displays bitcoin price");
                embed.WithColor(new Color(242, 169, 0));
                embed.AddField("Bitcoin Price:", double.Parse(Convert.ToString(json.rate)).ToString("C"));

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }
        [Command("corona")]
        public async Task Corona(string country)
        {
            await Context.Message.DeleteAsync();
            var client = new WebClient();
            {
                string covid = client.DownloadString("https://corona.lmao.ninja/countries/{country}");
                dynamic json = JsonConvert.DeserializeObject(covid);
                var embed = new EmbedBuilder();
                embed.WithTitle($"Corona Stats - {country}");
                embed.WithAuthor(Globals.MigraineImageURL);
                embed.AddField("Cases", double.Parse(Convert.ToString(json.cases)).ToString("C"), true);
                embed.AddField("Deaths", double.Parse(Convert.ToString(json.deaths)).ToString("C"), true);
                embed.AddField("Recovered", double.Parse(Convert.ToString(json.recovered)).ToString("C"), true);
                embed.WithColor(Globals.EmbedHexColor);
                embed.WithFooter("Migraine");
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }

        [Command("bomb")]
        public async Task Bomb()
        {
            await Context.Message.DeleteAsync();
            var Message = await Context.Channel.SendMessageAsync(":bomb:------:fire:");
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":bomb:-----:fire:";
            }, null);
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":bomb:----:fire:";
            }, null);
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":bomb:---:fire:";
            }, null);
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":bomb:--:fire:";
            }, null);
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":bomb:-:fire:";
            }, null);
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":boom:";
            }, null);
            Thread.Sleep(1000);
            await Context.Message.DeleteAsync();
        }

        [Command("pay btc")]
        public async Task Bitcoin()
        {
            await Context.Message.DeleteAsync();

            var embed = new EmbedBuilder();

            embed.WithAuthor("Bitcoin Address", "https://bitcoin.org/img/icons/opengraph.png", "https://migraine.best/");
            //embed.WithDescription($"``{bitcoin}``");
            embed.WithColor(Globals.EmbedHexColor);
            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }
        [Command("pay pp")]
        public async Task Paypal()
        {
            await Context.Message.DeleteAsync();

            var embed = new EmbedBuilder();
            embed.WithAuthor("Paypal Email", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b7/PayPal_Logo_Icon_2014.svg/887px-PayPal_Logo_Icon_2014.svg.png", "");
            //embed.WithDescription($"``{paypal}``");
            embed.WithColor(new Color(59, 123, 191));
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        public static JObject json;

        [Command("bye")]
        public async Task Bye(string ip, string port, string time)
        {
            //removed
        }

        [Command("bye stop")]
        public async Task byestop(string test_id)
        {
            //removed
        }

        [Command("bye restart")]
        public async Task byerestart(string test_id)
        {
            //removed
        }

        [Command("cat")]
        public async Task Cat()
        {
            WebClient client = new WebClient();
            string json = client.DownloadString("http://aws.random.cat/meow");
            var result = JsonConvert.DeserializeObject<API.API.RootObject>(json);

            var embed = new EmbedBuilder();
            embed.WithImageUrl(result.url.ToString());
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("clearc")]
        public async Task ClearChat()
        {
            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync("_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n");
        }

        [Command("cchan")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task CreateChannels([Remainder] string channelname)
        {
            for (var i = 0; i < 30; i++)
            {
                await Context.Guild.CreateTextChannelAsync(channelname);
                await Context.Guild.CreateVoiceChannelAsync(channelname);
            }
        }

        [Command("developer")]
        public async Task Developer()
        {
            await Context.Message.DeleteAsync();
            var embed = new EmbedBuilder();
            embed.WithAuthor("Migraine - Discord Multi-Tool", Globals.MigraineImageURL);
            embed.WithUrl(Globals.DiscordURL);
            embed.WithDescription($"Migraine is developed by [Twin Turbo]\nTo purchase this tool go to https://migraine.best/");
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithFooter("Migraine");
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("dog")]
        public async Task Dog()
        {
            await Context.Message.DeleteAsync();

            WebClient client = new WebClient();
            string json = client.DownloadString("https://random.dog/woof.json");
            var result = JsonConvert.DeserializeObject<API.API.RootObject>(json);

            var embed = new EmbedBuilder();
            embed.WithImageUrl(result.url.ToString());
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("donate")]
        public async Task Donate()
        {
            var embed = new EmbedBuilder();
            await Context.Message.DeleteAsync();
            embed.WithAuthor("Support the Developer", Globals.MigraineImageURL);
            embed.WithDescription(string.Concat("Bitcoin: ``1NXC8dD7udv4Sghf5J4pSeNYmTvgpoiGtG``"));
            embed.WithColor(new Color(255, 153, 0));
            embed.WithFooter("Migraine");
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("embed")]
        public async Task Embed([Remainder] string message)
        {
            await Context.Message.DeleteAsync(null);
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("");
            embed.WithDescription(message);
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithFooter("in #" + Context.Channel.Name + " - Today at " + DateTime.Now.ToShortTimeString(), null);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("coinflip")]
        public async Task CoinFlip()
        {
            await Context.Message.DeleteAsync();

            Random headsortails = new Random();
            var embed = new EmbedBuilder();

            var result = headsortails.Next(0, 2) == 0 ? "Heads" : "Tails";
            embed.WithDescription(result.ToString());
            embed.WithColor(Globals.EmbedHexColor);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("gay")]
        public async Task Gay([Remainder] SocketUser user = null)
        {
            if (user == null)
                user = Context.Message.Author as SocketUser;

            await Context.Message.DeleteAsync();

            var embed = new EmbedBuilder();
            Random random = new Random();

            int randomNumber = random.Next(0, 100);
            embed.WithAuthor("");
            embed.WithTitle("Gay Calculator");
            embed.WithDescription("See's how gay the person is");
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithDescription($"**{user}**, You are {randomNumber}% gay.");
            embed.WithFooter("Migraine");
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        [Command("iq")]
        public async Task IQ([Remainder] SocketUser user = null)
        {
            if (user == null)
                user = Context.Message.Author as SocketUser;

            await Context.Message.DeleteAsync();

            var embed = new EmbedBuilder();
            Random random = new Random();

            int randomNumber = random.Next(0, 1000);
            embed.WithAuthor("");
            embed.WithTitle("IQ Calculator");
            embed.WithDescription("Checks mentioned user IQ");
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithDescription($"**{user}**, you have {randomNumber} IQ.");
            embed.WithFooter("Migraine");
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("gp")]
        public async Task GhostPing(SocketUser user) => await Context.Message.DeleteAsync();

        [Command("hack")]
        public async Task Hack(SocketUser user)
        {
            await Context.Message.DeleteAsync();

            var Message = await Context.Channel.SendMessageAsync($"*Hacking {user}...*");
            Thread.Sleep(1500);

            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = string.Format("*Getting {0}'s IP Address...*", user);
            }, null);
            Thread.Sleep(1500);

            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "*IP: 192.168.1.1*";
            }, null);
            Thread.Sleep(1500);

            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = string.Format("*Getting {0}'s Address...*", user);
            }, null);
            Thread.Sleep(1500);

            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "*Address: 123 Elf dr*";
            }, null);
            Thread.Sleep(1500);

            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "*Grabbing all Info...*";
            }, null);
            Thread.Sleep(1500);

            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "*Sent to FBI*";
            }, null);
            Thread.Sleep(2500);
            await Context.Message.DeleteAsync();
        }

        [Command("help")]
        public async Task Help()
        {
            await Context.Message.DeleteAsync();

            //var embed = new EmbedBuilder();

            //embed.WithTitle("Migraine Selfbot Commands");
            //embed.WithDescription("**View all the Commands you can execute below!**");
            //embed.WithColor(Globals.EmbedHexColor);

            //List<CommandInfo> commands = Utils.Service.Commands.ToList();
            //foreach (CommandInfo command in commands)
            //{
            //    if (command.Summary != null)
            //    {
            //        embed.AddField(command.Name, command.Summary);
            //    }
            //}

            /*
            embed.AddField("stats", "View selfbot statistics");
            embed.AddField("ping", "Check client's latency");
            embed.AddField("spam", "Spams Channel (spam <message> <amount>)");
            embed.AddField("embed", "Embeds your message");
            embed.AddField("poll", "Creates a Poll");
            embed.AddField("quote", "Quote a Message (quote <messageid>)");
            embed.AddField("cquote", "Copy quote from a different channel (cquote <messageid>)");
            embed.AddField("ban", "Ban a user");
            embed.AddField("kick", "Kick a user");
            embed.AddField("userinfo", "Get information on a user");
            embed.AddField("purge", "Deletes messages (purge <amount>)");
            embed.AddField("massdm", "Dm all Members (Risky On Main Account)");
            embed.AddField("gp", "Makes the person your pinging clueless");
            embed.AddField("clearc", "Clears chat with invisible characters");
            embed.AddField("ascii", "Changes your text to ascii format <message>");
            embed.AddField("bye", "Booter (Usage: bye <ip> <port> <timetoseconds>)");
            embed.AddField("gay", "Gay Calculator <user>");
            embed.AddField("spfp", "Returns Server picture");
            embed.AddField("sstats", "Returns Server statistics");
            embed.AddField("ccmds", "Returns a list of your custom commands");
            embed.AddField("ccmd", "Creates a custom user defined command (ccmd <cmdname> <response>)");
            embed.AddField("dcmd", "Deletes a custom user defined command (dcmd <cmdname>)");
            embed.WithFooter("If you want to see more of Migraine's commands, look inside the program.");
            */

            //await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        [Command("online-test")]
        public async Task OnlineTest(string token)
        {
            var Socket = new WebSocket("wss://gateway.discord.gg/?v=7&encoding=json");

            Socket.OnMessage += Socket_OnMessage;
            Socket.OnOpen += Socket_OnOpen;
        }

        private void Socket_OnOpen(object sender, EventArgs e)
        {
            ConsoleLog.Log("Open!!");
        }

        private void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            ConsoleLog.Log(e.Data);
        }

        [Command("hentai")]

        public async Task Hentai()
        {
            await Context.Message.DeleteAsync();

            WebClient client = new WebClient();
            var embed = new EmbedBuilder();

            string json = client.DownloadString("https://nekos.life/");
            var result = JsonConvert.DeserializeObject<API.API.RootObject>(json);

            embed.WithImageUrl(result.image.ToString());
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("hug")]

        public async Task Hug()
        {
            await Context.Message.DeleteAsync();

            WebClient client = new WebClient();
            var embed = new EmbedBuilder();

            string json = client.DownloadString("https://api.computerfreaker.cf/v1/hug");
            var result = JsonConvert.DeserializeObject<API.API.RandomHug.RootObject>(json);

            embed.WithImageUrl(result.url.ToString());
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("invis")]
        public async Task Invis()
        {
            await Context.Message.DeleteAsync();

            string link = "https://cdn.discordapp.com/attachments/686716893515481230/689500991984238620/invis.png";

            var s = Context.Message.DeleteAsync();

            try {
                var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(link);

                var stream = new MemoryStream(imageBytes);
                await Context.Client.CurrentUser.ModifyAsync(x => x.Avatar = new Image(stream));
                var getuser = Context.Client.CurrentUser.Id;
                var final = await Context.Guild.GetUserAsync(getuser);
                await final.ModifyAsync(x => x.Nickname = "ٴٴٴ");
            }
            catch (Exception)
            {
                var embed = new EmbedBuilder();
                embed.WithDescription("Could not set the avatar!");
                embed.WithColor(new Color(255, 0, 0));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }

        //[Command("ip")]
        //public async Task IP([Remainder] string ip)
        //{
        //    await Context.Message.DeleteAsync();
        //    var client = new WebClient();
        //    var embed = new EmbedBuilder();

        //    var req = new HttpClient();
        //    {
        //        var PostData = new Dictionary<string, string>
        //        {
        //            ["ip"] = ip
        //        };
        //        var response = await req.PostAsync(new Uri($"https://ipapi.co/{ip}/json/"), new FormUrlEncodedContent(PostData));
        //        string json = client.DownloadString($"https://ipapi.co/{ip}/json/");
        //        var result = JsonConvert.DeserializeObject<API.API.IPInformation>(json);
        //        var city = double.Parse(Convert.ToString(result.city)).ToString();
        //        //var country = double.Parse(Convert.ToString(result.country)).ToString();
        //        //var callingcode = double.Parse(Convert.ToString(result.country_calling_code)).ToString();
        //        //var currency = double.Parse(Convert.ToString(result.currency)).ToString();
        //        embed.WithAuthor("IP Result. | Migraine");
        //        embed.AddField("**IP:**", $"{ip}", true);
        //        embed.AddField("**City:**", $"{city.ToString()}", true);
        //        //embed.AddField("**Country:**", $"{country}", true);
        //        //embed.AddField("**Area Code:**", $"{callingcode}", true);
        //        //embed.AddField("**Currency:**", $"{currency.}", true);
        //        embed.WithColor(Globals.EmbedHexColor);
        //        embed.WithFooter("Migraine");

        //        await Context.Channel.SendMessageAsync("", false, embed.Build());
        //    }
        //}

        [Command("kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick(SocketGuildUser userName = null, string reason = null)
        {
            await Context.Message.DeleteAsync();
            await userName.KickAsync(reason);
            await Context.Channel.SendMessageAsync($"kicked the mango called {userName}");
        }

        [Command("lenny")]
        public async Task Lenny()
        {
            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync("( ͡° ͜ʖ ͡°)");
        }

        [Command("massdm")]
        public async Task MassDM([Remainder] string message)
        {
            await Context.Message.DeleteAsync();

            var getmembers = await Context.Guild.GetUsersAsync();

            foreach (var user in getmembers)
            {
                await user.SendMessageAsync(message);
            }
        }

        [Command("migraine")]
        public async Task Migraine()
        {
            await Context.Message.DeleteAsync();

            var Message = await Context.Channel.SendMessageAsync("M");

            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "MI";
            }, null);
            Thread.Sleep(500);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "MIG";
            }, null);
            Thread.Sleep(500);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "MIGR";
            }, null);
            Thread.Sleep(500);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "MIGRA";
            }, null);
            Thread.Sleep(500);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "MIGRAI";
            }, null);
            Thread.Sleep(500);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "MIGRAIN";
            }, null);
            Thread.Sleep(500);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "MIGRAINE";
            }, null);
            Thread.Sleep(500);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "MIGRAINE |";
            }, null);
            Thread.Sleep(500);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "MIGRAINE ";
            }, null);
            Thread.Sleep(500);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "MIGRAINE |";
            }, null);
            Thread.Sleep(500);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "MIGRAINE ";
            }, null);
            Thread.Sleep(500);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "MIGRAINE |";
            }, null);
            Thread.Sleep(500);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = "MIGRAINE ";
            }, null);
            Thread.Sleep(1000);

            await Context.Message.DeleteAsync();
        }

        [Command("ping")]
        public async Task ping()
        {
            await Context.Message.DeleteAsync();

            var embed = new EmbedBuilder();
            embed.WithAuthor("Pong!", "http://pngimg.com/uploads/ping_pong/ping_pong_PNG10375.png");
            embed.WithDescription(string.Concat("**Socket Latency**: ", StartBot._Client.Latency.ToString("#,##0"), "ms"));
            embed.WithFooter("Migraine");
            embed.WithColor(Globals.EmbedHexColor);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
            Thread.Sleep(1000);
            await Context.Message.DeleteAsync();
        }

        [Command("poll")]
        public async Task poll([Remainder] string message)
        {
            await Context.Message.DeleteAsync();
            if (message == null) {
                await Context.Channel.SendMessageAsync("Please enter a message.");
            }
            var check = new Emoji("✅");
            var exit = new Emoji("❌");
            var embed = new EmbedBuilder();
            embed.Title = "Vote!";
            embed.WithThumbnailUrl(Context.Client.CurrentUser.GetAvatarUrl(0));
            embed.WithDescription($"```{message}```");
            embed.WithColor(Globals.EmbedHexColor);
            IUserMessage userMessage2 = await Context.Channel.SendMessageAsync("", false, embed.Build(), null);
            IUserMessage userMessage = userMessage2;
            userMessage2 = null;
            IUserMessage msg = userMessage;
            await msg.AddReactionAsync(check, null);
            await msg.AddReactionAsync(exit, null);
        }

        [Command("purge")]
        public async Task purge(int amount)
        {
            await Context.Message.DeleteAsync();
            var messages = Context.Channel.GetMessagesAsync(amount);
            var enumerator = messages.GetAsyncEnumerator();
            while (enumerator.MoveNextAsync().Result)
            {
                foreach (var message in enumerator.Current)
                {
                    if (message.Author.Mention == StartBot._Client.CurrentUser.Mention && message.Source == MessageSource.User)
                    {
                        await message.DeleteAsync();
                    }
                }
            }
        }
        [Command("react")]
        public async Task React(int amount, string emoji)
        {
            await Context.Message.DeleteAsync();
            var messages = Context.Channel.GetMessagesAsync(amount);
            var enumerator = messages.GetAsyncEnumerator();
            while (enumerator.MoveNextAsync().Result)
            {
                foreach (var message in enumerator.Current)
                {
                    var emoji1 = new Emoji(emoji);
                    await message.AddReactionAsync(emoji1);
                }
            }
        }
        [Command("purge all")]
        public async Task PurgeAll()
        {
            int amount = 192376;
            await Context.Message.DeleteAsync();
            var messages = Context.Channel.GetMessagesAsync(amount);
            var enumerator = messages.GetAsyncEnumerator();
            while (enumerator.MoveNextAsync().Result)
            {
                foreach (var message in enumerator.Current)
                {
                    if (message.Author.Mention == StartBot._Client.CurrentUser.Mention && message.Source == MessageSource.User)
                    {
                        await message.DeleteAsync();
                    }
                }
            }
        }

        [Command("quote")]
        public async Task quote([Remainder] string msg)
        {
            bool flag = msg == null;
            if (flag)
            {
                bool flag2 = Commands.author == null;
                if (flag2)
                {
                    await this.ReplyAsync("You must copy a quote or include a message id!", false, null, null);
                }
                else
                {
                    await Context.Message.DeleteAsync();
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor(Commands.author, null, null);
                    embed.WithDescription(Commands.Content);
                    embed.WithColor(Globals.EmbedHexColor);
                    embed.WithFooter("in #" + Commands.Channel + " - " + Commands.Time, null);
                    await Context.Channel.SendMessageAsync("", false, embed.Build(), null);
                }
            }
            else
            {
                ulong id = Convert.ToUInt64(msg);
                await Context.Message.DeleteAsync();
                IEnumerable<IMessage> enumerable = await Context.Channel.GetMessagesAsync(100, CacheMode.AllowDownload, null).FlattenAsync<IMessage>();
                IEnumerable<IMessage> messages = enumerable;
                foreach (IMessage message in messages)
                {
                    if (message.Id.ToString().Contains(id.ToString()))
                    {
                        EmbedBuilder embed2 = new EmbedBuilder();
                        embed2.WithAuthor(message.Author);
                        embed2.WithDescription(message.Content);
                        embed2.WithColor(Globals.EmbedHexColor);
                        embed2.WithFooter("in " + Context.Channel.Name + " - " + message.Timestamp.LocalDateTime.ToShortDateString(), null);
                        await Context.Channel.SendMessageAsync("", false, embed2.Build(), null);
                        break;
                    }
                }
            }
        }

        [Command("cquote")]
        public async Task CopyQuote(/*[Remainder]*/ ulong id)
        {
            await Context.Message.DeleteAsync(null);
            IEnumerable<IMessage> enumerable = await Context.Channel.GetMessagesAsync(100, CacheMode.AllowDownload, null).FlattenAsync<IMessage>();
            IEnumerable<IMessage> messages = enumerable;
            enumerable = null;
            foreach (IMessage message in messages)
            {
                if (message.Id.ToString().Contains(id.ToString()))
                {
                    author = message.Author.Username + "#" + message.Author.Discriminator;
                    Content = message.Content;
                    Channel = Context.Channel.Name;
                    Time = message.Timestamp.LocalDateTime.ToShortDateString();
                    break;
                }
            }
        }
        public static string author;

        public static string Content;

        public static string Channel;

        public static string Time;

        [Command("spfp")]
        public async Task spfp()
        {
            await Context.Message.DeleteAsync();
            string serveravatar = Context.Guild.IconUrl;
            var embed = new EmbedBuilder();
            embed.WithAuthor(Context.Message.Author.Username, Context.Message.Author.GetAvatarUrl(ImageFormat.Auto));
            embed.WithImageUrl(serveravatar);
            embed.WithColor(Globals.EmbedHexColor);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("sstats")]
        public async Task sstats()
        {
            await Context.Message.DeleteAsync();
            string serveravatar = Context.Guild.IconUrl;
            var server = Context.Guild;
            var embed = new EmbedBuilder();
            embed.WithThumbnailUrl(serveravatar);
            embed.AddField("**Server Name:** ", $" {server.Name}", true);
            embed.AddField("**Server ID:**", $" {server.Id}", true);
            embed.AddField("**Server Owner:** ", $"{server.OwnerId}", true);
            embed.AddField("**Server Created:** ", $" {server.CreatedAt}", true);
            embed.AddField("**Server ID:**", $"{server.Id}", true);
            embed.AddField("**Verification Level:**", $"{server.VerificationLevel}", true);
            embed.AddField("**Emotes:**", $"{server.Emotes.Count.ToString()}", true);
            embed.AddField("**Roles:**", $"{server.Roles.Count.ToString()}", true);
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithFooter("Migraine");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        //[Command("saveemojis")]
        //public async Task SaveEmojis()
        //{
        //    await Context.Message.DeleteAsync();

        //    var emojis = Context.Guild.Emotes;
        //    bool misc = !Directory.Exists("MISC");
        //    if (misc)
        //        Directory.CreateDirectory("MISC");
        //    string saver = "MISC/Saved Emojis/Saved Emojis-" + DateTime.Now.ToString("hh-mm tt" + "/");
        //    Directory.CreateDirectory(saver);
        //    var embed = new EmbedBuilder();
        //    embed.WithTitle("Success!");
        //    embed.WithAuthor(Context.User.Username, Context.User.GetAvatarUrl(ImageFormat.Auto));
        //    embed.WithDescription($"Successfully saved {emojis.Count.ToString()} servers to text file.");
        //    embed.WithColor(Globals.EmbedHexColor);
        //    await Context.Channel.SendMessageAsync("", false, embed.Build());

        //}

        //[Command("savemembers")]
        //public async Task SaveMembers()
        //{
        //    await Context.Message.DeleteAsync();

        //    var members = Context.Guild.DownloadUsersAsync().ToString();
        //    var members1 = Context.Guild.GetUsersAsync();
        //    bool misc = !Directory.Exists("MISC");
        //    if (misc)
        //        Directory.CreateDirectory("MISC");
        //    string saver = "MISC/Saved Members/Saved Members-" + DateTime.Now.ToString("hh-mm tt" + "/");
        //    Directory.CreateDirectory(saver);
        //    var embed = new EmbedBuilder();
        //    embed.WithTitle("Success!");
        //    embed.WithAuthor(Context.User.Username, Context.User.GetAvatarUrl(ImageFormat.Auto));
        //    embed.WithDescription($"Successfully saved {} members to text file.");
        //    embed.WithColor(Globals.EmbedHexColor);
        //    await Context.Channel.SendMessageAsync("", false, embed.Build());
        //}

        //[Command("spam")]
        //public async Task spam([Remainder] string message, int amount) // fix
        //{
        //    await Context.Message.DeleteAsync();
        //    for (int j = amount; amount > 11111; j++)
        //        await Context.Channel.SendMessageAsync(message);
        //}

        [Command("stats")]
        public async Task stats()
        {
            await Context.Message.DeleteAsync();
            TimeSpan time = DateTime.Now - Process.GetCurrentProcess().StartTime;
            string upTime = "Been Alive For: ";
            bool Day = time.Days > 0;
            if (Day)
            {
                bool Hour = time.Hours <= 0 || time.Minutes <= 0;
                if (Hour)
                    upTime += string.Format("{0} Day(s) and ", time.Days);
                else
                    upTime += string.Format("{0} Day(s),", time.Days);
            }
            bool Minute = time.Hours > 0;
            if (Minute)
            {
                bool flag4 = time.Minutes > 0;
                if (flag4)
                    upTime += string.Format(" {0} Hour(s), ", time.Hours);
                else
                    upTime += string.Format("{0} Hour(s) and ", time.Hours);
            }
            bool Second = time.Minutes > 0;
            if (Second)
                upTime += string.Format("{0} Minute(s)", time.Minutes);
            bool MilSec = time.Seconds >= 0;
            if (MilSec)
            {
                bool flag7 = time.Hours > 0 || time.Minutes > 0;
                if (flag7)
                    upTime += string.Format(" and {0} Second(s)", time.Seconds);
                else
                    upTime += string.Format("{0} Second(s)", time.Seconds);
            }
            Process process = Process.GetCurrentProcess();
            long mem = process.PrivateMemorySize64;
            long memory = mem / 1024L / 1024L;
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("Migraine Selfbot Stats", Globals.MigraineImageURL);
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithTimestamp(DateTimeOffset.UtcNow.UtcDateTime);
            embed.WithThumbnailUrl(Context.User.GetAvatarUrl(ImageFormat.Auto, 128));
            embed.WithFooter("Migraine Selfbot Stats");
            embed.AddField("Memory Usage:", string.Format("```fix\n{0}Mb```", memory), true);
            embed.AddField("Up-time:", "```prolog\n" + upTime + "```", true);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("playing")]
        public async Task Playing([Remainder] string args)
        {
            await Context.Message.DeleteAsync();
            await (Context.Client as DiscordSocketClient).SetGameAsync(args, null, ActivityType.Playing);
            await Context.Channel.SendMessageAsync($"Successfully changed status to '**{args}**'");
        }
        [Command("typing")]
        public async Task Typing()
        {
            await Context.Message.DeleteAsync();
            await Context.Channel.TriggerTypingAsync();
        }

        [Command("streaming")]
        public async Task Streaming([Remainder] string args)
        {
            await Context.Message.DeleteAsync();
            await (Context.Client as DiscordSocketClient).SetGameAsync(args, "https://twitch.tv/ninja", ActivityType.Streaming);
            await Context.Channel.SendMessageAsync($"Successfully changed status to '**{args}**'");
        }
        [Command("stop")]
        public async Task Stop()
        {
            await Context.Message.DeleteAsync();
            await (Context.Client as DiscordSocketClient).StopAsync();
            await Context.Channel.SendMessageAsync($"stopped task");
            Thread.Sleep(500);
            await Context.Message.DeleteAsync();
        }

        [Command("tcolor")]
        public async Task TextColor(string message, [Remainder] string args)
        {
            await Context.Message.DeleteAsync();
            if (args.Length > 1)
                if (message.Equals("orange"))
                    await Context.Channel.SendMessageAsync("```fix\n" + args + "```");
                else if (message == "green")
                    await Context.Channel.SendMessageAsync("```css\n" + args + "```");
                else if (message.Equals("cyan"))
                    await Context.Channel.SendMessageAsync("```yaml\n" + args + "```");
                else if (message.Equals("red"))
                    await Context.Channel.SendMessageAsync("```diff\n" + "- " + args + "```");
                else
                    await Context.Channel.SendMessageAsync("**Invalid Color.**");
                    Thread.Sleep(1000);
                    await Context.Message.DeleteAsync();
        }

        [Command("unban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Unban(/*[Remainder]*/ ulong userName)
        {
            await Context.Guild.RemoveBanAsync(userName, null);
            await ReplyAsync(string.Format("I Unbanned {0}", userName), false, null, null);
            ConsoleLog.Log($"Unbanned {userName} from " + Context.Guild.Name);
        }

        [Command("userinfo")]
        public async Task UserInformation(SocketGuildUser user = null)
        {
            await Context.Message.DeleteAsync();
            var RolesString = new List<string>();
            var embed = new EmbedBuilder();
            if (user == null)
                user = (Context.Message.Author as SocketGuildUser);
            embed.WithAuthor(user);
            embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto, 128));
            embed.AddField("Bot", user.IsBot, true);
            embed.AddField("Status", user.Activity, true);
            embed.AddField("Joined Server", user.JoinedAt.ToString().Split(new char[]
            { 
                '+'
            })[0], true);
            embed.AddField(string.Format("Roles[{0}]", RolesString.Count), string.Join(", ", RolesString), false);
            embed.AddField("Playing", user.Status.ToString(), false);
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithFooter(string.Format("ID: {0} - Today at {1}", user.Id, DateTime.Now.ToShortTimeString()), null);
            await Context.Channel.SendMessageAsync("", false, embed.Build(), null);
        }

        [Command("giveaway")]
        public async Task CreateGiveaway([Remainder] string info)
        {
            var random = new Random();
            await Context.Message.DeleteAsync();
            bool noinfo = info == null;
            if (noinfo)
                await Context.Channel.SendMessageAsync($"What are you going to giveaway? ex:({Globals.Prefix}giveaway Discord Nitro 24h)");
            else if (Globals.UsersEntered.Count != 0)
                await Context.Channel.SendMessageAsync("You can only have 1 giveaway at a time!");
            else
            {
                string[] GetEndTime = info.Split(new char[]
                {
                    ' '
                });
                int TimeTillEnd = 0;
                int num = 0;
                try
                {
                    ConsoleLog.Log(GetEndTime[GetEndTime.Length - 1]);
                    TimeTillEnd = Convert.ToInt32(GetEndTime[GetEndTime.Length - 1].Replace("h", ""));
                    info = info.Replace(GetEndTime[GetEndTime.Length - 1], "");
                }
                catch
                {
                    num = 1;
                }
                if (num != 1)
                {
                    Globals.StartTime = DateTime.Now;
                    Globals.TimeToEnd = DateTime.Now.AddHours((double)TimeTillEnd);
                    var emoji = new Emoji("\ud83c\udf81");
                    var host = Context.User;
                    var embed = new EmbedBuilder();
                    embed.WithAuthor(host);
                    embed.WithTitle($"{host.Username} has started a giveaway!");
                    embed.WithDescription(string.Format("{0}\n React with {1} to enter this giveaway", info, emoji));
                    embed.WithColor(Globals.EmbedHexColor);
                    embed.WithFooter("Starts - " + Globals.StartTime.ToString() + " - Ends - " + Globals.TimeToEnd.ToString());
                    IUserMessage userMessage2 = await Context.Channel.SendMessageAsync("", false, embed.Build());
                    IUserMessage userMessage = userMessage2;
                    userMessage2 = null;
                    IUserMessage msg = userMessage;
                    await msg.AddReactionAsync(emoji);
                    Globals.GiveawayName = info;
                    Globals.GiveawayMessage = Context.Message.Id;
                    Globals.GiveawayChannel = Context.Message.Channel.Id;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Invalid Time");
                }
            }
        }
        [Command("giveawayusers")]
        public async Task GiveawayUsers()
        {
            await Context.Message.DeleteAsync();
            bool flag = Globals.UsersEntered.Count == 0;
            if (flag)
            {
                await ReplyAsync((Globals.GiveawayName != "") ? "**There is no current giveaway**" : ("**There are no current users entered in** " + Globals.GiveawayName), false, null, null);
            }
            else
            {
                string users = string.Join(Environment.NewLine, Globals.UsersEntered);
                var embed = new EmbedBuilder();
                embed.WithTitle($"Users entered in **{Globals.GiveawayName}**");
                embed.WithDescription(users + string.Format("\nUsers Entered **{0}**", Globals.UsersEntered.Count));
                await ReplyAsync(string.Empty, false, embed.Build(), null);
            }
        }
        [Command("choosewinner")]
        public async Task Pick()
        {
            bool flag = Globals.UsersEntered.Count == 0;
            if (flag)
            {
                await ReplyAsync((Globals.GiveawayName == "") ? "**There is no current giveaway**" : ("**There are no current users entered in** \"" + Globals.GiveawayName + "\""), false, null, null);
            }
            else
            {
                SocketGuildChannel Chan = StartBot._Client.GetChannel(Globals.GiveawayChannel) as SocketGuildChannel;
                ulong serverid = Chan.Guild.Id;
                var random = new Random();
                int choice = random.Next(0, Globals.UsersEntered.Count);
                await StartBot._Client.GetGuild(serverid).GetTextChannel(Globals.GiveawayChannel).SendMessageAsync(string.Concat(new string[]
                {
                    "The winner for ",
                    Globals.GiveawayName,
                    " is \"",
                    Globals.UsersEntered[choice],
                    "\""
                }), false, null, null);
                Globals.UsersEntered.Clear();
                Globals.GiveawayName = "";
            }
        }
        [Command("cleargiveaway")]
        public async Task Clear()
        {
            int num = 0;
            try
            {
                var message = await Context.Channel.GetMessageAsync(Globals.GiveawayMessage, CacheMode.AllowDownload);
                var msg = message;
                message = null;
                await msg.DeleteAsync();
                await this.ReplyAsync("Closed Giveaway \"" + Globals.GiveawayName + "\"", false, null, null);
                Globals.UsersEntered.Clear();
                Globals.GiveawayName = "";
                msg = null;
            }
            catch
            {
                num = 1;
            }
            if (num == 1)
            {
                await ReplyAsync("Giveaway not found...", false, null, null);
            }
        }
        [Command("delemoji")]
        public async Task DeleteEmojis()
        {
            await Context.Message.DeleteAsync();
            var Emojis = Context.Guild.Emotes;
            foreach (var emoji in Emojis)
            {
                await Context.Guild.DeleteEmoteAsync(emoji);
            }
        }
        [Command("xoxo")]
        public async Task XOXO()
        {
            if (Context.User.Id.ToString() == Context.Client.CurrentUser.Id.ToString())
            {
                await Context.Message.DeleteAsync();

                string mention = "";

                var users = await Context.Guild.GetUsersAsync();

                foreach (SocketGuildUser member in users)
                {
                    if (member.IsBot)
                        continue;
                    mention += member.Mention + " ";
                    if (mention.Length >= 1977)
                    {
                        await Context.Channel.SendMessageAsync(mention);
                        Thread.Sleep(500);
                        await Context.Message.DeleteAsync();
                        mention = "";
                    }
                    await Context.Channel.SendMessageAsync(mention);
                    Thread.Sleep(500);
                    await Context.Message.DeleteAsync();
                }
            }
        }
        [Command("sizepp")]
        public async Task SizePenis([Remainder] SocketUser user)
        {
            await Context.Message.DeleteAsync();
            if (user == null)
                user = (Context.Message.Author as SocketUser);
            var random = new Random();
            string[] rand = new string[]
            {
                "Very Small pp",
                "Moderate pp",
                "Large pp",
                "EXTREMELY LARGE PP"
            };
            var embed = new EmbedBuilder();
            embed.WithAuthor($"{Context.Message.Author}", Globals.MigraineImageURL);
            embed.WithDescription($"User **{user.Username}** has a " + $"``{rand[random.Next(0, rand.Length)]}``");
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithFooter("Migraine");
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}
