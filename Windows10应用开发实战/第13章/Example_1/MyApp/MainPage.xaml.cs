using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Networking.Connectivity;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace MyApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// 用于接收数据的端口
        /// </summary>
        const string SERVICE_PORT = "795";

        /// <summary>
        /// 用于服务器的Socket
        /// </summary>
        DatagramSocket svrSocket = null;
        /// <summary>
        /// 用于客户端的Socket
        /// </summary>
        DatagramSocket clientSocket = null;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.Loaded += async (s1, e1) =>
            {
                svrSocket = new DatagramSocket();
                // 添加接收消息事件处理
                svrSocket.MessageReceived += sersocket_Received;
                // 绑定到本地端口
                await svrSocket.BindServiceNameAsync(SERVICE_PORT);
                clientSocket = new DatagramSocket();
            };

            this.Unloaded += (s2, e2) =>
                {
                    // 释放Socket实例
                    svrSocket.Dispose();
                    clientSocket.Dispose();
                };
        }

        async void sersocket_Received(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            // 获取相关的DataReader对象
            DataReader reader = args.GetDataReader();
            // 读取消息内容
            uint len = reader.UnconsumedBufferLength;
            string msg = reader.ReadString(len);
            // 远程主机
            string remoteHost = args.RemoteAddress.DisplayName;
            reader.Dispose();

            // 显示接收到的消息
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    lvMsg.Items.Add(new { FromIP = remoteHost, Message = msg });
                });
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // 显示服务器的IP
            var hosts = NetworkInformation.GetHostNames();
            HostName svName = hosts.FirstOrDefault(h => h.Type == HostNameType.Ipv4 && h.IPInformation.NetworkAdapter.IanaInterfaceType == 71);
            if (svName != null)
            {
                tbIp.Text = svName.DisplayName;
            }
        }

        private async void OnSend(object sender, RoutedEventArgs e)
        {
            if (txtServer.Text.Length == 0 || txtMessage.Text.Length == 0) return;

            Button b = sender as Button;
            b.IsEnabled = false;
            // 获取输出流
            IOutputStream outStream = await clientSocket.GetOutputStreamAsync(new HostName(txtServer.Text), SERVICE_PORT);
            using (DataWriter writer = new DataWriter(outStream))
            {
                // 写入消息
                writer.WriteString(txtMessage.Text);
                // 提交写入的内容
                await writer.StoreAsync();
            }
            b.IsEnabled = true;
            txtMessage.Text = "";
        }
    }
}
