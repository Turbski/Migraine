using System;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
using Migraine_v2.LoginClass;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using DiscordRPC;

namespace Migraine_v2.Registration
{
    public partial class doLogin : UserControl
    {
        public const string ProgramId = "235";
        private const string Secret = "TOYEPUK47IR5";
        public static string _Username;
        public doLogin() {
            InitializeComponent();
            Settings.getSettings();
            if (Settings._Username != "")
            {
                Username.Text = Settings._Username;
                Password.Text = Settings._Password;
            }
        }
        private void LoginButt_Click_1(object sender, EventArgs e)
        {
            string username = Username.Text;
            string password = Password.Text;
            if (CheckFiddler._())
                return;
            try
            {
                string HWID = AuthHow.HWID.Value();
                string Resp = new WebClient().DownloadString($"http://www.auth.how/API/User/Login?User={username}&Pass={password}&Hwid={HWID}&challenge={GetChallenge(username, password, HWID)}&ProgramId={ProgramId}");
                if (!Resp.Contains("Missing A Parameter") || !Resp.Contains("Failed To Solve Challenge!") || !Resp.Contains("Failed To Resolve Data!"))
                    SolveChallenge(Resp, username, password, HWID);
                if (Settings._Username != Username.Text || Settings._Password != Password.Text)
                {
                    Settings._Settings["userInfo"]["username"] = Username.Text;
                    Settings._Settings["userInfo"]["password"] = Password.Text;
                    string output = JsonConvert.SerializeObject(Settings._Settings, Formatting.Indented);
                    File.WriteAllText("Settings.json", output);
                }
                _Username = username;
                Login.ActiveForm.Hide();
                var frm = new Form1();
                frm.Show();
            }
            catch
            {
            }
        }
        public static string GetChallenge(string User, string Pass, string Hwid)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(SHA512($"{Secret} {User} {Pass} {Hwid} {new WebClient().DownloadString("http://auth.how/API/update.php")}")));
        }
        public static bool SolveChallenge(string Challenge, string User, string Pass, string Hwid)
        {
            if (sha256($"{Secret} {User} {Pass} {Hwid} {new WebClient().DownloadString("http://auth.how/API/update.php")}").Contains(Challenge)) {
                MessageBox.Show($"Successfully Logged In\nWelcome back, {User}", "Success");
                return true;
            }
            else
                MessageBox.Show("Incorrect Login Information", "Error");
                Application.Exit();
            return false;
        }
        private static string sha256(string input)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(input));
            foreach (byte theByte in crypto)
                hash += theByte.ToString("x2");
            return hash.ToUpper();
        }
        private static string SHA512(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create()) {
                var hashedInputBytes = hash.ComputeHash(bytes);

                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}

