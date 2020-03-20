using System;
using System.Windows.Forms;
using System.Threading;

namespace Migraine_v2
{
    public partial class Loading : Form {
        public Loading() {
            InitializeComponent();
            this.timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e) {
            progressbar.Increment(1);
            Thread.Sleep(20);
            progressbar.Maximum = 100;
            progressbar.Value++;
            if (progressbar.Value == 100) {
                timer1.Enabled = false;
                var f2 = new Login();
                f2.Show();
                this.Hide();
            }
        }
    }
}
