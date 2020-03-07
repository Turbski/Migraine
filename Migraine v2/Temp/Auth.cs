using Migraine_v2.LoginClass;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AuthHow
{
    public class Auth
    {
        public const string ProgramId = "235";
        private const string Secret = "TOYEPUK47IR5";

        public static bool Register(string Key, string User, string Pass)
        {
            if (CheckFiddler._())
            try {
                string HWID = AuthHow.HWID.Value();
                if (new System.Net.WebClient().DownloadString($"http://www.auth.how/API/User/Register?Key={Key}&User={User}&Pass={Pass}&Hwid={HWID}&ProgramId={ProgramId}").Contains("true"))
                {
                    return true;
                }
            } catch { }
            return false;
        }
        public static bool Login(string User, string Pass)
        {
            if (CheckFiddler._())
                try
                {            
                string HWID = AuthHow.HWID.Value();
                string Response = new System.Net.WebClient().DownloadString($"http://www.auth.how/API/User/Login?User={User}&Pass={Pass}&Hwid={HWID}&challenge={GetChallenge(User,Pass,HWID)}&ProgramId={ProgramId}");
                if (!Response.Contains("Missing A Parameter") || !Response.Contains("Failed To Solve Challenge!") || !Response.Contains("Failed To Resolve Data!"))
                {
                    return SolveChallenge(Response, User, Pass, HWID);
                }
            }
            catch { }
            return false;
        }
        public static bool SolveChallenge(string Challenge, string User, string Pass, string Hwid)
        {
            if (sha256($"{Secret} {User} {Pass} {Hwid} {new System.Net.WebClient().DownloadString("http://auth.how/API/update.php")}").Contains(Challenge))
            {
                return true;
            }
            return false;
        }
        public static string GetChallenge(string User, string Pass, string Hwid)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(SHA512($"{Secret} {User} {Pass} {Hwid} {new System.Net.WebClient().DownloadString("http://auth.how/API/update.php")}")));
        }
        private static string sha256(string input)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash.ToUpper();
        }
        private static string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
