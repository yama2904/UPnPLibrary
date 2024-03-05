using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UPnPLibrary;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UPnPDevicesDiscovery.FindDevices();
        }
    }
}
