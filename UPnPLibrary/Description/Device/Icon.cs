using System.Xml.Serialization;

namespace UPnPLibrary.Description.Device
{
    /// <summary>
    /// デバイスのアイコン情報管理クラス
    /// </summary>
    public class Icon
    {
        /// <summary>
        /// アイコンのMIME画像タイプ
        /// </summary>
        [XmlElement("mimetype")]
        public string Mimetype { get;  set; } = null;

        /// <summary>
        /// 整数で表されるアイコンの横幅（px）
        /// </summary>
        [XmlElement("width")]
        public string Width { get;  set; } = null;

        /// <summary>
        /// 整数で表されるアイコンの縦幅（px）
        /// </summary>
        [XmlElement("height")]
        public string Height { get;  set; } = null;

        /// <summary>
        /// 整数で表される色深度
        /// </summary>
        [XmlElement("depth")]
        public string Depth { get;  set; } = null;

        /// <summary>
        /// HTTPで取得可能なアイコン画像のパス
        /// </summary>
        [XmlElement("url")]
        public string Url { get;  set; } = null;
    }
}
