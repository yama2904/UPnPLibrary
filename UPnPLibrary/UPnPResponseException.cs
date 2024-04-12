using System;

namespace UPnPLibrary
{
    /// <summary>
    /// UPnPレスポンスで受信したUPnPエラー
    /// </summary>
    public class UPnPResponseException : Exception
    {
        public string FaultCode { get; set; } = string.Empty;

        public string FaultString { get; set; } = string.Empty;

        public string ErrorCode { get; set; } = string.Empty;

        public string ErrorDescription { get; set; } = string.Empty;

        public UPnPResponseException() { }

        public UPnPResponseException(string message) : base(message) { }

        public UPnPResponseException(string faultCode, string faultString, string errorCode, string errorDescription, string message) : base(message)
        {
            FaultCode = faultCode;
            FaultString = faultString;
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }
    }
}
