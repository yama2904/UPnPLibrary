using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPnPLibrary.Description.Service
{
    /// <summary>
    /// UPnPのサービス情報管理クラス
    /// </summary>
    public class ServiceDescription
    {
        /// <summary>
        /// サービス情報XML
        /// </summary>
        private readonly string _descriptionXml = string.Empty;

        /// <summary>
        /// サービス情報初期化
        /// </summary>
        /// <param name="descriptionXml">SCPDURLへのリクエストにより取得したサービス情報XML</param>
        public ServiceDescription(string descriptionXml) 
        {
            _descriptionXml = descriptionXml;
        }
    }
}
