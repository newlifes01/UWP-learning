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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace MyApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class checkBoxSamplePage : CommonPage
    {
        public checkBoxSamplePage ()
        {
            this.InitializeComponent();
        }

        private void OnChecked ( object sender, RoutedEventArgs e )
        {
            List<string> items = new List<string>();
            var checkedItems = from u in checks.Children
                               where (u is CheckBox) && ((u as CheckBox).IsChecked == true)
                               select u as CheckBox;
            foreach (CheckBox cb in checkedItems)
            {
                items.Add(cb.Content as string);
            }
            tbRes.Text = "你喜欢的水果有：" + string.Join("、", items.ToArray());
        }

        private void OnUnchecked ( object sender, RoutedEventArgs e )
        {
            List<string> items = new List<string>();
            var checkedItems = from u in checks.Children
                               where (u is CheckBox) && ((u as CheckBox).IsChecked == true)
                               select u as CheckBox;
            foreach (CheckBox cb in checkedItems)
            {
                items.Add(cb.Content as string);
            }
            tbRes.Text = "你喜欢的水果有：" + string.Join("、", items.ToArray());
        }
    }
}
