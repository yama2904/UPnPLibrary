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
        /// M-SEARCHリクエストのタイムアウト時間（秒）
        /// </summary>
        public int Mx { get; set; }

        /// <summary>
        /// M-SEARCHリクエストの対象デバイス
        /// </summary>
        public string St { get; set; }

        /// <summary>
        /// M-SEARCHリクエストに追加するフィールド
        /// </summary>
        public string Append { get; set; }

        /// <summary>
        /// リクエスト雛形
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
        /// MX値とST値を指定してメッセージ初期化
        /// </summary>
        /// <param name="mx">M-SEARCHリクエストのタイムアウト時間（秒）</param>
        /// <param name="st">M-SEARCHリクエストの対象デバイス</param>
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
        public string CreateMessage()
        {
            string message = string.Format(REQUEST_FORMAT, MULTICAST_IP, MULTICAST_PORT, Mx, St);
            if (!string.IsNullOrEmpty(Append))
            {
                message += Append;
            }

            return message;
        }

        /// <summary>
        /// バイト配列でM-SEARCHメッセージ作成
        /// </summary>
        /// <returns>M-SEARCHメッセージ</returns>
        public byte[] CreateMessageAsByteArray()
        {
            return _encoding.GetBytes(CreateMessage());
        }
    }
}
