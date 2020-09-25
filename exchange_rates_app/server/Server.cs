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
using System.Threading;

namespace server
{
    public enum Currency
    {
        USD, EUR, RUB, UAH
    }

    public class Server : INotifyPropertyChanged
    {
        private TcpListener _serverSocket;
        private IPEndPoint _endPoint;
        private string _log;

        public List<TcpClient> ConnectedUsers { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

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
        public Server()
        {
            ConnectedUsers = new List<TcpClient>();
        }
        public void Listen(string ip, string port, int maxHosts)
        {
            try
            {
                CloseSocket();
                _endPoint = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
                _serverSocket = new TcpListener(_endPoint);
                _serverSocket.Start(maxHosts);

                Log += "Server started!";

                Thread acceptThread = new Thread(new ThreadStart(AcceptingThreadFunct));
                acceptThread.Start();
            }
            catch (Exception ex)
            {
                Log += ex.Message;
            }
        }
        public void AcceptingThreadFunct()
        {
            var clientSocket = _serverSocket.AcceptTcpClient();
            Log += clientSocket.Client.RemoteEndPoint.ToString();
            ConnectedUsers.Add(clientSocket);           
        }
        public void ClearLog()
        {
            Log = "Clear";
        }

        private bool Authentication(TcpClient clientSocket)
        {
            using (NetworkStream ns = clientSocket.GetStream())
            {
                using(StreamReader sr=new StreamReader(ns))
                {
                    string login = sr.ReadLine();
                    string pass = sr.ReadLine();

                    return true;
                }
                

            }

            
        }

        public void CloseSocket()
        {
            //if (_clientSocket != null && _clientSocket.Connected)
            //{
            //    Log += "Disconnected!";
            //    _clientSocket.Close();
            //}
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
