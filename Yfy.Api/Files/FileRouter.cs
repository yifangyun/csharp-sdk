namespace Yfy.Api.Files
{
    using System;
    using System.IO;
    using Yfy.Api.ShareLink;
    using Yfy.Api.Comment;
    using Yfy.Api.Exceptions;

    /// <summary>
    /// 文件相关api列表
    /// </summary>
    public class FileRouter
    {
        private readonly ITransport _transport;

        internal FileRouter(ITransport transport)
        {
            this._transport = transport;
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns>通用文件对象</returns>
        public YfyFile Info(long fileId)
        {
            return this._transport.SendRpcRequest<GetArg, YfyFile>(new GetArg(), UriHelper.GetFileInfoUri(fileId));
        }

        /// <summary>
        /// 更新文件信息
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <param name="name">文件名，文件名称必须是1到222个字符，并且不能含有/ ? : * \ " > \ </param>
        /// <param name="description">文件描述，长度必须小于等于140个字符</param>
        /// <returns>通用文件对象</returns>
        public YfyFile Update(long fileId, string name, string description = null)
        {
            var requestArg = new UpdateFileArg(name, description);
            return this._transport.SendRpcRequest<UpdateFileArg, YfyFile>(requestArg, UriHelper.UpdateFileInfoUri(fileId));
        }

        /// <summary>
        /// 删除文件至回收站
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns>是否成功</returns>
        public bool Delete(long fileId)
        {
            this._transport.SendRpcRequest<EmptyPostArg, YfySuccess>(new EmptyPostArg(), UriHelper.TrashFileUri(fileId));
            return true;
        }

        /// <summary>
        /// 从回收站删除文件
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns>是否成功</returns>
        public bool DeleteFromTrash(long fileId)
        {
            this._transport.SendRpcRequest<EmptyPostArg, YfySuccess>(new EmptyPostArg(), UriHelper.DeleteFileUri(fileId));
            return true;
        }

        /// <summary>
        /// 从回收站恢复文件
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns>是否成功</returns>
        public bool RestoreFromTrash(long fileId)
        {
            this._transport.SendRpcRequest<EmptyPostArg, YfySuccess>(new EmptyPostArg(), UriHelper.RestoreFileUri(fileId));
            return true;
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <param name="targetFolderId">目标文件夹id</param>
        /// <returns>是否成功</returns>
        public bool Move(long fileId, long targetFolderId)
        {
            var requestArg = new MoveFileArg(targetFolderId);
            this._transport.SendRpcRequest<MoveFileArg, YfySuccess>(requestArg, UriHelper.MoveFileUri(fileId));
            return true;
        }

        /// <summary>
        /// 获取新文件上传地址
        /// </summary>
        /// <param name="parentId">上传至的文件夹id</param>
        /// <param name="name">文件名称，文件名称必须是1到222个字符，并且不能含有/ ? : * \" > \ </param>
        /// <param name="strategy">文件名冲突时的处理策略</param>
        /// <returns>上传链接，接下来往该链接上传即可上传文件，上传链接的有效时间为1个小时，且只能被使用一次</returns>
        public string GetUploadUrl(long parentId, string name, UploadStrategy strategy = UploadStrategy.Rename)
        {
            var requestArg = new UploadFileArg(parentId, name, strategy);
            return this._transport.SendRpcRequest<UploadFileArg, PreSignatureUploadUrl>(requestArg, UriHelper.UploadNewFileUri()).PresignUrl;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="parentId">上传至的文件夹id</param>
        /// <param name="name">文件名称，文件名称必须是1到222个字符，并且不能含有/ ? : * \" > \</param>
        /// <param name="stream">待上传的文件流</param>
        /// <param name="strategy">文件名冲突时的处理策略</param>
        /// <returns>通用文件对象</returns>
        public YfyFile Upload(long parentId, string name, Stream stream, UploadStrategy strategy = UploadStrategy.Rename)
        {
            var uploadUrl = GetUploadUrl(parentId, name, strategy);
            return this._transport.SendUploadRequest<string, YfyFile>(name, new Uri(uploadUrl), stream);
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="parentId">上传至的文件夹id</param>
        /// <param name="name">文件名称，文件名称必须是1到222个字符，并且不能含有/ ? : * \" > \</param>
        /// <param name="uploadFilePath">待上传的文件本地路径</param>
        /// <param name="strategy">文件名冲突时的处理策略</param>
        /// <returns>通用文件对象</returns>
        public YfyFile Upload(long parentId, string name, string uploadFilePath, UploadStrategy strategy = UploadStrategy.Rename)
        {
            var stream = File.OpenRead(uploadFilePath);
            var file = this.Upload(parentId, name, stream, strategy);
            stream.Close();
            return file;
        }

        /// <summary>
        /// 获取文件上传新版本地址
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <param name="name">新版本文件名称，文件名称必须是1到222个字符，并且不能含有\ / ? : * \" > \ </param>
        /// <param name="remark">上传新版本的备注。</param>
        /// <returns>上传链接，接下来往该链接上传即可上传文件，上传链接的有效时间为1个小时，且只能被使用一次</returns>
        public string GetUploadNewVersionUrl(long fileId, string name, string remark = null)
        {
            var requestArg = new UploadFileNewVersionArg(name, remark);
            return this._transport.SendRpcRequest<UploadFileNewVersionArg, PreSignatureUploadUrl>(requestArg, UriHelper.UploadNewFileVersionUri(fileId)).PresignUrl;
        }

        /// <summary>
        /// Upload file new version using stream
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <param name="name">新版本文件名称，文件名称必须是1到222个字符，并且不能含有\ / ? : * \" > \</param>
        /// <param name="remark">上传新版本的备注。</param>
        /// <param name="stream">待上传的文件流</param>
        /// <returns>通用文件对象</returns>
        public YfyFile UploadNewVersion(long fileId, string name, Stream stream, string remark = null)
        {
            var uploadUrl = GetUploadNewVersionUrl(fileId, name, remark);
            return this._transport.SendUploadRequest<string, YfyFile>(name, new Uri(uploadUrl), stream);
        }

        /// <summary>
        /// Upload file new version using stream
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <param name="name">新版本文件名称，文件名称必须是1到222个字符，并且不能含有\ / ? : * \" > \</param>
        /// <param name="remark">上传新版本的备注。</param>
        /// <param name="newVersionFilePath">待上传的文件本地路径</param>
        /// <returns>通用文件对象</returns>
        public YfyFile UploadNewVersion(long fileId, string name, string newVersionFilePath, string remark = null)
        {
            var uploadUrl = GetUploadNewVersionUrl(fileId, name, remark);
            var stream = File.OpenRead(newVersionFilePath);
            return this._transport.SendUploadRequest<string, YfyFile>(name, new Uri(uploadUrl), stream);
        }

        /// <summary>
        /// 获取文件的下载地址
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <param name="version">下载版本号</param>
        /// <returns>下载链接，访问该下载链接即可下载文件，下载链接的有效时间为1个小时，且只能被使用一次</returns>
        public string GetDownloadUrl(long fileId, int version = 0)
        {
            return this._transport.SendRpcRequest<GetArg, DownloadFileUrl>(new GetArg(), UriHelper.DownloadFileUri(fileId, version)).DownloadUrl;
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <param name="saveFilePath">本地保存的文件路径</param>
        /// <param name="version">下载版本号</param>
        /// <param name="checkSha1">是否检查文件sha1值。如果sha1值和服务器不一致，会删除已经下载的文件，返回false</param>
        /// <returns>是否成功</returns>
        public bool Download(long fileId, string saveFilePath, int version = 0, bool checkSha1 = false)
        {
            var downloadUrl = GetDownloadUrl(fileId, version);
            this._transport.SendDownloadRequest(saveFilePath, new Uri(downloadUrl));
            if (checkSha1)
            {
                var fileInfo = this.Info(fileId);
                if (Sha1Helper.ComputeSha1(saveFilePath) != fileInfo.Sha1)
                {
                    File.Delete(saveFilePath);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <param name="targetFolderId">拷贝至的文件夹id</param>
        /// <returns>通用文件对象</returns>
        public YfyFile Copy(long fileId, long targetFolderId)
        {
            var requestArg = new CopyFileArg(targetFolderId);
            return this._transport.SendRpcRequest<CopyFileArg, YfyFile>(requestArg, UriHelper.CopyFileUri(fileId));
        }

        /// <summary>
        /// 获取文件的分享链接列表
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <param name="ownerId">分享链接创建者id</param>
        /// <param name="pageId">页码</param>
        /// <returns>通用分享链接集合</returns>
        public YfyShareLinkCollection ShareLinks(long fileId, long ownerId = 0, int pageId = 0)
        {
            return this._transport.SendRpcRequest<GetArg, YfyShareLinkCollection>(new GetArg(), UriHelper.GetFileShareLinksUri(fileId, ownerId, pageId));
        }

        /// <summary>
        /// 获取文件的评论列表
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns>通用评论集合</returns>
        public YfyCommentCollection Comments(long fileId)
        {
            return this._transport.SendRpcRequest<GetArg, YfyCommentCollection>(new GetArg(), UriHelper.GetFileCommentsUri(fileId));
        }

    }
}
