namespace Yfy.Api.Items
{
    /// <summary>
    /// 文件（文件夹）通用api
    /// </summary>
    public class ItemRouter
    {
        private readonly ITransport _transport;

        internal ItemRouter(ITransport transport)
        {
            this._transport = transport;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="queryWords">搜索关键词</param>
        /// <param name="searchInFolder">指定父文件夹，匹配的结果都是该文件夹的子文件或子文件夹</param>
        /// <param name="type">搜索类型，分为file，folder，all三种，默认为all</param>
        /// <param name="pageNumber">第几页，每页默认20，默认为第0页</param>
        /// <param name="queryFilter">搜索过滤类型，分为file_name, content, all三种，默认为all</param>
        /// <returns>通用文件（文件夹）集合</returns>
        public YfyItemCollection Search(string queryWords, long searchInFolder = 0, ItemType type = ItemType.all, int pageNumber = 0, QueryFilter queryFilter = QueryFilter.all)
        {
            return this._transport.SendRpcRequest<GetArg, YfyItemCollection>(new GetArg(), UriHelper.SearchUri(queryWords, searchInFolder, type, pageNumber, queryFilter));
        }
    }
}
