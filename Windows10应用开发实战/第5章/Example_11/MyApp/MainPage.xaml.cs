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


        private void OnRichtextblockLoaded ( object sender, RoutedEventArgs e )
        {
            // 用于处理溢出文本的RichTextBlockOverflow对象
            RichTextBlockOverflow overFlow = null;
            // 文本是否已经溢出的标志
            bool isTextOverflow = false;
            if (this.rtTextblock.HasOverflowContent) //文本溢出
            {
                overFlow = new RichTextBlockOverflow();
                // 必须将RichTextBlockOverflow对象赋给RichTextBlock的
                // OverflowContentTarget属性
                rtTextblock.OverflowContentTarget = overFlow;
                // 将新的RichTextBlockOverflow对象添加到FlipView的项列表中
                this.flipView.Items.Add(overFlow);
                // 调用UpdateLayout方法，确保呈现的文本已经过测量
                // 才能准确判断文本是否溢出
                overFlow.UpdateLayout();
                isTextOverflow = overFlow.HasOverflowContent;
                // 通过循环继续添加RichTextBlockOverflow对象
                while (isTextOverflow)
                {
                    RichTextBlockOverflow temp = overFlow;
                    overFlow = new RichTextBlockOverflow();
                    temp.OverflowContentTarget = overFlow;
                    this.flipView.Items.Add(overFlow);
                    // 记得调用以下方法更新布局计算
                    overFlow.UpdateLayout();
                    isTextOverflow = overFlow.HasOverflowContent;
                }
            }
        }
    }
}
