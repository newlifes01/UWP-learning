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
using Windows.Storage;
using Windows.Storage.Streams;

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

        private async void OnWrite(object sender, RoutedEventArgs e)
        {
            // 获取本地目录引用
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            // 创建新文件
            StorageFile file = await localFolder.CreateFileAsync("demo.dat", CreationCollisionOption.ReplaceExisting);
            // 打开文件流
            using (IRandomAccessStream stream= await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                // 实例化DataWriter
                DataWriter dw = new DataWriter(stream);
                // 设置默认编码格式
                dw.UnicodeEncoding = UnicodeEncoding.Utf8;
                // 写入bool值
                dw.WriteBoolean(true);
                // 写入日期时间值
                DateTime dt = new DateTime(2010, 8, 21);
                dw.WriteDateTime(dt);
                // 写入字符串
                string str = "测试文本";
                // 计算字符串的长度
                uint len = dw.MeasureString(str);
                // 先写入字符串的长度
                dw.WriteUInt32(len);
                // 再写入字符串
                dw.WriteString(str);
                // 以下方法必须调用
                await dw.StoreAsync();
                // 解除DataWriter与流的关联
                dw.DetachStream();
                dw.Dispose();
            }
            Windows.UI.Popups.MessageDialog msgDlg = new Windows.UI.Popups.MessageDialog("保存成功。");
            await msgDlg.ShowAsync();
        }

        private async void OnRead(object sender, RoutedEventArgs e)
        {
            // 获取本地目录的引用
            StorageFolder local = ApplicationData.Current.LocalFolder;
            // 获取文件
            StorageFile file = await local.GetFileAsync("demo.dat");
            if (file != null)
            {
                string strres = "读到的内容：\n";
                // 打开文件流
                using (IRandomAccessStream stream = await file.OpenReadAsync())
                {
                    // 实例化DataReader
                    DataReader dr = new DataReader(stream);
                    // 读出时的编码格式要与写入时使用的编码格式相同
                    dr.UnicodeEncoding = UnicodeEncoding.Utf8;
                    // 从流中加载所有数据
                    await dr.LoadAsync((uint)stream.Size);
                    // 读出的顺序与写入的顺序相同
                    // 读取bool值
                    bool b = dr.ReadBoolean();
                    strres += b.ToString() + "\n";
                    // 读取日期时间值
                    DateTimeOffset dt = dr.ReadDateTime();
                    strres += dt.ToString("yyyy-M-d") + "\n";
                    // 读取字符串
                    // 读取长度
                    uint len = dr.ReadUInt32();
                    if (len > 0)
                    {
                        strres += dr.ReadString(len);
                    }
                    dr.Dispose();
                }
                tbResult.Text = strres;
            }
        }

    }
}
