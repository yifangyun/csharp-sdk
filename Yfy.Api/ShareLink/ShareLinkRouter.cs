namespace Yfy.Api.ShareLink
{
    using System;
    using Yfy.Api.Exception;
    using Yfy.Api.Items;

    /// <summary>
    /// 分享链接相关api列表
    /// </summary>
    public class ShareLinkRouter
    {
        private ITransport _transport;

        internal ShareLinkRouter(ITransport transport)
        {
            this._transport = transport;
        }

        /// <summary>
        /// 获取分享链接信息
        /// </summary>
        /// <param name="uniqueName">分享标识符</param>
        /// <returns>通用分享链接对象</returns>
        public YfyShareLink Info(string uniqueName)
        {
            return this._transport.SendRpcRequest<GetArg, YfyShareLink>(new GetArg(), UriHelper.GetShareLinkInfoUri(uniqueName));
        }

        /// <summary>
        /// 创建分享链接
        /// </summary>
        /// <param name="id">文件（文件夹）id</param>
        /// <param name="type">文件（文件夹）类型</param>
        /// <param name="access">权限范围</param>
        /// <param name="dueTime">到期时间(格式如:yyyy-MM-dd)</param>
        /// <param name="disableDownload">是否不允许下载(默认false)</param>
        /// <param name="passwordProtected">是否有密码(默认false)</param>
        /// <param name="password">密码</param>
        /// <returns>通用分享链接对象</returns>
        public YfyShareLink Create(long id, ItemType type, ShareLinkAccess access, DateTime dueTime, bool disableDownload = false, bool passwordProtected = false, string password = null)
        {
            var requestArg = new CreateShareLinkArg(id, type, access, dueTime, disableDownload, passwordProtected, password);
            return this._transport.SendRpcRequest<CreateShareLinkArg, YfyShareLink>(requestArg, UriHelper.CreateShareLinkUri());
        }

        /// <summary>
        /// 更新分享链接
        /// </summary>
        /// <param name="uniqueName">分享标识符</param>
        /// <param name="access">权限范围</param>
        /// <param name="dueTime">到期时间(格式如:yyyy-MM-dd)</param>
        /// <param name="disableDownload">是否不允许下载(默认false)</param>
        /// <param name="passwordProtected">是否有密码(默认false)</param>
        /// <param name="password">密码</param>
        /// <returns>通用分享链接对象</returns>
        public YfyShareLink Update(string uniqueName, ShareLinkAccess access, DateTime dueTime, bool disableDownload = false, bool passwordProtected = false, string password = null)
        {
            var requestArg = new UpdateShareLinkArg(access, dueTime, disableDownload, passwordProtected, password);
            return this._transport.SendRpcRequest<UpdateShareLinkArg, YfyShareLink>(requestArg, UriHelper.UpdateShareLinkUri(uniqueName));
        }

        /// <summary>
        /// 删除分享链接
        /// </summary>
        /// <param name="uniqueName">分享标识符</param>
        /// <returns>是否成功</returns>
        public bool Revoke(string uniqueName)
        {
            return this._transport.SendRpcRequest<EmptyPostArg, YfySuccess>(new EmptyPostArg(), UriHelper.RevokeShareLinkUri(uniqueName)).Success;
        }
    }
}
