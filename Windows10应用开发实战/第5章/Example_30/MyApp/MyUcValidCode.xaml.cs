using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace MyApp
{
    public sealed partial class MyUcValidCode : UserControl
    {
        Random rand = new Random();
        public MyUcValidCode ()
        {
            this.InitializeComponent();
            RefreshCode();
            this.txtInputCode.LostFocus += txtInputCode_LostFocus;
        }

        void txtInputCode_LostFocus ( object sender, RoutedEventArgs e )
        {
            symbTip.Visibility = Visibility.Visible;

            if (string.IsNullOrWhiteSpace(txtInputCode.Text) || txtInputCode.Text != tbCode.Text)
            {
                SetValue(IsCheckedProperty, false);
                symbTip.Symbol = Symbol.Cancel;
                symbTip.Foreground = new SolidColorBrush(Colors.Red);
                return;
            }
            symbTip.Symbol = Symbol.Accept;
            symbTip.Foreground = new SolidColorBrush(Colors.Green);
            SetValue(IsCheckedProperty, true);
        }

        private void OnTBTapped ( object sender, TappedRoutedEventArgs e )
        {
            RefreshCode();
        }

        void RefreshCode ()
        {
            int i = rand.Next(0, 10000);
            tbCode.Text = i.ToString("0000");
        }

        #region 依赖项属性
        private static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(MyUcValidCode), new PropertyMetadata(true,new PropertyChangedCallback(OnIsCheckedPropertyChanged)));

        private static void OnIsCheckedPropertyChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            MyUcValidCode uc = d as MyUcValidCode;
            bool b = (bool)e.NewValue;
            if (uc.IsCheckedChanged != null)
            {
                uc.IsCheckedChanged(b);
            }
            
        }

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
        }
        #endregion


        #region 事件
        public event Action<bool> IsCheckedChanged;
        #endregion
    }
}
