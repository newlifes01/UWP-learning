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
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

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

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // 查找本地目录下的.jpg文件
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            IReadOnlyList<StorageFile> files = await localFolder.GetFilesAsync();
            StorageFile[] jpgfiles = (from f in files where f.FileType.ToLower().Contains("jpg") select f).ToArray();
            // 显示图片文件列表
            lvPics.Items.Clear();
            foreach (StorageFile file in jpgfiles)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.DecodePixelWidth = 150;
                bmp.SetSource(await file.OpenReadAsync());
                lvPics.Items.Add(bmp);
            }
        }
    }
}
