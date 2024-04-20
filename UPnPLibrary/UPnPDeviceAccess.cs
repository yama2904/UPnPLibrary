using System;
using System.Net;

namespace UPnPLibrary
{
    /// <summary>
    /// UPnPデバイスアクセス情報
    /// </summary>
    public class UPnPDeviceAccess
    {
        /// <summary>
        /// UPnPデバイスのIPアドレス
        /// </summary>
        public IPAddress IpAddress { get; set; } = null;

        /// <summary>
        /// UPnPデバイスへのリクエスト用URL
        /// </summary>
        public Uri UPnPUrl { get; private set; } = null;

        /// <summary>
        /// UPnPデバイス情報取得用URL
        /// </summary>
        public string Location
        {
            get {  return location; }
            set
            {
                location = value;

                string url = new Uri(location).GetLeftPart(UriPartial.Authority);
                UPnPUrl = new Uri(url);
            }
        }
        private string location = null;
    }
}
