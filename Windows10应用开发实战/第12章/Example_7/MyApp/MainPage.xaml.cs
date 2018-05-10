using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;

namespace MyApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void OnPickFile(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            // 要打开的文件类型
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                string info = string.Empty;
                info += string.Format("文件名：{0}\n", file.Name);
                info += string.Format("文件路径：{0}\n", file.Path);
                info += string.Format("创建时间：{0:yyyy-M-d HH:mm:ss}\n", file.DateCreated.DateTime);
                ImageProperties imgprop = await file.Properties.GetImagePropertiesAsync();
                info += string.Format("相机厂商：{0}\n", imgprop.CameraManufacturer);
                info += string.Format("相机型号：{0}\n", imgprop.CameraModel);
                info += string.Format("宽度：{0}\n", imgprop.Width);
                info += string.Format("高度：{0}", imgprop.Height);

                // 显示文件信息
                tbFileinfo.Text = info;
            }
        }

        private async void OnFileSave(object sender, RoutedEventArgs e)
        {
            FileSavePicker picker = new FileSavePicker();
            // 指定文件类型
            picker.FileTypeChoices.Add("自定义文件", new string[] { ".data" });
            // 默认定位到文档库中
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            // 默认文件名
            picker.SuggestedFileName = "test.data";
            // 显示选择器
            StorageFile file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                // 随机产生200个字节
                byte[] data = new byte[200];
                Random rand = new Random();
                rand.NextBytes(data);
                // 将字节数组写入文件
                await FileIO.WriteBytesAsync(file, data);
            }
        }

        private async void OnOpenFolder(object sender, RoutedEventArgs e)
        {
            FolderPicker picker = new FolderPicker();
            picker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            // 指定文件类型
            picker.FileTypeFilter.Add("*");
            // 显示选择器
            StorageFolder folder = await picker.PickSingleFolderAsync();
            if (folder != null)
            {
                // 获取目录下的文件列表
                var subFiles = await folder.GetFilesAsync();
                // 向ListView中添加文件
                this.lvFiles.Items.Clear();
                foreach (StorageFile file in subFiles)
                {
                    // 获取文件图标
                    StorageItemThumbnail thumb = await file.GetScaledImageAsThumbnailAsync(ThumbnailMode.ListView);
                    BitmapImage bmp = new BitmapImage();
                    bmp.DecodePixelWidth = 40;
                    bmp.SetSource(thumb);
                    this.lvFiles.Items.Add(new { Image = bmp, Name = file.DisplayName });
                }
            }
        }
    }
}
