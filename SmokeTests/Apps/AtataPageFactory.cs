using Atata;
using Microsoft.Extensions.DependencyInjection;
using SmokeTests.Utils;
using System;
using System.Reflection;
using System.Text;

namespace SmokeTests.Apps
{
    /// <summary>
    /// Atata specific page factory to go to a given page based the passed in IBasePage
    /// </summary>
    public class AtataPageFactory : IPageFactory
    {
        public AtataPageFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Constructs a page using Atata's Go factory, also allows navigating to a specific url via uriSegment.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uriSegment"></param>
        /// <returns>Constructed PageObject</returns>
        public T GetPage<T>(string uriSegment = null, bool navigate = true) where T : IBasePage
        {
            var page = _serviceProvider.GetRequiredService<T>();

            var method = typeof(Go).GetMethod("To", BindingFlags.Public | BindingFlags.Static);

            var pageUrlAttribute = page.GetType().GetAttribute<UrlAttribute>();

            var sb = new StringBuilder();
            sb.Append(pageUrlAttribute?.Url);
            sb.Append(uriSegment?.Prepend("/"));
            var endpoint = navigate == true ? sb.ToString() : null;

            var pageObject = method.MakeGenericMethod(page.GetType()).Invoke(null, new object[] { page, endpoint, navigate, false });
            return (T)pageObject;
        }

        private readonly IServiceProvider _serviceProvider;
    }
}
