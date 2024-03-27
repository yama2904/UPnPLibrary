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
            UPnPDevice device = new UPnPDeviceDiscovery(UPnPTarget.InternetGatewayDevice).FindDevice();

            UPnPRequestMessage message = new UPnPRequestMessage("GetExternalIPAddress", "WANIPConnection:1");
            Dictionary<string, string> response = new UPnPClient(device).RequestAsync(message).Result;
            
            foreach (string key in response.Keys)
            {
                Console.WriteLine($"{key}={response[key]}");
            }

            string a = "";
        }
    }
}
