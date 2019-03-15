using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace API.Controllers.Pipeline.Steps
{
    public class SvgRemoverStep : IPipelineStep
    {
        private readonly IConfiguration _configuration;

        public SvgRemoverStep(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int OrderNumber { get; } = 9999;

        public void Execute(string path, PipelineFile formFile)
        {
            var files = Directory.GetFiles(path).OrderByDescending(File.GetCreationTime)
                .Skip(int.Parse(_configuration["FileNumber"]));

            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
        }
    }
}