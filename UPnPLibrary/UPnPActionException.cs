using System;

namespace UPnPLibrary
{
    /// <summary>
    /// UPnPアクションで発生したUPnPエラー管理クラス
    /// </summary>
    public class UPnPActionException : Exception
    {
        public string FaultCode { get; set; } = string.Empty;

        public string FaultString { get; set; } = string.Empty;

        /// <summary>
        /// UPnPエラーのエラーコード<br/>
        /// エラーコードの内容については下記URLのTable 3-3参照<br/>
        /// https://openconnectivity.org/upnp-specs/UPnP-arch-DeviceArchitecture-v2.0-20200417.pdf
        /// </summary>
        public string ErrorCode { get; set; } = string.Empty;

        /// <summary>
        /// UPnPエラーの概略説明
        /// </summary>
        public string ErrorDescription { get; set; } = string.Empty;

        /// <summary>
        /// インスタンス初期化
        /// </summary>
        public UPnPActionException() { }

        /// <summary>
        /// 例外メッセージを指定してインスタンス初期化
        /// </summary>
        /// <param name="message"></param>
        public UPnPActionException(string message) : base(message) { }

        /// <summary>
        /// 各パラメータを指定してインスタンスを初期化
        /// </summary>
        /// <param name="faultCode"></param>
        /// <param name="faultString"></param>
        /// <param name="errorCode">UPnPエラーのエラーコード</param>
        /// <param name="errorDescription">UPnPエラーの概略説明</param>
        /// <param name="message">例外メッセージ</param>
        public UPnPActionException(string faultCode, string faultString, string errorCode, string errorDescription, string message) : base(message)
        {
            FaultCode = faultCode;
            FaultString = faultString;
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }
    }
}
