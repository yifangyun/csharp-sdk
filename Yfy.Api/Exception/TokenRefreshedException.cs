namespace Yfy.Api.Exception
{
    /// <summary>
    /// 标记刷新token的操作
    /// </summary>
    internal class TokenRefreshedException : YfyException
    {
        public TokenRefreshedException()
            : base("")
        {
        }
    }
}
