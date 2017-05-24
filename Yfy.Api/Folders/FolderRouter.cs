namespace Yfy.Api.Folders
{
    using Yfy.Api.ShareLink;
    using Yfy.Api.Collab;
    using Yfy.Api.Exception;
    using Yfy.Api.Items;

    /// <summary>
    /// 文件夹api列表
    /// </summary>
    public class FolderRouter
    {
        private readonly ITransport _transport;

        internal FolderRouter(ITransport transport)
        {
            this._transport = transport;
        }

        /// <summary>
        /// 获取文件夹信息
        /// </summary>
        /// <param name="folderId">文件夹id</param>
        /// <returns>通用文件夹对象</returns>
        public YfyFolder Info(long folderId)
        {
            return this._transport.SendRpcRequest<GetArg, YfyFolder>(new GetArg(), UriHelper.GetFolderInfoUri(folderId));
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="parentId">父文件夹id</param>
        /// <param name="name">文件夹名，文件夹名称必须是1到222个字符，并且不能含有/ ? : * \“ > \ </param>
        /// <returns>通用文件夹对象</returns>
        public YfyFolder Create(long parentId, string name)
        {
            var requestArg = new CreateFolderArg(name, parentId);
            return this._transport.SendRpcRequest<CreateFolderArg, YfyFolder>(requestArg, UriHelper.CreateFolderUri());
        }

        /// <summary>
        /// 更新文件夹
        /// </summary>
        /// <param name="folderId">文件夹id</param>
        /// <param name="name">文件夹名，文件夹名称必须是1到222个字符，并且不能含有/ ? : * \“ > \ </param>
        /// <returns>通用文件夹对象</returns>
        public YfyFolder Update(long folderId, string name)
        {
            var requestArg = new UpdateFolderArg(name);
            return this._transport.SendRpcRequest<UpdateFolderArg, YfyFolder>(requestArg, UriHelper.UpdateFolderInfoUri(folderId));
        }

        /// <summary>
        /// 删除文件夹至回收站，不支持批量操作
        /// </summary>
        /// <param name="folderId">文件夹id</param>
        /// <returns>是否成功</returns>
        public bool Delete(long folderId)
        {
            this._transport.SendRpcRequest<EmptyPostArg, YfySuccess>(new EmptyPostArg(), UriHelper.TrashFolderUri(folderId));
            return true;
        }

        /// <summary>
        /// 从回收站删除文件夹
        /// </summary>
        /// <param name="folderId">文件夹id</param>
        /// <returns>是否成功</returns>
        public bool DeleteFromTrash(long folderId)
        {
            this._transport.SendRpcRequest<EmptyPostArg, YfySuccess>(new EmptyPostArg(), UriHelper.DeleteFolderUri(folderId));
            return true;
        }

        /// <summary>
        /// 从回收站恢复文件夹
        /// </summary>
        /// <param name="folderId">文件夹id</param>
        /// <returns>是否成功</returns>
        public bool RestoreFromTrash(long folderId)
        {
            this._transport.SendRpcRequest<EmptyPostArg, YfySuccess>(new EmptyPostArg(), UriHelper.RestoreFolderUri(folderId));
            return true;
        }

        /// <summary>
        /// 移动文件夹，不支持批量操作
        /// </summary>
        /// <param name="folderId">文件夹id</param>
        /// <param name="targetFolderId">目标文件夹id</param>
        /// <returns>是否成功</returns>
        public bool Move(long folderId, long targetFolderId)
        {
            var requestArg = new MoveFolderArg(targetFolderId);
            this._transport.SendRpcRequest<MoveFolderArg, YfySuccess>(requestArg, UriHelper.MoveFolderUri(folderId));
            return true;
        }

        /// <summary>
        /// 获取单层子文件和文件夹列表
        /// </summary>
        /// <param name="folderId">文件夹id</param>
        /// <param name="pageId">页号（默认0）</param>
        /// <param name="pageCapacity">页容量（默认20）</param>
        /// <param name="type">分为file，folder，all三种，默认为all</param>
        /// <returns>通用文件（和文件夹）集合</returns>
        public YfyItemCollection GetChildren(long folderId, int pageId = 0, int pageCapacity = 20, ItemType type = ItemType.all)
        {
            return this._transport.SendRpcRequest<GetArg, YfyItemCollection>(new GetArg(), UriHelper.GetDirectChildrenUri(folderId, pageId, pageCapacity, type));
        }

        /// <summary>
        /// 获取文件夹的分享链接列表
        /// </summary>
        /// <param name="folderId">文件夹id</param>
        /// <param name="ownerId">分享链接创建者id</param>
        /// <param name="pageId">页码（默认为0）</param>
        /// <returns>通用分享链接集合</returns>
        public YfyShareLinkCollection ShareLinks(long folderId, long ownerId = 0, int pageId = 0)
        {
            return this._transport.SendRpcRequest<GetArg, YfyShareLinkCollection>(new GetArg(), UriHelper.GetFolderShareLinksUri(folderId, ownerId, pageId));
        }

        /// <summary>
        /// 获取文件夹协作信息
        /// </summary>
        /// <param name="folderId">文件夹id</param>
        /// <returns>通用协作集合</returns>
        public YfyCollabCollection Collabs(long folderId)
        {
            return this._transport.SendRpcRequest<GetArg, YfyCollabCollection>(new GetArg(), UriHelper.GetFolderCollabsUri(folderId));
        }
    }
}
