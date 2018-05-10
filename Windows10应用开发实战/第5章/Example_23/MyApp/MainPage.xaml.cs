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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace MyApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage ()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo ( NavigationEventArgs e )
        {
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。
        }

        private void OnDialogClosing ( ContentDialog sender, ContentDialogClosingEventArgs args )
        {
            // 对输入内容进行验证
            bool isChecked = true;
            if (txtUserName.Text == "")
            {
                tbMsg.Text="请输入用户名。";
                isChecked = false;
            }
            if (pwdNew.Password == "" || pwdNew2.Password == "")
            {
                tbMsg.Text="请输入密码。";
                isChecked = false;
            }
            if (pwdNew.Password != pwdNew2.Password)
            {
                tbMsg.Text="两次输入的密码不一致。";
                isChecked = false;
            }
            // 当点击“注册”按钮时才进行处理
            if (args.Result == ContentDialogResult.Primary)
            {
                // 如果验证没有通过，则不允许关闭对话框
                args.Cancel = !isChecked;
            }
        }

        private async void OnClick ( object sender, RoutedEventArgs e )
        {
            ContentDialogResult res = await dlgReg.ShowAsync();
            // 如果点击了“注册”按钮则显示输入结果
            if (res == ContentDialogResult.Primary)
            {
                tbInfo.Text = string.Format("欢迎，新用户：{0}\n您输入的密码为：{1}", txtUserName.Text, pwdNew.Password);
            }
            else
            {
                tbInfo.Text = "未输入新用户。";
            }
        }

        private void OnDialogOpened ( ContentDialog sender, ContentDialogOpenedEventArgs args )
        {
            // 清空对话框中的所有文本
            txtUserName.ClearValue(TextBox.TextProperty);
            pwdNew.ClearValue(PasswordBox.PasswordProperty);
            pwdNew2.ClearValue(PasswordBox.PasswordProperty);
            tbMsg.ClearValue(TextBlock.TextProperty);
        }
    }
}
