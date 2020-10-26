using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    public class Server
    {
        private TcpListener _serverSocket;
        private IPEndPoint _endPoint;

        public List<TcpClient> ConnectedUsers { get; set; }
        public Server()
        {
            ConnectedUsers = new List<TcpClient>();
            
        }
        public void Listen(string ip, string port, int maxHosts)
        {
            try
            {
                //CloseSocket();
                _endPoint = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
                _serverSocket = new TcpListener(_endPoint);
                _serverSocket.Start(maxHosts);

                Console.WriteLine("server started");

                Thread acceptThread = new Thread(new ThreadStart(AcceptingThreadFunct));
                acceptThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void AcceptingThreadFunct()
        {
            Console.WriteLine("Waiting for accept...");
            var clientSocket = _serverSocket.AcceptTcpClient();
            Console.WriteLine("Tryed to connect: " + clientSocket.Client.RemoteEndPoint.ToString());

            try
            {
                bool auth = Authentication(clientSocket);

                if (auth)
                {
                    Console.WriteLine("Authentication succesfull");
                    ConnectedUsers.Add(clientSocket);
                    Receive(clientSocket);
                }
                else
                {
                    Console.WriteLine("Authentication fail");
                    //clientSocket.Client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        private void Receive(TcpClient client)
        {
            while (client!=null&&client.Connected)
            {
                var ns = client.GetStream();

                using (StreamReader sr = new StreamReader(ns, Encoding.Unicode))
                {
                    //string curr1 = sr.ReadLine();
                    //string curr2 = sr.ReadLine();
                    string curr1 = ReadInfo(ns);

                    if (curr1.Length != 0)
                    {
                        Console.WriteLine("Converting...");
                        var yesBuff = Encoding.Unicode.GetBytes("yes\n");
                        ns.Write(yesBuff, 0, yesBuff.Length);
                        //ns.Write(Encoding.Unicode.GetBytes("\n"), 0, 1);
                    }
                }
                            
            }
        }
        private string ReadInfo(NetworkStream ns)
        {
            Console.WriteLine("READDATDA");
            List<byte> allBytes = new List<byte>();
            while (ns.DataAvailable)
            {
                int i = 0;
                byte[] buff = new byte[256];

                i = ns.Read(buff, 0, buff.Length);
                Console.WriteLine(i);
                if (i <= 0)
                    break;

                allBytes.AddRange(buff.Take(i));
            }
            string res = Encoding.Unicode.GetString(allBytes.ToArray());
            Console.WriteLine(res);
            return res;
        }
        private bool Authentication(TcpClient clientSocket)
        {
            NetworkStream ns = clientSocket.GetStream();

            StreamReader sr = new StreamReader(ns,Encoding.Unicode);
                
                    string login = sr.ReadLine();
                    string pass = sr.ReadLine();

                    Console.WriteLine(login + pass);
                    Console.WriteLine(login.Length + ":" + pass.Length);

                    if (login.Equals("1") && pass.Equals("1"))
                    {
                        ns.WriteByte(1);
                        Console.WriteLine("auth succesfull");
                        return true;
                    }
                    else
                    {
                        ns.WriteByte(0);
                        Console.WriteLine("auth failed");
                        return false;
                    }
                  
                
            
        }

        public void CloseSocket()
        {

        }
    }
    class server
    {   
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Listen("127.0.0.1", "1024", 5);
        }
    }
}
