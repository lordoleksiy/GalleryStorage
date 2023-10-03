using GalleryStorage.Models;
using Newtonsoft.Json;
using System.Net;

namespace GalleryStorage.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = ex.Message
                };
                context.Response.Redirect($"/Home/Error?error={Uri.EscapeDataString(JsonConvert.SerializeObject(errorViewModel))}");
            }
        }
    }
}
