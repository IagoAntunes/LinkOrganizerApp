using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemark.Domain.Entities
{
    public class ImageEntity : IBaseEntity
    {
        /// <summary>
        /// O nome original do arquivo enviado pelo usuário.
        /// Ex: "queen_bohemian_rhapsody_cover.png"
        /// </summary>
        public string OriginalFileName { get; set; }

        /// <summary>
        /// O caminho para o arquivo no armazenamento (ex: blob storage ou disco).
        /// Poderia ser "uploads/a1b2c3d4-e5f6.jpg" ou uma URL completa.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// O tipo de conteúdo do arquivo (ex: "image/jpeg", "image/png").
        /// </summary>
        public string ContentType { get; set; }
    }
}
