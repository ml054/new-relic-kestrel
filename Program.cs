using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace kestrel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(x => { })
                .UseUrls(new [] { "http://localhost:8081" })                
                .UseStartup<Startup>();
    }
    
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(_ => RequestHandler);
        }
        
        private async Task RequestHandler(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.Headers["Content-Type"] = "application/json; charset=utf-8";

            var bytes = Encoding.UTF8.GetBytes("{}");
            context.Response.Body.Write(bytes, 0, bytes.Length);
        }
    }
}
