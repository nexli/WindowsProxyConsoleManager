using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProxyM
{
    class WinProxy
    {
        [DllImport("wininet.dll")]
        static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        const int INTERNET_OPTION_SETTINGS_CHANGED = 39;

        const int INTERNET_OPTION_REFRESH = 37;

        const int PROXY_ON = 1;

        const int PROXY_OFF = 0;

        const string UserRoot = "HKEY_CURRENT_USER";

        const string SubKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";

        const string ProxyRegPath = UserRoot + "\\" + SubKey;

        static event Action RegisterChanged;

        static WinProxy()
        {
            RegisterChanged += InternetOptionRefresh;
        }
        
        public static string ProxyServer
        {
            get
            {
                return Registry.GetValue(ProxyRegPath, "ProxyServer", "") as string;
            }

            set
            {
                Registry.SetValue(ProxyRegPath, "ProxyServer", value);

                RegisterChanged();
            }
        }

        public static bool ProxyEnable
        {
            get
            {
                return PROXY_ON == (int) Registry.GetValue(ProxyRegPath, "ProxyEnable", PROXY_OFF);
            }

            set
            {
                Registry.SetValue(ProxyRegPath, "ProxyEnable", value ? PROXY_ON : PROXY_OFF);

                RegisterChanged();
            }
        }

        static void InternetOptionRefresh()
        {
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);

            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }
    }
}
