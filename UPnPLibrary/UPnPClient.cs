using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using UPnPLibrary.Description.Device;
using UPnPLibrary.Description.Service;

namespace UPnPLibrary
{
    /// <summary>
    /// UPnPデバイスとの送受信を管理するクラス
    /// </summary>
    public class UPnPClient
    {
        /// <summary>
        /// 送受信先UPnPデバイスアクセス情報
        /// </summary>
        public UPnPDeviceAccess DeviceAccess { get; private set; } = null;

        /// <summary>
        /// 送受信先UPnPデバイスを指定してインスタンスを初期化する
        /// </summary>
        /// <param name="deviceAccess">送受信先UPnPデバイスアクセス情報</param>
        public UPnPClient(UPnPDeviceAccess deviceAccess) 
        {
            DeviceAccess = deviceAccess;
        }

        /// <summary>
        /// UPnPデバイス情報取得
        /// </summary>
        /// <returns>取得したUPnPデバイス情報</returns>
        public async Task<DeviceDescription> RequestDeviceDescriptionAsync()
        {
            // 戻り値
            DeviceDescription device = new DeviceDescription();

            using (var client = new HttpClient())
            {
                // GETリクエスト
                HttpResponseMessage response = await client.GetAsync(DeviceAccess.Location);
                Stream stream = await response.Content.ReadAsStreamAsync();

                // XML読み込み
                XmlSerializer serializer = new XmlSerializer(typeof(DeviceDescription));
                device = serializer.Deserialize(stream) as DeviceDescription;
            }

            return device;
        }

        /// <summary>
        /// UPnPサービス詳細情報取得
        /// </summary>
        /// <param name="service">取得するUPnPサービス</param>
        /// <returns>取得したUPnPサービス詳細情報</returns>
        public async Task<ServiceDescription> RequestServiceDescriptionAsync(Service service)
        {
            // 戻り値
            ServiceDescription serviceDescription = new ServiceDescription();

            using (var client = new HttpClient())
            {
                // GETリクエスト
                HttpResponseMessage response = await client.GetAsync(new Uri(DeviceAccess.UPnPUrl, service.ScpdUrl));
                Stream stream = await response.Content.ReadAsStreamAsync();

                // XML読み込み
                XmlSerializer serializer = new XmlSerializer(typeof(ServiceDescription));
                serviceDescription = serializer.Deserialize(stream) as ServiceDescription;
            }

            return serviceDescription;
        }

        /// <summary>
        /// UPnPアクションリクエスト
        /// </summary>
        /// <param name="message">リクエストメッセージ</param>
        /// <returns>レスポンスメッセージ</returns>
        public async Task<Dictionary<string, string>> RequestUPnPActionAsync(UPnPActionRequestMessage message)
        {
            // 戻り値
            Dictionary<string, string> responseMap = new Dictionary<string, string>();

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(DeviceAccess.UPnPUrl, message.Service.ControlURL)))
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
                    UPnPActionException exception = CreateUPnPActionException(responseXml, "UPnPリクエストに失敗しました。");
                    throw exception;
                }

                // Xml読み込み
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(responseXml);

                // 名前空間設定
                XmlNamespaceManager xmlns = new XmlNamespaceManager(xml.NameTable);
                xmlns.AddNamespace("n", $"urn:schemas-upnp-org:service:{message.Service.ServiceTypeName}");

                XmlNodeList values = xml.SelectNodes($"//n:{message.ActionName}Response/*", xmlns);
                foreach (XmlNode value in values)
                {
                    responseMap.Add(value.Name, value.InnerText);
                }
            }

            return responseMap;
        }

        /// <summary>
        /// UPnPエラーを解析してUPnPアクションエラーインスタンスを返す
        /// </summary>
        /// <param name="responseXml">UPnPエラーが記載されたXML</param>
        /// <param name="message">例外メッセージ</param>
        /// <returns>生成したUPnPアクションエラーインスタンス</returns>
        private UPnPActionException CreateUPnPActionException(string responseXml, string message)
        {
            // 戻り値
            UPnPActionException exception = new UPnPActionException(message);

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
