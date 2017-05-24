namespace Yfy.Api
{
    /// <summary>
    /// 如果需要在刷新AccessToken和RefreshToken的时候记录，需要实现该接口
    /// </summary>
    public interface IYfyTokenRegister
    {
        /// <summary>
        /// 刷新AccessToken时的回调
        /// </summary>
        /// <param name="accessToken">更新成功的AssessToken</param>
        void RegisterAccessToken(string accessToken);
    }
}
