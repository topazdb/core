﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Castle.DynamicProxy;
using api.db;
using api.Util;
using api.Models;

namespace api {
    public class Program {
        public static void Main(string[] args) {
            new System.Threading.Timer((e) => {
                System.Console.WriteLine("Polling /data for new X3P files.");
                new Populator("@/data");
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
