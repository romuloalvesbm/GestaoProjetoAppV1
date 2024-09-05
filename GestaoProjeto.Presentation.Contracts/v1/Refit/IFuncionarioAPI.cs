using GestaoProjeto.Presentation.Contracts.v1.Common;
using GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Request;
using GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Presentation.Contracts.v1.Refit
{
    public interface IFuncionarioAPI
    {
        [Post("/api/v1/funcionario")]
        Task<Result<FuncionarioResponse>> Post([Body] FuncionarioCadastroRequest request);

        [Get("/api/v1/funcionario/getfuncionario")]
        Task<Result<FuncionarioResponse>> GetFuncionario(Guid id);

        [Get("/api/v1/funcionario/GetAllFuncionario")]
        Task<PagedList<FuncionarioResponse>> GetAllFuncionario([Query] FuncionarioGridRequest request);
    }
}
