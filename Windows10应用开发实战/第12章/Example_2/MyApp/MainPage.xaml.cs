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
            if (txtInput.Text.Length < 1) return;

            Button button = sender as Button;
            button.IsEnabled = false;
            // 获取本地目录的引用
            StorageFolder local = ApplicationData.Current.LocalFolder;
            // 创建新文件
            StorageFile newFile = await local.CreateFileAsync("data.txt", CreationCollisionOption.ReplaceExisting);
            // 写入文件
            await FileIO.WriteTextAsync(newFile, txtInput.Text);
            button.IsEnabled = true;
        }

        private async void OnRead(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.IsEnabled = false;
            try
            {
                // 直接读取文件内容
                txtOutput.Text = await PathIO.ReadTextAsync("ms-appdata:///local/data.txt");
            }
            catch (Exception ex)
            {
                txtOutput.Text = ex.Message;
            }
            btn.IsEnabled = true;
        }

    }
}
