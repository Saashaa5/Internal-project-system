using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using HRSystemBLL.Contracts;
using HRSystemBLL.Services;
using HRSystemDAL;
using HRSystemDAL.UnitOfWork;
using HRSystemWeb.Models;
using Unity.Mvc5;

namespace HRSystemWeb
{
   public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            /// <summary>Регистрация типов при обращении к типу</summary>
            container.RegisterType<DbContext, ProjectsDbContext>(new PerRequestLifetimeManager()); 
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType<ICompanyService, CompanyService>(new PerRequestLifetimeManager());
            container.RegisterType<IEmployeeService, EmployeeService>(new PerRequestLifetimeManager());
            container.RegisterType<IProjectService, ProjectService>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new PerRequestLifetimeManager());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => System.Web.HttpContext.Current.GetOwinContext().Authentication));

        }
    }




}