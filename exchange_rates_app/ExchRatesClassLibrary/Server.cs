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

namespace ExchRatesClassLibrary
{
    public class ClientHost:AbsClient
    {
        private ServerSide _server;
        private int _requestsCount;

        public ClientHost(TcpClient client, ServerSide server)
        {
            _client = client;
            _server = server;
            _sw = null;
            _sr = null;
            _isConnected = true;
            _requestsCount = 0;

            ++_server.CurrClCount;
        }
        // разъединяем соединение (соккет еще есть)
        public void Disconnect()
        {
            _sw?.Close();
            _sr?.Close();
            _isConnected = false;
        }
        private void Close()
        {
            Disconnect();

            _client?.Close();
            --_server.CurrClCount;
            _server.ConnectedUsers.Remove(this);
        }
        private void ReadWriteCycle()
        {
            byte[] answBuff;

            while (_isConnected)
            {
                string curr1 = _sr.ReadLine();
                string curr2 = _sr.ReadLine();

                if (_requestsCount >= _server.MaxRequests)
                {
                    DateTime expire = DateTime.Now.AddSeconds(_server.BlockTime);
                    string limit = $"Client: {UserName} requests {_requestsCount} / {_server.MaxRequests}. " +
                        $"Blocked till: {expire}!";

                    _server.Log += limit;
                    answBuff = Encoding.Unicode.GetBytes(limit + "\r\n");
                    _sw.Write(answBuff, 0, answBuff.Length);
                    _server.BlockedUsers.Add(new KeyValuePair<string, DateTime>(UserName, expire));
                    break;
                }

                string answer = _server.CalcRates(curr1, curr2);
                answBuff = Encoding.Unicode.GetBytes(answer + "\r\n");
                _sw.Write(answBuff, 0, answBuff.Length);
                ++_requestsCount;
                _server.Log += $"Client: {UserName} : {curr1} to {curr2}";
                _server.Log += $"Answer: {curr1} - {curr2} : {answer}";
            }
        }
        public void StartClientLoop()
        {
            try
            {
                _sw = _client.GetStream();
                _sr = new StreamReader(_client.GetStream(), Encoding.Unicode);

                Authorization _auth = new ServerAuth(_sw, _sr, _server);
                bool auth = _auth.MakeAuthTemplate();
                _server.Log += _auth.AuthInfo;

                if (auth == false)
                    return;

                UserName = _auth.Login;

                _server.ConnectedUsers.Add(this);
                
                ReadWriteCycle();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
                if (_server.MaxClCount > _server.CurrClCount)
                    _server.Log += $"Client {UserName} " +
                        $"connection is over! Now connected: {_server.CurrClCount}";
            }

        }
        public override string ToString()
        {
            return UserName;
        }
    }

    public class ServerSide : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _log;
        private TcpListener _listener;
        private bool _isListening;

        // словарь валюта:курс к доллару (напр.: EUR:0,8)
        private Dictionary<string, float> _rates;

        public bool IsListening
        {
            get => _isListening;
            set
            {
                if (value != _isListening)
                {
                    _isListening = value;
                    NotifyPropertyChanged();
                }
            }
        }
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
        public BindingList<ClientHost> ConnectedUsers { get; set; }
        // сколько сейчас подключено
        public int CurrClCount { get; set; }
        // максимум одновременно подключенных
        public int MaxClCount { get; set; }
        // база логинов и паролей (лист а не словарь, тк привязывать удобнее к спискам)
        public BindingList<KeyValuePair<string, string>> UsersBase { get; set; }
        // пользователи, которые превысили лимит запросов
        public BindingList<KeyValuePair<string, DateTime>> BlockedUsers { get; set; }
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
            ConnectedUsers = new BindingList<ClientHost>();
            _rates = new Dictionary<string, float>();
            UsersBase = new BindingList<KeyValuePair<string, string>>();
            BlockedUsers = new BindingList<KeyValuePair<string, DateTime>>();
            IsListening = false;
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
            IsListening = true;
            _listener?.Start();
            Log += "Listening...";

            while (true)
            {
                //при _listener.Stop(); вылетит исключение которое остановит этот цикл
                TcpClient client = _listener.AcceptTcpClient();
                ClientHost clHandle = new ClientHost(client, this);

                Thread clThread = new Thread(new ThreadStart(clHandle.StartClientLoop));
                clThread.Start();

            }

        }
        public void StopServer()
        {
            DisconnectAll();
            if (_listener != null)
            {
                IsListening = false;
                _listener.Stop();
                Log += "Server stoped!";
            }
        }
        public bool AddUserToBase(string login, string pass)
        {
            if (UsersBase.Where(u => u.Key.Equals(login)).Count() != 0)
                return false;

            UsersBase.Add(new KeyValuePair<string, string>(login, pass));
            return true;
        }
        public bool RemoveUserFromBase(string login, string pass)
        {
            if (!UsersBase.Contains(new KeyValuePair<string, string>(login, pass)))
                return false;

            UsersBase.Remove(new KeyValuePair<string, string>(login, pass));
            return true;
        }
        public bool IsLoginPassOk(string login, string pass)
        {
            if (UsersBase.Contains(new KeyValuePair<string, string>(login, pass)))
                return true;

            return false;
        }
        public bool IsUserInBlockList(string login)
        {
            var blkUsr = BlockedUsers.Where(b => b.Key.Equals(login));

            if (blkUsr.Count() == 0)
                return false;

            DateTime expire = blkUsr.ElementAt(0).Value;

            if (expire >= DateTime.Now)
                return true;

            BlockedUsers.Remove(blkUsr.ElementAt(0));
            return false;

        }
        public bool IsUserAlreadyConnected(string login)
        {
            var connUsr = ConnectedUsers
                .Where(c => c.UserName.Equals(login))
                .ToList();

            if (connUsr.Count() != 0)
                return true;

            return false;
        }
        public DateTime GetUserBlockTime(string login)
        {
            var timeExpired = BlockedUsers
                    .Where(b => b.Key.Equals(login))
                    .ElementAt(0).Value;

            return timeExpired;
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
