using gameslist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace gameslist.HTTP
{
    /// <summary>
    /// 接口服务类
    /// </summary>
    class gamesService
    {
        GetInfoHelper<games> getgamesInfo = new GetInfoHelper<games>();        
        GetInfoHelper<string> getStringInfo = new GetInfoHelper<string>();        
        /// <summary>
        /// 随机查询律师
        /// </summary>
        static string _url_SearchgamesByRand = "http://localhost:8080/20161228/list.php?format={0}&page={1}&pagesize={2}";
        /// <summary>
        /// 随机查询律师
        /// </summary>
        /// <param name="format"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<Info<games>> SearchgamesByRand(string format = "json", int page = 1, int count = 10)
        {
            try
            {
                Info<games> info = await getgamesInfo.GetResultInfoGetHelper<Info<games>>(_url_SearchgamesByRand, format, page, count);
                return info;
            }
            catch (Exception ex)
            {
                return null;
                throw (ex);
            }
        }
    }
}
