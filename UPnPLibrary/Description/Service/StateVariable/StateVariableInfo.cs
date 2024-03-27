using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace UPnPLibrary.Description.Service.StateVariable
{
    public class StateVariableInfo
    {
        public string SendEvents { get; set; }

        public string Name { get; set; }

        public string DataType { get; set; }

        public string DefaultValue { get; set; }

        public AllowedValueRange AllowedValueRange { get; set; } = new AllowedValueRange();

        public List<string> AllowedValues { get; set; } = new List<string>();

        private const string SEND_EVENTS = "sendEvents";
        private const string NAME = "name";
        private const string DATA_TYPE = "dataType";
        private const string DEFAULT_VALUE = "defaultValue";
        private const string ALLOWED_VALUE_RANGE = "allowedValueRange";
        private const string ALLOWED_VALUE_LIST = "allowedValueList";

        public StateVariableInfo() 
        {
        }

        public StateVariableInfo(string name, string dataType, string defaultValue)
        {
            Name = name;
            DataType = dataType;
            DefaultValue = defaultValue;
        }

        public StateVariableInfo(string name, string dataType, string defaultValue, string minimum, string maximum, string step)
        {
            Name = name;
            DataType = dataType;
            DefaultValue = defaultValue;
            AllowedValueRange = new AllowedValueRange(minimum, maximum, step);
        }

        public StateVariableInfo(string name, string dataType, string defaultValue, params string[] allowedValues)
        {
            Name = name;
            DataType = dataType;
            DefaultValue = defaultValue;
            AllowedValues = allowedValues.ToList();
        }

        public void LoadXml(string sourceXml, string xmlNs = null)
        {
            // Xml読み込み
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(sourceXml);

            // ルート要素取得
            XmlNodeList root = null;
            if (string.IsNullOrWhiteSpace(xmlNs))
            {
                root = xml.DocumentElement.SelectNodes("/stateVariable");
            }
            else
            {
                // 名前空間設定
                XmlNamespaceManager nameSpace = new XmlNamespaceManager(xml.NameTable);
                nameSpace.AddNamespace("n", xmlNs);

                root = xml.DocumentElement.SelectNodes("/n:stateVariable", nameSpace);
            }

            // ルート要素無し
            if (root == null || root.Count == 0)
            {
                return;
            }

            // sendEvents属性取得
            SendEvents = root[0].Attributes[SEND_EVENTS].Value;

            foreach (XmlNode node in root[0].ChildNodes)
            {
                if (node.Name == NAME)
                {
                    Name = node.InnerText;
                }

                if (node.Name == DATA_TYPE)
                {
                    DataType = node.InnerText;
                }

                if (node.Name == DEFAULT_VALUE)
                {
                    DefaultValue = node.InnerText;
                }

                if (node.Name == ALLOWED_VALUE_RANGE)
                {
                    Dictionary<string, string> map = new Dictionary<string, string>();
                    foreach (XmlNode value in node.ChildNodes)
                    {
                        map.Add(value.Name, value.InnerText);
                    }

                    AllowedValueRange = new AllowedValueRange(map);
                }

                if (node.Name == ALLOWED_VALUE_LIST)
                {
                    foreach (XmlNode value in node.ChildNodes)
                    {
                        AllowedValues.Add(value.InnerText);
                    }
                }
            }
        }
    }
}
