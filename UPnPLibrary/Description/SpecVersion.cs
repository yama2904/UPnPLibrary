using System.Xml.Serialization;

namespace UPnPLibrary.Description
{
    public class SpecVersion
    {
        [XmlElement("major")]
        public string Major { get; set; }

        [XmlElement("minor")]
        public string Minor { get; set; }
    }
}
