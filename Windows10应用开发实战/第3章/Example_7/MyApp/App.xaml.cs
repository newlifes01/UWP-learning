using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;


namespace MyApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.Language = "zh-cn"; //设置默认语言
                Window.Current.Content = rootFrame;

                // 如果应用程序之前是被用户关闭的，或者被操作系统结束的
                // 就要考虑对导航状态进行恢复
                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated || e.PreviousExecutionState == ApplicationExecutionState.ClosedByUser)
                {
                    // 如果本地设置中包含导航状态，则进行恢复
                    // 否则进入默认页面
                    object value;
                    var localSettings = ApplicationData.Current.LocalSettings;
                    if (localSettings.Values.TryGetValue("nav", out value))
                    {
                        rootFrame.SetNavigationState(value as string);
                    }
                    else
                    {
                        rootFrame.Navigate(typeof(MainPage));
                    }
                }
                else //否则直接导航到主页面
                {
                    rootFrame.Navigate(typeof(MainPage));
                }
            }

            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            Frame rootFrame = Window.Current.Content as Frame;
            // 获取导航状态
            string navstate = rootFrame.GetNavigationState();
            // 在调试信息中输出该状态内容
            System.Diagnostics.Debug.WriteLine("导航状态：" + navstate);
            // 将导航状态保存到本地设置中
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["nav"] = navstate;

            deferral.Complete();
        }
    }
}
