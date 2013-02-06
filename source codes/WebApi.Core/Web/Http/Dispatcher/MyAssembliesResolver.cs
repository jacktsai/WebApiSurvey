using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Compilation;
using System.Web.Http.Dispatcher;
using System.Reflection;

namespace WebApi.Web.Http.Dispatcher
{
    class MyAssembliesResolver : IAssembliesResolver
    {
        ICollection<Assembly> IAssembliesResolver.GetAssemblies()
        {
            //DefaultAssembliesResolver
            var assemblies1 = AppDomain.CurrentDomain.GetAssemblies();
            //WebHostAssembliesResolver
            var assemblies2 = BuildManager.GetReferencedAssemblies().OfType<Assembly>().ToList();
            //多了未直接參考的組件
            var excepts = assemblies1.Except(assemblies2).ToList();

            var GetUserCacheFilePath = typeof(BuildManager).GetMethod("GetUserCacheFilePath", BindingFlags.Static | BindingFlags.NonPublic);
            var filePath = GetUserCacheFilePath.Invoke(null, new object[] { "MS-ApiControllerTypeCache.xml" });

            return assemblies2;
        }
    }
}
