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
using Windows.Graphics.Imaging;
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
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            StorageFile imgFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///1.jpg"));
            // 对图像进行解码
            using (IRandomAccessStream streamIn = await imgFile.OpenReadAsync())
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(BitmapDecoder.JpegDecoderId, streamIn);
                // 获取像素数据
                PixelDataProvider provd = await decoder.GetPixelDataAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, new BitmapTransform(), ExifOrientationMode.IgnoreExifOrientation, ColorManagementMode.DoNotColorManage);
                byte[] srcData = provd.DetachPixelData();
                // 灰度处理
                for (int i = 0; i < srcData.Length; i += 4)
                {
                    // 获取各通道的值
                    double b = srcData[i];
                    double g = srcData[i + 1];
                    double r = srcData[i + 2];
                    // 计算平均值
                    double v = (b + g + r) / 3d;
                    // 替换原来的B、G、R值
                    srcData[i] = srcData[i + 1] = srcData[i + 2] = Convert.ToByte(v);
                }
                // 显示处理后的图像
                WriteableBitmap wbitmap = new WriteableBitmap((int)decoder.PixelWidth, (int)decoder.PixelHeight);
                srcData.CopyTo(wbitmap.PixelBuffer);
                this.imgGray.Source = wbitmap;
            }
        }
    }
}
