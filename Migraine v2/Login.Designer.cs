namespace Migraine_v2
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.DragPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.exitbutton = new Bunifu.Framework.UI.BunifuFlatButton();
            this.minimizebutton = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LinkButton = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuDragControl2 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.CreateAccount = new Bunifu.Framework.UI.BunifuFlatButton();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.register1 = new Migraine_v2.Registration.Register();
            this.doLogin2 = new Migraine_v2.Registration.doLogin();
            this.DragPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LinkButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // DragPanel
            // 
            this.DragPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.DragPanel.Controls.Add(this.label1);
            this.DragPanel.Controls.Add(this.exitbutton);
            this.DragPanel.Controls.Add(this.minimizebutton);
            this.DragPanel.Location = new System.Drawing.Point(-1, -1);
            this.DragPanel.Name = "DragPanel";
            this.DragPanel.Size = new System.Drawing.Size(456, 23);
            this.DragPanel.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Helvetica", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "MIGRAINE";
            // 
            // exitbutton
            // 
            this.exitbutton.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.exitbutton.BackColor = System.Drawing.Color.Transparent;
            this.exitbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exitbutton.BorderRadius = 0;
            this.exitbutton.ButtonText = "X";
            this.exitbutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitbutton.DisabledColor = System.Drawing.Color.Gray;
            this.exitbutton.Iconcolor = System.Drawing.Color.Transparent;
            this.exitbutton.Iconimage = null;
            this.exitbutton.Iconimage_right = null;
            this.exitbutton.Iconimage_right_Selected = null;
            this.exitbutton.Iconimage_Selected = null;
            this.exitbutton.IconMarginLeft = 0;
            this.exitbutton.IconMarginRight = 0;
            this.exitbutton.IconRightVisible = true;
            this.exitbutton.IconRightZoom = 0D;
            this.exitbutton.IconVisible = true;
            this.exitbutton.IconZoom = 90D;
            this.exitbutton.IsTab = false;
            this.exitbutton.Location = new System.Drawing.Point(420, 0);
            this.exitbutton.Name = "exitbutton";
            this.exitbutton.Normalcolor = System.Drawing.Color.Transparent;
            this.exitbutton.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.exitbutton.OnHoverTextColor = System.Drawing.Color.White;
            this.exitbutton.selected = false;
            this.exitbutton.Size = new System.Drawing.Size(33, 24);
            this.exitbutton.TabIndex = 5;
            this.exitbutton.Text = "X";
            this.exitbutton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.exitbutton.Textcolor = System.Drawing.Color.Gray;
            this.exitbutton.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitbutton.Click += new System.EventHandler(this.Exitbutton_Click);
            // 
            // minimizebutton
            // 
            this.minimizebutton.FlatAppearance.BorderSize = 0;
            this.minimizebutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizebutton.ForeColor = System.Drawing.Color.Gray;
            this.minimizebutton.Location = new System.Drawing.Point(390, -3);
            this.minimizebutton.Name = "minimizebutton";
            this.minimizebutton.Size = new System.Drawing.Size(28, 26);
            this.minimizebutton.TabIndex = 5;
            this.minimizebutton.Text = "-";
            this.minimizebutton.UseVisualStyleBackColor = true;
            this.minimizebutton.Click += new System.EventHandler(this.Minimizebutton_Click);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(50)))));
            this.panel12.Location = new System.Drawing.Point(18, 70);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(30, 2);
            this.panel12.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.panel3.Controls.Add(this.LinkButton);
            this.panel3.Controls.Add(this.panel12);
            this.panel3.Location = new System.Drawing.Point(-1, 21);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(67, 413);
            this.panel3.TabIndex = 5;
            // 
            // LinkButton
            // 
            this.LinkButton.BackColor = System.Drawing.Color.Transparent;
            this.LinkButton.Image = ((System.Drawing.Image)(resources.GetObject("LinkButton.Image")));
            this.LinkButton.ImageActive = null;
            this.LinkButton.Location = new System.Drawing.Point(0, 3);
            this.LinkButton.Name = "LinkButton";
            this.LinkButton.Size = new System.Drawing.Size(65, 64);
            this.LinkButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LinkButton.TabIndex = 7;
            this.LinkButton.TabStop = false;
            this.LinkButton.Zoom = 10;
            this.LinkButton.Click += new System.EventHandler(this.LinkButton_Click);
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.DragPanel;
            this.bunifuDragControl1.Vertical = true;
            // 
            // bunifuDragControl2
            // 
            this.bunifuDragControl2.Fixed = true;
            this.bunifuDragControl2.Horizontal = true;
            this.bunifuDragControl2.TargetControl = this.label1;
            this.bunifuDragControl2.Vertical = true;
            // 
            // CreateAccount
            // 
            this.CreateAccount.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.CreateAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.CreateAccount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CreateAccount.BorderRadius = -4;
            this.CreateAccount.ButtonText = "Register";
            this.CreateAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CreateAccount.DisabledColor = System.Drawing.Color.Gray;
            this.CreateAccount.Iconcolor = System.Drawing.Color.Transparent;
            this.CreateAccount.Iconimage = null;
            this.CreateAccount.Iconimage_right = null;
            this.CreateAccount.Iconimage_right_Selected = null;
            this.CreateAccount.Iconimage_Selected = null;
            this.CreateAccount.IconMarginLeft = 0;
            this.CreateAccount.IconMarginRight = 0;
            this.CreateAccount.IconRightVisible = true;
            this.CreateAccount.IconRightZoom = 0D;
            this.CreateAccount.IconVisible = true;
            this.CreateAccount.IconZoom = 90D;
            this.CreateAccount.IsTab = false;
            this.CreateAccount.Location = new System.Drawing.Point(161, 342);
            this.CreateAccount.Margin = new System.Windows.Forms.Padding(4);
            this.CreateAccount.Name = "CreateAccount";
            this.CreateAccount.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.CreateAccount.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(120)))), ((int)(((byte)(218)))));
            this.CreateAccount.OnHoverTextColor = System.Drawing.Color.White;
            this.CreateAccount.selected = false;
            this.CreateAccount.Size = new System.Drawing.Size(187, 36);
            this.CreateAccount.TabIndex = 36;
            this.CreateAccount.Text = "Register";
            this.CreateAccount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CreateAccount.Textcolor = System.Drawing.Color.White;
            this.CreateAccount.TextFont = new System.Drawing.Font("Helvetica-Normal", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateAccount.Click += new System.EventHandler(this.CreateAccount_Click);
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.AutoSize = true;
            this.gunaLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaLabel1.ForeColor = System.Drawing.Color.Gray;
            this.gunaLabel1.Location = new System.Drawing.Point(194, 415);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(116, 15);
            this.gunaLabel1.TabIndex = 37;
            this.gunaLabel1.Text = "Made by Twin Turbo";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(161, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(175, 66);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 38;
            this.pictureBox1.TabStop = false;
            // 
            // register1
            // 
            this.register1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.register1.Location = new System.Drawing.Point(111, 114);
            this.register1.Name = "register1";
            this.register1.Size = new System.Drawing.Size(246, 213);
            this.register1.TabIndex = 7;
            // 
            // doLogin2
            // 
            this.doLogin2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.doLogin2.Location = new System.Drawing.Point(111, 117);
            this.doLogin2.Name = "doLogin2";
            this.doLogin2.Size = new System.Drawing.Size(281, 210);
            this.doLogin2.TabIndex = 39;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(451, 434);
            this.Controls.Add(this.doLogin2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.gunaLabel1);
            this.Controls.Add(this.CreateAccount);
            this.Controls.Add(this.register1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.DragPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Migraine - Login";
            this.DragPanel.ResumeLayout(false);
            this.DragPanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LinkButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DragPanel;
        private Bunifu.Framework.UI.BunifuFlatButton exitbutton;
        private System.Windows.Forms.Button minimizebutton;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel3;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl2;
        private Registration.Register register1;
        private Registration.doLogin doLogin1;
        private Bunifu.Framework.UI.BunifuFlatButton CreateAccount;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        public Bunifu.Framework.UI.BunifuImageButton LinkButton;
        public System.Windows.Forms.Label label1;
        private Registration.doLogin doLogin2;
    }
}