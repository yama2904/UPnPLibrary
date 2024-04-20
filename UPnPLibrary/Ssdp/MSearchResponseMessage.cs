using System;
using System.Text;

namespace UPnPLibrary.Ssdp
{
    /// <summary>
    /// M-SEARCHレスポンスメッセージクラス
    /// </summary>
    public class MSearchResponseMessage
    {
        /// <summary>
        /// UPnPデバイス情報取得用URL
        /// </summary>
        public string Location { get; set; } = null;

        /// <summary>
        /// 応答メッセージ
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// ヘッダフィールドの区切り文字
        /// </summary>
        private const string HEADER_FIELD_DELIMITER = ": ";

        /// <summary>
        /// 受信したバイト配列を指定してメッセージ初期化
        /// </summary>
        /// <param name="bytes">受信したバイト配列</param>
        public MSearchResponseMessage(string message)
        {
            Message = message;

            // メッセージを1行ごとに分割
            string[] lines = Message.Replace("\r", "").Split('\n');

            foreach (string line in lines)
            {
                // ヘッダフィールド判定
                int pos = line.IndexOf(HEADER_FIELD_DELIMITER);
                if (pos == -1)
                {
                    continue;
                }

                // フィールド名とフィールド値取得
                string field = line.Substring(0, pos);
                string value = line.Substring(pos + HEADER_FIELD_DELIMITER.Length);

                switch (field.ToLower())
                {
                    case "location":
                        Location = value; 
                        break;
                }
            }
        }

        /// <summary>
        /// リクエストが成功したか
        /// </summary>
        /// <returns>true:成功 false:失敗</returns>
        public bool IsSuccess()
        {
            return Message.StartsWith("HTTP/1.1 200 OK", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
