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


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            StorageFolder rmfolder = KnownFolders.RemovableDevices;
            if (rmfolder != null)
            {
                cmbRemovables.ItemsSource = await rmfolder.GetFoldersAsync();
            }
        }

        private async void OnSave(object sender, RoutedEventArgs e)
        {
            // 从下拉列表中取出被选项
            StorageFolder selFolder = cmbRemovables.SelectedItem as StorageFolder;
            if (selFolder == null || txtInput.Text.Length == 0)
            {
                return;
            }

            // 创建新文件
            StorageFile file = await selFolder.CreateFileAsync("abc.txt", CreationCollisionOption.ReplaceExisting);
            // 向文件写入内容
            await FileIO.WriteTextAsync(file, txtInput.Text);
        }

    }


}
