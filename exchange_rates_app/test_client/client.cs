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
    public class Client 
    {
        private TcpClient _clientSocket;
        private IPEndPoint _endPoint;

        public Client(string ip, string port)
        {
            try
            {
                _endPoint = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ip + " " + port);
            }
        }
        public bool Connect(string login, string pass)
        {
            try
            {
                //CloseSocket();
                _clientSocket = new TcpClient();
                Console.WriteLine("Connecting...");
                _clientSocket.Connect(_endPoint);

                Authentication(login, pass);

                //Log += "Connected";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return _clientSocket.Connected;
        }
        public string AskServerForRates(Currency curr1, Currency curr2)
        {
            string result = "No answer";

            if (_clientSocket != null && _clientSocket.Connected)
                try
                {
                    Console.WriteLine("CONVERTING!");    
                    NetworkStream ns = _clientSocket.GetStream();
                    

                        var curr1buff = Encoding.Unicode.GetBytes(curr1.ToString()+ "::"+curr2.ToString());
                        ns.Write(curr1buff, 0, curr1buff.Length);
                        //ns.Write(Encoding.Unicode.GetBytes("\n"), 0, 1);

                        //ns.Write(Encoding.Unicode.GetBytes("\n"), 0, 1);
                    Console.WriteLine("Wait for answer...!");
                    using (StreamReader sr = new StreamReader(ns,Encoding.Unicode))
                        {
                            result = sr.ReadLine();
                        }
                    
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            result = $"{curr1} - {curr2} : {result}";

            
            return result;
        }
        private bool Authentication(string login, string pass)
        {
            int answer = 0;
            NetworkStream ns = _clientSocket.GetStream();
            var loginBuff = Encoding.Unicode.GetBytes(login + '\n');
            var passBuff = Encoding.Unicode.GetBytes(pass + '\n');

            ns.Write(loginBuff,0,loginBuff.Length);
            //ns.Write(Encoding.Unicode.GetBytes("\n"), 0, 1);
            ns.Write(passBuff, 0, passBuff.Length);
            //ns.Write(Encoding.Unicode.GetBytes("\n"), 0, 1);

            answer = ns.ReadByte();
            if (answer == 1)
                Console.WriteLine("Succesfull authentification!" + answer);
            else
            {
                Console.WriteLine("Failed authentification!" + answer);
                _clientSocket.Client.Disconnect(true);
            }

            return _clientSocket.Connected;
        }
        public void CloseSocket()
        {
            if (_clientSocket != null && _clientSocket.Connected)
            {
                Console.WriteLine("Disconnected");
                _clientSocket.Close();
            }
        }
    }
    class client
    {
        static void Main(string[] args)
        {
            Client client = new Client("127.0.0.1", "1024");
            client.Connect("1", "1");
            var str = client.AskServerForRates(Currency.EUR, Currency.RUB);

            Console.WriteLine(str);
        }
    }
}
