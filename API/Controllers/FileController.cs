using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.Pipeline;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IHostingEnvironment _environment;
        private readonly IProcessPipeline _pipeline;

        public FileController(IHostingEnvironment environment, IProcessPipeline pipeline)
        {
            _environment = environment;
            _pipeline = pipeline;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var filePath = Path.Combine(_environment.WebRootPath);
            return Directory.GetFiles(filePath).Select(Path.GetFileName).Select(GetExternalFileUrl).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult Post()
        {
            var dir = Path.Combine(_environment.WebRootPath);

            if (HttpContext.Request.Form.Files != null)
            {
                IFormFileCollection files = HttpContext.Request.Form.Files;
                long size = files.Sum(f => f.Length);

                List<string> ids = new List<string>();
                foreach (IFormFile file in files)
                {
                    if (file.Length > 0)
                    {
                        var id = Guid.NewGuid();
                        var fileName = $"{id}.{file.FileName.Split(".").Last()}";
                        var filePath = Path.Combine(dir, fileName);
                        using (FileStream fs = System.IO.File.Create(filePath))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }


                        var pFile = new PipelineFile(file);
                        pFile.Rename(id.ToString());
                        _pipeline.Run(dir, pFile);

                        ids.Add(GetExternalFileUrl($"{id}.svg"));
                    }
                }

                return Ok(new {count = files.Count, size, filePath = dir, files = ids});
            }

            return Ok(new {count = 0, size = 0, filePath = dir});
        }

        public string GetExternalFileUrl(string fileName)
        {
            return $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{fileName}";
        }
    }
}