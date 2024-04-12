using System;
using System.Collections.Generic;
using System.Linq;
using UPnPLibrary;
using UPnPLibrary.Description.Device;
using UPnPLibrary.Description.Service;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UPnPDeviceDiscovery deviceClient = new UPnPDeviceDiscovery(UPnPType.InternetGatewayDevice);
            DeviceDescription device = deviceClient.FindDeviceAsync().Result;
            List<Service> services = device.GetServiceList();

            Service useService = services.Where(x => x.ServiceTypeName == "WANIPConnection:1").FirstOrDefault();

            UPnPClient serviceClient = new UPnPClient(deviceClient.UPnPUri);
            ServiceDescription service = serviceClient.RequestServiceDescriptionAsync(useService).Result;

            UPnPRequestMessage message = new UPnPRequestMessage("GetGenericPortMappingEntry", useService);
            Dictionary<string, string> response = serviceClient.RequestUPnPServiceAsync(message).Result;

            foreach (string key in response.Keys)
            {
                Console.WriteLine($"{key}={response[key]}");
            }

            string a = "";
        }
    }
}
