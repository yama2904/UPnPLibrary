using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPnPLibrary.Description.Device
{
    public class ServiceInfo
    {
        public string ServiceType { get; }


        public string ServiceId { get; }


        public string ControlURL { get; }


        public string EventSubURL { get; }


        public string ScpdUrl { get; }

        private const string SERVICE_TYPE_TAG = "serviceType";
        private const string SERVICE_ID_TAG = "serviceId";
        private const string CONTROL_URL_TAG = "controlURL";
        private const string EVENT_SUB_URL_TAG = "eventSubURL";
        private const string SCPD_URL_TAG = "SCPDURL";

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
