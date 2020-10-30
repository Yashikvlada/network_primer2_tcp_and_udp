using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    internal class ClientSide : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _log;

        private string _userName;
        private TcpClient _client;
        private NetworkStream _sw;
        private StreamReader _sr;
        private bool _isConnected;

        public bool IsConnected { get => _isConnected; }
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

        public ClientSide()
        {
            
        }
        private bool Auth(string login, string pass)
        {
            var servAvailble = _sr.ReadLine();
            Log += servAvailble;
            if (servAvailble.Contains("Server is not available"))
                return false;

            var loginBuff = Encoding.Unicode.GetBytes(login + "\r\n");
            var passBuff = Encoding.Unicode.GetBytes(pass + "\r\n");
            _sw.Write(loginBuff, 0, loginBuff.Length);
            _sw.Write(passBuff, 0, passBuff.Length);

            string answ = _sr.ReadLine();
            Log += answ;
            if (answ.Contains("Wrong password"))
                return false;

            answ = _sr.ReadLine();
            Log += answ;
            if (answ.Contains("You are in block list"))
                return false;

            return true;
        }
        public void Start(IPEndPoint ep, string login, string pass)
        {
            try
            {
                _client = new TcpClient();
                _isConnected = true;

                _userName = login;
                Log += "Trying to connect...";
                _client.Connect(ep);

                _sw = _client.GetStream();
                _sr = new StreamReader(_client.GetStream(), Encoding.Unicode);

                if (!Auth(login, pass))
                    this.Close();
            }
            catch(Exception ex)
            {
                Log += ex.Message;
                this.Close();
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
            catch(Exception ex)
            {
                Log += ex.Message;
                this.Close();
            }
            return msgFromServer;
        }
        public void Close()
        {
            _isConnected = false;
            _sr?.Close();
            _sw?.Close();

            if (_client != null){
                _client.Close();
                _client = null;
                Log += "Disconnected!";
            }
            
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
