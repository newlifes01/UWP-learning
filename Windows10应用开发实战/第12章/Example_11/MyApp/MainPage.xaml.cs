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
using Windows.ApplicationModel.DataTransfer;

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

        private void OnCopy(object sender, RoutedEventArgs e)
        {
            if (txtInput.Text.Length == 0) return;

            // 创建数据包
            DataPackage package = new DataPackage();
            // 向数据包中写入内容
            package.SetText(txtInput.Text);
            // 将内容放入剪贴板
            Clipboard.SetContent(package);
        }

        private async void OnPaste(object sender, RoutedEventArgs e)
        {
            // 获取数据包视图
            DataPackageView packageView = Clipboard.GetContent();
            // 判断是否存在文本内容
            if (packageView.Contains(StandardDataFormats.Text))
            {
                // 读取内容
                tbPaste.Text = await packageView.GetTextAsync();
            }
        }
    }
}
