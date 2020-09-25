﻿namespace server
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
            this.listBox_waiting_users = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_all_userts = new System.Windows.Forms.TabPage();
            this.tabPage_waiting_users = new System.Windows.Forms.TabPage();
            this.tabPage_connected_users = new System.Windows.Forms.TabPage();
            this.listBox_all_users = new System.Windows.Forms.ListBox();
            this.listBox_connected_users = new System.Windows.Forms.ListBox();
            this.button_add = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_maxClients = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage_all_userts.SuspendLayout();
            this.tabPage_waiting_users.SuspendLayout();
            this.tabPage_connected_users.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_listen
            // 
            this.button_listen.Location = new System.Drawing.Point(12, 12);
            this.button_listen.Name = "button_listen";
            this.button_listen.Size = new System.Drawing.Size(136, 45);
            this.button_listen.TabIndex = 0;
            this.button_listen.Text = "Listen";
            this.button_listen.UseVisualStyleBackColor = true;
            this.button_listen.Click += new System.EventHandler(this.button_listen_Click);
            // 
            // textBox_console
            // 
            this.textBox_console.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox_console.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.textBox_console.Location = new System.Drawing.Point(154, 12);
            this.textBox_console.Multiline = true;
            this.textBox_console.Name = "textBox_console";
            this.textBox_console.ReadOnly = true;
            this.textBox_console.Size = new System.Drawing.Size(293, 426);
            this.textBox_console.TabIndex = 1;
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(12, 63);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(136, 45);
            this.button_stop.TabIndex = 2;
            this.button_stop.Text = "Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            // 
            // listBox_waiting_users
            // 
            this.listBox_waiting_users.FormattingEnabled = true;
            this.listBox_waiting_users.Location = new System.Drawing.Point(15, 17);
            this.listBox_waiting_users.Name = "listBox_waiting_users";
            this.listBox_waiting_users.Size = new System.Drawing.Size(289, 368);
            this.listBox_waiting_users.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_all_userts);
            this.tabControl1.Controls.Add(this.tabPage_waiting_users);
            this.tabControl1.Controls.Add(this.tabPage_connected_users);
            this.tabControl1.Location = new System.Drawing.Point(453, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(327, 426);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage_all_userts
            // 
            this.tabPage_all_userts.Controls.Add(this.Delete);
            this.tabPage_all_userts.Controls.Add(this.button_add);
            this.tabPage_all_userts.Controls.Add(this.listBox_all_users);
            this.tabPage_all_userts.Location = new System.Drawing.Point(4, 22);
            this.tabPage_all_userts.Name = "tabPage_all_userts";
            this.tabPage_all_userts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_all_userts.Size = new System.Drawing.Size(319, 400);
            this.tabPage_all_userts.TabIndex = 0;
            this.tabPage_all_userts.Text = "All users";
            this.tabPage_all_userts.UseVisualStyleBackColor = true;
            // 
            // tabPage_waiting_users
            // 
            this.tabPage_waiting_users.Controls.Add(this.listBox_waiting_users);
            this.tabPage_waiting_users.Location = new System.Drawing.Point(4, 22);
            this.tabPage_waiting_users.Name = "tabPage_waiting_users";
            this.tabPage_waiting_users.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_waiting_users.Size = new System.Drawing.Size(319, 400);
            this.tabPage_waiting_users.TabIndex = 1;
            this.tabPage_waiting_users.Text = "Waiting users";
            this.tabPage_waiting_users.UseVisualStyleBackColor = true;
            // 
            // tabPage_connected_users
            // 
            this.tabPage_connected_users.Controls.Add(this.listBox_connected_users);
            this.tabPage_connected_users.Location = new System.Drawing.Point(4, 22);
            this.tabPage_connected_users.Name = "tabPage_connected_users";
            this.tabPage_connected_users.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_connected_users.Size = new System.Drawing.Size(319, 400);
            this.tabPage_connected_users.TabIndex = 2;
            this.tabPage_connected_users.Text = "Connected users";
            this.tabPage_connected_users.UseVisualStyleBackColor = true;
            // 
            // listBox_all_users
            // 
            this.listBox_all_users.FormattingEnabled = true;
            this.listBox_all_users.Location = new System.Drawing.Point(6, 6);
            this.listBox_all_users.Name = "listBox_all_users";
            this.listBox_all_users.Size = new System.Drawing.Size(307, 329);
            this.listBox_all_users.TabIndex = 6;
            // 
            // listBox_connected_users
            // 
            this.listBox_connected_users.FormattingEnabled = true;
            this.listBox_connected_users.Location = new System.Drawing.Point(6, 6);
            this.listBox_connected_users.Name = "listBox_connected_users";
            this.listBox_connected_users.Size = new System.Drawing.Size(294, 316);
            this.listBox_connected_users.TabIndex = 4;
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(6, 349);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(136, 45);
            this.button_add.TabIndex = 7;
            this.button_add.Text = "Add user";
            this.button_add.UseVisualStyleBackColor = true;
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(177, 349);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(136, 45);
            this.Delete.TabIndex = 8;
            this.Delete.Text = "Delete user";
            this.Delete.UseVisualStyleBackColor = true;
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(12, 126);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(136, 20);
            this.textBox_ip.TabIndex = 7;
            this.textBox_ip.Text = "127.0.0.1";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(12, 152);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(136, 20);
            this.textBox_port.TabIndex = 8;
            this.textBox_port.Text = "1024";
            // 
            // textBox_maxClients
            // 
            this.textBox_maxClients.Location = new System.Drawing.Point(12, 178);
            this.textBox_maxClients.Name = "textBox_maxClients";
            this.textBox_maxClients.Size = new System.Drawing.Size(136, 20);
            this.textBox_maxClients.TabIndex = 9;
            this.textBox_maxClients.Text = "1";
            // 
            // Form_server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox_maxClients);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.textBox_console);
            this.Controls.Add(this.button_listen);
            this.Name = "Form_server";
            this.Text = "Server";
            this.tabControl1.ResumeLayout(false);
            this.tabPage_all_userts.ResumeLayout(false);
            this.tabPage_waiting_users.ResumeLayout(false);
            this.tabPage_connected_users.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_listen;
        private System.Windows.Forms.TextBox textBox_console;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.ListBox listBox_waiting_users;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_all_userts;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.ListBox listBox_all_users;
        private System.Windows.Forms.TabPage tabPage_waiting_users;
        private System.Windows.Forms.TabPage tabPage_connected_users;
        private System.Windows.Forms.ListBox listBox_connected_users;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.TextBox textBox_maxClients;
    }
}

