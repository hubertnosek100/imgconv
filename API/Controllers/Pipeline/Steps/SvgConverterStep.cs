using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace API.Controllers.Pipeline.Steps
{
    public class SvgConverterStep : IPipelineStep
    {
        public int OrderNumber { get; } = 20;

        public void Execute(string path, PipelineFile formFile)
        {
            var args =
                $"{Path.Combine(path, $"{formFile.Name}.pnm")} -s -o {Path.Combine(path, $"{formFile.Name}.svg")}";

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "potrace",
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