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

        private void OnLoaded ( object sender, RoutedEventArgs e )
        {
            // 自定义HTML内容
            string html = "<html>" +
                            "<head>" +
                                "<title>test</title>" +
                                "<script type=\"text/javascript\">" +
                                    "function setFontSize(size) {" +
                                        "var ele = document.getElementById(\"sptext\");" +
                                        "ele.style.fontSize = size + 'px';" +
                                    "}" +
                                "</script>" +
                            "</head>" +
                            "<body style=\"color: white; background-color: black;\">" +
                                "<span id=\"sptext\" style=\"font-size:20px\">" +
                                    "示例文本" +
                                "</span>" +
                            "</body>" +
                        "</html>";
            // 加载HTML内容
            (sender as WebView).NavigateToString(html);
        }

        private async void OnClick ( object sender, RoutedEventArgs e )
        {
            Button btn = (Button)sender;
            short tag = Convert.ToInt16(btn.Tag);
            string arg = ""; //传递给脚本函数的参数
            switch (tag)
            {
                case 1:
                    arg = "20";
                    break;
                case 2:
                    arg = "32";
                    break;
                case 3:
                    arg = "45";
                    break;
            }
            btn.IsEnabled = false;
            // 执行HTML内容中的脚本函数
            await webView.InvokeScriptAsync("setFontSize", new string[] { arg });
            btn.IsEnabled = true;
        }
    }
}
