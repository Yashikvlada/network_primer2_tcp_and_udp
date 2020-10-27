using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace test_client
{
    public enum Currency
    {
        USD, EUR, RUB, UAH
    }
    internal class ClientSide{

        private string _userName;
        private TcpClient _client;
        private NetworkStream _sw;
        private StreamReader _sr;
        public ClientSide()
        {
            
            _client = new TcpClient();
        }
        public void Start(IPEndPoint ep, string userName)
        {
            _userName = userName;
            string msgToServer = string.Empty;
            _client.Connect(ep);

            _sw = _client.GetStream();
            _sr = new StreamReader(_client.GetStream(), Encoding.Unicode);

            msgToServer = _userName;
            msgToServer += "\r\n";

            byte[] msgBuff = Encoding.Unicode.GetBytes(msgToServer);
            _sw.Write(msgBuff, 0, msgBuff.Length);

            while (true)
            {
                Console.Write($"{_userName} : ");
                msgToServer = Console.ReadLine();
                msgToServer += "\r\n";

                msgBuff = Encoding.Unicode.GetBytes(msgToServer);
                _sw.Write(msgBuff, 0, msgBuff.Length);

                if (msgToServer.Contains("<QUIT>"))
                    break;

                string msgFromServer = _sr.ReadLine();
                Console.WriteLine($"Answer from server: {msgFromServer}");
            }
        }
        public void Close()
        {
            _sr?.Close();
            _sw?.Close();
            _client?.Close();
        }
    }
    class client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter UserName:");
            string userName = Console.ReadLine();

            ClientSide client = null;
            try
            {
                client = new ClientSide();
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),1024);
                client.Start(ep, userName);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
                Console.WriteLine("Disconnected!");
            }
        }
    }
}
