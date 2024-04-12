using System;
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


        public Uri UPnPUri { get; set; } = null;
    }
}
