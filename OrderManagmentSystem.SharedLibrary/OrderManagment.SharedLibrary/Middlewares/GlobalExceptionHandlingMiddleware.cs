using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderManagment.SharedLibrary.Exceptions;
using OrderManagment.SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagment.SharedLibrary.Middlewares
{
    public class GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleException(context, ex);
			}
        }

		public async Task HandleException(HttpContext context, Exception ex)
		{
			context.Response.ContentType = "application/json";
			var statusCode = ex switch
			{
				NotFoundRequestException => HttpStatusCode.NotFound,
				BadHttpRequestException => HttpStatusCode.BadRequest,
				_ => HttpStatusCode.InternalServerError
			};
			context.Response.StatusCode = (int)statusCode;

            var response = new ErrorDetail()
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
                RequestId = context.TraceIdentifier,
                StackTrace = context.RequestServices.GetService<IWebHostEnvironment>().IsDevelopment() ? ex.StackTrace : null,
                Details = ex.InnerException?.Message
            };


            await context.Response.WriteAsync(response.ToString());
        }
    }
}
