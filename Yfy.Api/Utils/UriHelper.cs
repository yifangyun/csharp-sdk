namespace Yfy.Api
{
    using System;
    using Yfy.Api.Items;

    internal static class UriHelper
    {
        public static string ApiHost = "https://open.fangcloud.com/";

        public static string OAuthHost = "https://oauth.fangcloud.com/";

        #region OAuth

        public static Uri GetOAuthAuthorizeUri(string clientId, string redirectUri, string state)
        {
            return new Uri(
                OAuthHost +
                $"oauth/authorize?response_type=code&client_id={clientId}&redirect_uri={redirectUri}&state={state}");
        }

        public static Uri GetOAuthTokenUri(string code, string redirectUri)
        {
            return new Uri(
                OAuthHost + $"oauth/token?grant_type=authorization_code&code={code}&redirect_uri={redirectUri}");
        }

        public static Uri GetOAuthTokenJwtUri(string assertion)
        {
            return new Uri(
                OAuthHost + $"oauth/token?grant_type=jwt&assertion={assertion}");
        }

        public static Uri GetRefreshTokenUri(string refreshToken)
        {
            return new Uri(OAuthHost + $"oauth/token?grant_type=refresh_token&refresh_token={refreshToken}");
        }

        public static Uri GetRevokeTokenUri(string refreshToken)
        {
            return new Uri(OAuthHost + $"oauth/token/revoke?token={refreshToken}");
        }

        public static Uri GetOAuthPasswordUri(string userName, string password)
        {
            return new Uri(OAuthHost + $"oauth/token?grant_type=password&username={userName}&password={password}");
        }

#endregion

        #region user api list

        public static Uri GetUserInfoUri(long userId = 0)
        {
            return Convert.ToBoolean(userId) 
                ? new Uri(ApiHost + $"api/v2/user/{userId}/info")
                : new Uri(ApiHost + "api/v2/user/info");
        }

        public static Uri GetUserSpaceUsageUri()
        {
            return new Uri(ApiHost + "api/v2/user/space_usage");
        }

        public static Uri UpdateUserInfoUri()
        {
            return new Uri(ApiHost + "api/v2/user/update");
        }

        public static Uri GetProfilePicDownloadUri(string profilePicKey, long userId)
        {
            return new Uri(ApiHost + $"api/v2/user/{userId}/profile_pic_download?profile_pic_key={profilePicKey}");
        }

        public static Uri GetUserSearch(string queryWords = "", int pageId = 0)
        {
            var queryString = $"?page_id={pageId}";

            if (queryWords != "")
            {
                queryString += $"&query_words={queryWords}";
            }
            return new Uri(ApiHost + "api/v2/user/search" + queryString);
        }

#endregion

        #region common api list

        public static Uri SearchUri(string queryWords, long searchInFolder = 0, ItemType type = ItemType.all, int pageId = 0, QueryFilter queryFilter = QueryFilter.all, DateTime? begin = null, DateTime? end = null)
        {
            string datefilter = "";
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            if (begin != null)
            {
                datefilter += (int)((DateTime)begin - startTime).TotalSeconds;
            }
            datefilter += ",";
            if (end != null)
            {
                datefilter += (int)((DateTime)end - startTime).TotalSeconds;
            }
            return new Uri(ApiHost + $"api/v2/item/search?query_words={queryWords}&search_in_folder={searchInFolder}&type={type}&page_id={pageId}&query_filter={queryFilter}&updated_time_range={datefilter}");
        }

#endregion

        #region file api list

        public static Uri GetFileInfoUri(long id)
        {
            return new Uri(ApiHost + $"api/v2/file/{id}/info");
        }

        public static Uri UpdateFileInfoUri(long id)
        {
            return new Uri(ApiHost + $"api/v2/file/{id}/update");
        }

        public static Uri TrashFileUri(long id)
        {
            return new Uri(ApiHost + $"api/v2/file/{id}/delete");
        }

        public static Uri DeleteFileUri(long id)
        {
            return new Uri(ApiHost + $"api/v2/file/{id}/delete_from_trash");
        }

        public static Uri RestoreFileUri(long id)
        {
            return new Uri(ApiHost + $"api/v2/file/{id}/restore_from_trash");
        }

        public static Uri MoveFileUri(long id)
        {
            return new Uri(ApiHost + $"api/v2/file/{id}/move");
        }

        public static Uri UploadNewFileUri()
        {
            return new Uri(ApiHost + "api/v2/file/upload");
        }

        public static Uri UploadNewFileVersionUri(long id)
        {
            return new Uri(ApiHost + $"api/v2/file/{id}/new_version");
        }

        public static Uri DownloadFileUri(long id, int version = 0)
        {
            return new Uri(ApiHost + $"api/v2/file/{id}/download" + (
                    Convert.ToBoolean(version) ? $"?version={version}" : "")
                );
        }

        public static Uri CopyFileUri(long id)
        {
            return new Uri(ApiHost + $"api/v2/file/{id}/copy");
        }

        public static Uri PreSignatureUploadUri(long id)
        {
            return new Uri(ApiHost + $"api/v2/file/{id}/new_version");
        }

        public static Uri GetFileShareLinksUri(long id, long pageId = 0, long ownerId = 0)
        {
            string query = "";
            if (pageId != 0)
            {
                query += $"page_id={pageId}";
            }
            
            if (ownerId != 0)
            {
                query += $"&owner_id={ownerId}";
            }

            if (query != "")
            {
                query = "?" + query.TrimStart(new char[] { '&' });
            }
            return new Uri(ApiHost + $"api/v2/file/{id}/share_links{query}");
        }

        public static Uri GetFileCommentsUri(long fileId)
        {
            return new Uri(ApiHost + $"api/v2/file/{fileId}/comments");
        }

        public static Uri GetPreviewFileUri(long fileId)
        {
            return new Uri(ApiHost + $"api/file/{fileId}/preview");
        }

#endregion

        #region folder api list

        public static Uri GetFolderInfoUri(long id)
        {
            return new Uri(ApiHost + $"api/v2/folder/{id}/info");
        }

        public static Uri CreateFolderUri()
        {
            return new Uri(ApiHost + "api/v2/folder/create");
        }

        public static Uri UpdateFolderInfoUri(long id)
        {
            return new Uri(ApiHost + $"api/v2/folder/{id}/update");
        }

        public static Uri TrashFolderUri(long id)
        {
            return new Uri(ApiHost + $"api/v2/folder/{id}/delete");
        }

        public static Uri DeleteFolderUri(long id)
        {
            return new Uri(ApiHost + $"api/v2/folder/{id}/delete_from_trash");
        }

        public static Uri RestoreFolderUri(long id)
        {
            return new Uri(ApiHost + $"api/v2/folder/{id}/restore_from_trash");
        }

        public static Uri MoveFolderUri(long id)
        {
            return new Uri(ApiHost + $"api/v2/folder/{id}/move");
        }

        public static Uri GetDirectChildrenUri(long id, int pageId = 1, int pageCapacity = 20, ItemType type = ItemType.all)
        {
            return new Uri(
                ApiHost +
                $"api/v2/folder/{id}/children?folder_id={id}&page_id={pageId}&page_capacity={pageCapacity}&type={type}");
        }

        public static Uri GetFolderShareLinksUri(long id, long ownerId, int pageId = 0)
        {
            string query = "";
            if (pageId != 0)
            {
                query += $"page_id={pageId}";
            }

            if (ownerId != 0)
            {
                query += $"&owner_id={ownerId}";
            }

            if (query != "")
            {
                query = "?" + query.TrimStart(new char[] { '&' });
            }
            return new Uri(ApiHost + $"api/v2/folder/{id}/share_links{query}");
        }

        public static Uri GetFolderCollabsUri(long folderId)
        {
            return new Uri(ApiHost + $"api/v2/folder/{folderId}/collabs");
        }

        #endregion

        #region trash api list

        public static Uri ClearTrashUri()
        {
            return new Uri(ApiHost + "api/v2/trash/clear");
        }

        public static Uri RestoreAllFromTrashUri()
        {
            return new Uri(ApiHost + "api/v2/trash/restore_all");
        }

        #endregion

        #region share_link api list

        public static Uri GetShareLinkInfoUri(string uniqueName)
        {
            return new Uri(ApiHost + $"api/v2/share_link/{uniqueName}/info");
        }

        public static Uri CreateShareLinkUri()
        {
            return new Uri(ApiHost + "api/v2/share_link/create");
        }

        public static Uri UpdateShareLinkUri(string uniqueName)
        {
            return new Uri(ApiHost + $"api/v2/share_link/{uniqueName}/update");
        }

        public static Uri RevokeShareLinkUri(string uniqueName)
        {
            return new Uri(ApiHost + $"api/v2/share_link/{uniqueName}/revoke");
        }

        #endregion

        #region comment api list

        public static Uri CreateCommentUri()
        {
            return new Uri(ApiHost + "api/v2/comment/create");
        }

        public static Uri DeleteCommentUri(long commentId)
        {
            return new Uri(ApiHost + $"api/v2/comment/{commentId}/delete");
        }

        #endregion

        #region collab api list

        public static Uri InviteCollabUri()
        {
            return new Uri(ApiHost + "api/v2/collab/invite");
        }

        public static Uri GetCollabInfoUri(long collabId)
        {
            return new Uri(ApiHost + $"api/v2/collab/{collabId}/info");
        }

        public static Uri UpdateCollabUri(long collabId)
        {
            return new Uri(ApiHost + $"api/v2/collab/{collabId}/update");
        }

        public static Uri DeleteCollabUri(long collabId)
        {
            return new Uri(ApiHost + $"api/v2/collab/{collabId}/delete");
        }

        #endregion

        #region department api list

        public static Uri GetAdminDepartmentUsersUri(long deptId, string queryWords = "", int pageId = 0)
        {
            var queryString = $"?page_id={pageId}";

            if (queryWords != "")
            {
                queryString += $"&query_words={queryWords}";
            }
            return new Uri(ApiHost + $"api/v2/admin/department/{deptId}/users" + queryString);
        }

        #endregion
    }
}
