namespace Yfy.Api.Comment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Yfy.Api.Exceptions;

    /// <summary>
    /// 评论相关api列表
    /// </summary>
    public class CommentRouter
    {
        private ITransport _transport;

        internal CommentRouter(ITransport transport)
        {
            this._transport = transport;
        }

        /// <summary>
        /// 创建评论
        /// </summary>
        /// <param name="fileId">评论文件id</param>
        /// <param name="content">评论文本，长度不能超过1001个字符</param>
        /// <returns>通用评论对象</returns>
        public YfyComment Create(long fileId, string content)
        {
            var requestArg = new CreateCommentArg(fileId, content);
            return this._transport.SendRpcRequest<CreateCommentArg, YfyComment>(requestArg, UriHelper.CreateCommentUri());
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="commentId">评论id</param>
        /// <returns>是否成功</returns>
        public bool Delete(long commentId)
        {
            return this._transport.SendRpcRequest<EmptyPostArg, YfySuccess>(new EmptyPostArg(), UriHelper.DeleteCommentUri(commentId)).Success;
        }
    }
}
