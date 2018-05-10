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


using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;


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
            Uri uri = new Uri("http://msdn.microsoft.com/zh-cn/");
            wv.Navigate(uri);
        }

        private async void OnClick ( object sender, RoutedEventArgs e )
        {
            using (InMemoryRandomAccessStream ms=new InMemoryRandomAccessStream())
            {
                // 捕捉HTML内容并保存到内存流中
                await wv.CapturePreviewToStreamAsync(ms);
                // 创建位图对象
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(ms);
                // 显示图像
                img.Source = bmp;
            }
        }
    }
}
