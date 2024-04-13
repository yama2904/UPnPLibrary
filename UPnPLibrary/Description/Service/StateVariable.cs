using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace UPnPLibrary.Description.Service
{
    /// <summary>
    /// UPnPサービス状態変数管理クラス
    /// </summary>
    public class StateVariable
    {
        /// <summary>
        /// 状態変数が変化した時にイベントメッセージを生成するか
        /// </summary>
        [XmlAttribute("sendEvents")]
        public string SendEvents { get; set; } = null;

        /// <summary>
        /// イベントメッセージをマルチキャストイベンティングを使用して配信するか
        /// </summary>
        [XmlAttribute("multicast")]
        public string Multicast { get; set; } = null;

        /// <summary>
        /// 状態変数名
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; } = null;

        /// <summary>
        /// 状態変数のデータ型
        /// </summary>
        [XmlElement("dataType")]
        public StateVariableDataType DataType { get; set; } = null;

        /// <summary>
        /// 期待値、又は初期値
        /// </summary>
        [XmlElement("defaultValue")]
        public string DefaultValue { get; set; } = null;

        /// <summary>
        /// 引数に指定される数値の範囲
        /// </summary>
        [XmlElement("allowedValueRange")]
        public AllowedValueRange AllowedValueRange { get; set; } = null;

        /// <summary>
        /// 引数に指定される値のリスト
        /// </summary>
        [XmlArray("allowedValueList")]
        [XmlArrayItem("allowedValue")]
        public List<string> AllowedValueList { get; set; } = null;
    }
}
