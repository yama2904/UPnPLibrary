using System.Collections.Generic;
using System.Xml;

namespace UPnPLibrary.Description.Service.Action
{
    public class ActionInfo
    {
        public string Name { get; set; }

        public List<Argument> Arguments { get; set; } = new List<Argument>();

        private const string NAME = "name";
        private const string ARGUMENT_LIST = "argumentList";

        public ActionInfo() 
        {
        }

        public ActionInfo(string name, List<Dictionary<string, string>> args) 
        {
            Name = name;

            foreach (Dictionary<string, string> arg in args)
            {
                Arguments.Add(new Argument(arg));
            }
        }

        public void LoadXml(string sourceXml, string xmlNs = null)
        {
            // Xml読み込み
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(sourceXml);

            // ルート直下取得
            XmlNodeList nodes = null;
            if (string.IsNullOrWhiteSpace(xmlNs))
            {
                nodes = xml.SelectNodes("/action/*");
            }
            else
            {
                // 名前空間設定
                XmlNamespaceManager nameSpace = new XmlNamespaceManager(xml.NameTable);
                nameSpace.AddNamespace("n", xmlNs);

                nodes = xml.SelectNodes("/n:action/*", nameSpace);
            }

            foreach (XmlNode node in nodes)
            {
                if (node.Name == NAME)
                {
                    Name = node.InnerText;
                }

                if (node.Name == ARGUMENT_LIST)
                {
                    foreach (XmlNode argument in node.ChildNodes)
                    {
                        Dictionary<string, string> map = new Dictionary<string, string>();
                        foreach (XmlNode child in argument.ChildNodes)
                        {
                            map.Add(child.Name, child.InnerText);
                        }

                        Arguments.Add(new Argument(map));
                    }
                }
            }
        }
    }
}
