using System.Xml.Serialization;

namespace UPnPLibrary.Description.Service
{
    public class Argument
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("direction")]
        public string Direction { get; set; }

        [XmlElement("relatedStateVariable")]
        public string RelatedStateVariable { get; set; }
    }
}
