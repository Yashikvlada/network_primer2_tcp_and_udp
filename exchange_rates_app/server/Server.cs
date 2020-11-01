﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;

namespace server
{
    internal class ClientHandle
    {
        private TcpClient _tcpClient;
        private ServerSide _server;

        private NetworkStream _sw;
        private StreamReader _sr;
        private string _userName;
        private bool _isActive;
        private int _requestsCount;
        public ClientHandle(TcpClient client, ServerSide server)
        {
            _tcpClient = client;
            _server = server;
            _sw = null;
            _sr = null;
            _isActive = true;
            _requestsCount = 0;

            ++_server.CurrClCount;
            _server.ConnectedUsers.Add(this);
        }
        public void Disconnect()
        {
            _sw?.Close();
            _sr?.Close();
            _isActive = false; 
        }
        private void Close()
        {
            _sw?.Close();
            _sr?.Close();

            _tcpClient?.Close();
            --_server.CurrClCount;
            _server.ConnectedUsers.Remove(this);
        }
        private bool CheckLoginPass(string login, string pass)
        {
            byte[] answBuff;
            _server.Log += $"User: {login} connecting...!";
            if (!_server.IsLoginPassOk(login, pass))
            {
                _server.Log += $"User: {login} wrong password or login!";
                answBuff = Encoding.Unicode.GetBytes("Wrong password or login!\r\n");
                _sw.Write(answBuff, 0, answBuff.Length);
                return false;
            }
            answBuff = Encoding.Unicode.GetBytes("Password checked!\r\n");
            _sw.Write(answBuff, 0, answBuff.Length);
            return true;
        }
        private bool CheckBlockList(string login)
        {
            byte[] answBuff;
            if (_server.IsUserInBlockList(login))
            {
                _server.Log += $"User: {login} in block list!";
                var timeExpired = _server.BlockedUsers[login];
                answBuff = Encoding.Unicode.GetBytes($"You are in block list [{timeExpired}]!\r\n");
                _sw.Write(answBuff, 0, answBuff.Length);
                return false;
            }
            answBuff = Encoding.Unicode.GetBytes($"Server is ready to work. Enter request pls!\r\n");
            _sw.Write(answBuff, 0, answBuff.Length);

            return true;
        }
        private bool CheckMaxClients(string login)
        {
            byte[] answBuff;
            if (_server.CurrClCount > _server.MaxClCount)
            {
                _server.Log += $"User: {_tcpClient.Client.RemoteEndPoint} denied (already max clients)!";
                answBuff = Encoding.Unicode.GetBytes($"Server is not available. Try to connect later pls!\r\n");
                _sw.Write(answBuff, 0, answBuff.Length);
                return false;
            }
            answBuff = Encoding.Unicode.GetBytes($"Server is available!\r\n");
            _sw.Write(answBuff, 0, answBuff.Length);

            return true;
        }
        public void StartClientLoop()
        {
            if (_tcpClient == null)
                throw new NullReferenceException("Can`t start client loop! TcpClient is empty!");

            try
            {
                _sw = _tcpClient.GetStream();
                _sr = new StreamReader(_tcpClient.GetStream(), Encoding.Unicode);
                //
                if (!CheckMaxClients(_userName))
                    return;

                _userName = _sr.ReadLine();
                string pass = _sr.ReadLine();

                if (!CheckLoginPass(_userName, pass))
                    return;
                if (!CheckBlockList(_userName))
                    return;

                _server.Log += $"User: {_userName} connected!";
                //
                byte[] answBuff;

                while (_isActive)
                {
                    string curr1 = _sr.ReadLine();
                    string curr2 = _sr.ReadLine();

                    //if (msgFromClient.Contains("<QUIT>"))
                    //{
                    //    Console.WriteLine($"Client: {_userName} is disconnected!");
                    //    break;
                    //}

                    if (_requestsCount >= _server.MaxRequests)
                    {
                        DateTime expire = DateTime.Now.AddSeconds(_server.BlockTime);
                        string limit = $"Client: {_userName} requests {_requestsCount} / {_server.MaxRequests}. " +
                            $"Blocked till: {expire}!";

                        _server.Log += limit;
                        answBuff = Encoding.Unicode.GetBytes(limit + "\r\n");
                        _sw.Write(answBuff, 0, answBuff.Length);
                        _server.BlockedUsers.Add(_userName, expire);
                        break;
                    }

                    _server.Log += $"Client: {_userName} : {curr1} to {curr2}";

                    string answer = _server.CalcRates(curr1, curr2);
                    answBuff = Encoding.Unicode.GetBytes(answer + "\r\n");
                    _sw.Write(answBuff, 0, answBuff.Length);
                    ++_requestsCount;
                    _server.Log += $"Answer: {curr1} - {curr2} : {answer}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
                if (_server.MaxClCount > _server.CurrClCount)
                    _server.Log += $"Client {_userName} " +
                        $"connection is over! Connected: {_server.CurrClCount}";
            }

        }
    }
    internal class ServerSide : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _log;
        private TcpListener _listener;

        // словарь валюта:курс к доллару (напр.: EUR:0,8)
        private Dictionary<string, float> _rates;

        public string Log
        {
            get => _log;
            set
            {
                if (value != _log)
                {
                    _log = value + $" :[{DateTime.Now}]\r\n";
                    NotifyPropertyChanged();
                }
            }
        }
        // все кто сейчас подключены
        public List<ClientHandle> ConnectedUsers { get; set; }
        // сколько сейчас подключено
        public int CurrClCount { get; set; }
        // максимум одновременно подключенных
        public int MaxClCount { get; set; }
        // база логинов и паролей
        public Dictionary<string, string> UsersBase { get; set; }
        // пользователи, которые превысили лимит запросов
        public Dictionary<string, DateTime> BlockedUsers { get; set; }
        //макс количество запросов до блокировки
        public int MaxRequests { get; set; }
        // продолжительность блокировки (сек)
        public int BlockTime { get; }

        public ServerSide(IPEndPoint ep, int maxClCount, int maxRequests, int blockTime)
        {
            _listener = new TcpListener(ep);
            MaxClCount = maxClCount;
            CurrClCount = 0;
            MaxRequests = maxRequests;
            BlockTime = blockTime;
            ConnectedUsers = new List<ClientHandle>();
            _rates = new Dictionary<string, float>();
            UsersBase = new Dictionary<string, string>();
            BlockedUsers = new Dictionary<string, DateTime>();
        }

        /// <summary>
        /// Считываем .txt файл с курсами валют (USD:1\nEUR:0,8\nRUB:70)
        /// </summary>
        public void LoadRates(string ratesFile)
        {
            try
            {
                _rates.Clear();
                using (StreamReader sr = new StreamReader(ratesFile))
                {
                    while (!sr.EndOfStream)
                    {
                        var info = sr.ReadLine().Split(':');
                        string currName = info[0];
                        float currRate = float.Parse(info[1]);

                        _rates.Add(currName, currRate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Cant't get rates! [{ex.Message}]");
            }
        }
        public void StartListen()
        {
            _listener?.Start();

            while (true)
            {
                //if (_maxClCount > CurrClCount)
                //{
                TcpClient client = _listener.AcceptTcpClient();

                ClientHandle clHandle = new ClientHandle(client, this);

                Thread clThread = new Thread(new ThreadStart(clHandle.StartClientLoop));
                clThread.Start();
                //}
            }
        }
        public void StopServer()
        {
            DisconnectAll();
            _listener?.Stop();
        }
        public bool AddUserToBase(string login, string pass)
        {
            if (UsersBase.ContainsKey(login))
                return false;

            UsersBase.Add(login, pass);
            return true;
        }
        public bool RemoveUserFromBase(string login)
        {
            if (!UsersBase.ContainsKey(login))
                return false;

            UsersBase.Remove(login);
            return true;
        }
        public bool IsLoginPassOk(string login, string pass)
        {
            if (UsersBase.ContainsKey(login))
                return UsersBase[login].Equals(pass);

            return false;
        }
        public bool IsUserInBlockList(string login)
        {
            if (!BlockedUsers.ContainsKey(login))
                return false;

            DateTime expire = BlockedUsers[login];

            if (expire >= DateTime.Now)
                return true;

            BlockedUsers.Remove(login);
            return false;

        }
        public string CalcRates(string curr1, string curr2)
        {

            if (!_rates.ContainsKey(curr1) || !_rates.ContainsKey(curr2))
                return "WRONG RATE NAME";

            if (_rates[curr1] == 0)
                return "WRONG SOURCE RATE";
            else
                return (_rates[curr2] / _rates[curr1]).ToString();

        }
        private void DisconnectAll()
        {
            if (ConnectedUsers.Count == 0)
                return;

            for (int i = 0; i < ConnectedUsers.Count; ++i)
            {
                ConnectedUsers[i].Disconnect();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
