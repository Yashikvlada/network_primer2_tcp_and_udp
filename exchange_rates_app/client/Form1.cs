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
        private Client _clientApp;

        public Form_client()
        {
            InitializeComponent();
            var server_ip = ConfigurationManager.AppSettings["server_ip"].ToString();
            var server_port = ConfigurationManager.AppSettings["server_port"].ToString();

            _clientApp = new Client(server_ip, server_port);

            textBox_console.DataBindings.Add("Text", _clientApp, "Log", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            _clientApp.Connect("ivan", "ivanov");
        }

        private void button_ask_Click(object sender, EventArgs e)
        {
            var str = _clientApp.AskServerForRates(Currency.EUR, Currency.USD);

        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            _clientApp.CloseSocket();
        }
    }
}
