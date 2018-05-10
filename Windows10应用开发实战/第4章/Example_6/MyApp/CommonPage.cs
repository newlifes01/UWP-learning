using System;
using Windows.UI.Xaml.Controls;

namespace MyApp
{
    public class CommonPage : Page
    {
        public CommonPage()
        {
            InitializeAppBar();
        }

        private void InitializeAppBar()
        {
            AppBar appbar = new AppBar();
            AppBarButton btn = new AppBarButton();
            btn.Icon = new SymbolIcon(Symbol.Back);
            btn.Label = "后退";
            btn.Click += Btn_Click;
            appbar.Content = btn;
            this.BottomAppBar = appbar;
        }

        private void Btn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
