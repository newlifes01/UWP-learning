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
using Windows.UI.Xaml.Media.Animation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace MyApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Storyboard storyboard = null;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            CreateAnimation();
        }

        private void CreateAnimation ()
        {
            DoubleAnimationUsingKeyFrames frames = new DoubleAnimationUsingKeyFrames();
            frames.Duration = new Duration(TimeSpan.FromSeconds(4));
            LinearDoubleKeyFrame f1 = new LinearDoubleKeyFrame
            {
                KeyTime = TimeSpan.FromSeconds(0),
                Value = 0d
            };
            LinearDoubleKeyFrame f2 = new LinearDoubleKeyFrame
            {
                KeyTime = TimeSpan.FromSeconds(1),
                Value = 1d
            };
            LinearDoubleKeyFrame f3 = new LinearDoubleKeyFrame
            {
                KeyTime = TimeSpan.FromSeconds(3),
                Value = 1d
            };
            LinearDoubleKeyFrame f4 = new LinearDoubleKeyFrame
            {
                KeyTime = TimeSpan.FromSeconds(4),
                Value = 0d
            };
            frames.KeyFrames.Add(f1);
            frames.KeyFrames.Add(f2);
            frames.KeyFrames.Add(f3);
            frames.KeyFrames.Add(f4);
            Storyboard.SetTarget(frames, bd);
            Storyboard.SetTargetProperty(frames, "Opacity");
            storyboard = new Storyboard();
            storyboard.Children.Add(frames);
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

        private void OnClick ( object sender, RoutedEventArgs e )
        {
            storyboard.Begin();
        }
    }
}
