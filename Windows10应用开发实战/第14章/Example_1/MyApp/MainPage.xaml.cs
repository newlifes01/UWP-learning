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
using Windows.Devices.Sensors;
using Windows.UI.Popups;


namespace MyApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        Compass _compass = null;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.Loaded += MainPage_Loaded;

            Window.Current.VisibilityChanged += OnWindowVisibilityChanged;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // 获取当前设备上的罗盘传感器
            _compass = Compass.GetDefault();
            if (_compass == null)
            {
                // 设备不支持罗盘传感器
                MessageDialog msgBox = new MessageDialog("此设备不支持罗盘。");
                var ac = msgBox.ShowAsync();
                return;
            }
            // 设置读取频率
            _compass.ReportInterval = 30;
            // 处理ReadingChanged事件
            _compass.ReadingChanged += _compass_ReadingChanged;
        }

        private void _compass_ReadingChanged(Compass sender, CompassReadingChangedEventArgs args)
        {
            // 读取数据
            CompassReading reading = args.Reading;
            double trueNorth = reading.HeadingTrueNorth.HasValue ? reading.HeadingTrueNorth.Value : 0d;
            double magNorth = reading.HeadingMagneticNorth;
            // 显示数据
            var r = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    runGN.Text = trueNorth.ToString("0.000");
                    runMN.Text = magNorth.ToString("0.000");
                });
        }

        private void OnWindowVisibilityChanged(object sender, Windows.UI.Core.VisibilityChangedEventArgs e)
        {
            if (_compass == null)
            {
                return;
            }
            if (e.Visible)
            {
                // 如果当前窗口可见，则读取数据
                _compass.ReadingChanged += _compass_ReadingChanged;
            }
            else
            {
                // 如果当前窗口不可见，则不再读取数据
                _compass.ReadingChanged -= _compass_ReadingChanged;
            }
        }
    }
}
