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
using System.Windows.Forms;
using System.Windows.Threading;

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
            Log += "Tryed to connect: " + clientSocket.Client.RemoteEndPoint.ToString();

            try
            {
                bool auth = Authentication(clientSocket);

                if (auth)
                {
                    Log += "Authentication succesfull: ";
                    Dispatcher.CurrentDispatcher.Invoke(() => ConnectedUsers.Add(clientSocket));

                    Receive(clientSocket);
                }
                else
                {
                    Log += "Authentication fail: ";
                    clientSocket.Client.Disconnect(true);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Receive(TcpClient client)
        {
            var ns = client.GetStream();
            while (client.Connected)
            {
                StreamReader sr = new StreamReader(ns, Encoding.Unicode);

                var curr1 = sr.ReadLine();
                var curr2 = sr.ReadLine();

                if (curr1.Length != 0 && curr2.Length != 0)
                {
                    var answ = Encoding.Unicode.GetBytes("Yes!\n");
                    Log += curr1 + " " + curr2 + " = " + answ;
                    ns.Write(answ, 0, answ.Length);
                }

            }
        }

        public void ClearLog()
        {
            Log = "Clear";
        }

        private bool Authentication(TcpClient clientSocket)
        {
            var ns = clientSocket.GetStream();

            StreamReader sr = new StreamReader(ns, Encoding.Unicode);

            string login = sr.ReadLine();
            string pass = sr.ReadLine();

            ns.WriteByte(1);
            return true;
        }
        public void StopListen()
        {
            if (_serverSocket == null)
                return;
            _serverSocket.Stop();
        }
        public void CloseSocket()
        {
            if (_serverSocket == null)
                return;

            _serverSocket.Stop();
            _serverSocket.Server.Close();

            Log += "Disconnected!";
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
