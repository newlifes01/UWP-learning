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

            List<News> newsList = new List<News>();
            for (int i = 1; i <= 4; i++)
            {
                newsList.Add(new News { Category = "社会", Title = "示例新闻" + i.ToString(), Content = "测试新闻内容 " + i.ToString(), PublishDate = DateTime.Now.AddDays(i) });
            }
            for (int i = 5; i <= 7; i++)
            {
                newsList.Add(new News { Category = "娱乐", Title = "示例新闻" + i.ToString(), Content = "测试新闻内容 " + i.ToString(), PublishDate = DateTime.Now.AddDays(i) });
            }
            for (int i = 8; i <= 10; i++)
            {
                newsList.Add(new News { Category = "法制", Title = "示例新闻" + i.ToString(), Content = "测试新闻内容 " + i.ToString(), PublishDate = DateTime.Now.AddDays(i) });
            }

            // 对数据进行分组
            var groups = from n in newsList
                         group n by n.Category;
            // 设置数据源
            this.cvs.Source = groups;
        }

    }


    public class News
    {
        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime PublishDate { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }


    public sealed class DateConverter : IValueConverter
    {
        public object Convert ( object value, Type targetType, object parameter, string language )
        {
            DateTime dt = (DateTime)value;
            return dt.ToString("yyyy-M-d");
        }

        public object ConvertBack ( object value, Type targetType, object parameter, string language )
        {
            return null;
        }
    }


}
