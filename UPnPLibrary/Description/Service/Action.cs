using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace UPnPLibrary.Description.Service
{
    /// <summary>
    /// UPnPサービスアクション情報管理クラス
    /// </summary>
    public class Action
    {
        /// <summary>
        /// アクション名
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; } = null;

        /// <summary>
        /// 引数リスト
        /// </summary>
        [XmlArray("argumentList")]
        [XmlArrayItem("argument")]
        public List<Argument> ArgumentList { get; set; } = null;
    }
}
