using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using UPnPLibrary.Description.Device;
using UPnPLibrary.Description.Service;
using UPnPLibrary.Ssdp;

namespace UPnPLibrary
{
    /// <summary>
    /// UPnP対応デバイス検索クラス
    /// </summary>
    public class UPnPDeviceDiscovery
    {
        /// <summary>
        /// リクエストの送信元ポート番号
        /// </summary>
        public int LocalPort { get; set; } = 9000;

        /// <summary>
        /// M-SEARCHリクエストのタイムアウト時間（秒）
        /// </summary>
        public int SearchTimeoutSec { get; set; } = 3;

        public UPnPTarget UPnPTarget { get; private set; }


        public MSearchMessage RequestMSearchMessage { get; private set; } = null;


        public NotifyMessage ResponseNotifyMessage { get; private set; } = null;

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


        public UPnPDeviceDiscovery(UPnPTarget target)
        {
            UPnPTarget = target;
        }

        public UPnPDevice FindDevice()
        {
            UPnPDevice device = null;

            // M-SEARCHメッセージ作成
            RequestMSearchMessage = new MSearchMessage(SearchTimeoutSec, SEATCH_TARGET);
            byte[] send = RequestMSearchMessage.CreateMessageAsByteArray();

            // M-SEARCH送信先
            IPAddress ip = IPAddress.Parse(MSearchMessage.MULTICAST_IP);
            int port = MSearchMessage.MULTICAST_PORT;

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
                ResponseNotifyMessage = new NotifyMessage(buffer.Take(size).ToArray());

                if (ResponseNotifyMessage.IsOk())
                {
                    device = new UPnPDevice();

                    device.IpAddress = (remoteEp as IPEndPoint).Address;

                    // Location取得
                    string location = ResponseNotifyMessage.GetHeaderField("location");

                    // Locationからパス部分を除去したURLを取得
                    device.UPnPUrl = new Uri(location).GetLeftPart(UriPartial.Authority);

                    // Locationからデバイス情報取得
                    device.DeviceDescription = RequestDeviceDescription(location);

                    // サービス情報取得
                    device.ServiceDescriptionList = RequestServiceDescriptions(device.UPnPUrl, device.DeviceDescription);
                }
            }

            return device;
        }

        private DeviceDescription RequestDeviceDescription(string location)
        {
            // 戻り値
            DeviceDescription device = new DeviceDescription();

            using (var client = new HttpClient())
            {
                // GETリクエスト
                HttpResponseMessage response = client.GetAsync(location).Result;
                byte[] bytes = response.Content.ReadAsByteArrayAsync().Result;
                
                // 文字列変換
                string xml = Encoding.UTF8.GetString(bytes);

                // XML読み込み
                device.LoadXml(xml);
            }

            return device;
        }

        private Dictionary<string, ServiceDescription> RequestServiceDescriptions(string uPnPUrl, DeviceDescription device)
        {
            // 戻り値
            Dictionary<string, ServiceDescription> services = new Dictionary<string, ServiceDescription>();

            // Url文字列をUriクラスへ変換
            Uri uri = new Uri(uPnPUrl);

            using (var client = new HttpClient())
            {
                foreach (ServiceInfo info in device.ServiceInfos) 
                {
                    // GETリクエスト
                    HttpResponseMessage response = client.GetAsync(new Uri(uri, info.ScpdUrl)).Result;
                    byte[] bytes = response.Content.ReadAsByteArrayAsync().Result;

                    // 文字列変換
                    string xml = Encoding.UTF8.GetString(bytes);

                    // XML読み込み
                    ServiceDescription service = new ServiceDescription();
                    service.LoadXml(xml);

                    services.Add(info.ServiceTypeName, service);
                }
            }

            return services;
        }
    }
}
