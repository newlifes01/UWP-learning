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
using Windows.System;
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


        private async void OnLinkClick(object sender, RoutedEventArgs e)
        {
            HyperlinkButton linkButton = sender as HyperlinkButton;
            string tag = linkButton.Tag as string;
            linkButton.IsEnabled = false;
            switch (tag)
            {
                case "b": //蓝牙设置
                    await Launcher.LaunchUriAsync(new Uri("ms-settings-bluetooth:"));
                    break;
                case "w": //Wlan设置
                    await Launcher.LaunchUriAsync(new Uri("ms-settings-wifi:"));
                    break;
                case "p": //节电模式设置
                    await Launcher.LaunchUriAsync(new Uri("ms-settings-power:"));
                    break;
                case "a": //飞行模式设置
                    await Launcher.LaunchUriAsync(new Uri("ms-settings-airplanemode:"));
                    break;
                case "l": //锁屏设置
                    await Launcher.LaunchUriAsync(new Uri("ms-settings-lock:"));
                    break;
                case "m": //打开音乐库目录
                    StorageFolder musicLeb = KnownFolders.MusicLibrary;
                    await Launcher.LaunchFolderAsync(musicLeb);
                    break;
            }
            linkButton.IsEnabled = true;
        }
    }
}
