using System;
using System.Collections.Generic;
using System.Linq;
using API.Controllers.Pipeline.Steps;
using API.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace API.Controllers.Pipeline
{
    public class ProcessPipeline : IProcessPipeline
    {
        private readonly IEnumerable<IPipelineStep> _steps;

        public ProcessPipeline(IServiceProvider provider)
        {
            _steps = ReflectiveEnumerator.GetEnumerableOfType<IPipelineStep>(provider).OrderBy(x => x.OrderNumber);
        }

        public void Run(string path, PipelineFile formFile)
        {
            foreach (var step in _steps)
            {
                step.Execute(path, formFile);
            }
        }
    }
}