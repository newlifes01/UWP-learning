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
using Windows.Data.Json;
using Windows.UI.Popups;

namespace FileActiveApp
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

        private async void OnClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MessageDialog msgBox = new MessageDialog("请输入姓名、城市和年龄。");
            if (txtName.Text =="" || txtCity.Text == "" || txtAge.Text == "")
            {
                await msgBox.ShowAsync();
                return;
            }
            btn.IsEnabled = false;
            // 获取文档库目录
            StorageFolder doclib = KnownFolders.DocumentsLibrary;
            // 将输入的内容转化为JSON对象
            JsonObject objjson = new JsonObject();
            objjson.Add("name", JsonValue.CreateStringValue(txtName.Text));
            objjson.Add("city", JsonValue.CreateStringValue(txtCity.Text));
            objjson.Add("age", JsonValue.CreateNumberValue(Convert.ToDouble(txtAge.Text)));
            // 提取出JSON字符串
            string jsonStr = objjson.Stringify();
            // 在文档库中创建新文件
            string fileName = DateTime.Now.ToString("yyyy-M-d-HHmmss") + ".myddc";
            StorageFile newFile = await doclib.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            // 将JSON字符串写入文件
            await FileIO.WriteTextAsync(newFile, jsonStr);
            btn.IsEnabled = true;
            msgBox.Content = "保存成功。";
            await msgBox.ShowAsync();
        }

        private void OnFileList(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FilesPage));
        }
    }
}
