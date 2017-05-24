namespace Yfy.Api.Trash
{
    using Yfy.Api.Exception;
    using Yfy.Api.Items;

    /// <summary>
    /// 回收站相关api列表
    /// </summary>
    public class TrashRouter
    {
        private ITransport _transport;

        internal TrashRouter(ITransport transport)
        {
            this._transport = transport;
        }

        /// <summary>
        /// 清空回收站
        /// </summary>
        /// <param name="type">item类型，分为file，folder，all三种，默认为all</param>
        /// <returns>是否成功</returns>
        public bool ClearTrash(ItemType type)
        {
            var requestArg = new ClearTrashArg(type);
            return this._transport.SendRpcRequest<ClearTrashArg, YfySuccess>(requestArg, UriHelper.ClearTrashUri()).Success;
        }

        /// <summary>
        /// 恢复回收站
        /// </summary>
        /// <param name="type">item类型，分为file，folder，all三种，默认为all</param>
        /// <returns>是否成功</returns>
        public bool RestoreAllFromTrash(ItemType type)
        {
            var requestArg = new RestoreAllFromTrashArg(type);
            return this._transport.SendRpcRequest<RestoreAllFromTrashArg, YfySuccess>(requestArg, UriHelper.RestoreAllFromTrashUri()).Success;
        }
    }
}
