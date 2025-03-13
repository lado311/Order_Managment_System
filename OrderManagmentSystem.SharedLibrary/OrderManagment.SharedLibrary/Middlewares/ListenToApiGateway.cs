using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagment.SharedLibrary.Middlewares
{
    public class ListenToApiGateway(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var signedHeader = context.Request.Headers["Api-Gateway"];

            if (signedHeader.FirstOrDefault() == null)
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            else
                await next(context);
        }
    }
}
