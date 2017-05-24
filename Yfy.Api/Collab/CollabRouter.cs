namespace Yfy.Api.Collab
{
    using Yfy.Api.Exception;

    /// <summary>
    /// 协作相关api
    /// </summary>
    public class CollabRouter
    {
        private ITransport _transport;

        internal CollabRouter(ITransport transport)
        {
            this._transport = transport;
        }

        /// <summary>
        /// 邀请新协作
        /// </summary>
        /// <param name="folderId">协作文件夹id</param>
        /// <param name="invitedUserId">邀请的用户id</param>
        /// <param name="role">邀请用户角色</param>
        /// <param name="invitationMessage">邀请信息，长度不能超过140个字符</param>
        /// <returns>通用协作对象</returns>
        public YfyCollab Invite(long folderId, long invitedUserId, CollabRole role, string invitationMessage = null)
        {
            var requestArg = new InviteCollabArg(folderId, invitedUserId, role, invitationMessage);
            return this._transport.SendRpcRequest<InviteCollabArg, YfyCollab>(requestArg, UriHelper.InviteCollabUri());
        }

        /// <summary>
        /// 获取协作信息
        /// </summary>
        /// <param name="collabId">协作id</param>
        /// <returns>通用协作对象</returns>
        public YfyCollab Info(long collabId)
        {
            return this._transport.SendRpcRequest<GetArg, YfyCollab>(new GetArg(), UriHelper.GetCollabInfoUri(collabId));
        }

        /// <summary>
        /// 更新协作
        /// </summary>
        /// <param name="collabId">协作id</param>
        /// <param name="role">更新用户角色</param>
        /// <returns>通用协作对象</returns>
        public YfyCollab Update(long collabId, CollabRole role)
        {
            var requestArg = new UpdateCollabArg(role);
            return this._transport.SendRpcRequest<UpdateCollabArg, YfyCollab>(requestArg, UriHelper.UpdateCollabUri(collabId));
        }

        /// <summary>
        /// 删除协作
        /// </summary>
        /// <param name="collabId">协作id</param>
        /// <returns>是否成功</returns>
        public bool Delete(long collabId)
        {
            return this._transport.SendRpcRequest<EmptyPostArg, YfySuccess>(new EmptyPostArg(), UriHelper.DeleteCollabUri(collabId)).Success;
        }
    }
}
