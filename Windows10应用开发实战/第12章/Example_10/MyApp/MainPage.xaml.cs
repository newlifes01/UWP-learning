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
using Windows.UI.Popups;

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

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。
        }

        private async void OnClick(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length == 0 || txtAge.Text.Length == 0 || txtNo.Text.Length == 0 || txtCity.Text.Length == 0)
            {
                MessageDialog msgbox = new MessageDialog("请输入相关信息。");
                await msgbox.ShowAsync();
                return;
            }

            // 实例化JsonObject对象
            JsonObject obj = new JsonObject();
            // 设置各字段的值
            obj["name"] = JsonValue.CreateStringValue(txtName.Text);
            obj["no"] = JsonValue.CreateStringValue(txtNo.Text);
            obj["city"] = JsonValue.CreateStringValue(txtCity.Text);
            obj["age"] = JsonValue.CreateNumberValue(Convert.ToDouble(txtAge.Text));
            // 显示JSON对象的字符串表示形式
            string jstr = obj.Stringify();
            tbJson.Text = jstr;
        }
    }
}
