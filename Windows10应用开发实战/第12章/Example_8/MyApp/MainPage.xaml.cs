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
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.AccessCache;

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


        private async void OnPick(object sender, RoutedEventArgs e)
        {
            FolderPicker picker = new FolderPicker();
            picker.FileTypeFilter.Add("*");
            StorageFolder folder = await picker.PickSingleFolderAsync();
            if (folder != null)
            {
                StorageApplicationPermissions.FutureAccessList.Clear();
                // 向访问列表添加项
                StorageApplicationPermissions.FutureAccessList.Add(folder);
                tbMsg.Text = "已添加目录" + folder.Path + "到访问列表中。";
            }
        }

        private async void OnListFiles(object sender, RoutedEventArgs e)
        {
            if (StorageApplicationPermissions.FutureAccessList.Entries.Count == 0)
            {
                return;
            }
            Button btn = sender as Button;
            btn.IsEnabled = false;

            // 获取访问列表项的标识
            string token = StorageApplicationPermissions.FutureAccessList.Entries[0].Token;
            // 获得访问列表中存储的目录引用
            StorageFolder fd = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
            if (fd != null)
            {
                // 列出目录中的子文件
                IReadOnlyList<StorageFile> files = await fd.GetFilesAsync();
                // 在ListView控件中显示文件列表
                lvFiles.Items.Clear();
                foreach (StorageFile f in files)
                {
                    lvFiles.Items.Add(f.Name);
                }
            }

            btn.IsEnabled = true;
        }
    }
}
