using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace UPnPLibrary.Description.Device
{
    /// <summary>
    /// UPnPサービス概要情報管理クラス
    /// </summary>
    public class Service
    {
        /// <summary>
        /// URIで表されるUPnPサービスタイプ
        /// </summary>
        [XmlElement("serviceType")]
        public string ServiceType { get;  set; } = null;

        /// <summary>
        /// URIで表されるサービス識別子
        /// </summary>
        [XmlElement("serviceId")]
        public string ServiceId { get;  set; } = null;

        /// <summary>
        /// UPnPサービスのリクエスト用相対URL
        /// </summary>
        [XmlElement("controlURL")]
        public string ControlURL { get;  set; } = null;

        /// <summary>
        /// UPnPサービスのイベント生成用相対URL
        /// </summary>
        [XmlElement("eventSubURL")]
        public string EventSubURL { get;  set; } = null;

        /// <summary>
        /// Service Control Protocol Description<br/>
        /// UPnPサービスの詳細情報取得用相対URL
        /// </summary>
        [XmlElement("SCPDURL")]
        public string ScpdUrl { get;  set; } = null;

        /// <summary>
        /// サービスタイプ名
        /// </summary>
        public string ServiceTypeName
        {
            get
            {
                return Regex.Replace(ServiceType, "^urn:.+:service:", "");
            }
        }

        /// <summary>
        /// サービス識別名
        /// </summary>
        public string ServiceIdName
        {
            get
            {
                return Regex.Replace(ServiceId, "^urn:.+:serviceId:", "");
            }
        }
    }
}
