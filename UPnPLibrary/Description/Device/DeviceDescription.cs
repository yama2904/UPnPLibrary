using System.Collections.Generic;
using System.Xml;

namespace UPnPLibrary.Description.Device
{
    /// <summary>
    /// UPnPのデバイス情報管理クラス
    /// </summary>
    public class DeviceDescription
    {
        public string RootXmlNs { get; set; } = "urn:schemas-upnp-org:device-1-0";

        public string MajorVersion { get; set; }

        public string MinorVersion { get; set; }

        public string PresentationURL { get; set; }

        public List<DeviceInfo> DeviceInfos { get; set; } = new List<DeviceInfo>();

        public List<ServiceInfo> ServiceInfos { get; set; } = new List<ServiceInfo>();

        public List<IconInfo> IconInfos { get; set; } = new List<IconInfo>();

        private const string MAJOR = "major";
        private const string MINOR = "minor";
        private const string PRESENTATION_URL = "presentationURL";

        /// <summary>
        /// デバイス情報XML
        /// </summary>
        private string _sourceXml = string.Empty;

        /// <summary>
        /// デバイス情報XML読み込み
        /// </summary>
        /// <param name="sourceXml">locationへのリクエストにより取得したデバイス情報XML</param>
        public void LoadXml(string sourceXml)
        {
            _sourceXml = sourceXml;

            // Xml読み込み
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(_sourceXml);

            LoadVersion(xml);
            LoadPresentationUrl(xml);
            LoadDeviceInfo(xml);
            LoadServiceInfo(xml);
            LoadIconInfo(xml);
        }

        private void LoadVersion(XmlDocument xml)
        {
            // 名前空間設定
            XmlNamespaceManager xmlns = new XmlNamespaceManager(xml.NameTable);
            xmlns.AddNamespace("n", RootXmlNs);

            // バージョン取得
            XmlNodeList versions = xml.SelectNodes("/n:root/n:specVersion/*", xmlns);

            foreach (XmlNode version in versions)
            {
                if (version.Name == MAJOR)
                {
                    MajorVersion = version.InnerText;
                }

                if (version.Name == MINOR)
                {
                    MinorVersion = version.InnerText;
                }
            }
        }

        private void LoadPresentationUrl(XmlDocument xml)
        {
            // 名前空間設定
            XmlNamespaceManager xmlns = new XmlNamespaceManager(xml.NameTable);
            xmlns.AddNamespace("n", RootXmlNs);

            // プレゼンテーションURL取得
            XmlNodeList presentation = xml.SelectNodes("/n:root//n:device/n:presentationURL", xmlns);

            foreach (XmlNode node in presentation)
            {
                if (node.Name == PRESENTATION_URL)
                {
                    PresentationURL = node.InnerText;
                    break;
                }
            }
        }

        private void LoadDeviceInfo(XmlDocument xml)
        {
            // 名前空間設定
            XmlNamespaceManager xmlns = new XmlNamespaceManager(xml.NameTable);
            xmlns.AddNamespace("n", RootXmlNs);

            // serviceタグ取得
            XmlNodeList services = xml.SelectNodes("/n:root//n:device", xmlns);

            // 各serviceタグの子要素取得
            foreach (XmlNode service in services)
            {
                Dictionary<string, string> map = new Dictionary<string, string>();
                foreach (XmlNode child in service.ChildNodes)
                {
                    map.Add(child.Name, child.InnerText);
                }

                DeviceInfos.Add(new DeviceInfo(map));
            }
        }

        private void LoadServiceInfo(XmlDocument xml)
        {
            // 名前空間設定
            XmlNamespaceManager xmlns = new XmlNamespaceManager(xml.NameTable);
            xmlns.AddNamespace("n", RootXmlNs);

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

        private void LoadIconInfo(XmlDocument xml)
        {
            // 名前空間設定
            XmlNamespaceManager xmlns = new XmlNamespaceManager(xml.NameTable);
            xmlns.AddNamespace("n", RootXmlNs);

            // serviceタグ取得
            XmlNodeList services = xml.SelectNodes("/n:root//n:iconList/n:icon", xmlns);

            // 各serviceタグの子要素取得
            foreach (XmlNode service in services)
            {
                Dictionary<string, string> map = new Dictionary<string, string>();
                foreach (XmlNode child in service.ChildNodes)
                {
                    map.Add(child.Name, child.InnerText);
                }

                IconInfos.Add(new IconInfo(map));
            }
        }
    }
}
