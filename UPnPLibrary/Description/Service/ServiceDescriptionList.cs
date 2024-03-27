using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPnPLibrary.Description.Service
{
    public class ServiceDescriptionList : IEnumerable
    {
        private Dictionary<string, ServiceDescription> serviceDescriptions = new Dictionary<string, ServiceDescription>();

        public ServiceDescription this[string serviceType] => serviceDescriptions[serviceType];

        public void Add(string serviceType, ServiceDescription service)
        {
            serviceDescriptions.Add(serviceType, service);
        }

        public bool Remove(string serviceType)
        {
            return serviceDescriptions.Remove(serviceType);
        }

        public bool Contains(string serviceType)
        {
            return serviceDescriptions.ContainsKey(serviceType);
        }

        public void Clear()
        {
            serviceDescriptions.Clear();
        }

        public IEnumerator GetEnumerator() => serviceDescriptions.GetEnumerator();
    }
}
