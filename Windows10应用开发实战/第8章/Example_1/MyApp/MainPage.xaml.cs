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
        Storyboard m_storyboard = null;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            // 初始化演示图板
            m_storyboard = new Storyboard();
            // 定义时间线
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = TimeSpan.FromSeconds(2d); //时间线持续时间
            da.From = 0d; //初始值
            da.To = 360d; //最终值
            // 设置为无限循环播放
            da.RepeatBehavior = RepeatBehavior.Forever;
            // 将时间线与RotateTransform对象关联
            Storyboard.SetTarget(da, rttrf);
            Storyboard.SetTargetProperty(da, "Angle");
            // 将时间线加入Storyboard的时间线集合中
            m_storyboard.Children.Add(da);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // 开始播放动画
            m_storyboard.Begin();
        }
    }
}
