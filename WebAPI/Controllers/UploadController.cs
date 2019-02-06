using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.Models.UploadModel;

namespace WebAPI.Controllers
{
    [Route("api/upload")]
    public class UploadController : Controller
    {
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UploadController(ILogger<UploadController> logger,
            IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost(Name = "UploadFile")]
        [ProducesResponseType(typeof(string), 201)]
        public ActionResult UploadFile([FromBody] CreatingUploadModel model)
        {
            var dirPath = _hostingEnvironment.ContentRootPath + $@"\UploadedImages\Incidents\{model.IncidentId}";
            var dirExists = Directory.Exists(dirPath);
            if (!dirExists) Directory.CreateDirectory(dirPath);

            foreach (var image in model.Images)
            {
                var str = image.Base64String.Split(',');
                var bytes = Convert.FromBase64String(str[1]);
                var fileName = Guid.NewGuid().ToString("D");
                System.IO.File.WriteAllBytes(dirPath + $@"\{fileName}.jpg", bytes);
            }

            var mappedPath = _hostingEnvironment.WebRootPath;
            _logger.LogCritical($"[Upload] {model.Images.Length} was uploaded! WebRootPath: {mappedPath}");
            return Ok("Files uploaded");
        }

        [HttpGet("{id:guid}", Name = "GetIncidentImages")]
        public ActionResult GetIncidentImages(Guid id)
        {
            var dirPath = _hostingEnvironment.ContentRootPath + $@"\UploadedImages\Incidents\{id}";
            var dirExists = Directory.Exists(dirPath);
            if (!dirExists) return BadRequest("Folder doesn't exist");

            var d = new DirectoryInfo(dirPath);
            var files = d.GetFiles("*.jpg");
            var filesPath = files.Select(x => x.FullName);
            return Ok(
                filesPath.Select(x =>
                {
                    var b = System.IO.File.ReadAllBytes(x);
                    return File(b, "image/jpeg");
                })
            );
        }
    }
}