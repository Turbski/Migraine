using System;
using System.Diagnostics;
using System.Windows.Forms;
using DiscordRPC;

namespace Migraine_v2 {
    public partial class Login : Form {
        public Login()
        {

            InitializeComponent();
            if (!clientRPC.IsInitialized) clientRPC.Initialize();
        }
        public static DiscordRpcClient clientRPC = new DiscordRpcClient("684562656023150696"); // fill this out bud
        private static RichPresence loginPresence = new RichPresence()
        {
            State = "Logging in...",
            Timestamps = Timestamps.Now,
            Assets = new Assets()
            {
                LargeImageKey = "logo",
            }
        };
        private void Exitbutton_Click(object sender, EventArgs e) => Application.Exit();
        private void Minimizebutton_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        private void CreateAccount_Click(object sender, EventArgs e) {
            if (CreateAccount.Text.Contains("Register")) {
                register1.Visible = true;
                doLogin1.Visible = false;
                CreateAccount.Text = "Login";
            } else {
                register1.Visible = false;
                doLogin1.Visible = true;
                CreateAccount.Text = "Register Account";
            }
        }
        private void LinkButton_Click(object sender, EventArgs e) {
            Process.Start("https://t.me/OfficialMigraine");
            Process.Start("https://discord.gg/JW5t7zV");
        }
    }
}
