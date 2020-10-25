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
                _clientSocket = new TcpClient();
            }
            catch (Exception ex)
            {
                Log += ex.Message;
            }
        }
        public bool Connect(string login, string pass)
        {
            try
            {
                Disconnect();
                _clientSocket.Connect(_endPoint);

                Authentication(login, pass);

                //Log += "Connected";
            }
            catch (Exception ex)
            {
                Log += ex.Message;
            }

            return _clientSocket.Connected;
        }
        public string AskServerForRates(Currency curr1, Currency curr2)
        {
            string result = "No answer";
            
            if (_clientSocket!=null && _clientSocket.Connected)
            try
            {
                var ns = _clientSocket.GetStream();
                    
                var curr1buff = Encoding.Unicode.GetBytes(curr1.ToString()+"\n");
                ns.Write(curr1buff, 0, curr1buff.Length);

                var curr2buff = Encoding.Unicode.GetBytes(curr2.ToString()+"\n");
                ns.Write(curr2buff, 0, curr2buff.Length);

                StreamReader sr = new StreamReader(ns, Encoding.Unicode);
                    if(_clientSocket!=null)
                        result = sr.ReadLine();              
                    
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            result+= $"{curr1} - {curr2} : {result}";
            Log += result;
            return result;
        }

        public void ClearLog()
        {
            Log = "Clear";
        }

        private bool Authentication(string login, string pass)
        {
            var ns = _clientSocket.GetStream();
            
            var loginBuff = Encoding.Unicode.GetBytes(login+"\n");
            ns.Write(loginBuff, 0, loginBuff.Length);

            var passBuff = Encoding.Unicode.GetBytes(pass+"\n");
            ns.Write(passBuff, 0, passBuff.Length);

            Log += "Connecting...";

            var answ = ns.ReadByte();

            if (answ == 1)
                Log += "Succesfull authentification!";
            else
            {
                Log += "Failed authentification!";
                this.Disconnect();
            }

            return _clientSocket.Connected;
        }
        private void ReadThreadFunc(object o)
        {
            var ns = o as NetworkStream;
        }

        public void Disconnect()
        {
            if (_clientSocket != null && _clientSocket.Connected)
                _clientSocket.Client.Disconnect(true);
        }
        public void CloseSocket()
        {
            if (_clientSocket == null)
                return;

            //Disconnect();

            Log += "Socket closed!";
            _clientSocket.Close();

        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
