namespace Migraine_v2.Registration
{
    partial class Register
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label21 = new System.Windows.Forms.Label();
            this.AccountCreation = new Bunifu.Framework.UI.BunifuFlatButton();
            this.Username = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ActivationCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Helvetica-Normal", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(146)))), ((int)(((byte)(151)))));
            this.label21.Location = new System.Drawing.Point(52, 14);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 17);
            this.label21.TabIndex = 32;
            this.label21.Text = "Username";
            // 
            // AccountCreation
            // 
            this.AccountCreation.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(51)))), ((int)(((byte)(57)))));
            this.AccountCreation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(51)))), ((int)(((byte)(57)))));
            this.AccountCreation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AccountCreation.BorderRadius = -3;
            this.AccountCreation.ButtonText = "Register";
            this.AccountCreation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AccountCreation.DisabledColor = System.Drawing.Color.Gray;
            this.AccountCreation.Iconcolor = System.Drawing.Color.Transparent;
            this.AccountCreation.Iconimage = null;
            this.AccountCreation.Iconimage_right = null;
            this.AccountCreation.Iconimage_right_Selected = null;
            this.AccountCreation.Iconimage_Selected = null;
            this.AccountCreation.IconMarginLeft = 0;
            this.AccountCreation.IconMarginRight = 0;
            this.AccountCreation.IconRightVisible = true;
            this.AccountCreation.IconRightZoom = 0D;
            this.AccountCreation.IconVisible = true;
            this.AccountCreation.IconZoom = 90D;
            this.AccountCreation.IsTab = false;
            this.AccountCreation.Location = new System.Drawing.Point(56, 167);
            this.AccountCreation.Margin = new System.Windows.Forms.Padding(4);
            this.AccountCreation.Name = "AccountCreation";
            this.AccountCreation.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(51)))), ((int)(((byte)(57)))));
            this.AccountCreation.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(60)))), ((int)(((byte)(67)))));
            this.AccountCreation.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(146)))), ((int)(((byte)(151)))));
            this.AccountCreation.selected = false;
            this.AccountCreation.Size = new System.Drawing.Size(173, 36);
            this.AccountCreation.TabIndex = 35;
            this.AccountCreation.Text = "Register";
            this.AccountCreation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AccountCreation.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(146)))), ((int)(((byte)(151)))));
            this.AccountCreation.TextFont = new System.Drawing.Font("Helvetica-Normal", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountCreation.Click += new System.EventHandler(this.AccountCreation_Click);
            // 
            // Username
            // 
            this.Username.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.Username.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Username.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Username.ForeColor = System.Drawing.Color.White;
            this.Username.Location = new System.Drawing.Point(56, 35);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(173, 25);
            this.Username.TabIndex = 31;
            this.Username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Username.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Helvetica-Normal", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(146)))), ((int)(((byte)(151)))));
            this.label2.Location = new System.Drawing.Point(52, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 34;
            this.label2.Text = "Password";
            // 
            // Password
            // 
            this.Password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Password.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Password.ForeColor = System.Drawing.Color.White;
            this.Password.Location = new System.Drawing.Point(56, 85);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(173, 25);
            this.Password.TabIndex = 33;
            this.Password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Password.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Helvetica-Normal", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(146)))), ((int)(((byte)(151)))));
            this.label1.Location = new System.Drawing.Point(52, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 17);
            this.label1.TabIndex = 37;
            this.label1.Text = "Authentication Key :";
            // 
            // ActivationCode
            // 
            this.ActivationCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.ActivationCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ActivationCode.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActivationCode.ForeColor = System.Drawing.Color.White;
            this.ActivationCode.Location = new System.Drawing.Point(56, 133);
            this.ActivationCode.Name = "ActivationCode";
            this.ActivationCode.Size = new System.Drawing.Size(173, 25);
            this.ActivationCode.TabIndex = 36;
            this.ActivationCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ActivationCode.WordWrap = false;
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ActivationCode);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.AccountCreation);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Password);
            this.Name = "Register";
            this.Size = new System.Drawing.Size(281, 213);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label21;
        private Bunifu.Framework.UI.BunifuFlatButton AccountCreation;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ActivationCode;
    }
}
