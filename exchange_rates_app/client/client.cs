using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
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
                CloseSocket();
                _clientSocket = new TcpClient();
                _clientSocket.Connect(_endPoint);

                Authentication(login, pass);

                Log += "Connected";
            }
            catch (Exception ex)
            {
                Log += ex.Message;
            }

            return _clientSocket.Connected;
        }
        public string AskServerForRates(Currency curr1, Currency curr2)
        {
            string result = "Not connected";
            
            if (_clientSocket!=null && _clientSocket.Connected)
            try
            {
                    using (NetworkStream ns = _clientSocket.GetStream())
                    {

                        var curr1buff = Encoding.Unicode.GetBytes(curr1.ToString());
                        ns.Write(curr1buff, 0, curr1buff.Length);

                        var curr2buff = Encoding.Unicode.GetBytes(curr2.ToString());
                        ns.Write(curr2buff, 0, curr2buff.Length);

                        using (StreamReader sr = new StreamReader(ns))
                        {
                            result = sr.ReadLine();
                        }
                    }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Log += result;
            return result;
        }

        public void ClearLog()
        {
            Log = "Clear";
        }

        private bool Authentication(string login, string pass)
        {
            using (NetworkStream ns = _clientSocket.GetStream())
            {

                var loginBuff = Encoding.Unicode.GetBytes(login);
                ns.Write(loginBuff, 0, loginBuff.Length);

                var passBuff = Encoding.Unicode.GetBytes(pass);
                ns.Write(passBuff, 0, passBuff.Length);

                if (_clientSocket.Connected == false)
                    Log += "Wrong login or pass!";

            }

            return _clientSocket.Connected;
        }

        public void CloseSocket()
        {
            if (_clientSocket != null && _clientSocket.Connected)
            {
                Log += "Disconnected!";
                _clientSocket.Close();
            }
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
