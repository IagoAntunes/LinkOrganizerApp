namespace Sitemark.Domain.Dtos
{
    public class ImageDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
    }
}
