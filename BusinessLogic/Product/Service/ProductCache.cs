using BusinessLogic.Product.Interface;
using Microsoft.Extensions.Caching.Memory;
using PruebaWebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Product.Service
{
    public class ProductCache : IProductCache
    {
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "ProductStatusDictionary";

        public ProductCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

            // Inicializar el caché con el diccionario por defecto
            if (!_memoryCache.TryGetValue(CacheKey, out _))
            {
                SetDefaultCache();
            }
        }

        public string GetStatusName(int status)
        {
            // Intentar obtener el diccionario desde el caché
            if (_memoryCache.TryGetValue(CacheKey, out Dictionary<int, string> statusDictionary))
            {
                return statusDictionary.TryGetValue(status, out var name) ? name : "Unknown";
            }

            // Si el caché ha expirado, volver a configurarlo
            SetDefaultCache();
            return _memoryCache.Get<Dictionary<int, string>>(CacheKey).TryGetValue(status, out var newName) ? newName : "Unknown";
        }

        private void SetDefaultCache()
        {
            var statusDictionary = new Dictionary<int, string>
        {
            { 1, "Active" },
            { 0, "Inactive" }
        };

            // Configurar el caché con una duración de 5 minutos
            _memoryCache.Set(CacheKey, statusDictionary, TimeSpan.FromMinutes(5));
        }
    }
}
