namespace Yfy.Api.Oauth
{
    using Org.BouncyCastle.OpenSsl;

    /// <summary>
    /// RSA 密码容器
    /// </summary>
    public class RSAPasswdFinder : IPasswordFinder
    {
        /// <summary>
        /// RSA 密钥
        /// </summary>
        public string Passwd { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="passwd">RSA私钥密码</param>
        public RSAPasswdFinder(string passwd)
        {
            this.Passwd = passwd;
        }

        /// <summary>
        /// 获得密码的字符数组
        /// </summary>
        /// <returns>密码字符数组</returns>
        public char[] GetPassword()
        {
            return this.Passwd.ToCharArray();
        }
    }
}
