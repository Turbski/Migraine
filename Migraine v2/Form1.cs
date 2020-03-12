using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Discord.WebSocket;
using Migraine_v2.Classes;
using Migraine_v2.LoginClass;
using Migraine_v2.DiscordRPC;
using Migraine_v2.Registration;
using Migraine_v2.SelfbotClasses;
using Migraine_v2.Nitro_Sniper_Lib;
using Migraine_v2.Discord_Spammer_Lib;
using Migraine_v2;
using DiscordRPC;
using DiscordRPC.Logging;

namespace Migraine_v2 {
    public partial class Form1 : Form {
        public DiscordRpcClient RPCClient;
        public Form1() {
            InitializeComponent();
            try
            {
                RPCClient = new DiscordRpcClient("685768251698970676");
                RPCClient.Logger = new ConsoleLogger
                {
                    Level = LogLevel.Trace
                };
                RPCClient.SetPresence(new RichPresence
                {
                    Details = "",
                    State = "Main Page",
                    Assets = new Assets
                    {
                        LargeImageKey = "migraine_logo",
                        LargeImageText = "Migraine FTW"
                    }
                });
                RPCClient.Initialize();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            LogConsole.Text = ConsoleLog.LogInfo;
            lblDiscordUser.Text = "" + doLogin._Username.ToString();
            DiscordUser.Text = DiscordUser.Text.Replace("Unknown", $"{Globals.SelfbotUser}");
            UserToken.Text = Token1;
            string[] array = Migraine_v2.CommandsList.Get();
            foreach (string text in array) {
                this.CommandsList.Rows.Add(new object[] {
                    text.Split(new char[] {
                        ':'
                    })[0],
                    text.Split(new char[] {
                        ':'
                    })[1]
                });
            }
            if (Settings._Token != "")
                this.UserToken.Text = Settings._Token;
                this.NitroToken.Text = Settings._Token;
            bool flag = File.Exists("RPC.json");
            if (flag) {
                string text = File.ReadAllText("RPC.json");
                Form1.json = JObject.Parse(text);
                this.Client_ID.Text = Form1.json["client"].ToString();
                this.State.Text = Form1.json["state"].ToString();
                this.Details.Text = Form1.json["details"].ToString();
                this.ImageText.Text = Form1.json["imageText"].ToString();
                this.ImageAsset.Text = Form1.json["imageAsset"].ToString();
            } else {
                File.AppendAllText("RPC.json", string.Concat(new string[] {
                    "{\n\"client\":\"unknown\",\n\"state\":\"unknown\",\n\"details\":\"unknown\",\n\"imageText\":\"unknown\",\n\"imageAsset\":\"unknown\"\n}"
                }));
                string text2 = File.ReadAllText("Settings.json");
                Form1.json = JObject.Parse(text2);
            }
        }
        private void StopSelfbot_Click(object sender, EventArgs e) {
            ConsoleLog.Log("[Console] Bot Stopped!");
            Thread.Sleep(200);
            this.ConstantlyRun.Stop();
            TimeElapsed.Stop();
            TimeElapsed.Reset();
            Globals.SelfbotRunning = false;
        }

        private void ConstantlyRun_Tick(object sender, EventArgs e) {
            bool getUser = Form1.GetUser;
            if (getUser) {
                this.DiscordUser.Text = Globals.SelfbotUser;
                Form1.GetUser = false;
            }
            LogConsole.Text = ConsoleLog.LogInfo;
            TimeSpan elapsed = Form1.TimeElapsed.Elapsed;
            this.TimeSinceStart.Text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", new object[] {
                elapsed.Hours,
                elapsed.Minutes,
                elapsed.Seconds,
                elapsed.Milliseconds / 10
            });
        }

        public void Form1_Load(object sender, EventArgs e) {
            this.ClearPages();
            home1.Visible = true;
            homeLabel.Visible = true;
            //if (Settings._RPC.Contains("true"))
            //{
            //    clientRPC.SetPresence(new RichPresence()
            //    {
            //        Details = "Main Menu",
            //        Timestamps = Timestamps.Now,
            //        Assets = new Assets()
            //        {
            //            LargeImageKey = "main_icon",
            //        }
            //    });
            //}
        }
        public void Exitbutton_Click_1(object sender, EventArgs e) {
            TimeElapsed.Stop();
            Globals.SelfbotRunning = false;
            Environment.Exit(0);
        }
        public void Minimizebutton_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
        public void BtnHome_Click(object sender, EventArgs e) {
            this.ClearPages();
            home1.Visible = true;
            homeLabel.Visible = true;
        }
        public void BtnTknChecker_Click(object sender, EventArgs e) {
            this.ClearPages();
            token1.Visible = true;
            tkncheckerLabel.Visible = true;
            tknlabel.Visible = true;
        }
        public void BtnSpammer_Click(object sender, EventArgs e) {
            this.ClearPages();
            spammer1.Visible = true;
            spammerLabel.Visible = true;
            splabel.Visible = true;
        }
        public void BtnSelfbot_Click(object sender, EventArgs e) {
            this.ClearPages();
            selfbot1.Visible = true;
            selfbotLabel.Visible = true;
            sbLabel.Visible = true;
        }
        private void NitrosnprBtn_Click(object sender, EventArgs e) {
            this.ClearPages();
            nitrosnprPanel.Visible = true;
            nitroD.Visible = true;
            nitroT.Visible = true;
        }
        public void BtnDiscordRPC_Click(object sender, EventArgs e) {
            this.ClearPages();
            rpcdlabel.Visible = true;
            rpcLabel.Visible = true;
            discordRPC1.Visible = true;
        }
        private void JoinDiscordButton_Click_1(object sender, EventArgs e) {
            Process.Start("https://discord.gg/JW5t7zV");
            Process.Start("https://t.me/OfficialMigraine");
        }
        public void BtnSettings_Click(object sender, EventArgs e) {
            this.ClearPages();
            this.SettingsPanel.Visible = true;
        }
        public void ClearPages() {
            home1.Visible = false;
            homeLabel.Visible = false;
            token1.Visible = false;
            tkncheckerLabel.Visible = false;
            tknlabel.Visible = false;
            selfbot1.Visible = false;
            spammerLabel.Visible = false;
            spammer1.Visible = false;
            splabel.Visible = false;
            selfbot1.Visible = false;
            sbLabel.Visible = false;
            selfbotLabel.Visible = false;
            dnnukerlabel.Visible = false;
            dndescriptionlabel.Visible = false;
            discordRPC1.Visible = false;
            rpcdlabel.Visible = false;
            rpcLabel.Visible = false;
            nitrosnprPanel.Visible = false;
            nitroD.Visible = false;
            nitroT.Visible = false;
            MassPing.Visible = false;
            SettingsPanel.Visible = false;
            Spotifybutt.Visible = false;
            SpotifyInviteButt.Visible = false;
            SpotifyPanel.Visible = false;
            DiscordPanel.Visible = false;
        }

        public void ClearAllTxt() {
            this.richTextBox.Text = "";
            this.ServerID.Text = "";
            this.ChannelID.Text = "";
            this.EmojiID.Text = "";
            this.MessageID.Text = "";
            this.InviteURL.Text = "";
            this.Nickname.Text = "";
            this.UserId.Text = "";
            this.UserIdText.Text = "";
            this.voiceChannelID.Text = "";
        }

        public void Spammer1() {
            bool flag = !Form1.Running;
            if (flag)
                Thread.CurrentThread.Abort();
            bool flag2 = Form1._MsgsSent >= Form1._Msgstosend;
            if (flag2)
                Form1.Running = false;
            bool running = Form1.Running;
            if (running) {
                try  {
                    string text;
                    ConsoleLog.Log("[Console] Initializing...");
                    Form1.ValidTokens.TryDequeue(out text);
                    bool flag3 = text != null && text != "";
                    if (!flag3) { return; }
                    Form1.ValidTokens.Enqueue(text);
                    int num = rDiscord.SendChannelMessage(Client.Create(false, null, text), Form1._ChannelID, Form1.MessageText);

                    bool flag4 = num == 1;
                    if (flag4)
                    {
                        this.tmessagessent.Text = Form1._MsgsSent.ToString();
                        Form1._MsgsSent++;
                    }
                    else { Thread.Sleep(1000); }
                    ConsoleLog.Log("[Console] Started Task");
                } catch { }
            }
            Thread.Sleep(1);
        }
        //public void MassPingSpammer() {
        //    bool flag = !Form1.Running;
        //    if (flag)
        //        Thread.CurrentThread.Abort();
        //    bool flag2 = Form1._MsgsSent >= Form1._Msgstosend;
        //    if (flag2)
        //        Form1.Running = false;
        //    bool running = Form1.Running;
        //    if (running) {
        //        try {
        //            string text;
        //            ConsoleLog.Log("[Console] Initializing...");
        //            Form1.ValidTokens.TryDequeue(out text);
        //            bool flag3 = text != null && text != "";
        //            if (!flag3) { return; }
        //            Form1.ValidTokens.Enqueue(text);
        //            string[] Members = rDiscord.GetMembers(Client.Create(false, null, text), this.ServerID.Text);
        //            //int num = rDiscord.SendMassPingMessage(Client.Create(false, null, text), Form1._ChannelID, Members);
        //            ConsoleLog.Log("[Console] Started Task");
        //            bool flag4 = num == 1;
        //            if (flag4) {
        //                this.tmessagessent.Text = Form1._MsgsSent.ToString();
        //                Form1._MsgsSent++;
        //            } else {  Thread.Sleep(1000); }
        //        } catch { }
        //    }
        //    Thread.Sleep(1);
        //}
        private void Stop_Click(object sender, EventArgs e) => Form1.Running = false;
        public void InviteThreaded() {
            try {
                string text;
                Form1.ValidTokens.TryDequeue(out text);
                Form1.ValidTokens.Enqueue(text);
                HttpResponseMessage httpResponseMessage = rDiscord.JoinServer(Client.Create(false, null, text), Form1._InviteURL);
                bool flag = httpResponseMessage.ToString().Contains("StatusCode: 200, ReasonPhrase: 'OK'");
                if (flag)
                    Form1.AmountJoined++;
                this.UsersJoinedInt.Text = Form1.AmountJoined.ToString();
            } catch { }
            Form1.Running = false;
            Thread.CurrentThread.Abort();
        }
        private void LeaveThreaded()  {
            string token;
            Form1.ValidTokens.TryDequeue(out token);
            try {
                new Thread(() =>
                {

                }).Start();
                rDiscord.LeaveServer(Client.Create(false, null, token), this.ServerID.Text);
                Form1.UsersLeft++;
                this._UsersLeft.Text = Form1.UsersLeft.ToString();
            } catch { }
            Thread.CurrentThread.Abort();
        }
        private void DmAllInGuild_Click(object sender, EventArgs e) => MessageBox.Show("Didnt have time to do this feature!", "Sorry");
        public void GetChannels(int i) { }
        public void SpamDmNigga() {
            bool flag = !Form1.Running;
            if (flag)
                Thread.CurrentThread.Abort();
            bool flag2 = Form1._MsgsSent >= Form1._Msgstosend;
            if (flag2)
                Form1.Running = false;
            bool running = Form1.Running;
            if (running) {
                bool flag3 = this.i >= Form1.ChannelIDS.Count;
                if (flag3)
                    this.i = 0;
                try {
                    int num = rDiscord.SendChannelMessage(Client.Create(false, null, Form1.ChannelIDS.ToArray()[this.i].Split(new char[] {
                        ':'
                    })[1]), Form1.ChannelIDS.ToArray()[this.i].Split(new char[] {
                        ':'
                    })[0], Form1.MessageText);
                    bool flag4 = num == 1;
                    if (flag4)
                        Form1._MsgsSent++;
                } catch { }
                this.i++;
            }
        }
        private void BoostisThread_ValueChanged(object sender, EventArgs e) => this.Boost.Text = "Threads: [" + this.boostisThread.Value.ToString() + "] (Recommend 20)";
        public void JoinVC(string voicechannelID) {
            int num = 0;
            string server = this.ServerID.Text;
            new Thread(() =>
            {
                foreach (string token in Form1.ValidTokens)
                {
                    try
                    {
                        rDiscord.JoinVC(Client.Create(false, null, token), voicechannelID, server);
                    } catch { }
                }
                num++;
            }).Start();
            MessageBox.Show(string.Format("Joined Voice Channel on {0} accounts!", num), "Done!");
        }
        public void ChangeNicknames(string ServerID, string nick) {
            int num = 0;
            new Thread(() =>
            {
                foreach (string token in Form1.ValidTokens)
                {
                    try
                    {
                        rDiscord.SetName(Client.Create(false, null, token), ServerID, nick);
                        num++;
                    }
                    catch { }
                }

                Form1.Running = false;
                MessageBox.Show(string.Format("Changed nick to '{0}' on {1} accounts!", nick, num), "Done!");
            }).Start();
        }
        [Obsolete]
        private void StartSelfbot_Click_1(object sender, EventArgs e) {

            bool notoken = this.UserToken.Text == null || this.UserToken.Text == "Insert Token" || this.UserToken.Text == "";
            if (notoken)
                MessageBox.Show("Please insert your token.", "Error");
            bool selfbotRunning = Globals.SelfbotRunning;
            if (selfbotRunning)
                MessageBox.Show("Selfbot is already running!", "Error");
            bool ifhasquote = this.UserToken.Text.Contains("\"");
            if (ifhasquote)
                MessageBox.Show("Invalid Token Format");
            else
            {
                if (Settings._Username != this.UserToken.Text)
                {
                    //Settings._Settings["misc"]["token"] = this.UserToken.Text;
                    string output = JsonConvert.SerializeObject(Settings._Settings, Formatting.Indented);
                    File.WriteAllText("Settings.json", output);
                }
                StartBot.Token = this.UserToken.Text;
                this.ConstantlyRun.Start();
                TimeElapsed.Start();
                ConsoleLog.Log("[Console] Bot Starting...");
                Globals.SelfbotRunning = true;
                StartBot.Init();
                Globals.SelfbotUser = this.DiscordUser.Text;
            }
        }
        private void StopSelfbot_Click_1(object sender, EventArgs e) {
            bool ifnotrunning = !Globals.SelfbotRunning;
            if (ifnotrunning)
                MessageBox.Show("Selfbot isn't running.", "Migraine - Error");
            else {
                Thread.Sleep(200);
                this.ConstantlyRun.Stop();
                TimeElapsed.Stop();
                TimeElapsed.Reset();
                Globals.SelfbotRunning = false;
                MessageBox.Show("Sucessfully stopped selfbot.", "Migraine");
            }
        }
        private void CustomPUTInvite_Click(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else {
                Form1.Running = true;
                ConsoleLog.Log("[Console] Started Inviting Bots...");
                try {
                    bool badinvite = this.InviteURL.Text == null || this.InviteURL.Text == "";
                    if (badinvite)
                        throw new Exception("Bad Invite URL");
                    Form1._InviteURL = this.InviteURL.Text.Replace("https://discord.gg/", "");
                } catch {
                    MessageBox.Show("Please include a invite URL!", "Error!");
                    return;
                }
                string text;
                Form1.ValidTokens.TryDequeue(out text);
                Form1.ValidTokens.Enqueue(text);
                bool flag2 = !rDiscord.IsValidInvite(Client.Create(false, null, text), Form1._InviteURL);
                if (flag2)
                    MessageBox.Show(this.InviteURL.Text + " Is Invalid!", "Errmmm");
                else {
                    for (int i = 0; i < Form1.ValidTokens.Count; i++) {
                        new Thread(delegate () {
                            while (Form1.Running) { this.InviteThreaded(); }
                        }).Start();
                    }
                }
            }
        }
        public void PUTInviteThreaded() {
            try {
                string text;
                Form1.ValidTokens.TryDequeue(out text);
                Form1.ValidTokens.Enqueue(text);
                rDiscord.CustomPUTInviteFucker(Client.Create(false, null, text), _InviteURL);
                Form1.AmountJoined++;
                this.UsersJoinedInt.Text = Form1.AmountJoined.ToString();
            } catch { }
            Form1.Running = false;
            Thread.CurrentThread.Abort();
        }
        private void ChangePrefHex_Click(object sender, EventArgs e) {
            try {
                string text = this.EmbedColor.Text;
                int r = Convert.ToInt32(text.Split(new char[] {
                    ','
                })[0]);
                int g = Convert.ToInt32(text.Split(new char[] {
                    ','
                })[1]);
                int b = Convert.ToInt32(text.Split(new char[] {
                    ','
                })[2]);
                Globals.EmbedHexColor = new Discord.Color(r, g, b);
                ConsoleLog.Log("Embed color changed to " + this.EmbedColor.Text);
            } catch { ConsoleLog.Log("Invalid Decimal Color Code"); }
            Globals.Prefix = this.Prefix.Text;
            ConsoleLog.Log("[Console] Prefix changed to: " + Globals.Prefix);
        }

        private void Start_Click_1(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else
            {
                bool nomessage = this.richTextBox.Text == "Enter a Message" || this.richTextBox.Text == "";
                if (nomessage)
                    MessageBox.Show("Please enter a message", "Error");
                bool flag = this.ChannelID.Text == "Unknown" || this.ChannelID.Text == "";
                if (flag)
                    MessageBox.Show("ChannelID?\n(Idk where to send msgs) xD", "Error!");
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Have you invited the bots?", "Quick Question", MessageBoxButtons.YesNo);
                    bool flag2 = dialogResult != DialogResult.Yes;
                    if (flag2)
                        MessageBox.Show("Invite the bots before trying to spam!", "Error");
                    else
                    {
                        try {
                            bool flag3 = this.MessageToSend.Text == "∞";
                            if (flag3)
                                Form1._Msgstosend = 1000000;
                            else
                                Form1._Msgstosend = int.Parse(this.MessageToSend.Text);
                            Form1._ChannelID = this.ChannelID.Text;
                            Form1.MessageText = this.richTextBox.Text;
                        } catch { return; }
                        Form1._MsgsSent = 0;
                        Form1.Running = true;
                        this.OnOffStatus.BaseColor = System.Drawing.Color.Lime;
                        this.MessagesSent.Start();
                        for (int i = 0; i < this.boostisThread.Value; i++) {
                            new Thread(delegate () {
                                while (Form1.Running) { this.Spammer1(); }
                            }).Start();
                        }
                    }
                }
            }
        }
        private void AddallButt_Click(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else {
                bool flag = this.ServerID.Text == "Unknown" || this.ServerID.Text == "";
                if (flag)
                    MessageBox.Show("Please include a \"Server ID\"!", "Error!");
                else {
                    Form1.Running = true;
                    MessageBox.Show("Getting all members in guild!");
                    string text;
                    Form1.ValidTokens.TryDequeue(out text);
                    Form1.ValidTokens.Enqueue(text);
                    string[] Members = rDiscord.GetMembers(Client.Create(false, null, text), this.ServerID.Text);
                    string arg = (Members.Length > 200) ? "This may take awhile xD" : "";
                    MessageBox.Show(string.Format("Spamming {0} members with {1} Friend Req's\n{2}", Members.Length, Form1.ValidTokens.Count, arg));
                    new Thread(delegate () { this.MassPing1(Members);
                    }).Start();
                }
            }
        }
        private void MassPing1(string[] Members) {
            int num = 0;
            foreach (string user in Members) {
                bool flag = !Form1.Running;
                if (flag) { break; }
                foreach (string token in Form1.ValidTokens) {
                    try {
                        rDiscord.SendMassPingMessage(Client.Create(false, null, token), Form1._ChannelID, user, Form1.MessageText);
                        num++;
                    } catch { }
                }
            }
            Form1.Running = false;
            MessageBox.Show(string.Format("Added {0} users\nOn {1} accounts", num, Form1.ValidTokens.Count), "Added Users");
        }
        private void FRAllinserver(string[] Members) {
            int num = 0;
            foreach (string user in Members) {
                bool flag = !Form1.Running;
                if (flag) { break; }
                foreach (string token in Form1.ValidTokens) {
                    try {
                        rDiscord.AddFriend(Client.Create(false, null, token), user);
                        num++;
                    } catch { }
                }
            }
            Form1.Running = false;
            MessageBox.Show(string.Format("Added {0} users\nOn {1} accounts", num, Form1.ValidTokens.Count), "Added Users");
        }
        private void Stop_Click_1(object sender, EventArgs e) {
            Form1.Running = false;
            this.OnOffStatus.BaseColor = System.Drawing.Color.Red;
            ConsoleLog.Log("[Console] Stopped Task");
        }
        private void Invite_Click_1(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else {
                bool nodata = InviteURL.Text == "Insert URL" || InviteURL.Text == "";
                if (nodata)
                    MessageBox.Show("Invite URL hasn't been loaded.", "Error");
                else {
                    Form1.Running = true;
                    this.OnOffStatus.BaseColor = System.Drawing.Color.Lime;
                    try {
                        bool flag = this.InviteURL.Text == null || this.InviteURL.Text == "";
                        if (flag)
                            throw new Exception("Bad Invite URL");
                        Form1._InviteURL = this.InviteURL.Text.Replace("https://discord.gg/", "");
                    } catch { MessageBox.Show("Please include a invite URL!", "Error!");
                        return;
                    }
                    string text;
                    Form1.ValidTokens.TryDequeue(out text);
                    Form1.ValidTokens.Enqueue(text);
                    bool flag2 = !rDiscord.IsValidInvite(Client.Create(false, null, text), Form1._InviteURL);
                    if (flag2)
                        MessageBox.Show(this.InviteURL.Text + " Is Invalid!", "Errmmm");
                    else {
                        for (int i = 0; i < Form1.ValidTokens.Count; i++) {
                            new Thread(delegate () {
                                while (Form1.Running) {  this.InviteThreaded(); }
                            }).Start();
                        }
                    }
                }
            }
        }
        private void LeaveServer_Click_1(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else {
                bool flag = this.ServerID.Text == "Unknown" || this.ServerID.Text == "";
                if (flag)
                    MessageBox.Show("Please include a \"Server ID\"!", "Error!");
                else {
                    Form1.Running = true;
                    this.LoadTokens.Text = "Load Tokens";
                    for (int i = 0; i < Form1.ValidTokens.Count; i++) {
                        new Thread(delegate () {
                            while (Form1.Running) {  this.LeaveThreaded(); }
                        }).Start();
                    }
                    ConsoleLog.Log("[Console] Leaving Server");
                    MessageBox.Show("Watch as they \"Disappear\"");
                }
            }
        }
        private void LaggerButt_Click_1(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else {
                bool flag = this.ChannelID.Text == "Unknown" || this.ChannelID.Text == "";
                if (flag)
                    MessageBox.Show("ChannelID?\n(Idk where to send msgs) xD", "Error!");
                else {
                    DialogResult dialogResult = MessageBox.Show("Have you invited the bots?", "Quick Question", MessageBoxButtons.YesNo);
                    bool flag2 = dialogResult != DialogResult.Yes;
                    if (flag2)
                        MessageBox.Show("Invite the bots before trying to spam!", "You should, uhhhh");
                    else {
                        try {
                            string lag = ":chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains: :chains:";
                            bool flag3 = this.MessageToSend.Text == "∞";
                            if (flag3)
                                Form1._Msgstosend = 1000000;
                            else
                                Form1._Msgstosend = int.Parse(this.MessageToSend.Text);
                            Form1._ChannelID = this.ChannelID.Text;
                            Form1.MessageText = lag;
                        } catch { return; }
                        Form1._MsgsSent = 0;
                        Form1.Running = true;
                        this.MessagesSent.Start();
                        for (int i = 0; i < this.boostisThread.Value; i++) {
                            new Thread(delegate () {
                                while (Form1.Running) { this.Spammer1(); }
                            }).Start();
                        }
                    }
                }
            }
        }
        private void DmAllInGuild_Click_1(object sender, EventArgs e) => MessageBox.Show("Didnt have time to do this feature!", "Sorry");
        private void LoadTokens_Click_1(object sender, EventArgs e) {
            try {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    RestoreDirectory = true,
                    Multiselect = true,
                    Title = "Select Tokens...",
                    Filter = "Text|*.txt|All|*.*"
                };
                bool flag4 = Token.Count != 0 || !Token.IsEmpty;
                if (flag4)
                {
                    while (Token.Count != 0)
                    {
                        string text;
                        Token.TryDequeue(out text);
                    }
                }
                openFileDialog.ShowDialog();
                string[] array = File.ReadAllLines(openFileDialog.FileName);
                Form1._LoadedTokens = array.Length;
                this.LoadTokens.Text = Form1._LoadedTokens.ToString() + " Tokens";
                foreach (string item in array) {
                    Form1.ValidTokens.Enqueue(item);
                }
            } catch {
            }
        }
        private void ProxyButt_Click_1(object sender, EventArgs e) {
            bool flag = Form1.Proxy.Count > 0;
            if (flag)
                foreach (string text in Form1.Proxy) {
                    string text2;
                    Form1.Proxy.TryDequeue(out text2);
                } try {
                OpenFileDialog openFileDialog = new OpenFileDialog {
                    RestoreDirectory = true,
                    Multiselect = true,
                    Title = "Select Proxies...",
                    Filter = "Text|*.txt|All|*.*"
                };
                openFileDialog.ShowDialog();
                string[] array = File.ReadAllLines(openFileDialog.FileName);
                for (int i = 0; i < array.Length; i++) {
                    bool flag2 = array[i].Contains(":");
                    if (flag2)
                        Form1.Proxy.Enqueue(array[i]);
                }
                this.LoadedProxies.Text = Form1.Proxy.Count.ToString();
                Form1.Proxyless = false;
            } catch { }
        }
        private void ChangeNicks_Click_1(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else {
                bool flag = this.Nickname.Text == "";
                if (flag)
                    MessageBox.Show("Please include a \"Nickname\"!", "Error!");
                else {
                    string nick = this.Nickname.Text;
                    string server = this.ServerID.Text;
                    Form1.Running = true;
                    this.OnOffStatus.BaseColor = System.Drawing.Color.Lime;
                    new Thread(delegate () { this.ChangeNicknames(server, nick);
                    }).Start();
                    ConsoleLog.Log("[Console] Changed Nicknames");
                    this.OnOffStatus.BaseColor = System.Drawing.Color.Red;
                }
            }
        }
        private void ChangeStatus_Click_1(object sender, EventArgs e) {
            bool flag = this.ChangeStatusBtn.selectedIndex == 1;
            if (flag) foreach (string token in Form1.ValidTokens) { rDiscord.SetStatusTokenOnline(Client.Create(false, null, token)); }
            else {
                bool flag2 = this.ChangeStatusBtn.selectedIndex == 2;
                if (flag2)
                    foreach (string token in Form1.ValidTokens) { rDiscord.SetStatusTokenIdle(Client.Create(false, null, token)); }
                else {
                    bool flag3 = this.ChangeStatusBtn.selectedIndex == 3;
                    if (flag3) foreach (string token in Form1.ValidTokens) { rDiscord.SetStatusTokendnd(Client.Create(false, null, token)); }
                    bool flag4 = this.ChangeStatusBtn.selectedIndex == 4;
                    if (flag4)  foreach (string token in Form1.ValidTokens) {  rDiscord.SetStatusTokenOffline(Client.Create(false, null, token)); }
                }
            } ConsoleLog.Log("Status set to " + this.ChangeStatusBtn.Text);
        }
        private void SpamRqSingleUser_Click_1(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else {
                bool flag = this.UserIdText.Text.Length < 5 || this.UserIdText.Text.ToLower() == "Unknown";
                if (flag)
                    MessageBox.Show("Include a user to spam", "Error!");
                else {
                    Form1.Running = true;
                    this.OnOffStatus.BaseColor = System.Drawing.Color.Lime;
                    foreach (string token in Form1.ValidTokens) { rDiscord.AddFriend(Client.Create(false, null, token), this.UserIdText.Text); }
                    Form1.Running = false;
                    MessageBox.Show("Added user on each account!", "Finished");
                    this.OnOffStatus.BaseColor = System.Drawing.Color.Red;
                }
            }
        }
        private void SpamSIngleUser_Click_1(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else {
                bool flag = this.UserId.Text == "Unknown" || this.UserId.Text == "";
                if (flag)
                    MessageBox.Show("Please include a \"User ID\"!", "Error!");
                else {
                    bool flag2 = this.MessageToSend.Text == "" || this.MessageToSend.Text == null;
                    if (flag2)
                        MessageBox.Show("Please include a \"Message\"!", "Error!");
                    else {
                        try {
                            bool flag3 = this.MessageToSend.Text == "∞";
                            if (flag3)
                                Form1._Msgstosend = 1000000;
                            else { Form1._Msgstosend = int.Parse(this.MessageToSend.Text); }
                            Form1.MessageText = this.MessageToSend.Text;
                        } catch { return; }
                        Form1._MsgsSent = 0;
                        Form1.Running = true;
                        this.i = 0;
                        int num;
                        int i;
                        for (i = 0; i < Form1.ValidTokens.Count; i = num + 1) {
                            new Thread(delegate () { this.GetChannels(i);
                            }).Start();
                            num = i;
                        }
                        Thread.Sleep(2000);
                        this.MessagesSent.Start();
                        MessageBox.Show("Starting to spam user!", "Migraine");
                        Form1.MessageText = this.MessageToSend.Text;
                        Form1._MsgsSent = 0;
                        for (int j = 0; j < 10; j++) {
                            new Thread(delegate () {
                                while (Form1.Running) { this.SpamDmNigga(); } }).Start();
                        }
                    }
                }
            }
        }
        private void BetterASBypassButt_Click(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else {
                bool nodata = InviteURL.Text == "Insert URL" || InviteURL.Text == "";
                if (nodata)
                    MessageBox.Show("Invite URL hasn't been loaded.", "Error");
                else {
                    Form1.Running = true;
                    ConsoleLog.Log("[Console] Started Inviting Bots...");
                    try {
                        bool flag = this.InviteURL.Text == null || this.InviteURL.Text == "";
                        if (flag) { throw new Exception("Bad Invite URL"); }
                        Form1._InviteURL = this.InviteURL.Text.Replace("https://discord.gg/", "");
                    } catch {
                        MessageBox.Show("Please include a invite URL!", "Error!");
                        return;
                    }
                    string text;
                    Form1.ValidTokens.TryDequeue(out text);
                    Form1.ValidTokens.Enqueue(text);
                    bool flag2 = !rDiscord.IsValidInvite(Client.Create(false, null, text), Form1._InviteURL);
                    if (flag2) {  MessageBox.Show(this.InviteURL.Text + " Is Invalid!", "Errmmm"); }
                    else {
                        for (int i = 0; i < Form1.ValidTokens.Count; i++) {
                            new Thread(delegate () {
                                while (Form1.Running) { this.InviteThreaded(); } }).Start();
                        }
                    }
                }
            }
        }
        private void RemoveFR_Click_1(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else {
                bool flag = this.UserIdText.Text.Length < 5 || this.UserIdText.Text.ToLower() == "Unknown";
                if (flag)
                    MessageBox.Show("Include a user to remove", "Error!");
                else {
                    Form1.Running = true;
                    this.OnOffStatus.BaseColor = System.Drawing.Color.Lime;
                    foreach (string token in Form1.ValidTokens) { rDiscord.RemoveFriend(Client.Create(false, null, token), this.UserIdText.Text); }
                    Form1.Running = false;
                    MessageBox.Show("Removed user on each account!", "Finished");
                    this.OnOffStatus.BaseColor = System.Drawing.Color.Red;
                }
            }
        }
        private void Startt_Click_1(object sender, EventArgs e) {
            Globals.CustomRPC = true;
            Form1.json["client"] = this.Client_ID.Text;
            Form1.json["state"] = this.State.Text;
            Form1.json["details"] = this.Details.Text;
            Form1.json["imageText"] = this.ImageText.Text;
            Form1.json["imageAsset"] = this.ImageAsset.Text;
            string contents = JsonConvert.SerializeObject(Form1.json, Formatting.Indented);
            File.WriteAllText("RPC.json", contents);
            RpcControl.Shutdown();
            try {
                RpcControl.Initialize(this.Client_ID.Text);
                RpcControl.UpdatePresence(this.State.Text, this.Details.Text, int.Parse(this.PartySize.Text), int.Parse(this.PartyMax.Text), this.ImageAsset.Text, this.ImageText.Text, true);
                MessageBox.Show("Successfully changed RPC.");
            } catch {  MessageBox.Show("Error Starting / Updating RPC", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }
        private void Stopp_Click_1(object sender, EventArgs e) => Globals.CustomRPC = false;
        private void TokenLoad_Click(object sender, EventArgs e) {
            bool flag = Form1.Token.Count > 0;
            if (flag)
                foreach (string text in Form1.Token) {
                    string text2;
                    Form1.Token.TryDequeue(out text2);
                } try {
                OpenFileDialog openFileDialog = new OpenFileDialog {
                    RestoreDirectory = true,
                    Multiselect = true,
                    Title = "Select Tokens...",
                    Filter = "Text|*.txt|All|*.*"
                };
                openFileDialog.ShowDialog();
                string[] array = File.ReadAllLines(openFileDialog.FileName);
                for (int i = 0; i < array.Length; i++) {
                    bool flag2 = array[i].Length > 5;
                    if (flag2)
                        Form1.Token.Enqueue(array[i]);
                }
                Form1._LoadedTokens = Form1.Token.Count;
                this.LoadedTokens.Text = Form1._LoadedTokens.ToString();
                this.LoadTokens.Text = this.LoadedTokens.Text + " Loaded";
            } catch { }
        }
        private void LoadProxyt_Click(object sender, EventArgs e) {
            bool flag = Form1.Proxy.Count > 0;
            if (flag)
                foreach (string text in Form1.Proxy) {
                    string text2;
                    Form1.Proxy.TryDequeue(out text2);
                } try {
                OpenFileDialog openFileDialog = new OpenFileDialog {
                    RestoreDirectory = true,
                    Multiselect = true,
                    Title = "Select Proxies...",
                    Filter = "Text|*.txt|All|*.*"
                };
                openFileDialog.ShowDialog();
                string[] array = File.ReadAllLines(openFileDialog.FileName);
                for (int i = 0; i < array.Length; i++) {
                    bool flag2 = array[i].Contains(":");
                    if (flag2)
                        Form1.Proxy.Enqueue(array[i]);
                }
                this.LoadedProxies.Text = Form1.Proxy.Count.ToString();
                this.LoadProxyt.Text = this.LoadedProxies.Text + " Loaded";
                Form1.Proxyless = false;
            } catch { }
        }
        private void StartChecker_Click(object sender, EventArgs e) {
            bool flag = Form1.Token.Count == 0;
            if (flag)
                MessageBox.Show("Load Tokens Dumbass", "Error");
            else {
                Form1.Running = true;
                this.LabelUpdate.Start();
                new Thread(delegate () { this.Threading(); }).Start();
            }
        }
        private void StopChecker_Click(object sender, EventArgs e) => Form1.Running = false;
        private void SaveAllTkn_Click(object sender, EventArgs e) {
            bool flag = !Directory.Exists("Results");
            if (flag)
                Directory.CreateDirectory("Results");
            string text = "Results/Tokens/Checked On-" + DateTime.Now.ToString("hh-mm tt") + "/";
            Directory.CreateDirectory(text);
            string contents = string.Join("\n", Form1._ValidTokens);
            File.AppendAllText(text + "Valid Tokens.txt", contents);
            MessageBox.Show(string.Format("Saved {0} Tokens to\n{1}Valid Tokens.txt", Form1._ValidTokens.Count, text), "Tokens Saved!");
        }
        private void LabelUpdate_Tick(object sender, EventArgs e) {
            bool flag = !Form1.Running;
            if (flag) { this.LabelUpdate.Stop(); }
            this.CheckedTokens.Text = (Form1._Valids + Form1._Invalids).ToString();
            this.Valids.Text = Form1._Valids.ToString();
            this.Invalids.Text = Form1._Invalids.ToString();
            this.Retries.Text = Form1._Retries.ToString();
            try { this.valid.Text = string.Join("\n", Form1._ValidTokens); }
            catch { }
        }
        private void Threading() {
            for (int i = 0; i < 20; i++) {
                new Thread(delegate () {
                    while (Form1.Running) {
                        bool proxyless = Form1.Proxyless;
                        if (proxyless)
                            this.Checker();
                        else
                            this.CheckerProxy();
                    }
                }).Start();
            }
        }
        private void Checker() {
            bool flag = Form1._Valids + Form1._Invalids >= Form1._LoadedTokens || !Form1.Running;
            if (flag) { Form1.Running = false;
                Thread.CurrentThread.Abort();
            }
            string text;
            Form1.Token.TryDequeue(out text);
            bool flag2 = text == null || text == "";
            if (flag2) { Form1._Retries++;
                Thread.Sleep(5000);
            }
            else { try {
                    string a = rDiscord.IfValidAccount(Client.Create(false, null, text));
                    bool flag3 = a == "valid";
                    if (flag3) { Form1._Valids++;
                        Form1._ValidTokens.Add(text);
                    } else {
                        bool flag4 = a == "invalid";
                        if (flag4) { Form1._Invalids++; }
                        else {
                            bool flag5 = a == "throttled";
                            if (flag5) { Form1.Running = false; }
                            else { Form1.Token.Enqueue(text);
                                Form1._Retries++;
                            }
                        }
                    }
                } catch (Exception ex) { Console.WriteLine(ex.ToString() + "Exception on catch"); }
            }
        }
        private void CheckerProxy() {
            bool flag = Form1._Valids + Form1._Invalids >= Form1._LoadedTokens || !Form1.Running;
            if (flag)  { Form1.Running = false;
                Thread.CurrentThread.Abort();
            }
            string text;
            Form1.Token.TryDequeue(out text);
            bool flag2 = text == null || text == "";
            if (flag2) { Form1._Retries++;
                Thread.Sleep(5000);
            } else { try {
                    string text2 = "";
                    try {
                        Form1.Proxy.TryDequeue(out text2);
                        bool flag3 = text2 != null;
                        if (flag3)
                            Form1.Proxy.Enqueue(text2);
                    } catch { return; }
                    bool flag4 = text2 == null;
                    if (!flag4) {
                        string a = rDiscord.IfValidAccount(Client.Create(true, text2, text));
                        bool flag5 = a == "valid";
                        if (flag5) { Form1._Valids++;
                            Form1._ValidTokens.Add(text);
                        } else {
                            bool flag6 = a == "invalid";
                            if (flag6) { Form1._Invalids++; }
                            else { Form1.Token.Enqueue(text);
                                Form1._Retries++;
                            }
                        }
                    }
                } catch (Exception ex) { Console.WriteLine(ex.ToString() + "Exception on catch"); }
            }
        }
        private void JoinVoiceChannelButton_Click(object sender, EventArgs e) {
            //bool running = Form1.Running;
            //if (running)
            //{
            //    MessageBox.Show("Stop current task first!", "Error!");
            //}
            //else
            //{
            //    bool noid = this.voiceChannelID.Text == "Insert ID" || this.voiceChannelID.Text == "";
            //    if (noid)
            //    {
            //        MessageBox.Show("Please include a \"Voice Channel ID\"!", "Error!");
            //    }
            //    bool noserver = this.ServerID.Text == "Insert ID" || this.ServerID.Text == "";
            //    if (noserver)
            //    {
            //        MessageBox.Show("Please include a \"Server ID\"!", "Error!");
            //    }
            //    else
            //    {

            //        for (int j = 0; j < 10; j++)
            //        {
            //            new Thread(delegate ()
            //            {
            //                string message = this.richTextBox.Text;
            //                Form1.Running = true;
            //                foreach (string token in Form1.ValidTokens)
            //                {
            //                    rCommands.Streaming(message);/*(Client.Create(false, null, token)*/
            //                }
            //            }).Start();
            //        }
            //    }
            //}
        }

        private void GetToken_Click_1(object sender, EventArgs e) {
            DialogResult dialogResult = MessageBox.Show("This will close \"Discord\"\nContinue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            bool flag = dialogResult != DialogResult.Yes;
            if (!flag)
                this.UserToken.Text = Usertoken.Get();
        }
        private void ResetNick_Click(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running) { MessageBox.Show("Stop current task first!", "Error!"); }
            else {
                string server = this.ServerID.Text;
                Form1.Running = true;
                new Thread(delegate () { this.ResetNicknames(server); }).Start();
                ConsoleLog.Log("[Console] Changed Nicknames back to Default");
            }
        }
        public void ResetNicknames(string ServerID) {
            int num = 0;
            foreach (string token in Form1.ValidTokens) {
                try { rDiscord.ResetNick(Client.Create(false, null, token), ServerID);
                    num++;
                } catch { }
            }
            Form1.Running = false;
            MessageBox.Show(string.Format("Changed nick back to Default on {0} accounts!", num), "Done!");
        }
        private void ClearChat_Click(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else {
                bool flag = this.ChannelID.Text == "Unknown" || this.ChannelID.Text == "";
                if (flag)
                    MessageBox.Show("ChannelID?\n(Idk where to send msgs) xD", "Error!");
                else {
                    DialogResult dialogResult = MessageBox.Show("Have you invited the bots?", "Quick Question", MessageBoxButtons.YesNo);
                    bool flag2 = dialogResult != DialogResult.Yes;
                    if (flag2)
                        MessageBox.Show("Invite the bots before trying to spam!", "You should, uhhhh");
                    else {
                        try {
                            string lag = "_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n_ _\n _ _\n _ _\n _ _\n _ _\n";
                            bool flag3 = this.MessageToSend.Text == "∞";
                            if (flag3)
                                Form1._Msgstosend = 1000000;
                            else {
                                Form1._Msgstosend = int.Parse(this.MessageToSend.Text);
                            }
                            Form1._ChannelID = this.ChannelID.Text;
                            Form1.MessageText = lag;
                        } catch {
                            return;
                        }
                        Form1._MsgsSent = 0;
                        Form1.Running = true;
                        this.MessagesSent.Start();
                        for (int i = 0; i < this.boostisThread.Value; i++) {
                            new Thread(delegate () {
                                while (Form1.Running) { this.Spammer1(); } }).Start();
                        }
                    }
                }
            }
        }
        private void ChangeAV_Click(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
            {
                MessageBox.Show("Stop current task first!", "Error!");
            }
            else
            {
                bool flag = this.ChannelID.Text == null;
                if (flag)
                {
                    MessageBox.Show("Include a Avatar Link to Change Avatars", "Error!");
                }
                else
                {
                    Form1.Running = true;
                    foreach (string token in Form1.ValidTokens)
                    {
                        rDiscord.ChangeAV(Client.Create(false, null, token), image);
                    }
                    Form1.Running = false;
                    MessageBox.Show("Changed Avatars on each account!", "Finished");
                }
            }
        }
        private void MassPing_Click(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else {
                bool flag = this.ChannelID.Text == "Unknown" || this.ChannelID.Text == "";
                if (flag)
                    MessageBox.Show("ChannelID?\n(Idk where to send msgs) xD", "Error!");
                else {
                    DialogResult dialogResult = MessageBox.Show("Have you invited the bots?", "Quick Question", MessageBoxButtons.YesNo);
                    bool flag2 = dialogResult != DialogResult.Yes;
                    if (flag2)
                        MessageBox.Show("Invite the bots before trying to spam!", "Error");
                    else {
                        try {
                            bool flag3 = this.MessageToSend.Text == "∞";
                            if (flag3)
                                Form1._Msgstosend = 1000000;
                            else {
                                Form1._Msgstosend = int.Parse(this.MessageToSend.Text);
                            }
                            Form1._ChannelID = this.ChannelID.Text;
                            Form1.MessageText = this.richTextBox.Text;
                        } catch {
                            return;
                        }
                        Form1._MsgsSent = 0;
                        Form1.Running = true;
                        string text;
                        ValidTokens.TryDequeue(out text);
                        ValidTokens.Enqueue(text);
                        string[] Members = rDiscord.GetMembers(Client.Create(false, null, text), this.ServerID.Text);
                        string arg = (Members.Length > 100) ? "This may take awhile xD" : "";
                        MessageBox.Show(string.Format("Started Mass Pinging the guild\n{2}", Members.Length, ValidTokens.Count, arg));
                        new Thread(delegate () { this.FRAllinserver(Members); }).Start();
                    }
                }
            }
        }
        private void NitroUpdate_Tick(object sender, EventArgs e) {
            bool ifnotrunning = !Form1.NitroRunning;
            if (ifnotrunning) { this.NitroUpdate.Stop(); } 
            this.ValidNitro.Text = Globals.ValidNitro.ToString();
            this.InvalidNitro.Text = Globals.InvalidNitro.ToString();
        }
        [Obsolete]
        private void StartNitro_Click(object sender, EventArgs e) {
            bool notoken = this.NitroToken.Text == null || this.NitroToken.Text == "Insert Token" || this.NitroToken.Text == "";
            if (notoken)
                MessageBox.Show("Please insert your token.", "Error");
            bool selfbotRunning = Globals.NitroSniperRunning;
            if (selfbotRunning)
                MessageBox.Show("Nitro Sniper is already running!", "Error");
            else {
                StartNitroSniper.Token = this.NitroToken.Text;
                this.NitroUpdate.Start();
                this.ConstantlyRun1.Start();
                TimeElapsed.Start();
                StartNitroSniper.Init();
                ConsoleLog.Log("[Console] Nitro Sniper Starting...");
                Globals.NitroSniperRunning = true;
                Globals.SelfbotUser = this.DiscordUser.Text;
            }
        }
        private void StopNitro_Click(object sender, EventArgs e) {
            ConsoleLog.Log("[Console] Stopped Task");
            Form1.NitroRunning = false;
            Globals.NitroSniperRunning = false;
            this.ConstantlyRun1.Enabled = false;
        }
        private void ConstantlyRun1_Tick(object sender, EventArgs e) {
            LogConsole.Text = ConsoleLog.LogInfo;
            TimeSpan elapsed = Form1.TimeElapsed.Elapsed;
            this.TimeSinceStart1.Text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", new object[] {
                elapsed.Hours,
                elapsed.Minutes,
                elapsed.Seconds,
                elapsed.Milliseconds / 10
            });
        }
        private void TypingButt_Click(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else {
                string channel = this.ChannelID.Text;
                Form1.Running = true;
                new Thread(delegate () { this.TypingSut(channel); }).Start();
                ConsoleLog.Log("[Console] Started Typing on all Tokens");
            }
        }
        public void TypingSut(string ChannelID) {
            int num = 0;
            foreach (string token in Form1.ValidTokens) {
                try {
                    rDiscord.Typing(Client.Create(false, null, token), ChannelID);
                    num++;
                } catch { }
            }
            Form1.Running = false;
            MessageBox.Show(string.Format("Started typing on {0} tokens.", num), "Done!");
        }
        private void React_Click(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else {
                string channel = this.ChannelID.Text;
                string message = this.MessageID.Text;
                string emoji = this.EmojiID.Text;
                Form1.Running = true;
                for (int i = 0; i < this.boostisThread.Value; i++) {
                    new Thread(delegate () { this.ReactSut(channel, message, emoji); }).Start();
                }
            }
        }
        public void ReactSut(string ChannelID, string MessageID, string EmojiID) {
            int num = 0;
            foreach (string token in Form1.ValidTokens) {
                try {
                    rDiscord.React(Client.Create(false, null, token), ChannelID, MessageID, EmojiID);
                    num++;
                } catch { }
            }
            Form1.Running = false;
            MessageBox.Show(string.Format("Started Reacting on {0} Tokens!", num), "Done!");
        }
        private void AuditSpam_Click(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else {
                string channelid = this.ChannelID.Text;
                Form1.Running = true;
                new Thread(delegate () { this.Audit(channelid); }).Start();
                ConsoleLog.Log("[Console] Started reacting on all Tokens");
            }
        }
        public void Audit(string Chanel) {
            int num = 0;
            foreach (string token in Form1.ValidTokens) {
                try {
                    rDiscord.AuditSpammer(Client.Create(false, null, token), Chanel);
                    num++;
                } catch { }
            }
            Form1.Running = false;
            MessageBox.Show(string.Format("Started Audit Log Spamming"), "Done!");
        }
        private void SpotifyInviteButt_Click(object sender, EventArgs e) {
            bool running = Form1.Running;
            foreach (string token in Form1.ValidTokens) {
                if (running)
                    MessageBox.Show("Stop current task first!", "Error!");
                else {
                    string channelid = this.ChannelID.Text;
                    string partyid = this.PartyID.Text;
                    string session = this.SessionID.Text;
                    Form1.Running = true;
                    new Thread(delegate () { this.Spotify(); }).Start();
                }
            }
        }
        public void Spotify() {
            int num = 0;
            foreach (string token in Form1.ValidTokens) {
                try {
                    rDiscord.SpotifyInviteSpam(Client.Create(false, null, token));
                    num++;
                } catch { }
            }
            Form1.Running = false;
        }
        private void MultiChannelButt_Click(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running) { MessageBox.Show("Stop current task first!", "Error!"); }
            else {
                bool nomessage = this.richTextBox.Text == "Enter a Message" || this.richTextBox.Text == "";
                if (nomessage) { MessageBox.Show("Please enter a message", "Error"); }
                bool flag = this.ChannelID.Text == "Unknown" || this.ChannelID.Text == "";
                if (flag) { MessageBox.Show("ChannelID?\n(Idk where to send msgs) xD", "Error!"); }
                bool channel2 = this.ChannelID2.Text == "Unknown" || this.ChannelID2.Text == "";
                if (channel2) { MessageBox.Show("Missing ID for the Second Channnel ID", "Error!"); }
                else {
                    DialogResult dialogResult = MessageBox.Show("Have you invited the bots?", "Quick Question", MessageBoxButtons.YesNo);
                    bool flag2 = dialogResult != DialogResult.Yes;
                    if (flag2)
                        MessageBox.Show("Invite the bots before trying to spam!", "Error");
                    else {
                        try {
                            bool flag3 = this.MessageToSend.Text == "∞";
                            if (flag3)
                                Form1._Msgstosend = 1000000;
                            else
                                Form1._Msgstosend = int.Parse(this.MessageToSend.Text);
                            Form1._ChannelID = this.ChannelID.Text;
                            Form1._ChannelID2 = this.ChannelID2.Text;
                            Form1.MessageText = this.richTextBox.Text;
                        } catch {
                            return;
                        }
                        Form1._MsgsSent = 0;
                        Form1.Running = true;
                        this.OnOffStatus.BaseColor = System.Drawing.Color.Lime;
                        this.MessagesSent.Start();
                        for (int i = 0; i < this.boostisThread.Value; i++) {
                            new Thread(delegate () {
                                while (Form1.Running) { this.MultiChannel(); } }).Start();
                        }
                    }
                }
            }
        }
        public void MultiChannel() {
            bool flag = !Form1.Running;
            if (flag)
                Thread.CurrentThread.Abort();
            bool flag2 = Form1._MsgsSent >= Form1._Msgstosend;
            if (flag2) { Form1.Running = false; }
            bool running = Form1.Running;
            if (running) { try {
                    string text;
                    Form1.ValidTokens.TryDequeue(out text);
                    bool flag3 = text != null && text != "";
                    if (!flag3) { return; }
                    Form1.ValidTokens.Enqueue(text);
                    int num = rDiscord.SendMultiChannelMessage(Client.Create(false, null, text), Form1._ChannelID, Form1._ChannelID2, Form1.MessageText);
                    bool flag4 = num == 1;
                    if (flag4) {
                        this.tmessagessent.Text = Form1._MsgsSent.ToString();
                        Form1._MsgsSent++;
                    } else
                    Thread.Sleep(1000);
                } catch { }
            }
            Thread.Sleep(1);
        }
        private void BASBypassButt_Click(object sender, EventArgs e) {
            bool running = Form1.Running;
            if (running) { MessageBox.Show("Stop current task first!", "Error!"); }
            else {
                bool nodata = InviteURL.Text == "Insert URL" || InviteURL.Text == "";
                if (nodata) { MessageBox.Show("Invite URL hasn't been loaded.", "Error"); }
                else {
                    Form1.Running = true;
                    this.OnOffStatus.BaseColor = System.Drawing.Color.Lime;
                    try {
                        bool flag = this.InviteURL.Text == null || this.InviteURL.Text == "";
                        if (flag) { throw new Exception("Bad Invite URL"); }
                        Form1._InviteURL = this.InviteURL.Text.Replace("https://discord.gg/", "");
                    } catch { MessageBox.Show("Please include a invite URL!", "Error!");
                        return;
                    }
                    string text;
                    Form1.ValidTokens.TryDequeue(out text);
                    Form1.ValidTokens.Enqueue(text);
                    bool flag2 = !rDiscord.IsValidInvite(Client.Create(false, null, text), Form1._InviteURL);
                    if (flag2) { MessageBox.Show(this.InviteURL.Text + " Is Invalid!", "Errmmm");}
                    else {
                        for (int i = 0; i < Form1.ValidTokens.Count; i++) {
                            new Thread(delegate () { while (Form1.Running) { this.InviteThreaded(); } }).Start();
                        }
                    }
                }
            }
        }
        public void BASBypass(string Invite) {
            try {
                string text;
                Form1.ValidTokens.TryDequeue(out text);
                Form1.ValidTokens.Enqueue(text);
                HttpResponseMessage httpResponseMessage = rDiscord.JoinServer(Client.Create(false, null, text), Form1._InviteURL);
                bool flag = httpResponseMessage.ToString().Contains("StatusCode: 200, ReasonPhrase: 'OK'");
                if (flag) { Form1.AmountJoined++; }
                this.UsersJoinedInt.Text = Form1.AmountJoined.ToString();
            } catch { }
            Form1.Running = false;
            Thread.CurrentThread.Abort();
        }
        private void BunifuFlatButton2_Click(object sender, EventArgs e) => this.ClearAllTxt();
        private void MaximizeButt_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Maximized;
        private void Stream_Click(object sender, EventArgs e)
        {
            //bool running = Form1.Running;
            //if (running)
            //    MessageBox.Show("Stop current task first!", "Error!");
            //else
            //{
            //    string Message = this.richTextBox.Text;
            //    Form1.Running = true;
            //    new Thread(delegate () {
            //        rCommands.ASCII(rStart.Token), Message;
            //    }).Start();
            //    // ConsoleLog.Log("[Console] Started reacting on all Tokens");
            //}
        }
        public void Embed1(string ChannelID) {
            //int num = 0;
            //foreach (string token in Form1.ValidTokens)
            //{
            //    try
            //    {
            //        rDiscord.Status(Client.Create(false, null, token), ChannelID);
            //        num++;
            //    }
            //    catch
            //    {
            //    }
            //}
            //Form1.Running = false;
            //MessageBox.Show(string.Format("Started typing on {0} tokens.", num), "Done!");
        }
        public static bool GetUser = false;
        public static string Token1;
        public static bool Running;
        public static bool NitroRunning;
        public static int _LoadedTokens;
        public static string _InviteURL;
        public static int AmountJoined;
        public static int _Msgstosend;
        public static int _MsgsSent;
        public static string _ChannelID;
        public static string _ChannelID2;
        public static string MessageText;
        public static bool is429 = false;
        public static int UsersLeft;
        private int i = 0;
        public static bool StatusUpdaterRunning;
        public static int _Valids;
        public static int _Invalids;
        public static int _Retries;
        public static bool Proxyless = true;
        public static JObject json;
        public static ConcurrentQueue<string> ValidTokens = new ConcurrentQueue<string>();
        public static ConcurrentQueue<string> Proxy = new ConcurrentQueue<string>();
        public static ConcurrentQueue<string> Token = new ConcurrentQueue<string>();
        public static Stopwatch TimeElapsed = new Stopwatch();
        public static List<string> _UsersJoined = new List<string>();
        public static List<string> ChannelIDS = new List<string>();
        public static List<string> _ValidTokens = new List<string>();
        public static List<string> _MsgsSent2 = new List<string>();
        public static List<byte> image = new List<byte>();

        private void LoadAvatar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                RestoreDirectory = true,
                Multiselect = false,
                Title = "Select an Image...",
                Filter = "*.png|*.*"
            };
            openFileDialog.ShowDialog();
            byte[] img = File.ReadAllBytes(openFileDialog.FileName);
            foreach (byte image1 in img)
            {
                image.Add(image1);
            }
        }

        private void RandomMessage_Click(object sender, EventArgs e)
        {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Error!");
            else
            {
                bool flag = this.ChannelID.Text == "Unknown" || this.ChannelID.Text == "";
                if (flag)
                    MessageBox.Show("ChannelID?\n(Idk where to send msgs) xD", "Error!");
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Have you invited the bots?", "Quick Question", MessageBoxButtons.YesNo);
                    bool flag2 = dialogResult != DialogResult.Yes;
                    if (flag2)
                        MessageBox.Show("Invite the bots before trying to spam!", "You should, uhhhh");
                    else
                    {
                        try
                        {
                            bool flag3 = this.MessageToSend.Text == "∞";
                            if (flag3)
                                Form1._Msgstosend = 1000000;
                            else
                            {
                                Form1._Msgstosend = int.Parse(this.MessageToSend.Text);
                            }
                            Form1._ChannelID = this.ChannelID.Text;
                            
                        }
                        catch
                        {
                            return;
                        }
                        Form1._MsgsSent = 0;
                        Form1.Running = true;
                        this.MessagesSent.Start();
                        for (int i = 0; i < this.boostisThread.Value; i++)
                        {
                            new Thread(delegate ()
                            {
                                while (Form1.Running)
                                {
                                    this.RandomSpam();
                                }
                            }).Start();
                        }
                    }
                }
            }
        }
        public void RandomSpam()
        {
            bool flag = !Form1.Running;
            if (flag)
                Thread.CurrentThread.Abort();
            bool flag2 = Form1._MsgsSent >= Form1._Msgstosend;
            if (flag2)
                Form1.Running = false;
            bool running = Form1.Running;
            if (running)
            {
                try
                {
                    string text;
                    Form1.ValidTokens.TryDequeue(out text);
                    bool flag3 = text != null && text != "";
                    if (!flag3) { return; }
                    Form1.ValidTokens.Enqueue(text);
                    int num = rDiscord.RandomMessage(Client.Create(false, null, text), Form1._ChannelID);
                    bool flag4 = num == 1;
                    if (flag4)
                    {
                        this.tmessagessent.Text = Form1._MsgsSent.ToString();
                        Form1._MsgsSent++;
                    }
                    else {
                        Thread.Sleep(1000);
                    }
                }
                catch { }
            }
            Thread.Sleep(1);
        }

        private void EmbedPanelExit_Click(object sender, EventArgs e) => this.EmbedPanel.Visible = false;

        private void EmbedMenu_Click(object sender, EventArgs e) => this.EmbedPanel.Visible = true;

        private void StartEmbedSpam_Click(object sender, EventArgs e)
        {
            bool running = Form1.Running;
            if (running)
                MessageBox.Show("Stop current task first!", "Migraine");
            else
            {
                bool flag = this.ChannelID.Text == "Unknown" || this.ChannelID.Text == "";
                if (flag)
                    MessageBox.Show("ChannelID?\n(Idk where to send msgs) xD", "Migraine");
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Have you invited the bots?", "Quick Question", MessageBoxButtons.YesNo);
                    bool flag2 = dialogResult != DialogResult.Yes;
                    if (flag2)
                        MessageBox.Show("Invite the bots before trying to spam!", "Migraine");
                    else
                    {
                        try
                        {
                            bool flag3 = this.MessageToSend.Text == "∞";
                            if (flag3)
                                Form1._Msgstosend = 1000000;
                            else
                            {
                                Form1._Msgstosend = int.Parse(this.MessageToSend.Text);
                            }
                            Form1._ChannelID = this.ChannelID.Text;
                        }
                        catch
                        {
                            return;
                        }
                        Form1._MsgsSent = 0;
                        Form1.Running = true;
                        this.MessagesSent.Start();
                        for (int i = 0; i < this.boostisThread.Value; i++)
                        {
                            new Thread(delegate ()
                            {
                                while (Form1.Running)
                                {
                                    this.EmbedSpam();
                                }
                            }).Start();
                        }
                    }
                }
            }
        }
        public void EmbedSpam()
        {
            bool flag = !Form1.Running;
            if (flag)
                Thread.CurrentThread.Abort();
            bool flag2 = Form1._MsgsSent >= Form1._Msgstosend;
            if (flag2)
                Form1.Running = false;
            bool running = Form1.Running;
            if (running)
            {
                try
                {
                    string channel = Form1._ChannelID;
                    ulong channelid = Convert.ToUInt64(channel);
                    string text;
                    Form1.ValidTokens.TryDequeue(out text);
                    bool flag3 = text != null && text != "";
                    if (!flag3) { return; }
                    Form1.ValidTokens.Enqueue(text);
                    int ready = 0;
                    bool ready1 = Convert.ToBoolean(ready);
                    ready1 = rDiscord.SpawnEmbed(Client.Create(false, null, text), channelid, EmbedTitle.Text, this.EmbedText.Text, null);
                    bool flag4 = ready1;
                    if (flag4)
                    {
                        this.tmessagessent.Text = Form1._MsgsSent.ToString();
                        Form1._MsgsSent++;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch { }
            }
            Thread.Sleep(1);
        }

        private void Revert_Click(object sender, EventArgs e)
        {
             
        }

        private void DiscordButton_clicked(object sender, EventArgs e)
        {
            this.ClearPages();
            DiscordPanel.Visible = true;
        }

        private void InjectLabel_Clicked(object sender, EventArgs e)
        {
            DiscordPanel.Visible = false;
        }

        private void InjectButton_Clicked(object sender, EventArgs e)
        {
            DiscordPanel.Visible = false;
        }
    }
}
