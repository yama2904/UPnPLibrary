using System.Text;

namespace UPnPLibrary.Ssdp
{
    /// <summary>
    /// M-SEARCHメッセージ管理クラス
    /// </summary>
    public class MSearchMessage
    {
        /// <summary>
        /// 宛先マルチキャストアドレス
        /// </summary>
        public const string MULTICAST_IP = "239.255.255.250";

        /// <summary>
        /// 宛先ポート番号
        /// </summary>
        public const int MULTICAST_PORT = 1900;

        /// <summary>
        /// M-SEARCHリクエストのMX値
        /// </summary>
        public int Mx { get; set; }

        /// <summary>
        /// M-SEARCHリクエストのST値
        /// </summary>
        public string St { get; set; }

        /// <summary>
        /// M-SEARCHリクエストに追加するフィールド
        /// </summary>
        public string Append { get; set; }

        /// <summary>
        /// リクエスト
        /// </summary>
        private const string REQUEST_FORMAT = @"M-SEARCH * HTTP/1.1
HOST: {0}:{1}
MAN: ""ssdp:discover""
MX: {2}
ST: {3}
";

        /// <summary>
        /// メッセージのエンコーディング
        /// </summary>
        private readonly Encoding _encoding = Encoding.UTF8;

        /// <summary>
        /// メッセージ初期化
        /// </summary>
        /// <param name="mx">M-SEARCHリクエストのMX値</param>
        /// <param name="st">M-SEARCHリクエストのST値</param>
        /// <param name="append">M-SEARCHリクエストに追加するフィールド</param>
        public MSearchMessage(int mx, string st, string append = "")
        {
            Mx = mx;
            St = st;
            Append = append;
        }

        /// <summary>
        /// M-SEARCHメッセージ作成
        /// </summary>
        /// <returns>M-SEARCHメッセージ</returns>
        public string CreateMSearch()
        {
            string message = string.Format(REQUEST_FORMAT, MULTICAST_IP, MULTICAST_PORT, Mx, St);
            if (!string.IsNullOrEmpty(Append))
            {
                message += Append;
            }

            return message;
        }

        /// <summary>
        /// M-SEARCHメッセージをバイト変換
        /// </summary>
        /// <returns>M-SEARCHメッセージ</returns>
        public byte[] CreateMSearchWithBytes()
        {
            return _encoding.GetBytes(CreateMSearch());
        }
    }
}
