using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Planner
{
    internal class UiFilesMiddleware
    {
        private readonly RequestDelegate _nextDelegate;

        public UiFilesMiddleware(RequestDelegate next)
        {
            _nextDelegate = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.ToString() == "/")
            {
                context.Request.Path = "/ui";
            }

            var stringPath = context.Request.Path.ToString();


            var emberPrefix = "/ui/assets/";

            if (stringPath.StartsWith(emberPrefix))
            {
                var parts = stringPath.Split(emberPrefix);

                var result = emberPrefix + parts[1].Replace("/", "~*~");
                context.Request.Path = result;
            }

            await _nextDelegate.Invoke(context);
        }
    }
}