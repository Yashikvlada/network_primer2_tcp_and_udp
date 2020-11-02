namespace client
{
    partial class Form_client
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
            this.button_connect = new System.Windows.Forms.Button();
            this.button_ask = new System.Windows.Forms.Button();
            this.textBox_answer = new System.Windows.Forms.TextBox();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.label_login = new System.Windows.Forms.Label();
            this.label_password = new System.Windows.Forms.Label();
            this.textBox_login = new System.Windows.Forms.TextBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.textBox_console = new System.Windows.Forms.TextBox();
            this.textBox_curr1 = new System.Windows.Forms.TextBox();
            this.textBox_curr2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(18, 18);
            this.button_connect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(136, 35);
            this.button_connect.TabIndex = 0;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // button_ask
            // 
            this.button_ask.Location = new System.Drawing.Point(18, 189);
            this.button_ask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_ask.Name = "button_ask";
            this.button_ask.Size = new System.Drawing.Size(112, 35);
            this.button_ask.TabIndex = 2;
            this.button_ask.Text = "Ask server";
            this.button_ask.UseVisualStyleBackColor = true;
            this.button_ask.Click += new System.EventHandler(this.button_ask_Click);
            // 
            // textBox_answer
            // 
            this.textBox_answer.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox_answer.ForeColor = System.Drawing.Color.Black;
            this.textBox_answer.Location = new System.Drawing.Point(18, 234);
            this.textBox_answer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_answer.Multiline = true;
            this.textBox_answer.Name = "textBox_answer";
            this.textBox_answer.ReadOnly = true;
            this.textBox_answer.Size = new System.Drawing.Size(307, 312);
            this.textBox_answer.TabIndex = 3;
            // 
            // button_disconnect
            // 
            this.button_disconnect.Location = new System.Drawing.Point(182, 18);
            this.button_disconnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(146, 35);
            this.button_disconnect.TabIndex = 4;
            this.button_disconnect.Text = "Disconnect";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // label_login
            // 
            this.label_login.AutoSize = true;
            this.label_login.Location = new System.Drawing.Point(18, 83);
            this.label_login.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_login.Name = "label_login";
            this.label_login.Size = new System.Drawing.Size(48, 20);
            this.label_login.TabIndex = 5;
            this.label_login.Text = "Login";
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(18, 126);
            this.label_password.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(78, 20);
            this.label_password.TabIndex = 6;
            this.label_password.Text = "Password";
            // 
            // textBox_login
            // 
            this.textBox_login.Location = new System.Drawing.Point(120, 78);
            this.textBox_login.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_login.Name = "textBox_login";
            this.textBox_login.Size = new System.Drawing.Size(205, 26);
            this.textBox_login.TabIndex = 7;
            this.textBox_login.Text = "yv";
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(120, 122);
            this.textBox_password.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(205, 26);
            this.textBox_password.TabIndex = 8;
            this.textBox_password.Text = "0000";
            // 
            // textBox_console
            // 
            this.textBox_console.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox_console.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.textBox_console.Location = new System.Drawing.Point(366, 22);
            this.textBox_console.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_console.Multiline = true;
            this.textBox_console.Name = "textBox_console";
            this.textBox_console.ReadOnly = true;
            this.textBox_console.Size = new System.Drawing.Size(474, 524);
            this.textBox_console.TabIndex = 9;
            // 
            // textBox_curr1
            // 
            this.textBox_curr1.Location = new System.Drawing.Point(137, 193);
            this.textBox_curr1.Name = "textBox_curr1";
            this.textBox_curr1.Size = new System.Drawing.Size(80, 26);
            this.textBox_curr1.TabIndex = 10;
            this.textBox_curr1.Text = "RUB";
            // 
            // textBox_curr2
            // 
            this.textBox_curr2.Location = new System.Drawing.Point(223, 193);
            this.textBox_curr2.Name = "textBox_curr2";
            this.textBox_curr2.Size = new System.Drawing.Size(80, 26);
            this.textBox_curr2.TabIndex = 11;
            this.textBox_curr2.Text = "EUR";
            // 
            // Form_client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 566);
            this.Controls.Add(this.textBox_curr2);
            this.Controls.Add(this.textBox_curr1);
            this.Controls.Add(this.textBox_console);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.textBox_login);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.label_login);
            this.Controls.Add(this.button_disconnect);
            this.Controls.Add(this.textBox_answer);
            this.Controls.Add(this.button_ask);
            this.Controls.Add(this.button_connect);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form_client";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.Button button_ask;
        private System.Windows.Forms.TextBox textBox_answer;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.Label label_login;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.TextBox textBox_login;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.TextBox textBox_console;
        private System.Windows.Forms.TextBox textBox_curr1;
        private System.Windows.Forms.TextBox textBox_curr2;
    }
}

