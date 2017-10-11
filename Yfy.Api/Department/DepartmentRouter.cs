
namespace Yfy.Api.Department
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Yfy.Api.Users;

    /// <summary>
    /// 部门相关api列表
    /// </summary>
    public class DepartmentRouter
    {
        private readonly ITransport _transport;

        internal DepartmentRouter(ITransport transport)
        {
            this._transport = transport;
        }

        /// <summary>
        /// 获取部门成员详细列表
        /// </summary>
        /// <param name="deptId">部门id</param>
        /// <param name="queryWords">部门成员搜索关键字</param>
        /// <param name="pageId">页码</param>
        /// <returns>通用用户集合</returns>
        public YfyUserCollection GetDepartmentUsers(long deptId, string queryWords = "", int pageId = 0)
        {
            return this._transport.SendRpcRequest<GetArg, YfyUserCollection>(new GetArg(), UriHelper.GetAdminDepartmentUsersUri(deptId, queryWords, pageId));
        }
    }
}
