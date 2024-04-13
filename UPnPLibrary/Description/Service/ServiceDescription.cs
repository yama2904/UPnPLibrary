using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace UPnPLibrary.Description.Service
{
    /// <summary>
    /// UPnPサービス詳細情報管理クラス
    /// </summary>
    [XmlRoot("scpd", Namespace = "urn:schemas-upnp-org:service-1-0")]
    public class ServiceDescription
    {
        /// <summary>
        /// UPnPデバイスアーキテクチャのバージョン
        /// </summary>
        [XmlElement("specVersion")]
        public SpecVersion SpecVersion { get; set; } = null;

        /// <summary>
        /// サービスアクションリスト
        /// </summary>
        [XmlArray("actionList")]
        [XmlArrayItem("action")]
        public List<Action> ActionList = null;

        /// <summary>
        /// サービス状態変数リスト
        /// </summary>
        [XmlArray("serviceStateTable")]
        [XmlArrayItem("stateVariable")]
        public List<StateVariable> ServiceStateTable = null;
    }
}
