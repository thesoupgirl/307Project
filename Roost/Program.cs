using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Roost
{
    public class Program
    {
        public static void Main(string[] args)
        {
	    var port = Environment.GetEnvironmentVariable("PORT") == null ?
                5000 : int.Parse(Environment.GetEnvironmentVariable("PORT"));
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:" + port)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}

