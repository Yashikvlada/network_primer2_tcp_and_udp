using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public enum Currency
    {
        USD, EUR, RUB, UAH
    }
    public class Client
    {
        private TcpClient _clientSocket;
        private IPEndPoint _endPoint;
        private NetworkStream _stream;
        private string _log;

        public string Log { get => _log; }

        public Client(string ip, string port)
        {
            try
            {
                _endPoint = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
                _clientSocket = new TcpClient(_endPoint);
            }
            catch (Exception ex)
            {
                _log += DateTime.Now + ex.Message + "\n";
            }
        }
        public bool Connect(string login, string pass)
        {
            try
            {
                _clientSocket.Connect(_endPoint);
                _stream = _clientSocket.GetStream();

                Authentication(login, pass);
            }
            catch (Exception ex)
            {
                _log += DateTime.Now + ex.Message + "\n";
            }

            return _clientSocket.Connected;
        }
        public string AskServerForRates(Currency curr1, Currency curr2)
        {
            string result = "Not connected";

            if(_clientSocket!=null && _clientSocket.Connected)
            try
            {
                var curr1buff = Encoding.Unicode.GetBytes(curr1.ToString());
                _stream.Write(curr1buff, 0, curr1buff.Length);

                var curr2buff = Encoding.Unicode.GetBytes(curr2.ToString());
                _stream.Write(curr2buff, 0, curr2buff.Length);

                using (StreamReader sr = new StreamReader(_stream))
                {
                    result = sr.ReadLine();
                }

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            _log += DateTime.Now + result + "\n";
            return result;
        }

        public void ClearLog()
        {
            _log = string.Empty;
        }

        private bool Authentication(string login, string pass)
        {
            var loginBuff = Encoding.Unicode.GetBytes(login);
            _stream.Write(loginBuff, 0, loginBuff.Length);

            var passBuff = Encoding.Unicode.GetBytes(pass);
            _stream.Write(passBuff, 0, passBuff.Length);

            if (_clientSocket.Connected == false)
                _log += DateTime.Now + "Wrong login or pass\n";

            return _clientSocket.Connected;
        }

    }
}
