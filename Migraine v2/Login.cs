using System;
using System.Diagnostics;
using System.Windows.Forms;
using DiscordRPC;
using DiscordRPC.Logging;

namespace Migraine_v2 {
    public partial class Login : Form {
        public static DiscordRpcClient RPCClient;

        public Login()
        {
            InitializeComponent();
            //try
            //{
            //    RPCClient = new DiscordRpcClient("685768251698970676");
            //    RPCClient.Logger = new ConsoleLogger
            //    {
            //        Level = LogLevel.Trace
            //    };
            //    RPCClient.SetPresence(new RichPresence
            //    {
            //        Details = "",
            //        State = "Logging in...",
            //        Assets = new Assets
            //        {
            //            LargeImageKey = "migraine_logo",
            //            LargeImageText = "Migraine FTW"
            //        }
            //    });
            //    RPCClient.Initialize();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //}
        }
        private void Exitbutton_Click(object sender, EventArgs e) => Application.Exit();
        private void Minimizebutton_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        private void CreateAccount_Click(object sender, EventArgs e) {
            if (CreateAccount.Text.Contains("Register")) {
                register1.Visible = true;
                doLogin2.Visible = false;
                CreateAccount.Text = "Login";
                //RPCClient.SetPresence(new RichPresence
                //{
                //    Details = "",
                //    State = "Registering new account...",
                //    Assets = new Assets
                //    {
                //        LargeImageKey = "migraine_logo",
                //        LargeImageText = "Migraine FTW"
                //    }
                //});
            }
            else {
                register1.Visible = false;
                doLogin2.Visible = true;
                CreateAccount.Text = "Register Account";
                //RPCClient.SetPresence(new RichPresence
                //{
                //    Details = "",
                //    State = "Logging in...",
                //    Assets = new Assets
                //    {
                //        LargeImageKey = "migraine_logo",
                //        LargeImageText = "Migraine FTW"
                //    }
                //});
            }
        }
        private void LinkButton_Click(object sender, EventArgs e) {
            Process.Start("https://t.me/OfficialMigraine");
            Process.Start("https://discord.gg/JW5t7zV");
        }
    }
}
