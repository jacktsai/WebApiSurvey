using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8088");
            var response = client.GetStringAsync("api/values/").Result;
            Console.WriteLine(response);
        }
    }
}
