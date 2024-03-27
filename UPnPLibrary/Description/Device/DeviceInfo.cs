using System.Collections.Generic;

namespace UPnPLibrary.Description.Device
{
    public class DeviceInfo
    {
        public string FriendlyName { get;  set; }


        public string Manufacturer { get;  set; }


        public string ManufacturerURL { get;  set; }


        public string ModelDescription { get;  set; }


        public string ModelName { get;  set; }

        public string ModelNumber { get;  set; }

        public string ModelURL { get;  set; }

        public string SerialNumber { get;  set; }

        public string Udn { get;  set; }

        public string Upc { get;  set; }

        private const string FRIENDLY_NAME = "friendlyName";
        private const string MANUFACTURER = "manufacturer";
        private const string MANUFACTURER_URL = "manufacturerURL";
        private const string MODEL_DESCRIPTION = "modelDescription";
        private const string MODEL_NAME = "modelName";
        private const string MODEL_NUMBER = "modelNumber";
        private const string MODEL_URL = "modelURL";
        private const string SERIAL_NUMBER = "serialNumber";
        private const string UDN = "UDN";
        private const string UPC = "UPC";

        public DeviceInfo()
        {
        }

        public DeviceInfo(
                    string friendlyName,
                    string manufacturer,
                    string manufacturerURL,
                    string modelDescription,
                    string modelName,
                    string modelNumber,
                    string modelURL,
                    string serialNumber,
                    string udn,
                    string upc)
        {
            FriendlyName = friendlyName;
            Manufacturer = manufacturer;
            ManufacturerURL = manufacturerURL;
            ModelDescription = modelDescription;
            ModelName = modelName;
            ModelNumber = modelNumber;
            ModelURL = modelURL;
            SerialNumber = serialNumber;
            Udn = udn;
            Upc = upc;
        }


        public DeviceInfo(Dictionary<string, string> map)
        {
            if (map.ContainsKey(FRIENDLY_NAME))
            {
                FriendlyName = map[FRIENDLY_NAME];
            }

            if (map.ContainsKey(MANUFACTURER))
            {
                Manufacturer = map[MANUFACTURER];
            }

            if (map.ContainsKey(MANUFACTURER_URL))
            {
                ManufacturerURL = map[MANUFACTURER_URL];
            }

            if (map.ContainsKey(MODEL_DESCRIPTION))
            {
                ModelDescription = map[MODEL_DESCRIPTION];
            }

            if (map.ContainsKey(MODEL_NAME))
            {
                ModelName = map[MODEL_NAME];
            }

            if (map.ContainsKey(MODEL_NUMBER))
            {
                ModelNumber = map[MODEL_NUMBER];
            }

            if (map.ContainsKey(MODEL_URL))
            {
                ModelURL = map[MODEL_URL];
            }

            if (map.ContainsKey(SERIAL_NUMBER))
            {
                SerialNumber = map[SERIAL_NUMBER];
            }

            if (map.ContainsKey(UDN))
            {
                Udn = map[UDN];
            }

            if (map.ContainsKey(UPC))
            {
                Upc = map[UPC];
            }
        }
    }
}
