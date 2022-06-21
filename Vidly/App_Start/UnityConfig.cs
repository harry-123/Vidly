using System.Web.Http;
using Unity;
using Unity.WebApi;
using Vidly.DAL.Repositories;
using Vidly.DAL.Services;

namespace Vidly
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<ICustomerService, CustomerRepository>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}