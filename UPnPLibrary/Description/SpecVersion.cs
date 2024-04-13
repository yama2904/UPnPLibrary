using System.Xml.Serialization;

namespace UPnPLibrary.Description
{
    /// <summary>
    /// UPnPデバイスアーキテクチャのバージョン情報管理クラス
    /// </summary>
    public class SpecVersion
    {
        /// <summary>
        /// UPnPデバイスアーキテクチャのメジャーバージョン
        /// </summary>
        [XmlElement("major")]
        public string Major { get; set; } = null;

        /// <summary>
        /// UPnPデバイスアーキテクチャのマイナーバージョン
        /// </summary>
        [XmlElement("minor")]
        public string Minor { get; set; } = null;
    }
}
