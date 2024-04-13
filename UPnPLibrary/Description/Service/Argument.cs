using System.Xml.Serialization;

namespace UPnPLibrary.Description.Service
{
    /// <summary>
    /// UPnPアクション引数情報管理クラス
    /// </summary>
    public class Argument
    {
        /// <summary>
        /// 引数名
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; } = null;

        /// <summary>
        /// in:送信時に指定する引数<br/>
        /// out:受信時に渡される引数
        /// </summary>
        [XmlElement("direction")]
        public string Direction { get; set; } = null;

        /// <summary>
        /// 対応するstateVariable名
        /// </summary>
        [XmlElement("relatedStateVariable")]
        public string RelatedStateVariable { get; set; } = null;
    }
}
