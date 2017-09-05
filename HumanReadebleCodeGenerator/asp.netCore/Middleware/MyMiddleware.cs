using asp.netCore.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp.netCore.Middleware
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly OptionsInfo _environmentInfoOptions;
        private readonly ICodeGenerator _codeGenerator;

        public MyMiddleware(RequestDelegate next, IOptions<OptionsInfo> options, ICodeGenerator codeGenerator)
        {
            _next = next;
            _codeGenerator = codeGenerator;
            _environmentInfoOptions = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            DateTime date = DateTime.Now;
            context.Response.HttpContext.Items["prefix"] = _environmentInfoOptions.CodePrefix + (date.Day / 10).ToString() + (date.Day % 10).ToString() + (date.Month / 10).ToString() + (date.Month % 10).ToString();
            await this._next(context);
        }
    }
}
