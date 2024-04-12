using System.Collections.Generic;
using System.Xml.Serialization;

namespace UPnPLibrary.Description.Device
{
    public class Device
    {
        [XmlElement("deviceType")]
        public string DeviceType { get; set; }

        [XmlElement("friendlyName")]
        public string FriendlyName { get;  set; }

        [XmlElement("manufacturer")]
        public string Manufacturer { get;  set; }

        [XmlElement("manufacturerURL")]
        public string ManufacturerURL { get;  set; }

        [XmlElement("modelDescription")]
        public string ModelDescription { get;  set; }

        [XmlElement("modelName")]
        public string ModelName { get;  set; }

        [XmlElement("modelNumber")]
        public string ModelNumber { get;  set; }

        [XmlElement("modelURL")]
        public string ModelURL { get;  set; }

        [XmlElement("serialNumber")]
        public string SerialNumber { get;  set; }

        [XmlElement("UDN")]
        public string Udn { get;  set; }

        [XmlElement("UPC")]
        public string Upc { get;  set; }

        [XmlArray("iconList")]
        [XmlArrayItem("icon")]
        public List<Icon> IconList { get; set; } = new List<Icon>();

        [XmlArray("serviceList")]
        [XmlArrayItem("service")]
        public List<Service> ServiceList { get; set; } = new List<Service>();

        [XmlArray("deviceList")]
        [XmlArrayItem("device")]
        public List<Device> DeviceList { get; set; } = new List<Device>();

        [XmlElement("presentationURL")]
        public string PresentationURL { get; set; }
    }
}
