using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Media;
using Windows.Foundation.Collections;
using Windows.Media.Playback;

namespace AudioTask
{
    public sealed class BgTask : IBackgroundTask
    {
        #region 私有字段
        SystemMediaTransportControls mdcontrol = null;
        BackgroundTaskDeferral deferral = null;
        MediaPlayer currentPlayer = null;
        // 后台音频是否已运行的标志
        bool isRunning = false;
        // 指示是否第一次播放
        bool isFirstPlaying = false;
        #endregion
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            mdcontrol = SystemMediaTransportControls.GetForCurrentView();
            mdcontrol.IsEnabled = true;
            // 允许使用播放/暂停按钮
            mdcontrol.IsPlayEnabled = true;
            mdcontrol.IsPauseEnabled = true;
            // 处理ButtonPressed事件
            mdcontrol.ButtonPressed += mdcontrol_ButtonPressed;
            // 获取MediaPlayer实例
            currentPlayer = BackgroundMediaPlayer.Current;
            // 处理事件，接收来自前台应用程序的消息
            BackgroundMediaPlayer.MessageReceivedFromForeground += BackgroundMediaPlayer_MessageReceivedFromForeground;
            // 关闭自动开始播放
            currentPlayer.AutoPlay = false;
            // 设置播放源
            Uri audioUri = new Uri("ms-appx:///我爱我家.mp3");
            currentPlayer.SetUriSource(audioUri);
            deferral = taskInstance.GetDeferral();
            // 当后台任务被取消时引发事件
            taskInstance.Canceled += task_Canceled;
            isRunning = true;
        }

        private void task_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            // 解除事件绑定
            mdcontrol.ButtonPressed -= mdcontrol_ButtonPressed;
            BackgroundMediaPlayer.MessageReceivedFromForeground -= BackgroundMediaPlayer_MessageReceivedFromForeground;
            // 通知系统后台任务完成
            deferral.Complete();
        }

        private void BackgroundMediaPlayer_MessageReceivedFromForeground(object sender, MediaPlayerDataReceivedEventArgs e)
        {
            // 接收消息
            ValueSet val = e.Data;
            if (val.ContainsKey("action"))
            {
                string msg = val["action"] as string;
                if (msg.Equals("play")) //播放
                {
                    isFirstPlaying = true;
                    Play();
                    isFirstPlaying = false;
                }
                else //暂停
                {
                    Pause();
                }
            }
        }

        void mdcontrol_ButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            // 如果用户按下了播放键
            if (args.Button == SystemMediaTransportControlsButton.Play)
            {
                Play();
            }
            // 如果用户按下了暂停按钮
            else if (args.Button == SystemMediaTransportControlsButton.Pause)
            {
                if (currentPlayer.CurrentState == MediaPlayerState.Playing)
                {
                    Pause();
                }
            }
        }

        #region 私有方法
        /// <summary>
        /// 播放
        /// </summary>
        void Play()
        {
            if (isRunning)
            {
                currentPlayer.Play();
                if (isFirstPlaying)
                {
                    // 更新系统多媒体控制界面的显示内容
                    SystemMediaTransportControlsDisplayUpdater displayUpdater = mdcontrol.DisplayUpdater;
                    // 显示类型为音乐
                    displayUpdater.Type = MediaPlaybackType.Music;
                    // 设置显示的字段值
                    displayUpdater.MusicProperties.Artist = "戴娆";
                    displayUpdater.MusicProperties.Title = "我爱我家";
                    // 更新显示
                    displayUpdater.Update();
                }
            }
        }

        /// <summary>
        /// 暂停
        /// </summary>
        void Pause()
        {
            if (isRunning)
            {
                currentPlayer.Pause();
            }
        }
        #endregion
    }
}
