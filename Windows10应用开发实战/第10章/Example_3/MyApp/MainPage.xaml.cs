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
using Windows.Storage;
using Windows.Media.Transcoding;
using Windows.Media.MediaProperties;

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

        private async void OnTransCode(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.IsEnabled = false;

            // 从项目目录中获取待转码的MP3文件
            Uri sourceFileUri = new Uri("ms-appx:///1.mp3");
            StorageFile srcFile = await StorageFile.GetFileFromApplicationUriAsync(sourceFileUri);
            // 获取“音乐”文件目录
            StorageFolder musicFd = KnownFolders.MusicLibrary;
            // 新文件名
            string newFileName = "new_file.m4a";
            // 创建新文件
            StorageFile disFile = await musicFd.CreateFileAsync(newFileName, CreationCollisionOption.ReplaceExisting);
            
            // 响应转码处理进度
            IProgress<double> progress = new Progress<double>((p) =>
            {
                // 更新进度
                this.pb.Value = p;
            });

            // 实例化MediaTranscoder对象
            MediaTranscoder transcoder = new MediaTranscoder();
            PrepareTranscodeResult result = await transcoder.PrepareFileTranscodeAsync(srcFile, disFile, MediaEncodingProfile.CreateM4a(AudioEncodingQuality.High));
            // 开始转码
            if (result.CanTranscode)
            {
                await result.TranscodeAsync().AsTask(progress);
            }

            btn.IsEnabled = true;
        }
    }
}
