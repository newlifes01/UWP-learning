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
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;

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


        private async void OnClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.IsEnabled = false;
            //项目中的图像文件
            Uri gifuri = new Uri("ms-appx:///1.gif");
            StorageFile gifFile = await StorageFile.GetFileFromApplicationUriAsync(gifuri);
            using (IRandomAccessStream inputStream = await gifFile.OpenReadAsync())
            {
                // 创建图像解码器实例
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(BitmapDecoder.GifDecoderId, inputStream);
                // 获取图像文件中的帧总数
                uint frameCount = decoder.FrameCount;
                // 分别读取各个帧中图像，并添加到ListView控件中
                lvimgs.Items.Clear();
                for (uint n = 0; n < frameCount; n++)
                {
                    BitmapFrame curFrame = await decoder.GetFrameAsync(n);
                    // 获取图像的像素数据
                    // 在获取数据时，对图像进行变换处理
                    // 将宽度和高度变为原来的1/3
                    BitmapTransform btf = new BitmapTransform();
                    btf.ScaledWidth = curFrame.PixelWidth / 3;
                    btf.ScaledHeight = curFrame.PixelHeight / 3;
                    var pxprd = await curFrame.GetPixelDataAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, btf, ExifOrientationMode.IgnoreExifOrientation, ColorManagementMode.DoNotColorManage);
                    byte[] data = pxprd.DetachPixelData();
                    // 创建内存位图对象
                    WriteableBitmap bmp = new WriteableBitmap((int)btf.ScaledWidth, (int)btf.ScaledHeight);
                    data.CopyTo(bmp.PixelBuffer);
                    this.lvimgs.Items.Add(bmp);
                }
            }
            btn.IsEnabled = true;
        }
    }
}
