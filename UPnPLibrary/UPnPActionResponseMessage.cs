using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;

namespace UPnPLibrary
{
    /// <summary>
    /// UPnPアクションのレスポンスメッセージクラス
    /// </summary>
    public class UPnPActionResponseMessage
    {
        /// <summary>
        /// HTTP応答のステータスコード
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// HTTP応答ヘッダーのコレクション
        /// </summary>
        public HttpResponseHeaders Headers { get; set; }

        /// <summary>
        /// レスポンスパラメータ
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }
    }
}
