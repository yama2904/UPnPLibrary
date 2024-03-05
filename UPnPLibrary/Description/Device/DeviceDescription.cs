using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UPnPLibrary.Description.Device
{
    /// <summary>
    /// UPnPのデバイス情報管理クラス
    /// </summary>
    public class DeviceDescription
    {
        public List<ServiceInfo> ServiceInfos { get; set; } = new List<ServiceInfo>();

        private const string ROOT_XMLNS = "urn:schemas-upnp-org:device-1-0";

        /// <summary>
        /// デバイス情報XML
        /// </summary>
        private readonly string _sourceXml = string.Empty;

        /// <summary>
        /// デバイス情報初期化
        /// </summary>
        /// <param name="sourceXml">locationへのリクエストにより取得したデバイス情報XML</param>
        public DeviceDescription(string sourceXml) 
        {
            _sourceXml = sourceXml;

            LoadServiceInfo();
        }

        private void LoadServiceInfo()
        {
            // Xml読み込み
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(_sourceXml);

            // 名前空間設定
            XmlNamespaceManager xmlns = new XmlNamespaceManager(xml.NameTable);
            xmlns.AddNamespace("n", ROOT_XMLNS);

            // serviceタグ取得
            XmlNodeList services = xml.SelectNodes("/n:root/n:device//n:serviceList/n:service", xmlns);

            // 各serviceタグの子要素取得
            foreach (XmlNode service in services)
            {
                Dictionary<string, string> map = new Dictionary<string, string>();
                foreach (XmlNode child in service.ChildNodes)
                {
                    map.Add(child.Name, child.InnerText);
                }

                ServiceInfos.Add(new ServiceInfo(map));
            }
        }
    }
}
