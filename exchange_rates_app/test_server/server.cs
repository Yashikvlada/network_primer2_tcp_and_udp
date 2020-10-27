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
        public TcpClient TcpClient { get; set; }

        public void StartClientLoop()
        {
            if (TcpClient == null)
                throw new NullReferenceException("Can`t start client loop! TcpClient is empty!");

            NetworkStream sw = null;
            StreamReader sr = null;
            try
            {
                sw = TcpClient.GetStream();
                sr = new StreamReader(TcpClient.GetStream(), Encoding.Unicode);

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
                TcpClient?.Close();
                Console.WriteLine("Client connection is over!");
            }

        }
    }

    class server
    {
        static void Main(string[] args)
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 1024);
                listener.Start(1);

                Console.WriteLine("Listening...");

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();

                    ClientHandle ch = new ClientHandle();
                    ch.TcpClient = client;

                    Thread clThread = new Thread(new ThreadStart(ch.StartClientLoop));
                    clThread.Start();
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


