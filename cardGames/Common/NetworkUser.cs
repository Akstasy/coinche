using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class NetworkUser
    {
        public string Ip { get; private set; }
        public UInt16 Port { get; private set; }
        public string Hash { get; private set; }

        public NetworkUser(string ip, UInt16 port, string hash = "")
        {
            Ip = ip;
            Port = port;
            Hash = hash;
        }

        public void sendObject() { }

    }
}
