using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UPnPLibrary
{
    public class UPnPResponseMessage
    {
        /// <summary>
        /// HTTP応答のステータスコード
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// HTTP応答ヘッダーのコレクション
        /// </summary>
        public HttpResponseHeaders Headers { get; set; }


        public Dictionary<string, string> Arguments { get; set; }
    }
}
