using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameslist.ViewModel
{
    public class VMLocator
    {
        /// <summary>
        /// 上锁
        /// </summary>
        private static readonly object SynObject = new object();

        private static VMLocator _instance;
        /// <summary>
        /// ViewModel实例控制器
        /// </summary>
        public static VMLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SynObject)
                        _instance = new VMLocator();
                }
                return _instance;
            }

            set
            {
                _instance = value;
            }
        }

        private HomeViewModel _homeVM;
        /// <summary>
        /// 主页面View Model
        /// </summary>
        public HomeViewModel HomeVM
        {
            get
            {
                return _homeVM ?? (_homeVM = new HomeViewModel());
            }
        }      
    }
}
