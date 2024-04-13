using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace UPnPLibrary.Description.Device
{
    /// <summary>
    /// UPnPデバイス情報管理クラス
    /// </summary>
    [XmlRoot("root", Namespace = "urn:schemas-upnp-org:device-1-0")]
    public class DeviceDescription
    {
        /// <summary>
        /// UPnPデバイスアーキテクチャのバージョン
        /// </summary>
        [XmlElement("specVersion")]
        public SpecVersion SpecVersion { get; set; } = null;

        /// <summary>
        /// UPnPデバイス情報
        /// </summary>
        [XmlElement("device")]
        public Device Device { get; set; } = null;

        /// <summary>
        /// 自身が管理している全てのServiceをリストにまとめて返す
        /// </summary>
        /// <returns></returns>
        public List<Service> GetServiceList()
        {
            return GetServiceList(Device);
        }

        /// <summary>
        /// 指定したデバイスが管理している全てのServiceを再帰的にリストにまとめて返す
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        private List<Service> GetServiceList(Device device)
        {
            // 戻り値
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
