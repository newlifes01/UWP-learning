using System;
using Windows.UI.Xaml.Controls;

namespace MyApp
{
    public class CommonPage : Page
    {
        public CommonPage()
        {
            CreateAppBar();
        }

        private void CreateAppBar()
        {
            AppBar bar = new AppBar();
            this.BottomAppBar = bar;
            AppBarButton btn = new AppBarButton
            {
                Label = "后退",
                Icon = new SymbolIcon(Symbol.Back)
            };
            btn.Click += Btn_Click;
            bar.Content = btn;
        }

        private void Btn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
