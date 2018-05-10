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
using Windows.UI;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace MyApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ShowColorPage : Page
    {
        public ShowColorPage()
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
            IDictionary<string, string> dic = e.Parameter as IDictionary<string, string>;
            if (dic != null)
            {
                // 读出各个颜色通道的值
                byte r, g, b;
                if (!byte.TryParse(dic["r"],out r))
                {
                    r = 0;
                }
                if (!byte.TryParse(dic["g"],out g))
                {
                    g = 0;
                }
                if (!byte.TryParse(dic["b"], out b))
                {
                    b = 0;
                }
                // 修改画刷的颜色
                this.sldbrush.Color = ColorHelper.FromArgb(255, r, g, b);
            }
        }

        private void OnHome(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
