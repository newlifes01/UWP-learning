using System;
using Windows.UI.Xaml.Controls;
namespace MyApp
{
    public class CommonPage :Page
    {
        public CommonPage()
        {
            CreateAppBar();
        }

        private void CreateAppBar()
        {
            AppBar bar = new AppBar();
            AppBarButton b = new AppBarButton();
            b.Label = "后退";
            b.Icon = new SymbolIcon(Symbol.Back);
            bar.Content = b;
            b.Click += B_Click;
            this.BottomAppBar = bar;
        }

        private void B_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
