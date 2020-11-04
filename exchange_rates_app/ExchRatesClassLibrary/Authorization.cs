using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExchRatesClassLibrary
{
    abstract public class Authorization
    {
        protected const string SERVER_OFF = "Server is not avaible!";
        protected const string LOGINPASS_ERROR = "Wrong login or password!";
        protected const string ALREADY_CONNECTED = "User is already connected!";
        protected const string USER_BLOCKED = "User is in block list!";

        protected NetworkStream _sw;
        protected StreamReader _sr;

        public string Pass { get; set; }
        public string Login { get; set; }
        public string AuthInfo { get; set; }

        public Authorization( NetworkStream sw, StreamReader sr)
        {
            _sw = sw;
            _sr = sr;
        }
        public bool MakeAuthTemplate() 
        {
            if (!IsServerAvaible())
                return false;

            if (!IsLoginPassOk())
                return false;

            if (IsUserAlreadyConnected())
                return false;

            if (IsUserInBlockList())
                return false;

            AuthInfo += "OK";
            return true;
        }
        abstract protected bool IsServerAvaible();
        abstract protected bool IsLoginPassOk();
        abstract protected bool IsUserInBlockList();
        abstract protected bool IsUserAlreadyConnected();
    }
    public class ServerAuth : Authorization
    {
        private ServerSide _server;
        public ServerAuth(NetworkStream sw, StreamReader sr, ServerSide server) 
            :base(sw, sr)
        {
            _server = server;
        }
        protected override bool IsServerAvaible()
        {
            byte[] answBuff;
            if (_server.CurrClCount > _server.MaxClCount)
            {
                answBuff = Encoding.Unicode.GetBytes(SERVER_OFF+"\r\n");
                _sw.Write(answBuff, 0, answBuff.Length);
                return false;
            }
            answBuff = Encoding.Unicode.GetBytes($"Server is available!\r\n");
            _sw.Write(answBuff, 0, answBuff.Length);

            return true;
        }

        protected override bool IsLoginPassOk()
        {
            byte[] answBuff;
            Login = _sr.ReadLine();
            Pass = _sr.ReadLine();

            AuthInfo += $"{Login} - connecting...";

            if (!_server.IsLoginPassOk(Login, Pass))
            {
                answBuff = Encoding.Unicode.GetBytes(LOGINPASS_ERROR+"\r\n");
                _sw.Write(answBuff, 0, answBuff.Length);
                AuthInfo += "Wrong Login/Pass";
                return false;
            }
            answBuff = Encoding.Unicode.GetBytes("Password checked!\r\n");
            _sw.Write(answBuff, 0, answBuff.Length);
            return true;
        }

        protected override bool IsUserAlreadyConnected()
        {
            byte[] answBuff;
            if (_server.IsUserAlreadyConnected(Login))
            {
                answBuff = Encoding.Unicode.GetBytes(ALREADY_CONNECTED+"\r\n");
                _sw.Write(answBuff, 0, answBuff.Length);
                AuthInfo += "Already connected";
                return true;
            }
            answBuff = Encoding.Unicode.GetBytes("You are original user!\r\n");
            _sw.Write(answBuff, 0, answBuff.Length);

            return false;
        }

        protected override bool IsUserInBlockList()
        {
            byte[] answBuff;
            if (_server.IsUserInBlockList(Login))
            {
                var timeExpired = _server.GetUserBlockTime(Login);

                answBuff = Encoding.Unicode.GetBytes($"{USER_BLOCKED} [{timeExpired}]!\r\n");
                _sw.Write(answBuff, 0, answBuff.Length);
                AuthInfo += "Blocked";
                return true;
            }
            answBuff = Encoding.Unicode.GetBytes($"You are not in block list!\r\n");
            _sw.Write(answBuff, 0, answBuff.Length);

            return false;
        }
    }

    public class ClientAuth : Authorization
    {
        public ClientAuth(string login, string pass, NetworkStream sw, StreamReader sr) 
            : base(sw, sr)
        {
            Pass = pass;
            Login = login;
        }
        protected override bool IsServerAvaible()
        {
            var servAvailble = _sr.ReadLine();
            
            if (servAvailble.Contains(SERVER_OFF))
            {
                AuthInfo += servAvailble;
                return false;
            }
            return true;
        }
        protected override bool IsLoginPassOk()
        {
            var loginBuff = Encoding.Unicode.GetBytes(Login + "\r\n");
            var passBuff = Encoding.Unicode.GetBytes(Pass + "\r\n");
            _sw.Write(loginBuff, 0, loginBuff.Length);
            _sw.Write(passBuff, 0, passBuff.Length);

            string answ = _sr.ReadLine();
            
            if (answ.Contains(LOGINPASS_ERROR))
            {
                AuthInfo += answ;
                return false;
            }

            return true;
        }
        protected override bool IsUserAlreadyConnected()
        {
            string answ = _sr.ReadLine();

            if (answ.Contains(ALREADY_CONNECTED))
            {
                AuthInfo += answ;
                return true;
            }

            return false;
        }
        protected override bool IsUserInBlockList()
        {
            string answ = _sr.ReadLine();

            if (answ.Contains(USER_BLOCKED))
            {
                AuthInfo += answ;
                return true;
            }

            return false;
        }
    }
}
