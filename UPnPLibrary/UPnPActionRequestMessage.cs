using System.Collections.Generic;
using System.Text;
using UPnPLibrary.Description.Device;

namespace UPnPLibrary
{
    /// <summary>
    /// UPnPアクションのリクエストメッセージクラス
    /// </summary>
    public class UPnPActionRequestMessage
    {
        /// <summary>
        /// リクエスト先サービス
        /// </summary>
        public Service Service { get; set; } = null;

        /// <summary>
        /// サービスアクション
        /// </summary>
        public string ActionName { get; set; } = null;

        /// <summary>
        /// アクション引数
        /// </summary>
        public Dictionary<string, string> Arguments { get; set; } = null;

        /// <summary>
        /// リクエスト雛形
        /// </summary>
        private const string REQUEST_FORMAT = @"<?xml version=""1.0""?>
<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
  <SOAP-ENV:Body>
    <m:{0} xmlns:m=""urn:schemas-upnp-org:service:{1}"">
      {2}
    </m:{0}>
  </SOAP-ENV:Body>
</SOAP-ENV:Envelope>";

        /// <summary>
        /// リクエスト内容を指定してメッセージ初期化
        /// </summary>
        /// <param name="service">リクエスト先サービス</param>
        /// <param name="actionName">サービスアクション</param>
        /// <param name="arguments">アクション引数</param>
        public UPnPActionRequestMessage(Service service, string actionName, Dictionary<string, string> arguments = null)
        {
            ActionName = actionName;
            Service = service;

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
            return string.Format(REQUEST_FORMAT, ActionName, Service.ServiceTypeName, CreateArgmentXml());
        }
        
        /// <summary>
        /// リクエストメッセージの引数部作成
        /// </summary>
        /// <returns>作成した引数部のメッセージ</returns>
        private string CreateArgmentXml()
        {
            if (Arguments == null)
            {
                return string.Empty;
            }

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
