namespace Sitemark.API.Dtos.Requests
{
    public class LinkCreateRequest
    {
        public string Name { get; set; }
        public string Platform { get; set; }
        public string Url { get; set; }
        public IFormFile File { get; set; }
    }
}
