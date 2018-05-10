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

            cmb.ItemsSource = new Product[]
            {
                new Product { ID = "T-001", Name = "试验产品 1" },
                new Product { ID = "T-002", Name = "试验产品 2" },
                new Product { ID = "T-003", Name = "试验产品 3" },
                new Product { ID = "T-004", Name = "试验产品 4" }
            };
        }

        private void OnSelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            object val = cmb.SelectedValue;
            runID.Text = val == null ? "无" : val.ToString();
            object selitem = cmb.SelectedItem;
            runType.Text = selitem == null ? "未知" : selitem.GetType().Name;
        }

    }


    public class Product
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }
    }
    
}
