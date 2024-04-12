using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace UPnPLibrary.Description.Service
{
    public class Action
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlArray("argumentList")]
        [XmlArrayItem("argument")]
        public List<Argument> ArgumentList { get; set; } = new List<Argument>();
    }
}
