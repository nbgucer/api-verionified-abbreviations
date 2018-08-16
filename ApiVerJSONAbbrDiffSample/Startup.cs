using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using ApiVerJSONAbbrDiffSample.JsonNetExtensions;
using ApiVerJSONAbbrDiffSample.Models;
using ApiVerJSONAbbrDiffSample.Abbreviations;

namespace ApiVerJSONAbbrDiffSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new RouteJsonContractResolver(new RouteJsonNamingStrategyOptions
                {
                    DefaultStrategy = new MyMapper(new V1Mapping()).GetAbbreviations(),
                    HttpContextAccessorProvider = services.BuildServiceProvider().GetService<IHttpContextAccessor>,
                    NamingStrategies = new Dictionary<string, Dictionary<string, string>>
                {
                    {"v1", new MyMapper(new V1Mapping()).GetAbbreviations() },
                    {"v2", new MyMapper(new V2Mapping()).GetAbbreviations() }
                }

                }, services.BuildServiceProvider().GetService<IMemoryCache>);
            });
            services.AddApiVersioning();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
