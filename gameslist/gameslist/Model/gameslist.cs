﻿using gameslist.Constant;
using gameslist.Data;
using gameslist.HTTP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using static gameslist.Constant.ConstantValue;

namespace gameslist.Model
{
    /// <summary>
    /// 律师
    /// </summary>
    class gamesList : ObservableCollection<games>, ISupportIncrementalLoading
    {
        gamesService gamesService = new gamesService();
        private bool _busy = false;
        private bool _has_more_items = false;
        private int _current_page = 1;
        public event DataLoadingEventHandler DataLoading;
        public event DataLoadedEventHandler DataLoaded;
        public bool HasMoreItems
        {
            get
            {
                if (_busy)
                {
                    return false;
                }
                else
                {
                    return _has_more_items;
                }
            }
            private set
            {
                _has_more_items = value;
            }
        }
        /// <summary>
        /// 参数列表
        /// </summary>
        private object[] parameters;
        /// <summary>
        /// 类型
        /// </summary>
        private Method method;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="method">传入的类型</param>
        /// <param name="parameters">传入的参数</param>
        public gamesList(Method method, params object[] parameters)
        {
            this.parameters = parameters;
            this.method = method;
            HasMoreItems = true;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return InnerLoadMoreItemsAsync(count).AsAsyncOperation();
        }

        private async Task<LoadMoreItemsResult> InnerLoadMoreItemsAsync(uint expectedCount)
        {
            _busy = true;
            var actualCount = 0;
            Info<games> info = null;
            List<games> list = null;
            try
            {
                if (DataLoading != null)
                {
                    DataLoading();
                }
                //根据选择类型，调用不同的接口
                switch (method)
                {
                    case Method.gamesByRand:
                        info = await gamesService.SearchgamesByRand("json",_current_page);
                        break;
                    default:
                        break;
                }
                list = info.result;
            }
            catch (Exception)
            {
                HasMoreItems = false;
            }

            if (list != null && list.Any()&& _current_page<2)
            {
                actualCount = list.Count;
                _current_page++;
                HasMoreItems = true;
                //判断，如果查询的量小于请求个数的话，就把HasMoreItems设置为false，防止，再次去请求，提示没有结果
                if (info.result.Count < count)
                {
                    HasMoreItems = false;
                }
                list.ForEach((c) => { this.Add(c); });
            }
            else
            {
                HasMoreItems = false;
            }
            if (DataLoaded != null)
            {
                DataLoaded();
            }
            _busy = false;
            return new LoadMoreItemsResult
            {
                Count = (uint)actualCount
            };
        }
    }
}
