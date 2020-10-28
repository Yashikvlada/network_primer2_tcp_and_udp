using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace test_server
{
    public enum Currency
    {
        USD, EUR, RUB, UAH
    }

    internal class ClientHandle
    {
        private TcpClient _tcpClient;
        private ServerSide _server;

        private NetworkStream _sw;
        private StreamReader _sr;
        private string _userName;
        private bool _isActive;
        public ClientHandle(TcpClient client, ServerSide server)
        {
            _tcpClient = client;
            _server = server;
            _sw = null;
            _sr = null;
            _isActive = true;

            ++_server.CurrClCount;
            _server.ConnectedUsers.Add(this);
        }
        public void Disconnect()
        {
            _isActive = false;
        }
        private void Close()
        {
            _sw?.Close();
            _sr?.Close();

            _tcpClient?.Close();
            --_server.CurrClCount;
            _server.ConnectedUsers.Remove(this);
        }
        public void StartClientLoop()
        {
            if (_tcpClient == null)
                throw new NullReferenceException("Can`t start client loop! TcpClient is empty!");

            try
            {
                _sw = _tcpClient.GetStream();
                _sr = new StreamReader(_tcpClient.GetStream(), Encoding.Unicode);

                string msgFromClient = _sr.ReadLine();
                _userName = msgFromClient;

                Console.WriteLine($"Client: {_userName} connected!");

                while (_isActive)
                {
                    msgFromClient = _sr.ReadLine();

                    if (msgFromClient.Contains("<QUIT>"))
                    {
                        Console.WriteLine($"Client: {_userName} is disconnected!");
                        break;
                    }

                    Console.WriteLine($"Client: {_userName} : {msgFromClient}");

                    string answer = msgFromClient + "\r\n";
                    byte[] answBuff = Encoding.Unicode.GetBytes(answer);
                    _sw.Write(answBuff, 0, answBuff.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
                Console.WriteLine($"Client {_userName} connection is over! Connected: {_server.CurrClCount}");
            }

        }
    }
    internal class ServerSide
    {
        private TcpListener _listener;

        public List<ClientHandle> ConnectedUsers { get; set; }
        private int _maxClCount;
        public int CurrClCount { get; set; }
        private Dictionary<string, float> _rates;

        public ServerSide(IPEndPoint ep, int maxClCount)
        {
            _rates = new Dictionary<string, float>();

            _listener = new TcpListener(ep);
            ConnectedUsers = new List<ClientHandle>();
            _maxClCount = maxClCount;
            CurrClCount = 0;
        }

        /// <summary>
        /// Считываем .txt файл с курсами валют (USD:1\nEUR:0,8\nRUB:70)
        /// </summary>
        public void LoadRates(string ratesFile)
        {
            try
            {
                using (StreamReader sr = new StreamReader(ratesFile))
                {
                    while (!sr.EndOfStream)
                    {
                        var info = sr.ReadLine().Split(':');
                        string currName = info[0];
                        float currRate = float.Parse(info[1]);

                        _rates.Add(currName, currRate);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException($"Cant't get rates! [{ex.Message}]");
            }
        }
        public void StartListen()
        {
            _listener?.Start();

            while (true)
            {
                if (_maxClCount > CurrClCount)
                {
                    TcpClient client = _listener.AcceptTcpClient();

                    ClientHandle clHandle = new ClientHandle(client, this);

                    Thread clThread = new Thread(new ThreadStart(clHandle.StartClientLoop));
                    clThread.Start();
                }
            }
        }
        private void DisconnectAll()
        {
            if (ConnectedUsers.Count == 0)
                return;

            for (int i = 0; i < ConnectedUsers.Count; ++i)
            {
                ConnectedUsers[i].Disconnect();
            }
        }
        public void StopServer()
        {
            DisconnectAll();
            _listener?.Stop();
        }

    }
    class server
    {

        static void Main(string[] args)
        {
            ServerSide server = null;

            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1024);
                server = new ServerSide(ep, 2);

                Console.WriteLine("Read rates...");
                server.LoadRates("rates.txt");

                Console.WriteLine("Listening...");
                server.StartListen();
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                server.StopServer();
                Console.WriteLine("Server is over!");
            }          
        }
    }
}


