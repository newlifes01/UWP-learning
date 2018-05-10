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
using Windows.Data.Json;

namespace FileActiveApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class DisplayPage : Page
    {
        public DisplayPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // 获取参数
            JsonObject jsobj = e.Parameter as JsonObject;
            // 显示内容
            string displayContent = "";
            if (jsobj != null)
            {
                string name = jsobj.GetNamedString("name");
                string city = jsobj.GetNamedString("city");
                string age = jsobj.GetNamedNumber("age").ToString();
                displayContent = string.Format("姓名：{0}\n城市：{1}\n年龄：{2}", name, city, age);
            }
            tbContent.Text = displayContent;
        }

        private void OnMain(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
