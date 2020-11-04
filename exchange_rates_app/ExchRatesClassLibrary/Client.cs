using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExchRatesClassLibrary
{   
    abstract public class AbsClient
    {      
        protected TcpClient _client;
        protected NetworkStream _sw;
        protected StreamReader _sr;
        protected bool _isConnected;

        public string UserName { get; set; }
    }

    public class Client : AbsClient, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _log;

        public string Log
        {
            get => _log;
            set
            {
                if (value != _log)
                {
                    _log = value + $" :[{DateTime.Now}]\r\n";
                    NotifyPropertyChanged();
                }
            }
        }
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                if (value != _isConnected)
                {
                    _isConnected = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public void Start(IPEndPoint ep, string login, string pass)
        {
            try
            {
                IsConnected = true;
                _client = new TcpClient();

                UserName = login;
                Log += "Trying to connect...";
                _client.Connect(ep);

                _sw = _client.GetStream();
                _sr = new StreamReader(_client.GetStream(), Encoding.Unicode);

                Authorization _auth = new ClientAuth(login, pass, _sw, _sr);

                if(_auth.MakeAuthTemplate() == false)
                    this.Close();

                Log += _auth.AuthInfo;
            }
            catch (Exception ex)
            {
                Log += ex.Message;
                this.Close();
            }
        }
        public void Close()
        {
            IsConnected = false;
            _sr?.Close();
            _sw?.Close();

            if (_client != null)
            {
                _client.Close();
                _client = null;
                Log += "Disconnected!";
            }

        }
        public string AskServer(string curr1, string curr2)
        {
            string msgFromServer = "Error";
            try
            {
                byte[] curr1Buff = Encoding.Unicode.GetBytes(curr1 + "\r\n");
                _sw.Write(curr1Buff, 0, curr1Buff.Length);

                byte[] curr2Buff = Encoding.Unicode.GetBytes(curr2 + "\r\n");
                _sw.Write(curr2Buff, 0, curr2Buff.Length);

                msgFromServer = _sr.ReadLine();
                Log += $"Answer from server: {msgFromServer}";
            }
            catch (Exception ex)
            {
                Log += "Can't get answer!";
                this.Close();
            }
            return msgFromServer;
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
