namespace Yfy.Api.Users
{
    /// <summary>
    /// 用户相关api列表
    /// </summary>
    public class UserRouter
    {
        private readonly ITransport _transport;

        internal UserRouter(ITransport transport)
        {
            this._transport = transport;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>通用用户对象</returns>
        public YfyUser Info(long userId = 0)
        {
            return this._transport.SendRpcRequest<GetArg, YfyUser>(new GetArg(), UriHelper.GetUserInfoUri(userId));
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="name">用户名字</param>
        /// <returns>通用用户对象</returns>
        public YfyUser Update(string name)
        {
            var requestArg = new UpdateUserArg(name);
            return this._transport.SendRpcRequest<UpdateUserArg, YfyUser>(requestArg, UriHelper.UpdateUserInfoUri());
        }

        /// <summary>
        /// 本企业用户搜索
        /// </summary>
        /// <param name="queryWords">查询关键字</param>
        /// <param name="pageId">页码</param>
        /// <returns>通用用户集合</returns>
        public YfyUserCollection Search(string queryWords = "", int pageId = 0)
        {
            return this._transport.SendRpcRequest<GetArg, YfyUserCollection>(new GetArg(), UriHelper.GetUserSearch(queryWords, pageId));
        }

        /// <summary>
        /// 下载用户头像
        /// </summary>
        /// <param name="savePath">头像保存路径</param>
        /// <param name="profilePicKey">下载头像所需的key</param>
        /// <param name="userId">用户id</param>
        /// <returns>是否成功</returns>
        public bool ProfilePicDownload(string savePath, string profilePicKey, long userId = 0)
        {
            this._transport.SendDownloadRequest(savePath, UriHelper.GetProfilePicDownloadUri(profilePicKey, userId));
            return true;
        }
        
    }
}
