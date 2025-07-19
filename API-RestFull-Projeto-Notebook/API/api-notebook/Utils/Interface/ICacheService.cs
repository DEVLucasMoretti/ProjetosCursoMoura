using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Interface
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, int cacheSeconds);
        /// <summary>
        /// Adcionar no cache com tempo de expiração em 30 segundos
        /// </summary>
        /// <typeparam name="T">Tipo do objeto do cache</typeparam>
        /// <param name="key">chave do cache</param>
        /// <param name="objeto">objeto do cache</param>
        void Set<T>(string key, T value);
        void Remove(string key);
    }
}
