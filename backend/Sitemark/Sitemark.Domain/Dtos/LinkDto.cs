namespace Sitemark.Domain.Dtos
{
    public class LinkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
        public string Url { get; set; }
        public Guid? ImageId { get; set; }

        public UserDto User { get; set; }

    }
}
