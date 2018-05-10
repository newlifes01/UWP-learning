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
using Windows.UI.Xaml.Media.Imaging;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.Networking.Sockets;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;
using Windows.UI.Core;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace MyApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// 服务器监听端口
        /// </summary>
        const string LISTEN_PORT = "2100";
        /// <summary>
        /// 监听组件
        /// </summary>
        StreamSocketListener listener = null;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.Loaded += async (s, e) =>
                {
                    // 实例化StreamSocketListener对象
                    listener = new StreamSocketListener();
                    // 添加ConnectionReceived事件处理程序
                    listener.ConnectionReceived += listener_ConnectionReceived;
                    // 绑定本地端口
                    await listener.BindServiceNameAsync(LISTEN_PORT);
                };
            this.Unloaded += (s, e) =>
                {
                    // 释放资源
                    if (listener != null)
                    {
                        listener.Dispose();
                        listener = null;
                    }
                };
        }

        async void listener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            string text = string.Empty;
            IRandomAccessStream imgStream = new InMemoryRandomAccessStream();
            // 处理接收到的连接
            using (StreamSocket socket = args.Socket)
            {
                using (DataReader reader = new DataReader(socket.InputStream))
                {
                    try
                    {
                        // 读出第一个整数，表示文件长度
                        await reader.LoadAsync(sizeof(uint));
                        uint len = reader.ReadUInt32();
                        await reader.LoadAsync(len);
                        IBuffer buffer = reader.ReadBuffer(len);
                        // 写入内存流
                        await imgStream.WriteAsync(buffer);
                        await reader.LoadAsync(sizeof(uint));
                        // 读入字符串的长度
                        len = reader.ReadUInt32();
                        // 读出字符串内容
                        if (len > 0)
                        {
                            await reader.LoadAsync(len);
                            text = reader.ReadString(len);
                        }
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage(ex.Message);
                    }
                }
            }

            // 显示接收到的内容
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.DecodePixelWidth = 50;
                    imgStream.Seek(0UL);
                    bmp.SetSource(imgStream);
                    imgStream.Dispose();
                    lbItems.Items.Add(new { Image = bmp, Text = text });
                });
        }

        private async void DisplayMessage(string msg)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    Windows.UI.Popups.MessageDialog d = new Windows.UI.Popups.MessageDialog(msg);
                    await d.ShowAsync();
                });
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // 获取当前主机的IP地址
            var hosts = NetworkInformation.GetHostNames();
            // 筛选出IPv4版本的地址
            HostName server = hosts.FirstOrDefault(h => h.Type == HostNameType.Ipv4 && h.IPInformation.NetworkAdapter.IanaInterfaceType == 71);
            // 显示IP地址
            tbSvIP.Text = server.DisplayName;
        }

        private async void OnPickImagFile(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                using (IRandomAccessStream stream = await file.OpenReadAsync())
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.DecodePixelHeight = 200;
                    bmp.SetSource(stream);
                    this.img.Source = bmp; //显示图像
                    using (DataReader reader = new DataReader(stream.GetInputStreamAt(0UL)))
                    {
                        uint len = (uint)stream.Size;
                        // 加载数据
                        await reader.LoadAsync(len);
                        IBuffer buffer = reader.ReadBuffer(len);
                        // 暂存在Tag属性中，稍后用到
                        this.img.Tag = buffer;
                    }
                }
            }
        }

        private async void OnSend(object sender, RoutedEventArgs e)
        {
            if (txtServerIp.Text.Length == 0)
            {
                DisplayMessage("请输入服务器IP。");
                return;
            }
            IBuffer bufferImg = img.Tag as IBuffer;
            if (bufferImg == null)
            {
                DisplayMessage("请选择图像。");
                return;
            }
            Button b = sender as Button;
            b.IsEnabled = false;

            using (StreamSocket socket = new StreamSocket())
            {
                try
                {
                    // 发起连接
                    await socket.ConnectAsync(new HostName(txtServerIp.Text), LISTEN_PORT);
                    // 准备发送数据
                    using (DataWriter writer = new DataWriter(socket.OutputStream))
                    {
                        // 首先写入图像数据
                        uint len = bufferImg.Length;
                        writer.WriteUInt32(len); //长度
                        writer.WriteBuffer(bufferImg);
                        // 接着写入文本
                        if (txtContent.Text.Length == 0)
                        {
                            writer.WriteUInt32(0);
                        }
                        else
                        {
                            len = writer.MeasureString(txtContent.Text);
                            writer.WriteUInt32(len); //长度
                            writer.WriteString(txtContent.Text);
                        }
                        // 提交数据到流
                        await writer.StoreAsync();
                    }
                    txtContent.Text = "";
                }
                catch (Exception ex)
                {
                    DisplayMessage(ex.Message);
                }
            }
            b.IsEnabled = true;
        }
    }
}
