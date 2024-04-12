using System.Xml.Serialization;

namespace UPnPLibrary.Description.Device
{
    public class Icon
    {
        [XmlElement("mimetype")]
        public string Mimetype { get;  set; }

        [XmlElement("width")]
        public string Width { get;  set; }

        [XmlElement("height")]
        public string Height { get;  set; }

        [XmlElement("depth")]
        public string Depth { get;  set; }

        [XmlElement("url")]
        public string Url { get;  set; }
    }
}
