using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using UPnPLibrary.Description.Device;
using UPnPLibrary.Ssdp;

namespace UPnPLibrary
{
    /// <summary>
    /// UPnP対応デバイス検索クラス
    /// </summary>
    public class UPnPDevicesDiscovery
    {
        /// <summary>
        /// M-SEARCHリクエストのタイムアウト時間（秒）
        /// </summary>
        private const int SEARCH_TIMEOUT = 3;

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
        /// M-SEARCHリクエストの送信元ポート番号
        /// </summary>
        private const int LOCAL_PORT = 9000;

        public static IPAddress FindDevices()
        {
            // M-SEARCHメッセージ作成
            MSearchMessage mSeaech = new MSearchMessage(SEARCH_TIMEOUT, SEATCH_TARGET);
            byte[] send = mSeaech.CreateMSearchWithBytes();

            // M-SEARCH送信先
            IPAddress ip = IPAddress.Parse(MSearchMessage.MULTICAST_IP);
            int port = MSearchMessage.MULTICAST_PORT;
            
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                // UDPマルチキャスト設定
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, ip.GetAddressBytes());

                // 送信
                EndPoint remoteEp = new IPEndPoint(ip, port);
                socket.Bind(new IPEndPoint(IPAddress.Any, LOCAL_PORT));
                socket.SendTo(send, remoteEp);

                // 受信
                byte[] buffer = new byte[MAX_RESULT_SIZE];
                int size = socket.ReceiveFrom(buffer, ref remoteEp);
                NotifyMessage notify = new NotifyMessage(buffer.Take(size).ToArray());

                if (notify.IsOk())
                {
                    // Locationからデバイス情報取得
                    string location = notify.GetHeaderField("location");
                    Uri uPnpUrl = new Uri(location);
                    string deviceXml = RequestDeviceDescription(location);

                    // デバイス情報初期化
                    DeviceDescription device = new DeviceDescription(deviceXml);

                    using (var client = new HttpClient())
                    using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(uPnpUrl, "/ctl/IPConn")))
                    using (var content = new StringContent("", Encoding.UTF8, "text/xml"))
                    {
                        request.Content = content;
                        request.Headers.Add("Host", uPnpUrl.Authority);
                        request.Headers.Add("SOAPACTION", "\"urn:schemas-upnp-org:service:WANPPPConnection:1#GetExternalIPAddress\"");    // 動的

                        var response = client.SendAsync(request).Result;
                        byte[] bytes = response.Content.ReadAsByteArrayAsync().Result;
                        Console.WriteLine(Encoding.UTF8.GetString(bytes));
                    }
                }
            }

            return null;
        }


        private static string RequestDeviceDescription(string location)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(location).Result;
                byte[] bytes = response.Content.ReadAsByteArrayAsync().Result;
                return Encoding.UTF8.GetString(bytes);
            }
        }
    }
}
