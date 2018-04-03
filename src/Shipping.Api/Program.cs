﻿using System.IO;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Shipping.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:50686")
                .UseContentRoot(basePath)
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
