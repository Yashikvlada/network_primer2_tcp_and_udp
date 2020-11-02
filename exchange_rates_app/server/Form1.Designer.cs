namespace server
{
    partial class Form_server
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
            this.button_listen = new System.Windows.Forms.Button();
            this.textBox_console = new System.Windows.Forms.TextBox();
            this.button_stop = new System.Windows.Forms.Button();
            this.listBox_blockUsr = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_allUsr = new System.Windows.Forms.TabPage();
            this.label_addPass = new System.Windows.Forms.Label();
            this.label_addLogin = new System.Windows.Forms.Label();
            this.textBox_addLogin = new System.Windows.Forms.TextBox();
            this.textBox_addPass = new System.Windows.Forms.TextBox();
            this.Delete = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.listBox_allUsr = new System.Windows.Forms.ListBox();
            this.tabPage_BlockUsr = new System.Windows.Forms.TabPage();
            this.tabPage_connUsr = new System.Windows.Forms.TabPage();
            this.listBox_connUsr = new System.Windows.Forms.ListBox();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_maxClients = new System.Windows.Forms.TextBox();
            this.label_ip = new System.Windows.Forms.Label();
            this.label_port = new System.Windows.Forms.Label();
            this.label_maxClients = new System.Windows.Forms.Label();
            this.label_maxReq = new System.Windows.Forms.Label();
            this.textBox_maxReq = new System.Windows.Forms.TextBox();
            this.label_blockTime = new System.Windows.Forms.Label();
            this.textBox_blockTime = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage_allUsr.SuspendLayout();
            this.tabPage_BlockUsr.SuspendLayout();
            this.tabPage_connUsr.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_listen
            // 
            this.button_listen.Location = new System.Drawing.Point(18, 18);
            this.button_listen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_listen.Name = "button_listen";
            this.button_listen.Size = new System.Drawing.Size(204, 69);
            this.button_listen.TabIndex = 0;
            this.button_listen.Text = "Listen";
            this.button_listen.UseVisualStyleBackColor = true;
            this.button_listen.Click += new System.EventHandler(this.button_listen_Click);
            // 
            // textBox_console
            // 
            this.textBox_console.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox_console.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.textBox_console.Location = new System.Drawing.Point(231, 18);
            this.textBox_console.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_console.Multiline = true;
            this.textBox_console.Name = "textBox_console";
            this.textBox_console.ReadOnly = true;
            this.textBox_console.Size = new System.Drawing.Size(520, 653);
            this.textBox_console.TabIndex = 1;
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(18, 97);
            this.button_stop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(204, 69);
            this.button_stop.TabIndex = 2;
            this.button_stop.Text = "Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // listBox_blockUsr
            // 
            this.listBox_blockUsr.FormattingEnabled = true;
            this.listBox_blockUsr.ItemHeight = 20;
            this.listBox_blockUsr.Location = new System.Drawing.Point(22, 26);
            this.listBox_blockUsr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox_blockUsr.Name = "listBox_blockUsr";
            this.listBox_blockUsr.Size = new System.Drawing.Size(432, 564);
            this.listBox_blockUsr.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_allUsr);
            this.tabControl1.Controls.Add(this.tabPage_BlockUsr);
            this.tabControl1.Controls.Add(this.tabPage_connUsr);
            this.tabControl1.Location = new System.Drawing.Point(763, 18);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(490, 655);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage_allUsr
            // 
            this.tabPage_allUsr.Controls.Add(this.label_addPass);
            this.tabPage_allUsr.Controls.Add(this.label_addLogin);
            this.tabPage_allUsr.Controls.Add(this.textBox_addLogin);
            this.tabPage_allUsr.Controls.Add(this.textBox_addPass);
            this.tabPage_allUsr.Controls.Add(this.Delete);
            this.tabPage_allUsr.Controls.Add(this.button_add);
            this.tabPage_allUsr.Controls.Add(this.listBox_allUsr);
            this.tabPage_allUsr.Location = new System.Drawing.Point(4, 29);
            this.tabPage_allUsr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage_allUsr.Name = "tabPage_allUsr";
            this.tabPage_allUsr.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage_allUsr.Size = new System.Drawing.Size(482, 622);
            this.tabPage_allUsr.TabIndex = 0;
            this.tabPage_allUsr.Text = "All users";
            this.tabPage_allUsr.UseVisualStyleBackColor = true;
            // 
            // label_addPass
            // 
            this.label_addPass.AutoSize = true;
            this.label_addPass.Location = new System.Drawing.Point(262, 506);
            this.label_addPass.Name = "label_addPass";
            this.label_addPass.Size = new System.Drawing.Size(67, 20);
            this.label_addPass.TabIndex = 12;
            this.label_addPass.Text = "Пароль";
            // 
            // label_addLogin
            // 
            this.label_addLogin.AutoSize = true;
            this.label_addLogin.Location = new System.Drawing.Point(262, 465);
            this.label_addLogin.Name = "label_addLogin";
            this.label_addLogin.Size = new System.Drawing.Size(55, 20);
            this.label_addLogin.TabIndex = 11;
            this.label_addLogin.Text = "Логин";
            // 
            // textBox_addLogin
            // 
            this.textBox_addLogin.Location = new System.Drawing.Point(9, 462);
            this.textBox_addLogin.Name = "textBox_addLogin";
            this.textBox_addLogin.Size = new System.Drawing.Size(204, 26);
            this.textBox_addLogin.TabIndex = 10;
            // 
            // textBox_addPass
            // 
            this.textBox_addPass.Location = new System.Drawing.Point(9, 503);
            this.textBox_addPass.Name = "textBox_addPass";
            this.textBox_addPass.Size = new System.Drawing.Size(204, 26);
            this.textBox_addPass.TabIndex = 9;
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(266, 537);
            this.Delete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(204, 69);
            this.Delete.TabIndex = 8;
            this.Delete.Text = "Delete user";
            this.Delete.UseVisualStyleBackColor = true;
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(9, 537);
            this.button_add.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(204, 69);
            this.button_add.TabIndex = 7;
            this.button_add.Text = "Add user";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // listBox_allUsr
            // 
            this.listBox_allUsr.FormattingEnabled = true;
            this.listBox_allUsr.ItemHeight = 20;
            this.listBox_allUsr.Location = new System.Drawing.Point(9, 9);
            this.listBox_allUsr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox_allUsr.Name = "listBox_allUsr";
            this.listBox_allUsr.Size = new System.Drawing.Size(458, 444);
            this.listBox_allUsr.TabIndex = 6;
            // 
            // tabPage_BlockUsr
            // 
            this.tabPage_BlockUsr.Controls.Add(this.listBox_blockUsr);
            this.tabPage_BlockUsr.Location = new System.Drawing.Point(4, 29);
            this.tabPage_BlockUsr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage_BlockUsr.Name = "tabPage_BlockUsr";
            this.tabPage_BlockUsr.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage_BlockUsr.Size = new System.Drawing.Size(482, 622);
            this.tabPage_BlockUsr.TabIndex = 1;
            this.tabPage_BlockUsr.Text = "Blocked users";
            this.tabPage_BlockUsr.UseVisualStyleBackColor = true;
            // 
            // tabPage_connUsr
            // 
            this.tabPage_connUsr.Controls.Add(this.listBox_connUsr);
            this.tabPage_connUsr.Location = new System.Drawing.Point(4, 29);
            this.tabPage_connUsr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage_connUsr.Name = "tabPage_connUsr";
            this.tabPage_connUsr.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage_connUsr.Size = new System.Drawing.Size(482, 622);
            this.tabPage_connUsr.TabIndex = 2;
            this.tabPage_connUsr.Text = "Connected users";
            this.tabPage_connUsr.UseVisualStyleBackColor = true;
            // 
            // listBox_connUsr
            // 
            this.listBox_connUsr.FormattingEnabled = true;
            this.listBox_connUsr.ItemHeight = 20;
            this.listBox_connUsr.Location = new System.Drawing.Point(9, 9);
            this.listBox_connUsr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox_connUsr.Name = "listBox_connUsr";
            this.listBox_connUsr.Size = new System.Drawing.Size(439, 484);
            this.listBox_connUsr.TabIndex = 4;
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(18, 227);
            this.textBox_ip.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.ReadOnly = true;
            this.textBox_ip.Size = new System.Drawing.Size(202, 26);
            this.textBox_ip.TabIndex = 7;
            this.textBox_ip.Text = "127.0.0.1";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(18, 297);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.ReadOnly = true;
            this.textBox_port.Size = new System.Drawing.Size(202, 26);
            this.textBox_port.TabIndex = 8;
            this.textBox_port.Text = "1024";
            // 
            // textBox_maxClients
            // 
            this.textBox_maxClients.Location = new System.Drawing.Point(18, 372);
            this.textBox_maxClients.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_maxClients.Name = "textBox_maxClients";
            this.textBox_maxClients.Size = new System.Drawing.Size(202, 26);
            this.textBox_maxClients.TabIndex = 9;
            this.textBox_maxClients.Text = "2";
            this.textBox_maxClients.TextChanged += new System.EventHandler(this.textBox_maxClients_TextChanged);
            // 
            // label_ip
            // 
            this.label_ip.AutoSize = true;
            this.label_ip.Location = new System.Drawing.Point(18, 197);
            this.label_ip.Name = "label_ip";
            this.label_ip.Size = new System.Drawing.Size(21, 20);
            this.label_ip.TabIndex = 10;
            this.label_ip.Text = "ip";
            // 
            // label_port
            // 
            this.label_port.AutoSize = true;
            this.label_port.Location = new System.Drawing.Point(18, 271);
            this.label_port.Name = "label_port";
            this.label_port.Size = new System.Drawing.Size(37, 20);
            this.label_port.TabIndex = 11;
            this.label_port.Text = "port";
            // 
            // label_maxClients
            // 
            this.label_maxClients.AutoSize = true;
            this.label_maxClients.Location = new System.Drawing.Point(18, 347);
            this.label_maxClients.Name = "label_maxClients";
            this.label_maxClients.Size = new System.Drawing.Size(87, 20);
            this.label_maxClients.TabIndex = 12;
            this.label_maxClients.Text = "max clients";
            // 
            // label_maxReq
            // 
            this.label_maxReq.AutoSize = true;
            this.label_maxReq.Location = new System.Drawing.Point(18, 429);
            this.label_maxReq.Name = "label_maxReq";
            this.label_maxReq.Size = new System.Drawing.Size(104, 20);
            this.label_maxReq.TabIndex = 14;
            this.label_maxReq.Text = "max requests";
            // 
            // textBox_maxReq
            // 
            this.textBox_maxReq.Location = new System.Drawing.Point(18, 454);
            this.textBox_maxReq.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_maxReq.Name = "textBox_maxReq";
            this.textBox_maxReq.Size = new System.Drawing.Size(202, 26);
            this.textBox_maxReq.TabIndex = 13;
            this.textBox_maxReq.Text = "5";
            this.textBox_maxReq.TextChanged += new System.EventHandler(this.textBox_maxReq_TextChanged);
            // 
            // label_blockTime
            // 
            this.label_blockTime.AutoSize = true;
            this.label_blockTime.Location = new System.Drawing.Point(18, 509);
            this.label_blockTime.Name = "label_blockTime";
            this.label_blockTime.Size = new System.Drawing.Size(119, 20);
            this.label_blockTime.TabIndex = 16;
            this.label_blockTime.Text = "block time (sec)";
            // 
            // textBox_blockTime
            // 
            this.textBox_blockTime.Location = new System.Drawing.Point(18, 534);
            this.textBox_blockTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_blockTime.Name = "textBox_blockTime";
            this.textBox_blockTime.Size = new System.Drawing.Size(202, 26);
            this.textBox_blockTime.TabIndex = 15;
            this.textBox_blockTime.Text = "60";
            this.textBox_blockTime.TextChanged += new System.EventHandler(this.textBox_blockTime_TextChanged);
            // 
            // Form_server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 692);
            this.Controls.Add(this.label_blockTime);
            this.Controls.Add(this.textBox_blockTime);
            this.Controls.Add(this.label_maxReq);
            this.Controls.Add(this.textBox_maxReq);
            this.Controls.Add(this.label_maxClients);
            this.Controls.Add(this.label_port);
            this.Controls.Add(this.label_ip);
            this.Controls.Add(this.textBox_maxClients);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.textBox_console);
            this.Controls.Add(this.button_listen);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form_server";
            this.Text = "Server";
            this.tabControl1.ResumeLayout(false);
            this.tabPage_allUsr.ResumeLayout(false);
            this.tabPage_allUsr.PerformLayout();
            this.tabPage_BlockUsr.ResumeLayout(false);
            this.tabPage_connUsr.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_listen;
        private System.Windows.Forms.TextBox textBox_console;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.ListBox listBox_blockUsr;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_allUsr;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.ListBox listBox_allUsr;
        private System.Windows.Forms.TabPage tabPage_BlockUsr;
        private System.Windows.Forms.TabPage tabPage_connUsr;
        private System.Windows.Forms.ListBox listBox_connUsr;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.TextBox textBox_maxClients;
        private System.Windows.Forms.Label label_ip;
        private System.Windows.Forms.Label label_port;
        private System.Windows.Forms.Label label_maxClients;
        private System.Windows.Forms.Label label_maxReq;
        private System.Windows.Forms.TextBox textBox_maxReq;
        private System.Windows.Forms.Label label_blockTime;
        private System.Windows.Forms.TextBox textBox_blockTime;
        private System.Windows.Forms.Label label_addPass;
        private System.Windows.Forms.Label label_addLogin;
        private System.Windows.Forms.TextBox textBox_addLogin;
        private System.Windows.Forms.TextBox textBox_addPass;
    }
}

