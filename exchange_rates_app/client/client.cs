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
  
    public class Client: INotifyPropertyChanged
    {

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
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
