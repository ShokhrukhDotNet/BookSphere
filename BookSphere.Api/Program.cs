//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BookSphere.Api
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>());
        }
    }
}
