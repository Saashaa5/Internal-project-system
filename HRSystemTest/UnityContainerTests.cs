using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Tests.Common
{
    public abstract class UnityContainerTests
    {
        /// <summary>
        /// Internal chip service locator. 
        /// </summary>
        protected IServiceLocator ServiceLocator;

        /// <summary> 
        /// Unity container. 
        /// </summary> 
        protected IUnityContainer UnityContainer;

        /// <summary>
        /// Method for initializing UnityContainer and ServiceLocator
        /// Must add attribute [TestInitialize]
        /// </summary>
        public abstract void InitializeTests();
    }
}
