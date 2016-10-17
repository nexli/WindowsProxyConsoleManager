using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using System.Net;

namespace ProxyM
{
    class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default
                .ParseArguments<ProxyOption>(args)
                .MapResult(
                    (ProxyOption opts) => Execute(opts),
                    errs => 1
                );
        }

        static int Execute(ProxyOption option)
        {
            try
            {
                if (option.Port != null)
                {
                    WinProxy.ProxyServer = string.Format(
                        "{0}:{1}", 
                        (option.Server != null ? option.Server : IPAddress.Loopback.ToString()), 
                        option.Port.Value
                    );
                }
                else if (option.Server != null)
                {
                    WinProxy.ProxyServer = option.Server;
                }

                if (option.Enable != null)
                {
                    WinProxy.ProxyEnable = option.Enable.Value;
                }

                Console.WriteLine();

                Console.WriteLine("Status: {0}", WinProxy.ProxyEnable ? "Enable" : "Disabled");

                Console.WriteLine("Server: {0}", WinProxy.ProxyServer);
            }
            catch
            {

                return 1;
            }

            return 0;
        }
    }
}
