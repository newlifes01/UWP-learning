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
using Windows.UI.Popups;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

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
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。
        }

        private async void OnClick ( object sender, RoutedEventArgs e )
        {
            // 实例化MessageDialog对象
            MessageDialog dlg = new MessageDialog("确定要执行任务吗?", "提示");
            // 添加命令按钮
            UICommand cmdOK = new UICommand("确定", new UICommandInvokedHandler(OnCommandAct), 1);
            UICommand cmdCancel = new UICommand("取消", new UICommandInvokedHandler(OnCommandAct), 2);
            dlg.Commands.Add(cmdOK);
            dlg.Commands.Add(cmdCancel);
            // 显示对话框
            await dlg.ShowAsync();
        }

        private void OnCommandAct ( IUICommand command )
        {
            int cmdID = (int)command.Id;
            if (cmdID == 1)
            {
                tbResult.Text = "您已经确认执行任务。";
            }
            else
            {
                tbResult.Text = "您已经放弃执行该任务。";
            }
        }
    }
}
