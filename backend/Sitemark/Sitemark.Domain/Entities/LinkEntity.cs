using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace Sitemark.Domain.Entities
{
    public class LinkEntity : IBaseEntity
    {
        public string Name { get; set; }
        public string Platform { get; set; }
        public string Url { get; set; }
        public string UserId { get; set; }
        public Guid? ImageId { get; set; }


        [ForeignKey(nameof(UserId))]
        public virtual IdentityUser User { get; set; }

        [ForeignKey(nameof(ImageId))]
        public virtual ImageEntity Image { get; set; }

    }
}
