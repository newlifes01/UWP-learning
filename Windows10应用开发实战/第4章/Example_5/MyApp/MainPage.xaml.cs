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

            // 单选模式
            lvHor.SelectionMode = lvVert.SelectionMode = ListViewSelectionMode.Single;
            // 列出水平对齐方式
            string[] horalgs = Enum.GetNames(typeof(HorizontalAlignment));
            lvHor.ItemsSource = horalgs;
            lvHor.SelectionChanged += lvHor_SelectionChanged;
            lvHor.SelectedIndex = 0;
            // 列出垂直对齐方式
            string[] vertalgs = Enum.GetNames(typeof(VerticalAlignment));
            lvVert.ItemsSource = vertalgs;
            lvVert.SelectionChanged += lvVert_SelectionChanged;
            lvVert.SelectedIndex = 0;
        }

        void lvVert_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            string item = e.AddedItems[0] as string;
            if (!string.IsNullOrEmpty(item))
            {
                VerticalAlignment vertalig = (VerticalAlignment)Enum.Parse(typeof(VerticalAlignment), item);
                rect.VerticalAlignment = vertalig;
            }
        }

        void lvHor_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            string item = e.AddedItems[0] as string;
            if (!string.IsNullOrEmpty(item))
            {
                HorizontalAlignment horalig = (HorizontalAlignment)Enum.Parse(typeof(HorizontalAlignment), item);
                rect.HorizontalAlignment = horalig;
            }
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
    }
}
