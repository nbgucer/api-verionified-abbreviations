using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;

namespace ApiVerJSONAbbrDiffSample.JsonNetExtensions
{
    public class RouteJsonContractResolver : DefaultContractResolver
    {
        private readonly RouteJsonNamingStrategy _routeJsonNamingStrategy;
        private readonly Func<IMemoryCache> _memoryCacheProvider;

        public RouteJsonContractResolver(RouteJsonNamingStrategyOptions namingStrategyOptions, Func<IMemoryCache> memoryCacheProvider)
        {
            _routeJsonNamingStrategy = new RouteJsonNamingStrategy(namingStrategyOptions);
            NamingStrategy = _routeJsonNamingStrategy;
            _memoryCacheProvider = memoryCacheProvider;
        }

        public override JsonContract ResolveContract(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            string cacheKey = _routeJsonNamingStrategy.GetValidNamingStrategyFromRoute() + type.ToString();
            JsonContract contract = _memoryCacheProvider().GetOrCreate(cacheKey, (entry) =>
            {
                entry.AbsoluteExpiration = DateTimeOffset.MaxValue;
                return CreateContract(type);
            });

            return contract;
        }
    }
}
