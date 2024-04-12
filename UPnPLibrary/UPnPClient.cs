using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UPnPLibrary.Description.Device;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UPnPLibrary.Description.Service;

namespace UPnPLibrary
{
    public class UPnPClient
    {
        public Uri UPnPUri { get; set; } = null;

        public UPnPClient(Uri uPnPUri) 
        {
            UPnPUri = uPnPUri;
        }

        public async Task<ServiceDescription> RequestServiceDescriptionAsync(Service service)
        {
            // 戻り値
            ServiceDescription serviceDescription = new ServiceDescription();

            using (var client = new HttpClient())
            {
                // GETリクエスト
                HttpResponseMessage response = await client.GetAsync(new Uri(UPnPUri, service.ScpdUrl));
                Stream stream = await response.Content.ReadAsStreamAsync();

                // XML読み込み
                XmlSerializer serializer = new XmlSerializer(typeof(ServiceDescription));
                serviceDescription = serializer.Deserialize(stream) as ServiceDescription;
            }

            return serviceDescription;
        }

        public async Task<Dictionary<string, string>> RequestUPnPServiceAsync(UPnPRequestMessage message)
        {
            // 戻り値
            Dictionary<string, string> responseMap = new Dictionary<string, string>();

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(UPnPUri, message.Service.ControlURL)))
            using (var content = new StringContent(message.CreateMessage(), Encoding.UTF8, "text/xml"))
            {
                request.Content = content;
                request.Headers.Add("SOAPACTION", $"\"urn:schemas-upnp-org:service:{message.Service}#{message.ActionName}\"");

                HttpResponseMessage response = await client.SendAsync(request);
                byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                string responseXml = Encoding.UTF8.GetString(bytes);

                // HTTPステータスコードが500の場合はUPnPError
                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    UPnPResponseException exception = CreateUPnPResponseException(responseXml, "UPnPリクエストに失敗しました。");
                    throw exception;
                }

                // Xml読み込み
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(responseXml);

                // 名前空間設定
                XmlNamespaceManager xmlns = new XmlNamespaceManager(xml.NameTable);
                xmlns.AddNamespace("n", $"urn:schemas-upnp-org:service:{message.Service}");

                XmlNodeList values = xml.SelectNodes($"//n:{message.ActionName}Response/*", xmlns);
                foreach (XmlNode value in values)
                {
                    responseMap.Add(value.Name, value.InnerText);
                }
            }

            return responseMap;
        }

        private UPnPResponseException CreateUPnPResponseException(string responseXml, string message)
        {
            // 戻り値
            UPnPResponseException exception = new UPnPResponseException(message);

            // Xml読み込み
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(responseXml);

            // 名前空間設定
            XmlNamespaceManager xmlns = new XmlNamespaceManager(xml.NameTable);
            xmlns.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");
            xmlns.AddNamespace("e", "urn:schemas-upnp-org:control-1-0");

            XmlNodeList faults = xml.SelectNodes("/s:Envelope/s:Body/s:Fault/*", xmlns);
            foreach(XmlNode fault in faults)
            {
                if (fault.Name == "faultcode")
                {
                    exception.FaultCode = fault.InnerText;
                }

                if (fault.Name == "faultstring")
                {
                    exception.FaultString = fault.InnerText;
                }

                if (fault.Name == "detail")
                {
                    XmlNodeList errors = fault.SelectNodes("e:UPnPError/*", xmlns);
                    foreach (XmlNode error in errors)
                    {
                        if (error.Name == "errorCode")
                        {
                            exception.ErrorCode = error.InnerText;
                        }

                        if (error.Name == "errorDescription")
                        {
                            exception.ErrorDescription = error.InnerText;
                        }
                    }
                }
            }

            return exception;
        }
    }
}
