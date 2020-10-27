using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public class ClientHandle
    {
        private TcpClient _tcpClient;
        public static int MaxClients { get; set; }
        public static int CurrClients { get; set; }

        static ClientHandle()
        {
            MaxClients = 1;
            CurrClients = 0;
        }
        public ClientHandle(TcpClient client)
        {
            _tcpClient = client;
            ++CurrClients;
        }
        public void StartClientLoop()
        {
            if (_tcpClient == null)
                throw new NullReferenceException("Can`t start client loop! TcpClient is empty!");

            NetworkStream sw = null;
            StreamReader sr = null;
            try
            {
                sw = _tcpClient.GetStream();
                sr = new StreamReader(_tcpClient.GetStream(), Encoding.Unicode);

                string msgFromClient = sr.ReadLine();
                string userName = msgFromClient;

                Console.WriteLine($"Client: {userName} connected!");

                while (true)
                {
                    msgFromClient = sr.ReadLine();

                    if (msgFromClient.Contains("<QUIT>"))
                    {
                        Console.WriteLine($"Client: {userName} is disconnected!");
                        break;
                    }

                    Console.WriteLine($"Client: {userName} : {msgFromClient}");

                    string answer = msgFromClient + "\r\n";
                    byte[] answBuff = Encoding.Unicode.GetBytes(answer);
                    sw.Write(answBuff, 0, answBuff.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sw?.Close();
                sr?.Close();
                _tcpClient?.Close();
                --CurrClients;
                Console.WriteLine("Client connection is over!");
            }

        }
    }

    class server
    {
        static void Main(string[] args)
        {
            TcpListener listener = null;
            List<TcpClient> connectedUsers = new List<TcpClient>();
            const int MAX_USERS = 2;
            const int PORT = 1024;
            const string IP_ADDR = "127.0.0.1";
            try
            {
                listener = new TcpListener(IPAddress.Parse(IP_ADDR), PORT);
                listener.Start(5);
                ClientHandle.MaxClients = MAX_USERS;

                Console.WriteLine("Listening...");

                while (true)
                {
                    if (ClientHandle.MaxClients > ClientHandle.CurrClients)
                    {
                        TcpClient client = listener.AcceptTcpClient();

                        ClientHandle clHandle = new ClientHandle(client);

                        Thread clThread = new Thread(new ThreadStart(clHandle.StartClientLoop));
                        clThread.Start();
                    }            
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                listener?.Stop();
                Console.WriteLine("Listening is over!");
            }          
        }
    }
}


