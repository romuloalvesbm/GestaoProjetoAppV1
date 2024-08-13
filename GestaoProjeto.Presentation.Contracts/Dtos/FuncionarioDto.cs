using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Presentation.Contracts.Dtos
{
    public class FuncionarioDto
    {
        public Guid FuncionarioId { get; set; }
        public string? Nome { get; set; }
        public Guid? SupervisorId { get; set; }
        public FuncionarioDto? Supervisor { get; set; }
        public ICollection<FuncionarioDto>? FuncionarioDtos { get; set; }
    }
}
