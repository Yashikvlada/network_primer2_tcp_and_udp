using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Configuration;
using System.IO;

namespace client
{
    public partial class Form_client : Form
    {
        private ClientSide _client;
        private IPEndPoint _endPoint;
        public Form_client()
        {
            InitializeComponent();

            try
            {
                var server_ip = ConfigurationManager.AppSettings["server_ip"].ToString();
                var server_port = ConfigurationManager.AppSettings["server_port"].ToString();

                _endPoint = new IPEndPoint(IPAddress.Parse(server_ip), int.Parse(server_port));

                _client = new ClientSide();
                textBox_console.DataBindings.Add("Text", _client, "Log", false, DataSourceUpdateMode.OnPropertyChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Критическая ошибка! [{ex.Message}]");
                _client?.Close();
                this.Close();
            }

            this.FormClosed += Form_client_FormClosed;
        }

        private void Form_client_FormClosed(object sender, FormClosedEventArgs e)
        {
            _client?.Close();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            var login = textBox_login.Text;
            var pass = textBox_password.Text;

            if (login == null || pass == null)
            {
                MessageBox.Show("Enter login and pass pls!");
                return;
            }
            Task.Run(new Action(() => { 
            _client.Start(_endPoint, login, pass);
            }));
  
        }

        private void button_ask_Click(object sender, EventArgs e)
        {
            string curr1 = textBox_curr1.Text;
            string curr2 = textBox_curr2.Text;

            if (curr1 == null || curr2 == null)
            {
                MessageBox.Show("Enter curr1 and curr2 pls!");
                return;
            }

            string answ = _client.AskServer(curr1, curr2);
            textBox_answer.Text += answ + "\r\n";
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            _client?.Close();
        }
    }
}
