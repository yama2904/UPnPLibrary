using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace UPnPLibrary.Description.Service
{
    public class StateVariable
    {
        [XmlAttribute("sendEvents")]
        public string SendEvents { get; set; }

        [XmlAttribute("multicast")]
        public string Multicast { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("dataType")]
        public string DataType { get; set; }

        [XmlElement("defaultValue")]
        public string DefaultValue { get; set; }

        [XmlElement("allowedValueRange")]
        public AllowedValueRange AllowedValueRange { get; set; } = new AllowedValueRange();

        [XmlArray("allowedValueList")]
        [XmlArrayItem("allowedValue")]
        public List<string> AllowedValueList { get; set; } = new List<string>();
    }
}
