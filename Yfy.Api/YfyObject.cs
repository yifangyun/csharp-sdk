namespace Yfy.Api
{
    using Newtonsoft.Json;

    /// <summary>
    /// 亿方云通用对象基类
    /// </summary>
    abstract public class YfyObject
    {
        /// <summary>
        /// 获得对象的json序列化字符串
        /// </summary>
        /// <returns>json序列化字符串</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
