using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gameslist.Model;
using GalaSoft.MvvmLight;
using Windows.UI.Xaml;
using System.Collections.ObjectModel;

namespace gameslist.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {

        /// <summary>
        /// 声明List
        /// </summary>
        private gamesList _gamesRandList;
        /// <summary>
        /// 构造函数
        /// </summary>
        public HomeViewModel()
        {
            //SetCitys();
            gamesList = _gamesRandList = new gamesList(Constant.Method.gamesByRand, "");
            _gamesRandList.DataLoaded += _gamesRandList_DataLoaded;
            _gamesRandList.DataLoading += _gamesRandList_DataLoading;
        }

        private void _gamesRandList_DataLoading()
        {
            ProgressRingVisibility = Visibility.Visible;
        }

        private void _gamesRandList_DataLoaded()
        {
            ProgressRingVisibility = Visibility.Collapsed;
        }

        #region 属性

        private ObservableCollection<games> _gamesList = new ObservableCollection<games>();
        /// <summary>
        /// 律师列表
        /// </summary>
        public ObservableCollection<games> gamesList
        {
            get { return _gamesList; }
            set
            {
                _gamesList = value;
                RaisePropertyChanged("gamesList");
            }
        }

        private Visibility _progressRingVisibility = Visibility.Collapsed;
        /// <summary>
        /// 等待是否隐藏
        /// </summary>
        public Visibility ProgressRingVisibility
        {
            get
            {
                return _progressRingVisibility;
            }

            set
            {
                _progressRingVisibility = value;
                RaisePropertyChanged("ProgressRingVisibility");
            }
        }
        #endregion

        
    }
}
