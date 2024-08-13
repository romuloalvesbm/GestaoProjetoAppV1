using GestaoProjeto.Infra.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Domain.Funcionarios
{
    public interface IFuncionarioRepository : IBaseRepository<Funcionario>
    {
        Task<bool> NameExist(string? nome);
    }
}
