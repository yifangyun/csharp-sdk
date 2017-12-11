
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
        /// 获取部门的信息
        /// </summary>
        /// <param name="deptId">部门id</param>
        /// <returns></returns>
        public YfyDepartment CommonInfo(long deptId)
        {
            return this._transport.SendRpcRequest<GetArg, YfyDepartment>(new GetArg(), UriHelper.GetDepartmentInfoUri(deptId));
        }

        /// <summary>
        /// 获取部门的子部门列表
        /// </summary>
        /// <param name="deptId">部门id</param>
        /// <param name="permissionFilter">是否过滤权限外的部门, 默认false</param>
        /// <returns></returns>
        public YfyMiniDepartmentCollection CommonChildren(long deptId, bool permissionFilter = false)
        {
            return this._transport.SendRpcRequest<GetArg, YfyMiniDepartmentCollection>(new GetArg(), UriHelper.GetDepartmentChildren(deptId, permissionFilter));
        }

        /// <summary>
        /// 获取部门成员列表
        /// </summary>
        /// <param name="deptId">部门id</param>
        /// <param name="queryWords">部门成员搜索关键字</param>
        /// <param name="pageId">页码</param>
        /// <returns></returns>
        public YfyMiniUserCollection CommonDepartmentUsers(long deptId, string queryWords = "", int pageId = 0)
        {
            return this._transport.SendRpcRequest<GetArg, YfyMiniUserCollection>(new GetArg(), UriHelper.GetDepartmentUsersUri(deptId, queryWords, pageId));
        }

    }
}
