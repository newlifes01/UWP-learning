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
using Windows.Data.Xml.Dom;

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

        private async void OnBuildXml(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.IsEnabled = false;

            // 创建XmlDocument实例
            XmlDocument xdoc = new XmlDocument();
            // 创建根节点
            XmlElement root = xdoc.CreateElement("books");
            // 创建子元素
            XmlElement book = xdoc.CreateElement("book");
            // 将根节点追加到XML文档中
            xdoc.AppendChild(root);
            // 设置特性值
            book.SetAttribute("ISBM", "100658425");
            // 创建文本节点
            XmlText text = xdoc.CreateTextNode("示例图书");
            // 将文本节点添加到book节点中
            book.AppendChild(text);
            // 将book节点添加到根节点上
            root.AppendChild(book);
            // 获取本地目录的引用
            StorageFolder local = ApplicationData.Current.LocalFolder;
            // 创建新文件
            StorageFile xmlFile = await local.CreateFileAsync("test.xml", CreationCollisionOption.ReplaceExisting);
            // 将新建的XML文档保存到文件
            await xdoc.SaveToFileAsync(xmlFile);

            btn.IsEnabled = true;
        }

        private async void OnReadXml(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.IsEnabled = false;

            // 从本地文件中加载XML
            StorageFolder local = ApplicationData.Current.LocalFolder;
            StorageFile xmlfile = await local.GetFileAsync("test.xml");
            XmlDocument xdoc = await XmlDocument.LoadFromFileAsync(xmlfile);
            // 显示XML文档
            tbXml.Text = xdoc.GetXml();

            btn.IsEnabled = true;
        }
    }
}
