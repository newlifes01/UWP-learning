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

            TaskItem item = new TaskItem
            {
                TaskID = 1000251,
                TaskName = "示例工序",
                TaskDesc = "示例描述。",
                TaskProgress = 60d
            };
            this.layoutRoot.DataContext = item;
        }

    }

    public class TaskItem
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public int TaskID { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        public string TaskDesc { get; set; }
        /// <summary>
        /// 任务进度
        /// </summary>
        public double TaskProgress { get; set; }
    }
}
