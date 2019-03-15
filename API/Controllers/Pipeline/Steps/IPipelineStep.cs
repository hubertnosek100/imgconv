using Microsoft.AspNetCore.Http;

namespace API.Controllers.Pipeline.Steps
{
    public interface IPipelineStep
    {
        void Execute(string path, PipelineFile formFile);
        int OrderNumber { get; }
    }
}