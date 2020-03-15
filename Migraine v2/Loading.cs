using System;
using System.Windows.Forms;
using System.Threading;
using xNet;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Net;
using Migraine_v2.LoginClass;
using System.Collections.Generic;
using DiscordRPC;
using DiscordRPC.Logging;
using System.Timers;

namespace Migraine_v2 {
    public partial class Loading : Form {
        public Loading() {
            InitializeComponent();
            this.timer1.Start();
        }
        public static string getDefaults()
        {
            List<string> str = new List<string>();
            str.Add("{");
            str.Add("\"misc\":{");
            str.Add("\"defaultRPC\":false");
            str.Add("}");
            str.Add("}");
            return string.Join("\n", str.ToArray());
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
