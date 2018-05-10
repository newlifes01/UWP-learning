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
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace ClientApp
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
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var uploadTasks = await BackgroundUploader.GetCurrentUploadsAsync();
            if (uploadTasks.Count > 0)
            {
                UploadOperation uploadOpt = uploadTasks[0];
                this.SetUpLoad(uploadOpt, false);
            }
        }

        private async void OnClick(object sender, RoutedEventArgs e)
        {
            int n = (await BackgroundUploader.GetCurrentUploadsAsync()).Count;
            if (n > 0)
            {
                return;
            }

            tbMsg.Text = "";
            FileOpenPicker picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            picker.FileTypeFilter.Add("*"); //表示所有文件类型
            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                BackgroundUploader uploader = new BackgroundUploader();
                // 设置文件名
                uploader.SetRequestHeader("file_name", file.Name);
                uploader.Method = "POST";
                // 创建上传任务
                UploadOperation uploadOpt = uploader.CreateUpload(new Uri(txtUploadUri.Text.Trim()), file);
                SetUpLoad(uploadOpt, true);
            }
        }

        /// <summary>
        /// 执行后台上传操作
        /// </summary>
        /// <param name="opr">操作后台传输任务的对象</param>
        /// <param name="starting">是否为新的上传任务</param>
        public async void SetUpLoad(UploadOperation opr, bool starting)
        {
            // 当上传进度更新时能收到报告
            Progress<UploadOperation> progressReporter = new Progress<UploadOperation>(OnProgressHandler);
            // 启动或附加任务
            try
            {
                if (starting)
                {
                    await opr.StartAsync().AsTask(progressReporter);
                }
                else
                {
                    await opr.AttachAsync().AsTask(progressReporter);
                }
            }
            catch (Exception ex)
            {
                var state = BackgroundTransferError.GetStatus(ex.HResult);
                ShowMessage("错误：" + state);
            }
        }

        private void ShowMessage(string msg)
        {
            var res = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    tbMsg.Text = msg;
                });
        }

        private void OnProgressHandler(UploadOperation p)
        {
            BackgroundUploadProgress progress = p.Progress;
            switch (progress.Status)
            {
                case BackgroundTransferStatus.Canceled: //已取消
                    ShowMessage("任务已取消。");
                    pb.Value = 0d;
                    break;
                case BackgroundTransferStatus.Completed: //完成
                    ShowMessage("任务已完成。");
                    pb.Value = 0d;
                    break;
                case BackgroundTransferStatus.Error: //错误
                    ShowMessage("发生错误。");
                    break;
                case BackgroundTransferStatus.Running: //正在执行
                    double ps = progress.BytesSent * 100 / progress.TotalBytesToSend;
                    pb.Value = ps;
                    ShowMessage(string.Format("已上传{0}字节，共{1}字节。", progress.BytesSent, progress.TotalBytesToSend));
                    break;
            }
        }
    }
}
