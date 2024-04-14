using System;
using System.Collections.Generic;
using System.Linq;
using UPnPLibrary;
using UPnPLibrary.Description.Device;
using UPnPLibrary.Ssdp;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // デバイス検索
            UPnPDeviceDiscover deviceClient = new UPnPDeviceDiscover(UPnPType.InternetGatewayDevice);
            DeviceDescription device = deviceClient.FindDeviceAsync().Result;
            
            // UPnPデバイスのサービス情報取得
            List<Service> services = device.GetServiceList();

            // 使用するサービス絞り込み
            Service useService = services.Where(x => x.ServiceTypeName == "WANIPConnection:1").FirstOrDefault();

            // UPnPクライアント初期化
            UPnPClient serviceClient = new UPnPClient(deviceClient.UPnPUri);

            // UPnPサービス詳細情報取得
            //ServiceDescription service = serviceClient.RequestServiceDescriptionAsync(useService).Result;

            // UPnPアクション実行
            UPnPActionRequestMessage message = new UPnPActionRequestMessage(useService, "GetGenericPortMappingEntry");
            Dictionary<string, string> response = serviceClient.RequestUPnPActionAsync(message).Result;

            // 戻り値出力
            foreach (string key in response.Keys)
            {
                Console.WriteLine($"{key}={response[key]}");
            }

            string a = "";
        }
    }
}
