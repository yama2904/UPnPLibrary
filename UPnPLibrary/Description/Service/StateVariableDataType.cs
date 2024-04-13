using System.Xml.Serialization;

namespace UPnPLibrary.Description.Service
{
    /// <summary>
    /// UPnPサービス状態変数のデータ型管理クラス
    /// </summary>
    public class StateVariableDataType
    {
        /// <summary>
        /// 状態変数のデータ型。type属性が存在する場合は常にstring
        /// </summary>
        [XmlText]
        public string DataType { get; set; } = null;

        /// <summary>
        /// ベンダーが定義した拡張データ型
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; } = null;
    }
}
