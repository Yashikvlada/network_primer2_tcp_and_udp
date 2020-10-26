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
                    
                var curr1buff = Encoding.Unicode.GetBytes(curr1.ToString()+"\n");
                _stream.Write(curr1buff, 0, curr1buff.Length);

                var curr2buff = Encoding.Unicode.GetBytes(curr2.ToString()+"\n");
                _stream.Write(curr2buff, 0, curr2buff.Length);

                StreamReader sr = new StreamReader(_stream, Encoding.Unicode);
                result = sr.ReadLine();

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            result= $"{curr1} - {curr2} : {result}";
            Log += result;
            return result;
        }

        public void ClearLog()
        {
            Log = "Clear";
        }

        private bool Authentication(string login, string pass)
        {
            
            var loginBuff = Encoding.Unicode.GetBytes(login+"\n");
            if(_stream.CanWrite)
                _stream.Write(loginBuff, 0, loginBuff.Length);

            var passBuff = Encoding.Unicode.GetBytes(pass+"\n");
            if(_stream.CanWrite)
                _stream.Write(passBuff, 0, passBuff.Length);

            Log += "Checking login and pass...";

            int answ = 0;
            if (_stream.CanRead) { }
                answ = _stream.ReadByte();

            if (answ == 1)
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
