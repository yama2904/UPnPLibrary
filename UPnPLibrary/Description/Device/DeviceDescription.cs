using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace UPnPLibrary.Description.Device
{
    /// <summary>
    /// UPnPのデバイス情報管理クラス
    /// </summary>
    [XmlRoot("root", Namespace = "urn:schemas-upnp-org:device-1-0")]
    public class DeviceDescription
    {
        [XmlElement("specVersion")]
        public SpecVersion SpecVersion { get; set; }

        [XmlElement("device")]
        public Device Device { get; set; }

        /// <summary>
        /// 自身が管理しているすべてのServiceをリストにまとめて返す
        /// </summary>
        /// <returns></returns>
        public List<Service> GetServiceList()
        {
            return GetServiceList(Device);
        }

        private List<Service> GetServiceList(Device device)
        {
            IEnumerable<Service> services = new List<Service>();

            if (device.DeviceList != null && device.DeviceList.Count > 0)
            {
                foreach (Device d in device.DeviceList)
                {
                    services = services.Concat(GetServiceList(d));
                }
            }

            return services.Concat(device.ServiceList).ToList();
        }
    }
}
