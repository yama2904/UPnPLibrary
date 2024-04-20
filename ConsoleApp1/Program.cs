using System;
using System.Collections.Generic;
using System.Linq;
using UPnPLibrary;
using UPnPLibrary.Description.Device;
using UPnPLibrary.Description.Service;
using UPnPLibrary.Ssdp;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // デバイス検索
            UPnPDeviceDiscover discover = new UPnPDeviceDiscover();
            List<UPnPDeviceAccess> deviceAccesses = discover.FindDeviceAsync().Result;
            
            // UPnPクライアント初期化
            UPnPClient upnpClient = new UPnPClient(deviceAccesses[0]);

            // UPnPデバイス情報取得
            DeviceDescription deviceDescription = upnpClient.RequestDeviceDescriptionAsync().Result;

            // UPnPデバイスのサービス情報取得
            List<Service> services = deviceDescription.GetServiceList();

            // 使用するサービス絞り込み
            Service useService = services.Where(x => x.ServiceTypeName == "WANIPConnection:1").FirstOrDefault();

            // UPnPサービス詳細情報取得
            ServiceDescription serviceDescription = upnpClient.RequestServiceDescriptionAsync(useService).Result;

            // UPnPアクション実行
            UPnPActionRequestMessage message = new UPnPActionRequestMessage(useService, "GetExternalIPAddress");
            Dictionary<string, string> response = upnpClient.RequestUPnPActionAsync(message).Result;

            // 戻り値出力
            foreach (string key in response.Keys)
            {
                Console.WriteLine($"{key}={response[key]}");
            }

            string a = "";
        }
    }
}
