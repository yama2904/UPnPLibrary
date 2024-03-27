using System.Collections.Generic;
using System.Net;
using UPnPLibrary.Description.Device;
using UPnPLibrary.Description.Service;

namespace UPnPLibrary
{
    public class UPnPDevice
    {
        /// <summary>
        /// UPnPデバイスのIPアドレス
        /// </summary>
        public IPAddress IpAddress { get; set; } = null;


        public string UPnPUrl
        {
            get 
            {
                return uPnpUrl; 
            }
            set
            {
                uPnpUrl = value;
                if (!uPnpUrl.EndsWith("/"))
                {
                    uPnpUrl += "/";
                }
            }
        }
        private string uPnpUrl = string.Empty;


        public DeviceDescription DeviceDescription { get; set; } = null;

        public Dictionary<string, ServiceDescription> ServiceDescriptionList { get; set; } = new Dictionary<string, ServiceDescription>();
    }
}
