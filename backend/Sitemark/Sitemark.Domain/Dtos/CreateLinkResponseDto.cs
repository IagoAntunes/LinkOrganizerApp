using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemark.Domain.Dtos
{
    public class CreateLinkResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
        public string Url { get; set; }
        public Guid? ImageId { get; set; }
        public string UserId { get; set; } // Retornamos apenas o ID do usuário, se necessário.
    }
}
