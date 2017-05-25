namespace Yfy.Api.Exceptions
{
    /// <summary>
    /// refresh token过期
    /// </summary>
    public class RefreshTokenExpiredException : System.Exception
    {
        /// <summary>
        /// 引发该异常时的refreshtoken值
        /// </summary>
        public string RefreshToken { get; private set; }

        internal RefreshTokenExpiredException(string refreshToken)
        {
            this.RefreshToken = refreshToken;
        }

        /// <summary>
        /// 返回此异常的通用错误信息
        /// </summary>
        /// <returns>
        /// 通用错误信息的字符串
        /// </returns>
        public override string ToString()
        {
            return $"RefreshTokenExpiredException! RefreshToken = {this.RefreshToken}.";
        }
    }
}
