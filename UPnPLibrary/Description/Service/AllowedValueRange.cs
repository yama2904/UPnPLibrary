using System.Xml.Serialization;

namespace UPnPLibrary.Description.Service
{
    /// <summary>
    /// UPnPサービスにおける引数値の範囲を管理するクラス
    /// </summary>
    public class AllowedValueRange
    {
        /// <summary>
        /// 引数の最小値
        /// </summary>
        [XmlElement("minimum")]
        public string Minimum { get; set; } = null;
        
        /// <summary>
        /// 引数の最大値
        /// </summary>
        [XmlElement("maximum")]
        public string Maximum { get; set; } = null;

        /// <summary>
        /// 引数の分割値。<br/>
        /// Minimum0、step0の場合は0,1,2...を指定し、step5の場合は0, 5,10...を指定する
        /// </summary>
        [XmlElement("step")]
        public string Step { get; set; } = null;
    }
}
