using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Memcached.ClientLibrary;
using System.Web;
using System.Web.Caching;

namespace ZGD.Common
{
    /*
     * Project：缓存操作类
     * Author：LJ
     * Data：2011-11-18
     * Updated：
     * Remark：对HttpRuntime.Cache的各种操作类
     */
    public class CacheManager
    {
        Cache _cache = null;
        public CacheManager()
        {
            _cache =HttpRuntime.Cache;
        }

        /// <summary>
        /// 清楚缓存
        /// </summary>
        /// <param name="cacheName">缓存名</param>
        public void RemoveCache(string cacheName)
        {
            lock (_cache)
            {
                _cache.Remove(cacheName);
            }
        }

        /// <summary>
        /// 是否 存在缓存
        /// </summary>
        /// <param name="cacheName">缓存名</param>
        /// <returns>bool 是否存在</returns>
        public bool CheckCache(string cacheName)
        {
            if (_cache[cacheName] != null) 
                return true;
            return false;
        }

        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheName">缓存名</param>
        /// <returns>缓存值</returns>
        public object ReadCache(string cacheName)
        {
            return _cache.Get(cacheName);
        }

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="cacheName">缓存名</param>
        /// <param name="cacheValue">缓存值</param>
        /// <param name="minutes">缓存时间(分钟)</param>
        public void WriteCache(string cacheName, object  cacheValue,int minutes)
        {
            _cache.Insert(cacheName, cacheValue, null, DateTime.Now.AddMinutes(minutes),TimeSpan.Zero);
        }

        //MemcachedClient _mc;
        //public CacheManager(string PoolName)
        //{
        //    _mc = new MemcachedClient();
        //    _mc.PoolName = PoolName;
        //}

        //public bool RemoveCache(string cacheName)
        //{
        //    return _mc.Delete(cacheName);
        //}
    
        ///// <summary>
        ///// 查看缓存是否存在
        ///// </summary>
        ///// <param name="cacheName"></param>
        ///// <returns></returns>
        //public bool CheckCache(string cacheName)
        //{
        //    return _mc.KeyExists(cacheName);
        //}

        ///// <summary>
        ///// 读取缓存
        ///// </summary>
        ///// <param name="cacheName"></param>
        ///// <returns></returns>
        //public object ReadCache(string cacheName)
        //{
        //    return _mc.Get(cacheName);
        //}

        ///// <summary>
        ///// 添加缓存
        ///// </summary>
        ///// <param name="cacheName"></param>
        ///// <param name="cacheValue"></param>
        ///// <param name="minutes"></param>
        //public void WriteCache(string cacheName, object  cacheValue,int minutes)
        //{
        //    _mc.Add(cacheName, cacheValue, DateTime.Now.AddMinutes(minutes));
        //}
    }
}
