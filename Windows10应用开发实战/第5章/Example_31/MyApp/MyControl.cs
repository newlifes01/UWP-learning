using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234235 上有介绍

namespace MyApp
{
    public sealed class MyControl : Control
    {
        public MyControl ()
        {
            this.DefaultStyleKey = typeof(MyControl);
        }

        /// <summary>
        /// 控件中Image控件的名字
        /// </summary>
        const string PART_IMAGE = "PART_Image";

        /// <summary>
        /// Image控件的引用
        /// </summary>
        Image m_image = null;


        #region 依赖项属性
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MyControl), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 要显示的文本
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(MyControl), new PropertyMetadata(null));

        /// <summary>
        /// 要显示的图像
        /// </summary>
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
        #endregion

        protected override void OnApplyTemplate ()
        {
            base.OnApplyTemplate();
            if (m_image != null)
            {
                // 解除事件处理绑定
                m_image.PointerPressed -= OnImagePointerPressed;
                m_image.PointerReleased -= OnImagePointerReleased;
                m_image = null;
            }
            // 找出控件模板中的Image控件
            m_image = GetTemplateChild(PART_IMAGE) as Image;
            if (m_image != null)
            {
                // 添加事件处理程序
                m_image.PointerPressed += OnImagePointerPressed;
                m_image.PointerReleased += OnImagePointerReleased;
            }
        }

        void OnImagePointerReleased ( object sender, PointerRoutedEventArgs e )
        {
            VisualStateManager.GoToState(this, "Normal", false);
            e.Handled = true;
        }

        void OnImagePointerPressed ( object sender, PointerRoutedEventArgs e )
        {
            VisualStateManager.GoToState(this, "Showtext", false);
            e.Handled = true;
        }

    }
}
