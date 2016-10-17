using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProxyM
{
    class ProxyOption
    {
        [Option('s', "server", HelpText = "Default server Loopback")]
        public string Server { get; set; }

        [Option('p', "port")]
        public int? Port { get; set; }

        [Option('e', "enable")]
        public bool? Enable { get; set; } 
    }
}
