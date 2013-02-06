using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using WebApi.BLL;
using WebApi.Web.Http;
using System.Threading;
using System.Security.Principal;
using System.Web;
using WebApi.Web.Http.Controllers;
using Microsoft.Practices.Unity;

namespace WebApi.Web.Http.Dependencies
{
    public class MyDependencyResolver : IDependencyResolver
    {
        protected IUnityContainer unityContainer;

        public MyDependencyResolver(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        IDependencyScope IDependencyResolver.BeginScope()
        {
            return this;
        }

        object IDependencyScope.GetService(Type serviceType)
        {
            if (unityContainer.IsRegistered(serviceType))
            {
                return unityContainer.Resolve(serviceType);
            }

            return null;
        }

        IEnumerable<object> IDependencyScope.GetServices(Type serviceType)
        {
            if (unityContainer.IsRegistered(serviceType))
            {
                return unityContainer.ResolveAll(serviceType);
            }

            return new object[0];
        }

        void IDisposable.Dispose()
        {
        }
    }
}