using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// デバイス検索に使用したM-SEARCHメッセージリスト
        /// </summary>
        public List<MSearchRequestMessage> RequestMSearchMessages { get; private set; } = null;

        /// <summary>
        /// M-SEARCHメッセージの応答メッセージリスト
        /// </summary>
        public List<MSearchResponseMessage> ResponseMSearchMessages { get; private set; } = null;

        /// <summary>
        /// M-SEARCHリクエストの検出対象デバイス
        /// </summary>
        private readonly string[] SEATCH_TARGETS = new string[] {
                                                        "urn:schemas-upnp-org:device:InternetGatewayDevice:1",
                                                        "urn:schemas-upnp-org:service:WANPPPConnection:1",
                                                        "urn:schemas-upnp-org:service:WANIPConnection:1"};

        /// <summary>
        /// M-SEARCHレスポンスの最大サイズ
        /// </summary>
        private const int MAX_RESULT_SIZE = 8096;

        /// <summary>
        /// UPnPデバイス検索
        /// </summary>
        /// <returns>発見したデバイスのアクセス情報</returns>
        public async Task<List<UPnPDeviceAccess>> FindDeviceAsync()
        {
            // 戻り値
            List<UPnPDeviceAccess> devices = new List<UPnPDeviceAccess>();

            // M-SEARCH送信先IP/ポート番号取得
            IPAddress ip = IPAddress.Parse(MSearchRequestMessage.MULTICAST_IP);
            int port = MSearchRequestMessage.MULTICAST_PORT;

            // M-SEARCHリクエスト/レスポンス実行
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                // UDPマルチキャスト設定
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, ip.GetAddressBytes());

                // 送信先設定
                EndPoint remoteEp = new IPEndPoint(ip, port);
                socket.Bind(new IPEndPoint(IPAddress.Any, LocalPort));

                // M-SEARCHメッセージ送信
                RequestMSearchMessages = new List<MSearchRequestMessage>();
                foreach (string st in SEATCH_TARGETS)
                {
                    // メッセージ作成
                    MSearchRequestMessage requestMsg = new MSearchRequestMessage(SearchTimeoutSec, st);
                    byte[] send = requestMsg.CreateMessageAsByteArray();
                    
                    // 送信
                    socket.SendTo(send, remoteEp);

                    // リクエストメッセージリストに追加
                    RequestMSearchMessages.Add(requestMsg);
                }

                // 受信
                ResponseMSearchMessages = new List<MSearchResponseMessage>();
                Task receiveTask = Task.Run(() =>
                {
                    while (true)
                    {
                        // 受信用バッファ/バイト数
                        byte[] buffer = new byte[MAX_RESULT_SIZE];
                        int size = -1;

                        // 受信待ち
                        try
                        {
                            size = socket.ReceiveFrom(buffer, ref remoteEp);
                        }
                        catch (SocketException ex)
                        {
                            // 受信キャンセル
                            if (ex.ErrorCode == 10004)
                            {
                                break;
                            }
                        }
                        catch (ObjectDisposedException)
                        {
                            // 受信キャンセル
                            break;
                        }

                        // 受信データ解析
                        string message = Encoding.UTF8.GetString(buffer.Take(size).ToArray());
                        MSearchResponseMessage responseMsg = new MSearchResponseMessage(message);

                        // リクエスト成功可否
                        if (responseMsg.IsSuccess())
                        {
                            // デバイスアクセス情報取得
                            UPnPDeviceAccess device = new UPnPDeviceAccess();
                            device.IpAddress = (remoteEp as IPEndPoint).Address;
                            device.Location = responseMsg.Location;
                            devices.Add(device);
                        }

                        // レスポンスリストに追加
                        ResponseMSearchMessages.Add(responseMsg);
                    }
                });

                // 受信タイムアウト設定
                await Task.Delay(SearchTimeoutSec * 1000);
                // タイムアウト
                socket.Close();
            }

            return devices;
        }
    }
}
