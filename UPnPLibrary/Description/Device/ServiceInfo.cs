using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UPnPLibrary.Description.Device
{
    public class ServiceInfo
    {
        /// <summary>
        /// serviceTypeタグの値
        /// </summary>
        public string ServiceType { get;  set; }

        /// <summary>
        /// serviceIdタグの値
        /// </summary>
        public string ServiceId { get;  set; }

        /// <summary>
        /// controlUrlタグの値
        /// </summary>
        public string ControlURL { get;  set; }

        /// <summary>
        /// eventSubUrlタグの値
        /// </summary>
        public string EventSubURL { get;  set; }

        /// <summary>
        /// SCPDURLタグの値
        /// </summary>
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

        private const string SERVICE_TYPE_TAG = "serviceType";
        private const string SERVICE_ID_TAG = "serviceId";
        private const string CONTROL_URL_TAG = "controlURL";
        private const string EVENT_SUB_URL_TAG = "eventSubURL";
        private const string SCPD_URL_TAG = "SCPDURL";

        public ServiceInfo() 
        {
        }

        public ServiceInfo(string serviceType, string serviceId, string controlURL, string eventSubURL, string scpdUrl)
        {
            ServiceType = serviceType;
            ServiceId = serviceId;
            ControlURL = controlURL;
            EventSubURL = eventSubURL;
            ScpdUrl = scpdUrl;
        }


        public ServiceInfo (Dictionary<string, string> map)
        {
            if (map.ContainsKey(SERVICE_TYPE_TAG))
            {
                ServiceType = map[SERVICE_TYPE_TAG];
            }

            if (map.ContainsKey(SERVICE_ID_TAG))
            {
                ServiceId = map[SERVICE_ID_TAG];
            }

            if (map.ContainsKey(CONTROL_URL_TAG))
            {
                ControlURL = map[CONTROL_URL_TAG];
            }

            if (map.ContainsKey(EVENT_SUB_URL_TAG))
            {
                EventSubURL = map[EVENT_SUB_URL_TAG];
            }

            if (map.ContainsKey(SCPD_URL_TAG))
            {
                ScpdUrl = map[SCPD_URL_TAG];
            }
        }
    }
}
