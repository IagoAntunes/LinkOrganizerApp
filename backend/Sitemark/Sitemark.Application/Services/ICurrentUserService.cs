using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemark.Application.Services
{
    public interface ICurrentUserService
    {
        /// <summary>
        /// Obtém o ID do usuário logado. Retorna nulo se não houver usuário autenticado.
        /// </summary>
        string? UserId { get; }

        /// <summary>
        /// Obtém o nome de usuário (ou email) do usuário logado.
        /// </summary>
        string? UserName { get; }
    }
}
