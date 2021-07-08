using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Planner.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UiController : Controller
    {
        private readonly string _root;

        public UiController()
        {
            var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            _root = Path.GetDirectoryName(location);
        }

        [HttpGet]
        [Route("ui")]
        [Route("")]
        public IActionResult GetIndex()
        {
            return ServeFile("index.html");
        }

        [HttpGet]
        [Route("ui/assets/{filename}")]
        public IActionResult Assets(string filename)
        {
            filename = Path.Combine("assets", filename.Replace("~*~", "/"));

            return ServeFile(filename);
        }

        private IActionResult ServeFile(string filename)
        {
            filename = Path.Combine(_root, filename);
            var content = System.IO.File.ReadAllBytes(filename);
            return File(content, GetContentType(filename));
        }

        private string GetContentType(string filename)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(filename, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}