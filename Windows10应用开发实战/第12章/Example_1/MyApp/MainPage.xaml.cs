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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace MyApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。
        }

        /// <summary>
        /// 向文件写入字节序列
        /// </summary>
        private async void OnWriteToFile(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.IsEnabled = false;
            // 生产字节数组
            Random rand = new Random();
            byte[] data = new byte[8];
            rand.NextBytes(data);
            // 从字节数组产生缓冲区对象
            Windows.Storage.Streams.IBuffer buffer = data.AsBuffer();
            // 获得本地目录引用
            Windows.Storage.StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            // 在本地目录中创建新文件
            Windows.Storage.StorageFile newFile = await local.CreateFileAsync("my.data", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            // 打开文件流
            using (Windows.Storage.Streams.IRandomAccessStream stream = await newFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
            {
                // 将缓冲区的内容写入流
                await stream.WriteAsync(buffer);
            }
            // 显示已写入的字节序列
            tbBytes.Text = string.Format("已向文件写入：{0}", BitConverter.ToString(data));
            btn.IsEnabled = true;
        }

        /// <summary>
        /// 从文件读取字节序列
        /// </summary>
        private async void OnReadFromFile(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.IsEnabled = false;
            // 获取本地目录引用
            Windows.Storage.StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            // 获取文件
            Windows.Storage.StorageFile file = await local.GetFileAsync("my.data");
            if (file != null)
            {
                // 用于存放读到的数据的缓冲区
                Windows.Storage.Streams.IBuffer buffer = null;
                // 打开流
                using (Windows.Storage.Streams.IRandomAccessStream streamIn = await file.OpenReadAsync())
                {
                    // 实例化缓冲区对象
                    buffer = System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeBuffer.Create((int)streamIn.Size);
                    // 从流中读入数据，存放在缓冲区中
                    await streamIn.ReadAsync(buffer, buffer.Capacity, Windows.Storage.Streams.InputStreamOptions.None);
                }
                // 显示读到的字节序列
                tbReadBytes.Text = "读到的字节序列：" + BitConverter.ToString(buffer.ToArray());
            }
            btn.IsEnabled = true;
        }

    }
}
