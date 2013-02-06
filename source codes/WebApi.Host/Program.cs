using System;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace WebApi
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8080");

            MyConfig.Config(config);

            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Press enter to quit.");
                Console.ReadLine();
            }
        }
    }
}