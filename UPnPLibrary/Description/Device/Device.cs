using System.Collections.Generic;
using System.Xml.Serialization;

namespace UPnPLibrary.Description.Device
{
    /// <summary>
    /// UPnPデバイス情報管理クラス
    /// </summary>
    public class Device
    {
        /// <summary>
        /// URIで表されるUPnPデバイスタイプ
        /// </summary>
        [XmlElement("deviceType")]
        public string DeviceType { get; set; } = null;

        /// <summary>
        /// エンドユーザー向けの概略説明
        /// </summary>
        [XmlElement("friendlyName")]
        public string FriendlyName { get;  set; } = null;

        /// <summary>
        /// メーカー名
        /// </summary>
        [XmlElement("manufacturer")]
        public string Manufacturer { get;  set; } = null;

        /// <summary>
        /// メーカーのWebサイト
        /// </summary>
        [XmlElement("manufacturerURL")]
        public string ManufacturerURL { get;  set; } = null;

        /// <summary>
        /// エンドユーザー向けの説明
        /// </summary>
        [XmlElement("modelDescription")]
        public string ModelDescription { get;  set; } = null;

        /// <summary>
        /// モデル名
        /// </summary>
        [XmlElement("modelName")]
        public string ModelName { get;  set; } = null;

        /// <summary>
        /// モデル番号
        /// </summary>
        [XmlElement("modelNumber")]
        public string ModelNumber { get;  set; } = null;

        /// <summary>
        /// モデルのWebサイト
        /// </summary>
        [XmlElement("modelURL")]
        public string ModelURL { get;  set; } = null;

        /// <summary>
        /// シリアル番号
        /// </summary>
        [XmlElement("serialNumber")]
        public string SerialNumber { get;  set; } = null;

        /// <summary>
        /// Unique Device Name<br/>
        /// uuid:で始まる汎用一意識別子
        /// </summary>
        [XmlElement("UDN")]
        public string Udn { get;  set; } = null;

        /// <summary>
        /// Universal Product Code<br/>
        /// 12桁の数字で表される統一商品コード
        /// </summary>
        [XmlElement("UPC")]
        public string Upc { get;  set; } = null;

        /// <summary>
        /// デバイスのアイコン情報
        /// </summary>
        [XmlArray("iconList")]
        [XmlArrayItem("icon")]
        public List<Icon> IconList { get; set; } = null;

        /// <summary>
        /// UPnPサービス概要情報
        /// </summary>
        [XmlArray("serviceList")]
        [XmlArrayItem("service")]
        public List<Service> ServiceList { get; set; } = null;

        /// <summary>
        /// 組み込みデバイス情報
        /// </summary>
        [XmlArray("deviceList")]
        [XmlArrayItem("device")]
        public List<Device> DeviceList { get; set; } = null;

        /// <summary>
        /// デバイスの制御用Webサイト
        /// </summary>
        [XmlElement("presentationURL")]
        public string PresentationURL { get; set; } = null;
    }
}
