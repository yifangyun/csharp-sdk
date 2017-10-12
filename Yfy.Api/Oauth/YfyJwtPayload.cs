namespace Yfy.Api.Oauth
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// 亿方云 Jwt Payload 对象
    /// </summary>
    public class YfyJwtPayload
    {
        /// <summary>
        /// 用于生成以及校验签名的算法，只支持RS256, RS384和RS512
        /// </summary>
        public JwtAlgorithms Alg { get; set; }

        /// <summary>
        /// 公钥的唯一标识
        /// </summary>
        public string Kid { get; set; }

        /// <summary>
        /// 授权对象类型，只能是enterprise或者user
        /// </summary>
        public YfySubType SubType { get; set; }

        /// <summary>
        /// 授权对象id，必须是数字
        /// </summary>
        public string Sub { get; set; }

        /// <summary>
        /// 过期时间戳，不能超过iat(若iat不存在则记请求到达服务器时间)60s
        /// </summary>
        public int Exp { get; set; }

        /// <summary>
        /// jwt生成时的时间戳
        /// </summary>
        public int Iat { get; set; }

        /// <summary>
        /// jwt的唯一标识，每个jwt应当都有唯一的jti
        /// </summary>
        public string Jti { get; set; }

        /// <summary>
        /// JwtPayload构造函数
        /// </summary>
        /// <param name="subType"></param>
        /// <param name="kid"></param>
        /// <param name="sub"></param>
        /// <param name="alg"></param>
        public YfyJwtPayload(YfySubType subType, string kid, long sub, JwtAlgorithms alg = JwtAlgorithms.RS256)
        {
            this.SubType = subType;
            this.Kid = kid;
            this.Sub = Convert.ToString(sub);
            this.Alg = alg;
            var begin = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            this.Iat = Convert.ToInt32((TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Now) - begin).TotalSeconds);
            this.Exp = this.Iat + 60;
            this.Jti = InitJti();
        }

        private string InitJti()
        {
            return BitConverter.ToString(
                MD5.Create().ComputeHash(
                    Encoding.UTF8.GetBytes(DateTime.Now.ToString()))
            ).Replace("-", string.Empty).ToLower();
        }
    }

    /// <summary>
    /// 授权对象类型 枚举
    /// </summary>
    public enum YfySubType
    {
        /// <summary>
        /// 授权范围 企业
        /// </summary>
        Enterprise,

        /// <summary>
        /// 授权范围 用户
        /// </summary>
        User
    }

    /// <summary>
    /// 支持的jwt算法枚举
    /// </summary>
    public enum JwtAlgorithms
    {
        /// <summary>
        /// RS256
        /// </summary>
        RS256,

        /// <summary>
        /// RS384
        /// </summary>
        RS384,

        /// <summary>
        /// RS512
        /// </summary>
        RS512
    }
}
