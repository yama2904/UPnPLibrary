using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UPnPLibrary.Description.Device;

namespace UPnPLibrary.Ssdp
{
    /// <summary>
    /// UPnP対応デバイス検索クラス
    /// </summary>
    public class UPnPDeviceDiscover
    {
        /// <summary>
        /// リクエストの送信元ポート番号
        /// </summary>
        public int LocalPort { get; set; } = 9000;

        /// <summary>
        /// M-SEARCHリクエストのタイムアウト時間（秒）
        /// </summary>
        public int SearchTimeoutSec { get; set; } = 3;

        public UPnPType UPnPType { get; private set; }

        /// <summary>
        /// デバイス検索に使用したM-SEARCHメッセージ
        /// </summary>
        public MSearchRequestMessage RequestMSearchMessage { get; private set; } = null;

        /// <summary>
        /// M-SEARCHメッセージの応答で解析したNOTIFYメッセージ
        /// </summary>
        public MSearchResponseMessage ResponseNotifyMessage { get; private set; } = null;

        /// <summary>
        /// UPnPデバイスのIPアドレス
        /// </summary>
        public IPAddress IpAddress { get; set; } = null;

        /// <summary>
        /// UPnPデバイスのリクエスト用URL
        /// </summary>
        public Uri UPnPUri { get; set; } = null;

        /// <summary>
        /// M-SEARCHリクエストの検出対象デバイス
        /// </summary>
        private const string SEATCH_TARGET = "urn:schemas-upnp-org:device:InternetGatewayDevice:1";
        //private const string SEATCH_TARGET = "urn:schemas-upnp-org:service:WANPPPConnection:1";
        //private const string SEATCH_TARGET = "urn:schemas-upnp-org:service:WANIPConnection:1";

        /// <summary>
        /// M-SEARCHレスポンスの最大サイズ
        /// </summary>
        private const int MAX_RESULT_SIZE = 8096;

        /// <summary>
        /// インスタンス初期化
        /// </summary>
        /// <param name="type"></param>
        public UPnPDeviceDiscover(UPnPType type)
        {
            UPnPType = type;
        }

        /// <summary>
        /// UPnPデバイス検索
        /// </summary>
        /// <returns>発見したデバイス情報</returns>
        public async Task<DeviceDescription> FindDeviceAsync()
        {
            DeviceDescription device = new DeviceDescription();

            // M-SEARCHメッセージ作成
            RequestMSearchMessage = new MSearchRequestMessage(SearchTimeoutSec, SEATCH_TARGET);
            byte[] send = RequestMSearchMessage.CreateMessageAsByteArray();

            // M-SEARCH送信先
            IPAddress ip = IPAddress.Parse(MSearchRequestMessage.MULTICAST_IP);
            int port = MSearchRequestMessage.MULTICAST_PORT;

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                // UDPマルチキャスト設定
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, ip.GetAddressBytes());

                // 送信
                EndPoint remoteEp = new IPEndPoint(ip, port);
                socket.Bind(new IPEndPoint(IPAddress.Any, LocalPort));
                socket.SendTo(send, remoteEp);

                // 受信
                byte[] buffer = new byte[MAX_RESULT_SIZE];
                int size = socket.ReceiveFrom(buffer, ref remoteEp);
                string message = Encoding.UTF8.GetString(buffer.Take(size).ToArray());
                ResponseNotifyMessage = new MSearchResponseMessage(message);

                if (ResponseNotifyMessage.IsSuccess())
                {
                    IpAddress = (remoteEp as IPEndPoint).Address;

                    // Locationからパス部分を除去したURLを取得
                    string url = new Uri(ResponseNotifyMessage.Location).GetLeftPart(UriPartial.Authority);
                    UPnPUri = new Uri(url);

                    // Locationからデバイス情報取得
                    device = await RequestDeviceDescriptionAsync(ResponseNotifyMessage.Location);
                }
            }

            return device;
        }

        /// <summary>
        /// UPnPデバイス情報取得
        /// </summary>
        /// <param name="deviceUrl">UPnPデバイスのリクエスト用URL</param>
        /// <returns>取得したUPnPデバイス情報</returns>
        private async Task<DeviceDescription> RequestDeviceDescriptionAsync(string deviceUrl)
        {
            // 戻り値
            DeviceDescription device = new DeviceDescription();

            using (var client = new HttpClient())
            {
                // GETリクエスト
                HttpResponseMessage response = await client.GetAsync(deviceUrl);
                Stream stream = await response.Content.ReadAsStreamAsync();

                // XML読み込み
                XmlSerializer serializer = new XmlSerializer(typeof(DeviceDescription));
                device = serializer.Deserialize(stream) as DeviceDescription;
            }

            return device;
        }
    }
}
