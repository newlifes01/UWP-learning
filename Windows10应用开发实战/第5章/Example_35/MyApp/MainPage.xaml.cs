using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Navigation;

namespace MyApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<ViewItem> m_items = null;
        public MainPage()
        {
            this.InitializeComponent();

            m_items = new ObservableCollection<ViewItem>();
            this.lvPrev.ItemsSource = m_items;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            m_items.Clear();
            // 添加项列表
            m_items.Add(new ViewItem { Text = "雪花", Uri = new Uri("ms-appx:///Assets/images/1.jpg") });
            m_items.Add(new ViewItem { Text = "风筝", Uri = new Uri("ms-appx:///Assets/images/2.jpg") });
            m_items.Add(new ViewItem { Text = "核桃", Uri = new Uri("ms-appx:///Assets/images/3.jpg") });
            m_items.Add(new ViewItem { Text = "小溪", Uri = new Uri("ms-appx:///Assets/images/4.jpg") });
            m_items.Add(new ViewItem { Text = "胡杨", Uri = new Uri("ms-appx:///Assets/images/5.jpg") });
            m_items.Add(new ViewItem { Text = "红梅", Uri = new Uri("ms-appx:///Assets/images/6.jpg") });
        }

        private void OnClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.splitView.IsPaneOpen = !this.splitView.IsPaneOpen;
        }
    }


    public class ViewItem
    {
        public string Text { get; set; }
        public Uri Uri { get; set; }
    }
}
