using System;
using System.Net;

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
