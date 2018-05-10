using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

            this.frame.Navigate(typeof(Page1));
        }

        private void OnPage1 ( object sender, RoutedEventArgs e )
        {
            this.frame.Navigate(typeof(Page1));
        }

        private void OnPage2 ( object sender, RoutedEventArgs e )
        {
            this.frame.Navigate(typeof(Page2));
        }

        private void OnPage3 ( object sender, RoutedEventArgs e )
        {
            this.frame.Navigate(typeof(Page3));
        }
    }
}
