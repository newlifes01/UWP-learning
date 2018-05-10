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
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MyApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<StorageFile> picFiles = null;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            picFiles = new ObservableCollection<StorageFile>();
            this.lbPics.ItemsSource = picFiles;
        }


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // 从照片目录中读取文件
            StorageFolder camrroll = KnownFolders.CameraRoll;
            var files = await camrroll.GetFilesAsync();
            picFiles.Clear();
            foreach (StorageFile f in files)
            {
                picFiles.Add(f);
            }
        }

        private async void OnClick(object sender, RoutedEventArgs e)
        {
            if (lbPics.SelectedIndex < 0) return;
            Button btn = sender as Button;
            btn.IsEnabled = false;
            StorageFile curFile = lbPics.SelectedItem as StorageFile;
            // 打开文件流
            using (IRandomAccessStream streamIn = await curFile.OpenReadAsync())
            {
                // 解码图像文件
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(streamIn);
                // 获取像素数据
                PixelDataProvider pxprd = await decoder.GetPixelDataAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, new BitmapTransform(), ExifOrientationMode.RespectExifOrientation, ColorManagementMode.DoNotColorManage);
                byte[] data = pxprd.DetachPixelData();
                // 创建新文件
                StorageFolder savedPics=KnownFolders.SavedPictures;
                StorageFile newFile=await savedPics.CreateFileAsync(curFile.DisplayName + ".png", CreationCollisionOption.ReplaceExisting);
                // 编码为PNG图像
                using(IRandomAccessStream streamOut = await newFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    // 实例化编码器
                    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, streamOut);
                    // 写入像素数据
                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, decoder.PixelWidth, decoder.PixelHeight, decoder.DpiX, decoder.DpiY, data);
                    // 提交数据
                    await encoder.FlushAsync();
                }
            }
            btn.IsEnabled = true;
        }
    }


    public sealed class FileToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            StorageFile srcFile = value as StorageFile;
            // 获取文件缩略图
            var task = srcFile.GetThumbnailAsync(Windows.Storage.FileProperties.ThumbnailMode.PicturesView).AsTask();
            task.Wait();
            IRandomAccessStream inputStream = task.Result;
            // 创建图像缩略图
            BitmapImage bmp = new BitmapImage();
            bmp.DecodePixelHeight = 100;
            bmp.SetSource(inputStream);
            return new
            {
                Preview = bmp,
                Name = srcFile.DisplayName,
                Type = srcFile.DisplayType
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

}
