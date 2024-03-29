﻿using System.Collections.Generic;
using System.Text;

namespace UPnPLibrary
{
    public class UPnPRequestMessage
    {
        public string ActionName { get; set; }

        public string ServiceType { get; set; }

        public Dictionary<string, string> Arguments { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// リクエスト雛形
        /// </summary>
        private const string REQUEST_FORMAT = @"<?xml version=""1.0""?>
<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
  <SOAP-ENV:Body>
    <m:{0} xmlns:m=""urn:schemas-upnp-org:service:{1}"">
      {2}
    </m:AddPortMapping>
  </SOAP-ENV:Body>
</SOAP-ENV:Envelope>";

        public UPnPRequestMessage(string actionName, string serviceType, Dictionary<string, string> arguments = null)
        {
            ActionName = actionName;
            ServiceType = serviceType;

            if (arguments != null)
            {
                Arguments = arguments;
            }
        }

        /// <summary>
        /// リクエストメッセージ作成
        /// </summary>
        /// <returns>リクエストメッセージ</returns>
        public string CreateMessage()
        {
            return string.Format(REQUEST_FORMAT, ActionName, ServiceType, CreateArgmentXml());
        }
        
        private string CreateArgmentXml()
        {
            // 戻り値
            StringBuilder xml = new StringBuilder();

            bool first = true;
            foreach (var argument in Arguments)
            {
                if (!first)
                {
                    xml.Append("\n");
                }

                xml.Append(string.Format("<{0}>{1}<{0}/>", argument.Key, argument.Value));
                first = false;
            }

            return xml.ToString();
        }
    }
}
