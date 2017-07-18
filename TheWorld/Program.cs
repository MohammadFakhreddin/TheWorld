using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace TheWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()//The name of webserver
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()//Support for iis
                .UseStartup<Startup>()//use startup class for my code
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
