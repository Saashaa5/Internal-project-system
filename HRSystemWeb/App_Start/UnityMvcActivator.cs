using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using HRSystemWeb;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(HRSystemWeb.UnityWebActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(HRSystemWeb.UnityWebActivator), "Shutdown")]

namespace HRSystemWeb
{
    /// <summary>Bootstrapping для интегрирования в Asp.Net Mvc приложение</summary>
    public static class UnityWebActivator
    {
        /// <summary>Интегрировать Unity, когда приложение стартует</summary>
        public static void Start()
        {
            var container = UnityConfig.GetConfiguredContainer();

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
           
        }

        /// <summary>Удаление контейнера</summary>
        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}