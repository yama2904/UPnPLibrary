using System;
using System.Text;

namespace UPnPLibrary.Ssdp
{
    /// <summary>
    /// NOTIFYメッセージ管理クラス
    /// </summary>
    public class NotifyMessage
    {
        /// <summary>
        /// NOTIFYメッセージ
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// ヘッダフィールドの区切り文字
        /// </summary>
        private const string HEADER_FIELD_DELIMITER = ": ";

        /// <summary>
        /// メッセージのエンコーディング
        /// </summary>
        private readonly Encoding _encoding = Encoding.UTF8;

        /// <summary>
        /// NOTIFYメッセージ初期化
        /// </summary>
        /// <param name="bytes">NOTIFYメッセージ受信データ</param>
        public NotifyMessage(byte[] bytes)
        {
            Message = _encoding.GetString(bytes);
        }

        /// <summary>
        /// NOTIFYメッセージが正常であるか
        /// </summary>
        /// <returns>true:正常 false:エラー</returns>
        public bool IsOk()
        {
            return Message.StartsWith("HTTP/1.1 200 OK", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// ヘッダフィールド取得
        /// </summary>
        /// <param name="field">取得するヘッダフィールド</param>
        /// <returns>ヘッダフィールドの値</returns>
        public string GetHeaderField(string field)
        {
            // メッセージを1行ごとに分割
            string[] lines = Message.Replace("\r", "").Split('\n');

            foreach (string line in lines)
            {
                // 対象のヘッダフィールドであるか
                if (line.StartsWith(field, StringComparison.OrdinalIgnoreCase))
                {
                    // ヘッダフィールドの値を返す
                    int pos = line.IndexOf(HEADER_FIELD_DELIMITER);
                    if (pos >= 0)
                    {
                        return line.Substring(pos + HEADER_FIELD_DELIMITER.Length);
                    }
                }
            }

            return string.Empty;
        }
    }
}
