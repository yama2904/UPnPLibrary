using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace UPnPLibrary.Description.Service
{
    /// <summary>
    /// UPnPのサービス情報管理クラス
    /// </summary>
    [XmlRoot("scpd", Namespace = "urn:schemas-upnp-org:service-1-0")]
    public class ServiceDescription
    {
        [XmlElement("specVersion")]
        public SpecVersion SpecVersion { get; set; }

        [XmlArray("actionList")]
        [XmlArrayItem("action")]
        public List<Action> ActionList = new List<Action>();

        [XmlArray("serviceStateTable")]
        [XmlArrayItem("stateVariable")]
        public List<StateVariable> ServiceStateTable = new List<StateVariable>();
    }
}
