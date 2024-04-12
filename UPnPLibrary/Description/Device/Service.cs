using System.Xml.Serialization;

namespace UPnPLibrary.Description.Device
{
    public class Service
    {
        [XmlElement("serviceType")]
        public string ServiceType { get;  set; }

        [XmlElement("serviceId")]
        public string ServiceId { get;  set; }

        [XmlElement("controlURL")]
        public string ControlURL { get;  set; }

        [XmlElement("eventSubURL")]
        public string EventSubURL { get;  set; }

        [XmlElement("SCPDURL")]
        public string ScpdUrl { get;  set; }

        /// <summary>
        /// serviceType名
        /// </summary>
        public string ServiceTypeName
        {
            get
            {
                return ServiceType.Replace("urn:schemas-upnp-org:service:", "");
            }
        }

        /// <summary>
        /// serviceId名
        /// </summary>
        public string ServiceIdName
        {
            get
            {
                return ServiceId.Replace("urn:upnp-org:serviceId:", "");
            }
        }
    }
}
