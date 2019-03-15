using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace API.Controllers.Pipeline.Steps
{
    public class NoSvgRemoverStep : IPipelineStep
    {
        private readonly string _requiredExtension = "svg";
        public int OrderNumber { get; } = 999;
        
        public void Execute(string path, PipelineFile formFile)
        {
            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                var extension = file.Split(".").Last();
                if (extension != _requiredExtension)
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
            }
        }
    }
}