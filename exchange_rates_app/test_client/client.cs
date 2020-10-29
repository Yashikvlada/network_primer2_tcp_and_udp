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
        private bool Auth(string login, string pass)
        {
            var loginBuff = Encoding.Unicode.GetBytes(login + "\r\n");
            var passBuff = Encoding.Unicode.GetBytes(pass + "\r\n");
            _sw.Write(loginBuff, 0, loginBuff.Length);
            _sw.Write(passBuff, 0, passBuff.Length);

            string answ = _sr.ReadLine();
            Console.WriteLine(answ);
            if (answ.Contains("Wrong password"))
                return false;

            answ = _sr.ReadLine();
            Console.WriteLine(answ);
            if (answ.Contains("You are in block list"))
                return false;

            return true;
        }
        public void Start(IPEndPoint ep, string login, string pass)
        {
            _userName = login;
            _client.Connect(ep);

            _sw = _client.GetStream();
            _sr = new StreamReader(_client.GetStream(), Encoding.Unicode);

            if (!Auth(login, pass))
                return;

            byte[] msgBuff;
            string msgToServer = string.Empty;

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
            string login = "yv";
            string pass = "0000";

            ClientSide client = null;
            try
            {
                client = new ClientSide();
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),1024);
                client.Start(ep, login, pass);
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
