using Microsoft.AspNetCore.Http;

namespace API.Controllers.Pipeline
{
    public interface IProcessPipeline
    {
        void Run(string path, PipelineFile formFile);
    }
}