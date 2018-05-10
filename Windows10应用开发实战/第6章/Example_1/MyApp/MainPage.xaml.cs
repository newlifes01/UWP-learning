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

            Student stu = new Student { Name = "小杨", Course = "C语言" };
            // 实例化Binding对象
            Binding nameBinding = new Binding();
            // 设置源
            nameBinding.Source = stu;
            // 指定获取数据源中Name属性的值
            nameBinding.Path = new PropertyPath("Name");

            Binding courseBinding = new Binding();
            // 设置源
            courseBinding.Source = stu;
            // 指定从数据源的Course属性获取内容
            courseBinding.Path = new PropertyPath("Course");

            // 绑定方向为单向
            nameBinding.Mode = courseBinding.Mode = BindingMode.OneWay;

            // 将Binding实例与TextBlock控件的Text属性关联
            tbName.SetBinding(TextBlock.TextProperty, nameBinding);
            tbCourse.SetBinding(TextBlock.TextProperty, courseBinding);
        }

    }


    public class Student
    {
        /// <summary>
        /// 学员姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Course { get; set; }
    }
}
