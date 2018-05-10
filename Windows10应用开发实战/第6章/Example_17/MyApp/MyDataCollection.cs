using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;

namespace MyApp
{
    public sealed class MyDataCollection : ObservableCollection<double> , ISupportIncrementalLoading
    {
        #region 私有字段
        /// <summary>
        /// 指示加载操作是否正在进行
        /// </summary>
        bool isLoading = false;
        /// <summary>
        /// 用于产生随机数的对象
        /// </summary>
        Random rand = new Random();
        /// <summary>
        /// 要加载的最大项目数
        /// </summary>
        const uint MAX_ITEM_COUNT = 10000;
        #endregion

        #region 实现ISupportIncrementalLoading接口的成员
        public bool HasMoreItems
        {
            get 
            {
                if (this.Count >= MAX_ITEM_COUNT)
                {
                    return false;
                }
                return true;
            }
        }

        public Windows.Foundation.IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync ( uint count )
        {
            if (isLoading)
            {
                throw new InvalidOperationException("当前正在加载，请等待本次加载完成后再试。");
            }
            // 实际加载的项数不应超过最大项目数
            // 当前已加载的项与最大项目数的差值
            uint x = MAX_ITEM_COUNT - (uint)this.Count;
            uint itemCountToLoad = 0;
            // 如果差值x小于预期加载的数量，则实际应加载的项目数应为x的值
            // 否则，实际应加载的项的数量等于预期要加载的项目数
            if (x < count)
            {
                itemCountToLoad = x;
            }
            else
            {
                itemCountToLoad = count;
            }
            return AsyncInfo.Run(c => LoadMoreItemsPrivateAsync(c, itemCountToLoad));
        }
        #endregion

        #region 私有方法
        private async Task<LoadMoreItemsResult> LoadMoreItemsPrivateAsync ( CancellationToken ct, uint n )
        {
            // 标识加载操作已经开始
            isLoading = true;
            if (LoadItemsStarted != null)
            {
                LoadItemsStarted(this, EventArgs.Empty);
            }
            // 用于统计已成功加载的项目数量
            uint hadLoaded = 0;
            for (uint i = 0; i < n; i++)
            {
                await Task.Delay(200);
                double val = rand.NextDouble();
                this.Add(val);
                hadLoaded++;
            }
            // 标识加载操作完成
            isLoading = false;
            if (LoadItemsCompleted != null)
            {
                LoadItemsCompleted(this, EventArgs.Empty);
            }
            return new LoadMoreItemsResult { Count = hadLoaded };
        }
        #endregion

        #region 事件
        /// <summary>
        /// 当开始加载操作时发生
        /// </summary>
        public event EventHandler LoadItemsStarted;
        /// <summary>
        /// 当加载操作完成时发生
        /// </summary>
        public event EventHandler LoadItemsCompleted;
        #endregion
    }
}
