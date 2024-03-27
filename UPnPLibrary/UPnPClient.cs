using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UPnPLibrary.Description.Device;
using System.Xml;
using System.Collections.Generic;

namespace UPnPLibrary
{
    public class UPnPClient
    {
        public UPnPDevice UPnPDevice { get; private set; }

        public UPnPClient(UPnPDevice device)
        {
            UPnPDevice = device;
        }

        public async Task<Dictionary<string, string>> RequestAsync(UPnPRequestMessage message)
        {
            // 戻り値
            Dictionary<string, string> responseMap = new Dictionary<string, string>();

            ServiceInfo serviceInfo = UPnPDevice.DeviceDescription.ServiceInfos.Where(x => x.ServiceTypeName == message.ServiceType).FirstOrDefault();

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(new Uri(UPnPDevice.UPnPUrl), serviceInfo.ControlURL)))
            using (var content = new StringContent(message.CreateMessage(), Encoding.UTF8, "text/xml"))
            {
                request.Content = content;
                request.Headers.Add("SOAPACTION", $"\"urn:schemas-upnp-org:service:{message.ServiceType}#{message.ActionName}\"");

                HttpResponseMessage response = await client.SendAsync(request);
                byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                string responseXml = Encoding.UTF8.GetString(bytes);

                // Xml読み込み
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(responseXml);

                // 名前空間設定
                XmlNamespaceManager xmlns = new XmlNamespaceManager(xml.NameTable);
                xmlns.AddNamespace("n", serviceInfo.ServiceType);

                XmlNodeList values = xml.SelectNodes($"//n:{message.ActionName}Response/*", xmlns);
                foreach (XmlNode value in values)
                {
                    responseMap.Add(value.Name, value.InnerText);
                }
            }

            return responseMap;
        }
    }
}
