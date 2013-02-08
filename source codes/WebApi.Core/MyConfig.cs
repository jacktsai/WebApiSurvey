using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;
using System.Web.Http.OData.Builder;
using Microsoft.Practices.Unity;
using WebApi.BLL.Entities;
using WebApi.Net.Http;
using WebApi.Security.Cryptography;
using WebApi.Web.Http.Dependencies;
using WebApi.Web.Http.Description;
using WebApi.Web.Http.Dispatcher;
using WebApi.Web.Http.Filters;

namespace WebApi
{
    public static class MyConfig
    {
        private static readonly IUnityContainer unityContainer = new UnityContainer();
        private static HttpConfiguration config;

        static MyConfig()
        {
            //unityContainer.RegisterType<IHttpContentEncryptor, FakeHttpContentEncryptor>();
            unityContainer.RegisterType<IHttpContentEncryptor, AesHttpContentEncryptor>();
            unityContainer.RegisterType<IKeyProvider, DefaultKeyProvider>();
            unityContainer.RegisterType<ServerSideSecurityHandler>();
            unityContainer.RegisterType<ClientSideSecurityHandler>();
        }

        public static void Config(HttpConfiguration config)
        {
            MyConfig.config = config;

            config.DependencyResolver = new MyDependencyResolver(unityContainer);

            RegisterMessageHandlers(config.MessageHandlers);
            RegisterServices(config.Services);
            RegisterFilters(config.Filters);
            RegisterRoute(config.Routes);
            RegisterFormatters(config.Formatters);
        }

        public static T GetService<T>() where T : class
        {
            if (unityContainer.IsRegistered<T>())
            {
                return unityContainer.Resolve<T>();
            }

            if (config != null)
            {
                var service = config.Services.GetService(typeof(T));

                if (service != null)
                {
                    return (T)service;
                }
            }

            return default(T);
        }

        private static void RegisterMessageHandlers(Collection<DelegatingHandler> handlers)
        {
            handlers.Add(new DumpMessageHandler());

            //var securityHandler = GetService<ServerSideSecurityHandler>() as DelegatingHandler;
            //handlers.Add(securityHandler);

            handlers.Add(new AuthenticationHandler());

            //handlers.Add(new DumpMessageHandler());
        }

        private static void RegisterServices(ServicesContainer services)
        {
            services.Replace(typeof(IAssembliesResolver), new MyAssembliesResolver());

            //services.Replace(typeof(ITraceWriter), new MyTraceWriter());
            services.Replace(typeof(IDocumentationProvider), new MyDocumentationProvider());
        }

        private static void RegisterFilters(HttpFilterCollection filters)
        {
            //要求所有的 action 都需要經過授權，除了各別指定 AllowAnonymous。
            filters.Add(new MyAuthorizationFilter());

            //在執行各 action 之前做一些共同的處理。
            filters.Add(new MyActionFilter());
        }

        private static void RegisterRoute(HttpRouteCollection routes)
        {
            //此預設會在相同 http method 對應到多個 action 時造成 HTTP runtime 錯誤。
            //routes.MapHttpRoute("API Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            routes.MapHttpRoute("API Default", "api/{controller}/{action}");

            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Category>("Category"); // {name}Controller
            routes.MapODataRoute("EntitySet test", "odata", builder.GetEdmModel());

            //foreach (Route r in RouteTable.Routes)
            //{
            //    Trace.WriteLine(string.Format("{0} {1}", r.Url, r.RouteHandler));
            //}
        }

        private static void RegisterFormatters(MediaTypeFormatterCollection formatters)
        {
            //var jsonFormatter = formatters.First(f => f.SupportedMediaTypes.Any(m => m.MediaType == "application/json"));
            //formatters.Remove(jsonFormatter);
            //formatters.Add(new MyJsonFormatter());

            //foreach (var f in formatters)
            //{
            //    Trace.WriteLine(string.Format("[{0}]", f.GetType().FullName));
            //    foreach (var t in f.SupportedMediaTypes)
            //    {
            //        Trace.WriteLine(string.Format("MediaType: {0}, CharSet: {1}", t.MediaType, t.CharSet));
            //    }
            //}
        }
    }
}