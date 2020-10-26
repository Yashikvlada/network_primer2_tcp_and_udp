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
            _serverSocket = null;
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
                    Log += "Authentication succesfull!: ";
                    Dispatcher.CurrentDispatcher.Invoke(() => ConnectedUsers.Add(clientSocket));

                    ReceiveLoop(clientSocket);
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
                clientSocket.Client.Disconnect(true);
            }

        }
        private void ReceiveLoop(TcpClient client)
        {
            var ns = client.GetStream();
            try
            {
                while (client.Connected)
                {
                    string msgFromClient = ReadInfo(ns);

                    if (msgFromClient.Length != 0)
                    {
                        Log += "Получен запрос: " + msgFromClient;
                        var answ = GenerateAnsw(msgFromClient);
                        WriteInfo(ns, answ);
                        Log += "Отправлен ответ: " + answ;
                    }
                }
            }
            finally
            {
                ns.Close();
                client.Close();
            }
        
        }
        private string ReadInfo(NetworkStream ns)
        {

            List<byte> allBytes = new List<byte>();
            while (ns.DataAvailable)
            {
                int i = 0;
                byte[] buff = new byte[256];

                i = ns.Read(buff, 0, buff.Length);
                
                if (i <= 0)
                    break;

                allBytes.AddRange(buff.Take(i));
            }
            string res = Encoding.Unicode.GetString(allBytes.ToArray());

            return res;
        }
        private string GenerateAnsw(string msg)
        {
            return "YES";
        }
        private void WriteInfo(NetworkStream ns, string msg)
        {
            var buff = Encoding.Unicode.GetBytes(msg);
            ns.Write(buff, 0, buff.Length);
        }

        public void ClearLog()
        {
            Log = "Clear";
        }

        private bool Authentication(TcpClient clientSocket)
        {
            var ns = clientSocket.GetStream();

            string loginPass = ReadInfo(ns);
            WriteInfo(ns, "1");
            
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
            foreach (var s in ConnectedUsers)
            {
                s.Client.Disconnect(false);
               
                //s.GetStream().Close();                
                s.Close();
                
            }

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
