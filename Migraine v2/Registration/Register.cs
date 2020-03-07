using System;
using System.Windows.Forms;
using Migraine_v2.LoginClass;
using System.Net;

namespace Migraine_v2.Registration
{
    public partial class Register : UserControl
    {
        public const string ProgramId = "235";
        public Register() => InitializeComponent();
        private void AccountCreation_Click(object sender, EventArgs e)
        {
            string Key = ActivationCode.Text;
            string User = Username.Text;
            string Pass = Password.Text;
            if (CheckFiddler._())
                return;
            try
            {
                string HWID = AuthHow.HWID.Value();
                if (new WebClient().DownloadString($"http://www.auth.how/API/User/Register?Key={Key}&User={User}&Pass={Pass}&Hwid={HWID}&ProgramId={ProgramId}").Contains("true"))
                    MessageBox.Show($"You have sucessfully registered!\n Username: {User}\n Password: {Pass}", "Success!", MessageBoxButtons.OK);
            }
            catch
            {
                MessageBox.Show($"Please insert a Valid Key or Contact Twin Turbo on Discord", "Invalid Key Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
