using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemark.Domain.Entities
{
    /// <summary>
    /// Representa a base para todas as entidades do sistema,
    /// garantindo uma chave primária e campos de auditoria.
    /// </summary>
    public abstract class IBaseEntity
    {
        /// <summary>
        /// Chave primária única da entidade.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Data e hora em que a entidade foi criada (em UTC).
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Identificador do usuário que criou a entidade.
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Data e hora da última atualização da entidade (em UTC).
        /// Nulo se nunca foi atualizada.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Identificador do usuário que realizou a última atualização.
        /// </summary>
        public string? UpdatedBy { get; set; }
    }
}
