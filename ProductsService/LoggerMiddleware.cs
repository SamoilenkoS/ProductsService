using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsPresentationLayer
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var streamWriter = new StreamWriter("middlewareLogs.txt", true))
            {
                streamWriter.WriteLine(
                    context.Request.Scheme + context.Request.Method + context.Request.QueryString);
            }
            
            await _next(context);
        }
    }
}
