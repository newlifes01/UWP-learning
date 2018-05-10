using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyApp
{
    class UserData : INotifyPropertyChanged
    {
        #region 私有成员
        // 保存当前类的实例引用
        private static UserData _userData = null;
        private int _count = default(int);
        #endregion
        // 静态构造函数，当静态成员被首次访问时调用
        static UserData ()
        {
            _userData = new UserData();
        }
        /// <summary>
        /// 获取或设置当前计数值
        /// </summary>
        public int Count
        {
            get { return _count; }
            set
            {
                if (value != _count)
                {
                    _count = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// 获取类的当前实例
        /// </summary>
        public static UserData CurrentInstance
        {
            get
            {
                return _userData;
            }
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        public static void LoadData ()
        {
            object settingVal = null;
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("num", out settingVal))
            {
                CurrentInstance.Count = Convert.ToInt32(settingVal);
            }
            else
            {
                CurrentInstance.Count = 0;
            }
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        public static void SaveData ()
        {
            ApplicationData.Current.LocalSettings.Values["num"] = CurrentInstance.Count;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged ( [CallerMemberName] string propName = "" )
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
