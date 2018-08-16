using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVerJSONAbbrDiffSample.JsonNetExtensions
{
    public class RouteJsonNamingStrategyOptions
    {
        public string HeaderName { get; set; }
        public Dictionary<string, Dictionary<string, string>> NamingStrategies { get; set; }
        public Dictionary<string, string> DefaultStrategy { get; set; }
        public Func<IHttpContextAccessor> HttpContextAccessorProvider { get; set; }
    }

    public class RouteJsonNamingStrategy : NamingStrategy
    {
        private readonly RouteJsonNamingStrategyOptions _options;

        public RouteJsonNamingStrategy(RouteJsonNamingStrategyOptions options)
        {
            _options = options;
        }

        public string GetValidNamingStrategyFromRoute()
        {
            var httpContext = _options.HttpContextAccessorProvider().HttpContext;
            var namingStrategyRoute = httpContext.Request.Path.ToString().Split(@"/").ElementAtOrDefault(2)?.ToLower();

            if (string.IsNullOrEmpty(namingStrategyRoute) || !_options.NamingStrategies.ContainsKey(namingStrategyRoute))
            {
                return string.Empty;
            }

            return namingStrategyRoute;
        }

        protected override string ResolvePropertyName(string name)
        {
            Dictionary<string, string> strategy = new Dictionary<string, string>();

            var namingStrategyRoute = GetValidNamingStrategyFromRoute();

            if (string.IsNullOrEmpty(namingStrategyRoute))
            {
                strategy = _options.DefaultStrategy;
            }
            else
            {
                strategy = _options.NamingStrategies[namingStrategyRoute];
            }

            return strategy[name];
        }
    }
}
