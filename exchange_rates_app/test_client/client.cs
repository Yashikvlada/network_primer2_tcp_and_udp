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
    
    class client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter UserName:");
            string userName = Console.ReadLine();

            TcpClient client = null;
            NetworkStream sw = null;
            StreamReader sr = null;

            try
            {
                client = new TcpClient();
                client.Connect(IPAddress.Parse("127.0.0.1"), 1024);

                sw = client.GetStream();
                sr = new StreamReader(client.GetStream(), Encoding.Unicode);

                string msgToServer = string.Empty;

                msgToServer = userName;
                msgToServer += "\r\n";

                byte[] msgBuff = Encoding.Unicode.GetBytes(msgToServer);
                sw.Write(msgBuff, 0, msgBuff.Length);

                while (true)
                {
                    Console.Write($"{userName} : ");
                    msgToServer = Console.ReadLine();
                    msgToServer += "\r\n";

                    msgBuff = Encoding.Unicode.GetBytes(msgToServer);
                    sw.Write(msgBuff, 0, msgBuff.Length);

                    if (msgToServer.Contains("<QUIT>"))
                        break;

                    string msgFromServer = sr.ReadLine();
                    Console.WriteLine($"Answer from server: {msgFromServer}");
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sr?.Close();
                sw?.Close();
                client?.Close();
                Console.WriteLine("Disconnected!");
            }
        }
    }
}
