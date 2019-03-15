using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace API.Controllers.Pipeline.Steps
{
    public class PnmConverterStep : IPipelineStep
    {
        public int OrderNumber { get; } = 10;

        public void Execute(string path, PipelineFile formFile)
        {
            var args = $"{Path.Combine(path, formFile.FileName)} {Path.Combine(path, $"{formFile.Name}.pnm")}";
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "convert",
                    Arguments = args,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            process.StandardOutput.ReadToEnd();
            process.WaitForExit();
        }
    }
}