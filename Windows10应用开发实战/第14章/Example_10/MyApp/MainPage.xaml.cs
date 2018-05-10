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
using Windows.Devices.Geolocation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Geolocator _geo = null;
        public MainPage()
        {
            this.InitializeComponent();
            // 实例化Geolocator对象
            _geo = new Geolocator();
            // 设置报告读数频率
            _geo.ReportInterval = 100;
            // 响应PositionChanged事件
            _geo.PositionChanged += _geo_PositionChanged;
        }

        private async void _geo_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
             {
                 Geoposition position = args.Position;
                 Geocoordinate coordinate = position.Coordinate;
                 // 显示位置信息
                 string msg = "";
                 msg += "经度：" + coordinate.Point.Position.Longitude.ToString("0.00") + "\n";
                 msg += "纬度：" + coordinate.Point.Position.Latitude.ToString("0.00") + "\n";
                 msg += "来源：" + coordinate.PositionSource.ToString();
                 tbLocInfo.Text = msg;
             });
        }
    }
}
