using Microsoft.AspNetCore.Http;

namespace Sitemark.Domain.Dtos
{
    public class LinkCreateDto
    {
        public string Name { get; set; }
        public string Platform { get; set; }
        public string Url { get; set; }
        public IFormFile File { get; set; }
    }
}
