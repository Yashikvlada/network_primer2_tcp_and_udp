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

                SetBindings();               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Критическая ошибка! [{ex.Message}]");
                _client?.Close();
                this.Close();
            }

            this.FormClosed += Form_client_FormClosed;
        }
        private void SetBindings()
        {
            // байндим консоль
            textBox_console.DataBindings.Add("Text", _client, "Log", false, DataSourceUpdateMode.OnPropertyChanged);
            //байндим контролы на прямое значение (если IsConnected true, то и контрол true)
            button_ask.DataBindings.Add("Enabled", _client, "IsConnected", false, DataSourceUpdateMode.OnPropertyChanged);
            button_disconnect.DataBindings.Add("Enabled", _client, "IsConnected", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_curr1.DataBindings.Add("Enabled", _client, "IsConnected", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_curr2.DataBindings.Add("Enabled", _client, "IsConnected", false, DataSourceUpdateMode.OnPropertyChanged);
            //байндим контролы на обратное значение
            Binding connBind = new Binding("Enabled", _client, "IsConnected");
            connBind.Parse += ReverseBoolProperty;
            connBind.Format += ReverseBoolProperty;
            button_connect.DataBindings.Add(connBind);
        }
        private void ReverseBoolProperty(object s, ConvertEventArgs e)
        {
            e.Value = !(bool)e.Value;
        }
        private void Form_client_FormClosed(object sender, FormClosedEventArgs e)
        {
            _client?.Close();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            var login = textBox_login.Text;
            var pass = textBox_password.Text;

            if (login == string.Empty || pass == string.Empty)
            {
                MessageBox.Show("Enter login and pass pls!");
                return;
            }
            var connTask = Task.Run(new Action(() => { 
            _client.Start(_endPoint, login, pass);
            }));
            //button_connect.Enabled = false;
            //Task.Run(new Action(() =>
            //{
            //    while (connTask.Status == TaskStatus.Running);
            //    button_connect.Enabled = !_client.IsConnected;

            //}));

        }

        private void button_ask_Click(object sender, EventArgs e)
        {
            string curr1 = textBox_curr1.Text;
            string curr2 = textBox_curr2.Text;

            if (curr1 == string.Empty || curr2 == string.Empty)
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
