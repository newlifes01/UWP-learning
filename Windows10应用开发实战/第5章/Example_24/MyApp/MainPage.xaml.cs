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
using Windows.UI.Popups;
using Windows.UI;

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

        private async void OnTapped ( object sender, TappedRoutedEventArgs e )
        {
            TextBlock tb = sender as TextBlock;
            PopupMenu menu = new PopupMenu();
            // 用于响应菜单命令的回调
            UICommandInvokedHandler invokedHandler = ( cmd ) =>
            {
                SolidColorBrush brush = cmd.Id as SolidColorBrush;
                tb.Foreground = brush;
            };
            // 添加菜单项
            UICommand cmdRed = new UICommand("红", invokedHandler, new SolidColorBrush(Colors.Red));
            UICommand cmdYellow = new UICommand("黄", invokedHandler, new SolidColorBrush(Colors.Yellow));
            UICommand cmdPurple = new UICommand("紫", invokedHandler, new SolidColorBrush(Colors.Purple));
            menu.Commands.Add(cmdRed);
            menu.Commands.Add(cmdYellow);
            menu.Commands.Add(cmdPurple);
            // 计算TextBlock对象相对于当前窗口的位置坐标
            GeneralTransform gt = tb.TransformToVisual(null);
            Point popupPoint = gt.TransformPoint(new Point(0d, tb.ActualHeight));
            // 显示菜单
            await menu.ShowAsync(popupPoint);
        }


    }
}
