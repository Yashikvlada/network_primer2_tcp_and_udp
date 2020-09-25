using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server
{
    public partial class Form_server : Form
    {
        Server _serverApp;
        public Form_server()
        {
            InitializeComponent();

            _serverApp = new Server();
            textBox_console.DataBindings.Add("Text", _serverApp, "Log");
        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            try
            {
                _serverApp.Listen(
                    textBox_ip.Text,
                    textBox_port.Text,
                    int.Parse(textBox_maxClients.Text));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
