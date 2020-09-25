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
            this.comboBox_currency = new System.Windows.Forms.ComboBox();
            this.button_ask = new System.Windows.Forms.Button();
            this.textBox_answer = new System.Windows.Forms.TextBox();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.label_login = new System.Windows.Forms.Label();
            this.label_password = new System.Windows.Forms.Label();
            this.textBox_login = new System.Windows.Forms.TextBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.textBox_console = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(12, 12);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(91, 23);
            this.button_connect.TabIndex = 0;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // comboBox_currency
            // 
            this.comboBox_currency.FormattingEnabled = true;
            this.comboBox_currency.Location = new System.Drawing.Point(93, 123);
            this.comboBox_currency.Name = "comboBox_currency";
            this.comboBox_currency.Size = new System.Drawing.Size(121, 21);
            this.comboBox_currency.TabIndex = 1;
            // 
            // button_ask
            // 
            this.button_ask.Location = new System.Drawing.Point(12, 123);
            this.button_ask.Name = "button_ask";
            this.button_ask.Size = new System.Drawing.Size(75, 23);
            this.button_ask.TabIndex = 2;
            this.button_ask.Text = "Ask server";
            this.button_ask.UseVisualStyleBackColor = true;
            this.button_ask.Click += new System.EventHandler(this.button_ask_Click);
            // 
            // textBox_answer
            // 
            this.textBox_answer.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox_answer.ForeColor = System.Drawing.Color.Black;
            this.textBox_answer.Location = new System.Drawing.Point(12, 152);
            this.textBox_answer.Multiline = true;
            this.textBox_answer.Name = "textBox_answer";
            this.textBox_answer.ReadOnly = true;
            this.textBox_answer.Size = new System.Drawing.Size(206, 204);
            this.textBox_answer.TabIndex = 3;
            // 
            // button_disconnect
            // 
            this.button_disconnect.Location = new System.Drawing.Point(121, 12);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(97, 23);
            this.button_disconnect.TabIndex = 4;
            this.button_disconnect.Text = "Disconnect";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // label_login
            // 
            this.label_login.AutoSize = true;
            this.label_login.Location = new System.Drawing.Point(12, 54);
            this.label_login.Name = "label_login";
            this.label_login.Size = new System.Drawing.Size(33, 13);
            this.label_login.TabIndex = 5;
            this.label_login.Text = "Login";
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(12, 82);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(53, 13);
            this.label_password.TabIndex = 6;
            this.label_password.Text = "Password";
            // 
            // textBox_login
            // 
            this.textBox_login.Location = new System.Drawing.Point(80, 51);
            this.textBox_login.Name = "textBox_login";
            this.textBox_login.Size = new System.Drawing.Size(138, 20);
            this.textBox_login.TabIndex = 7;
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(80, 79);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(138, 20);
            this.textBox_password.TabIndex = 8;
            // 
            // textBox_console
            // 
            this.textBox_console.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox_console.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.textBox_console.Location = new System.Drawing.Point(244, 14);
            this.textBox_console.Multiline = true;
            this.textBox_console.Name = "textBox_console";
            this.textBox_console.ReadOnly = true;
            this.textBox_console.Size = new System.Drawing.Size(317, 342);
            this.textBox_console.TabIndex = 9;
            // 
            // Form_client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 368);
            this.Controls.Add(this.textBox_console);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.textBox_login);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.label_login);
            this.Controls.Add(this.button_disconnect);
            this.Controls.Add(this.textBox_answer);
            this.Controls.Add(this.button_ask);
            this.Controls.Add(this.comboBox_currency);
            this.Controls.Add(this.button_connect);
            this.Name = "Form_client";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.ComboBox comboBox_currency;
        private System.Windows.Forms.Button button_ask;
        private System.Windows.Forms.TextBox textBox_answer;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.Label label_login;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.TextBox textBox_login;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.TextBox textBox_console;
    }
}

