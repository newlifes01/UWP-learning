using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;


namespace MyApp
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    public sealed partial class App : Application
    {
        public App ()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 用于导航的Frame对象
        /// </summary>
        public Frame RootFrame { get; private set; }

        protected override void OnLaunched ( LaunchActivatedEventArgs args )
        {
            // 实例化Frame对象
            RootFrame = new Frame();
            // 将当前Frame作为窗口的内容
            Window.Current.Content = RootFrame;
            // 导航到页面一
            RootFrame.Navigate(typeof(FirstPage));
            // 激活当前窗口
            Window.Current.Activate();
        }
    }
}