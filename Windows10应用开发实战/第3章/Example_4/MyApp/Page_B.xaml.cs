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
    public sealed partial class Page_B : Page
    {
        public Page_B ()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo ( NavigationEventArgs e )
        {
            this.tbInfo.Text = "导航模式：" + e.NavigationMode.ToString();
        }

        private void OnBack ( object sender, RoutedEventArgs e )
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void OnForward ( object sender, RoutedEventArgs e )
        {
            if (this.Frame.CanGoForward)
            {
                this.Frame.GoForward();
            }
            else
            {
                this.Frame.Navigate(typeof(Page_C));
            }
        }

    }
}
