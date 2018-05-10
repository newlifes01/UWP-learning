using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Capture;
using Windows.Devices.Enumeration;
using System.Threading.Tasks;

namespace MyApp
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    public sealed partial class App : Application
    {
        private TransitionCollection transitions;

        /// <summary>
        /// 初始化单一实例应用程序对象。    这是执行的创作代码的第一行，
        /// 逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
            this.Resuming += this.OnResuming;
        }

        async void OnResuming(object sender, object e)
        {
            await InitializeCapture();
        }

        /// <summary>
        /// 访问MediaCapture对象
        /// </summary>
        public MediaCapture AppMediaCapture { get; private set; }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 当启动应用程序以打开特定的文件或显示搜索结果等操作时，
        /// 将使用其他入口点。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            await this.InitializeCapture();
            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                // TODO: 将此值更改为适合您的应用程序的缓存大小
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // 删除用于启动的旋转门导航。
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;

                // 当导航堆栈尚未还原时，导航到第一页，
                // 并通过将所需信息作为导航参数传入来配置
                // 新页面
                if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // 确保当前窗口处于活动状态
            Window.Current.Activate();
        }

        /// <summary>
        /// 启动应用程序后还原内容转换。
        /// </summary>
        /// <param name="sender">附加了处理程序的对象。</param>
        /// <param name="e">有关导航事件的详细信息。</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。    在不知道应用程序
        /// 将被终止还是恢复的情况下保存应用程序状态，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起的请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: 保存应用程序状态并停止任何后台活动
            CleanupCapture();

            deferral.Complete();
        }


        #region 私有方法
        /// <summary>
        /// 初始化捕捉组件
        /// </summary>
        private async Task InitializeCapture()
        {
            // 实例化MediaCapture
            this.AppMediaCapture = new MediaCapture();
            // 查找后置摄像头
            DeviceInformationCollection dvcls = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
            DeviceInformation backCamera = dvcls.FirstOrDefault(d => d.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Back);
            if (backCamera == null)
            {
                throw new NullReferenceException("没有找到后置摄像头");
            }
            MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings();
            // 引用后置摄像头的ID
            settings.VideoDeviceId = backCamera.Id;
            // 不使用音频输入设备
            settings.AudioDeviceId = "";
            // 仅捕捉视频
            settings.StreamingCaptureMode = StreamingCaptureMode.Video;
            // 初始化MediaCapture对象
            await AppMediaCapture.InitializeAsync(settings);
            System.Diagnostics.Debug.WriteLine("捕捉组件已经初始化。");
        }

        /// <summary>
        /// 清理捕捉组件
        /// </summary>
        private void CleanupCapture()
        {
            if (AppMediaCapture != null)
            {
                AppMediaCapture.Dispose();
                AppMediaCapture = null;
                System.Diagnostics.Debug.WriteLine("捕捉组件已经释放。");
            }
        }
        #endregion
    }
}