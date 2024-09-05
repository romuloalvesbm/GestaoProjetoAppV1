using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Response
{
    public class FuncionarioResponse
    {
        public Guid FuncionarioId { get; set; }
        public string? Nome { get; set; }
        public Guid? SupervisorId { get; set; }
        public FuncionarioResponse? Supervisor { get; set; }
        public ICollection<FuncionarioResponse>? FuncionarioDtos { get; set; }
    }
}
