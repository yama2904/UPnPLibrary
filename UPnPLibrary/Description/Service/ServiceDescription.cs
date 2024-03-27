using System.Collections.Generic;
using System.Xml;
using UPnPLibrary.Description.Service.Action;
using UPnPLibrary.Description.Service.StateVariable;

namespace UPnPLibrary.Description.Service
{
    /// <summary>
    /// UPnPのサービス情報管理クラス
    /// </summary>
    public class ServiceDescription
    {
        public string RootXmlNs { get; set; } = "urn:schemas-upnp-org:service-1-0";

        public string MajorVersion { get; set; }

        public string MinorVersion { get; set; }

        public List<ActionInfo> ActionInfos = new List<ActionInfo>();

        public List<StateVariableInfo> StateVariableInfos = new List<StateVariableInfo>();

        private const string MAJOR = "major";
        private const string MINOR = "minor";

        /// <summary>
        /// サービス情報XML
        /// </summary>
        private string _sourceXml = string.Empty;

        /// <summary>
        /// サービス情報XML読み込み
        /// <param name="sourceXml">SCPDURLへのリクエストにより取得したサービス情報XML</param>
        public void LoadXml(string sourceXml) 
        {
            _sourceXml = sourceXml;

            // Xml読み込み
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(_sourceXml);

            LoadVersion(xml);
            LoadActionInfo(xml);
            LoadStateVariableInfo(xml);
        }

        private void LoadVersion(XmlDocument xml)
        {
            // 名前空間設定
            XmlNamespaceManager xmlns = new XmlNamespaceManager(xml.NameTable);
            xmlns.AddNamespace("n", RootXmlNs);

            // バージョン取得
            XmlNodeList versions = xml.SelectNodes("/n:scpd/n:specVersion/*", xmlns);

            foreach (XmlNode version in versions)
            {
                if (version.Name == MAJOR)
                {
                    MajorVersion = version.InnerText;
                }

                if (version.Name == MINOR)
                {
                    MinorVersion = version.InnerText;
                }
            }
        }

        private void LoadActionInfo(XmlDocument xml)
        {
            // 名前空間設定
            XmlNamespaceManager xmlns = new XmlNamespaceManager(xml.NameTable);
            xmlns.AddNamespace("n", RootXmlNs);

            // actionタグ取得
            XmlNodeList actions = xml.SelectNodes("/n:scpd/n:actionList/n:action", xmlns);

            // 各actionタグ読み込み
            foreach (XmlNode action in actions)
            {
                ActionInfo actionInfo = new ActionInfo();
                actionInfo.LoadXml(action.OuterXml, RootXmlNs);
                ActionInfos.Add(actionInfo);
            }
        }

        private void LoadStateVariableInfo(XmlDocument xml)
        {
            // 名前空間設定
            XmlNamespaceManager xmlns = new XmlNamespaceManager(xml.NameTable);
            xmlns.AddNamespace("n", RootXmlNs);

            // stateVariableタグ取得
            XmlNodeList states = xml.SelectNodes("/n:scpd/n:serviceStateTable/n:stateVariable", xmlns);

            // 各stateVariableタグの子要素取得
            foreach (XmlNode state in states)
            {
                StateVariableInfo stateInfo = new StateVariableInfo();
                stateInfo.LoadXml(state.OuterXml, RootXmlNs);
                StateVariableInfos.Add(stateInfo);
            }
        }
    }
}
