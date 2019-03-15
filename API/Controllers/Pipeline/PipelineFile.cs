using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Controllers.Pipeline
{
    public class PipelineFile : IFormFile
    {
        private readonly IFormFile _formFile;
        private string _name;
        private readonly string _extension;
        
        public PipelineFile(IFormFile formFile)
        {
            _formFile = formFile;
            _name = _formFile.FileName.Split(".").First();
            _extension = _formFile.FileName.Split(".").Last();
        }

        public Stream OpenReadStream()
        {
            return _formFile.OpenReadStream();
        }

        public void CopyTo(Stream target)
        {
            _formFile.CopyTo(target);
        }

        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = new CancellationToken())
        {
            await _formFile.CopyToAsync(target, cancellationToken);
        }

        public string ContentType => _formFile.ContentType;
        public string ContentDisposition => _formFile.ContentDisposition;
        public IHeaderDictionary Headers => _formFile.Headers;
        public long Length => _formFile.Length;
        public string Name => _name;
        public string FileName => $"{_name}.{_extension}";
        public string Extension => _extension;

        public void Rename(string name)
        {
            _name = name;
        }
    }
}