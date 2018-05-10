using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace MyApp
{
    public sealed partial class PopupControl : UserControl
    {
        Frame m_frame = null;
        public PopupControl ( Frame frame )
        {
            this.InitializeComponent();
            m_frame = frame;
            this.Loaded += ( s1, a1 ) =>
                {
                    m_frame.Navigated += m_frame_Navigated;
                };
            this.Unloaded += ( s2, a2 ) =>
                {
                    m_frame.Navigated -= m_frame_Navigated;
                };
        }

        void m_frame_Navigated ( object sender, NavigationEventArgs e )
        {
            lvBack.Items.Clear();
            foreach (PageStackEntry item in m_frame.BackStack)
            {
                lvBack.Items.Add(item.SourcePageType.Name);
            }
            lvForward.Items.Clear();
            foreach (PageStackEntry item in m_frame.ForwardStack)
            {
                lvForward.Items.Add(item.SourcePageType.Name);
            }
        }
    }
}
