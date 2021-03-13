﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSize
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] Args) =>
            Host.CreateDefaultBuilder(Args)
               .UseContentRoot(App.CurrentDirectory)
               .ConfigureAppConfiguration((host, cfg) => cfg
                   .SetBasePath(App.CurrentDirectory))
               .ConfigureServices(App.ConfigureServices);
    }
}
