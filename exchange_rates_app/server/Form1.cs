using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server
{
    public partial class Form_server : Form
    {
        private ServerSide _server;
        public Form_server()
        {
            InitializeComponent();

            try
            {
                var server_ip = ConfigurationManager.AppSettings["server_ip"].ToString();
                var server_port = ConfigurationManager.AppSettings["server_port"].ToString();

                textBox_ip.Text = server_ip;
                textBox_port.Text = server_port;

                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(server_ip), 
                    int.Parse(server_port));

                _server = new ServerSide(ep, 
                    int.Parse(textBox_maxClients.Text),
                    int.Parse(textBox_maxReq.Text),
                    int.Parse(textBox_blockTime.Text));

                textBox_console.DataBindings.Add("Text", _server, "Log", false, DataSourceUpdateMode.OnPropertyChanged);
                listBox_connUsr.DataSource = _server.ConnectedUsers;

                //добавим тестового пользователя
                _server.UsersBase.Add("yv", "0000");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Критическая ошибка! [{ex.Message}]");
                _server?.StopServer();
                this.Close();
            }

            this.FormClosed += Form_server_FormClosed;
        }

        private void Form_server_FormClosed(object sender, FormClosedEventArgs e)
        {
            _server?.StopServer();
        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            try
            {
                _server.Log += "Read rates...";              
                _server.LoadRates("rates.txt");

                _server.Log += "Listening...";

                Task.Run(new Action(() =>
                {
                    _server.StartListen();
                }));
            }
            catch(Exception ex)
            {
                _server.Log += ex.Message;
                _server.Log += "Server is stoped!";
                _server?.StopServer();
            }
            
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            _server?.StopServer();
        }

        private void textBox_maxClients_TextChanged(object sender, EventArgs e)
        {
            int maxCl;
            if(int.TryParse(textBox_maxClients.Text, out maxCl) && maxCl > 0)
            {
                _server.MaxClCount = maxCl;
            }
            else
            {
                MessageBox.Show("Макс число клиентов должно быть положительным числом!");
                textBox_maxClients.Text = "2";
            }
            
        }
        private void textBox_maxReq_TextChanged(object sender, EventArgs e)
        {
            int maxReq;
            if (int.TryParse(textBox_maxReq.Text, out maxReq) && maxReq > 0)
            {
                _server.MaxRequests = maxReq;
            }
            else
            {
                MessageBox.Show("Макс число запросов должно быть положительным числом!");
                textBox_maxReq.Text = "5";
            }
        }
        private void textBox_blockTime_TextChanged(object sender, EventArgs e)
        {
            int maxBlockTime;
            if (int.TryParse(textBox_blockTime.Text, out maxBlockTime) && maxBlockTime > 0)
            {
                _server.MaxRequests = maxBlockTime;
            }
            else
            {
                MessageBox.Show("Длительность блокировки должна быть положительным числом!");
                textBox_blockTime.Text = "60";
            }
        }
    }
}
