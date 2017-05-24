namespace Yfy.Api
{
    using System.Collections.Generic;

    /// <summary>
    /// 亿方云C#SDK客户端工厂类，帮助管理多个客户端
    /// </summary>
    /// <typeparam name="Tkey">工厂类中字典的索引类型</typeparam>
    public class YfyClientFactory<Tkey>
    {
        /// <summary>
        /// 工厂中最多可管理的客户端总数
        /// </summary>
        public int MaxCapacity { get; set; }

        /// <summary>
        /// 工厂中客户端的Http连接配置
        /// </summary>
        public YfyClientHttpConfig HttpConfig { get; set; }

        internal Dictionary<Tkey, LinkedListNode<LRUItems<Tkey, YfyClient>>> YfyClients { get; set; }

        internal LinkedList<LRUItems<Tkey, YfyClient>> LRUList { get; set; }

        private readonly object _locker = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxCapacity">工厂中最多可管理的客户端总数</param>
        /// <param name="httpConfig">工厂中客户端的Http连接配置</param>
        public YfyClientFactory(int maxCapacity, YfyClientHttpConfig httpConfig)
        {
            this.MaxCapacity = maxCapacity;
            this.HttpConfig = httpConfig;
            this.YfyClients = new Dictionary<Tkey, LinkedListNode<LRUItems<Tkey, YfyClient>>>(2 * maxCapacity);
            this.LRUList = new LinkedList<LRUItems<Tkey, YfyClient>>();
        }

        /// <summary>
        /// 增加一个客户端
        /// </summary>
        /// <param name="T">客户端索引类型</param>
        /// <param name="client">待增加的客户端</param>
        public void AddClient(Tkey T, YfyClient client)
        {
            var items = new LRUItems<Tkey, YfyClient>(T, client);
            var node = new LinkedListNode<LRUItems<Tkey, YfyClient>>(items);

            lock (_locker)
            {
                if (LRUList.Count >= MaxCapacity)
                {
                    this.RemoveFirst();
                }
                LRUList.AddLast(node);
                YfyClients.Add(T, node);
            }
        }

        /// <summary>
        /// 增加一个客户端
        /// </summary>
        /// <param name="T">客户端索引类型</param>
        /// <param name="accessToken">客户端的AccessToken</param>
        /// <param name="refreshToken">客户端的RefreshToken</param>
        public void AddClient(Tkey T, string accessToken, string refreshToken = "")
        {
            YfyClient client;
            if (refreshToken == "")
            {
                client = new YfyClient(new YfyClientConfig(accessToken, this.HttpConfig));
            }
            else
            {
                client = new YfyClient(new YfyClientConfig(accessToken, refreshToken, this.HttpConfig));
            }

            this.AddClient(T, client);
        }

        /// <summary>
        /// 根据给出的key，尝试得到相应的客户端
        /// </summary>
        /// <param name="T">客户端索引类型</param>
        /// <param name="client">得到的客户端</param>
        /// <returns>是否成功</returns>
        public bool TryGetClient(Tkey T, out YfyClient client)
        {
            LinkedListNode<LRUItems<Tkey, YfyClient>> node;

            var result = YfyClients.TryGetValue(T, out node);
            if (result)
            {
                lock (_locker)
                {
                    //LRU cache
                    LRUList.Remove(node);
                    LRUList.AddLast(node);
                }
                client = node.Value.Value;
            }
            else
            {
                client = null;
            }
            return result;
        }

        /// <summary>
        /// 从工厂中删除最近使用最少的客户端
        /// </summary>
        public void RemoveFirst()
        {
            var first = LRUList.First;
            lock (_locker)
            {
                YfyClients.Remove(first.Value.Key);
                LRUList.RemoveFirst();
            }
        }

        internal class LRUItems<K, V>
        {
            public K Key { get; set; }
            public V Value { get; set; }

            public LRUItems(K k, V v)
            {
                this.Key = k;
                this.Value = v;
            }
        }
    }
}
