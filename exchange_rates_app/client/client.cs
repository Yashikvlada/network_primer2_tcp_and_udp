using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace client
{
    public enum Currency
    {
        USD, EUR, RUB, UAH
    }
    public class Client: INotifyPropertyChanged
    {
        private TcpClient _clientSocket;
        private IPEndPoint _endPoint;
        private NetworkStream _stream;
        private string _log;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Log
        {
            get => _log;
            set
            {
                if (value != _log)
                {
                    _log = value +$" :[{DateTime.Now}]\r\n";
                    NotifyPropertyChanged();
                }
            }
        }
        public Client(string ip, string port)
        {
            try
            {
                _endPoint = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
                _clientSocket = null;
                _stream = null;
            }
            catch (Exception ex)
            {
                Log += "Создание соккета! " + ex.Message;
            }
        }
        public void Connect(string login, string pass)
        {
            try
            {
                CloseSocket();
                _clientSocket = new TcpClient();
                _clientSocket.Connect(_endPoint);
                _stream = _clientSocket.GetStream();

                Log += "Connected";

                Authentication(login, pass);
               
            }
            catch (Exception ex)
            {
                Log += ex.Message;
            }

        }
        public string AskServerForRates(Currency curr1, Currency curr2)
        {
            string result = "No answer";
            
            if (_stream!=null)
            try
            {
                
                string quest = curr1.ToString() + "::" + curr2.ToString();

                WriteInfo(quest);
                Log += "Отправлен запрос: " + quest;

                result = ReadInfo();
                Log += "Получен ответ: " + result;
            }
            catch (Exception ex)
            {
                Log+= ex.Message;
            }
            result= $"{curr1} - {curr2} : {result}";

            return result;
        }

        private string ReadInfo()
        {

            List<byte> allBytes = new List<byte>();
            while (_stream.DataAvailable)
            {
                int i = 0;
                byte[] buff = new byte[256];

                i = _stream.Read(buff, 0, buff.Length);

                if (i <= 0)
                    break;

                allBytes.AddRange(buff.Take(i));
            }
            string res = Encoding.Unicode.GetString(allBytes.ToArray());

            return res;
        }
        private void WriteInfo(string msg)
        {
            var buff = Encoding.Unicode.GetBytes(msg);
            _stream.Write(buff, 0, buff.Length);
        }
        public void ClearLog()
        {
            Log = "Clear";
        }

        private bool Authentication(string login, string pass)
        {
            
            WriteInfo(login + "::" + pass);

            Log += "Checking login and pass...";

            string answ = ReadInfo();

            if (answ == "1")
            {
                Log += "Succesfull authentification!";
                return true;
            }              
            else
            {
                Log += "Failed authentification!";
                CloseSocket();
                return false;
            }

        }
        private void ReadThreadFunc(object o)
        {
            var ns = o as NetworkStream;
        }

        public void CloseSocket()
        {
            _stream?.Close();

            if (_clientSocket != null)
            {
                _clientSocket.Close();
                _clientSocket.Dispose();
                _clientSocket = null;
                Log += "Соккет очищен!";
            }
            

        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
