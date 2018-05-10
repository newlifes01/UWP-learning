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

namespace FileActiveApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FilesPage : Page
    {
        public FilesPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // 获取文档库目录
            StorageFolder docLib = KnownFolders.DocumentsLibrary;
            // 获取文档库目录下的文件列表
            IReadOnlyList<StorageFile> files = await docLib.GetFilesAsync();
            // 设置数据源
            this.lvFiles.ItemsSource = files;
        }

        private async void OnLvItemClick(object sender, ItemClickEventArgs e)
        {
            // 获得被点击的项
            StorageFile theFile = e.ClickedItem as StorageFile;
            if (theFile != null)
            {
                // 通过Launcher组件打开文件
                await Windows.System.Launcher.LaunchFileAsync(theFile);
            }
        }

        private void OnHome(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
