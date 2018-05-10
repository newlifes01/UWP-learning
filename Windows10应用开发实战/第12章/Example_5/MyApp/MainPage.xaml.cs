﻿using System;
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
            // 从应用设置中读取信息
            var rootContainer = ApplicationData.Current.LocalSettings;
            object cmbIndex, text1, text2, toggleVal;
            if (rootContainer.Values.TryGetValue(SETTING_UPDATE_FREQUENCY, out cmbIndex))
            {
                cmb.SelectedIndex = (int)cmbIndex;
            }
            if (rootContainer.Values.TryGetValue(SETTING_TEXT_A, out text1))
            {
                txt1.Text = text1 as string;
            }
            if (rootContainer.Values.TryGetValue(SETTING_TEXT_B, out text2))
            {
                txt2.Text = text2 as string;
            }
            if (rootContainer.Values.TryGetValue(SETTING_TOGGLE_VALUE, out toggleVal))
            {
                tgswitch.IsOn = (bool)toggleVal;
            }
        }

        #region 常量列表
        const string SETTING_UPDATE_FREQUENCY = "update_frequency";
        const string SETTING_TEXT_A = "text_a";
        const string SETTING_TEXT_B = "text_b";
        const string SETTING_TOGGLE_VALUE = "toggle_val";
        #endregion

        private void OnCmbSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = cmb.SelectedIndex;
            if (index > -1)
            {
                // 将下拉列表框中选中项的索引存入应用设置中
                // 获取根容器的引用
                ApplicationDataContainer root = ApplicationData.Current.LocalSettings;
                // 向根容器写入新设置项
                root.Values[SETTING_UPDATE_FREQUENCY] = index;
            }
        }

        private void OnText1Lostfocus(object sender, RoutedEventArgs e)
        {
            // 将文本框中输入的文本存入设置中
            if (!string.IsNullOrWhiteSpace(txt1.Text))
            {
                ApplicationData.Current.LocalSettings.Values[SETTING_TEXT_A] = txt1.Text;
            }
        }

        private void OnText2Lostfocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt2.Text))
            {
                ApplicationData.Current.LocalSettings.Values[SETTING_TEXT_B] = txt2.Text;
            }
        }

        private void OnToggled(object sender, RoutedEventArgs e)
        {
            // 将ToggleSwitch控件的当前状态存入应用设置
            ApplicationData.Current.LocalSettings.Values[SETTING_TOGGLE_VALUE] = tgswitch.IsOn;
        }
    }
}
