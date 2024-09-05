using GestaoProjeto.Presentation.Contracts;
using GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Request;
using GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Application.Interfaces.Services
{
    public interface IFuncionarioApplicationService
    {
        Task<Result> Create(FuncionarioCadastroRequest model);
        Task<Result> Update(FuncionarioEdicaoRequest model);
        Task<Result> Delete(FuncionarioExclusaoRequest model);
        Task<Result<ICollection<FuncionarioResponse>>> GetAll(FuncionarioGridRequest model);
        Task<Result<FuncionarioResponse>> GetById(Guid id);
    }
}
