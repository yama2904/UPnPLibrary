using System.Xml.Serialization;

namespace UPnPLibrary.Description.Service
{
    public class AllowedValueRange
    {
        [XmlElement("minimum")]
        public string Minimum { get; set; }

        [XmlElement("maximum")]
        public string Maximum { get; set; }

        [XmlElement("step")]
        public string Step { get; set; }
    }
}
