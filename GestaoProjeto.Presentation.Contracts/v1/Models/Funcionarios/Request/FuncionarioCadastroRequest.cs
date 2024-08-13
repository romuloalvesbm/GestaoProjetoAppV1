using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Request
{
    public class FuncionarioCadastroRequest
    {
        public string? Nome { get; set; }
        public Guid? SupervisorId { get; set; }
        public ICollection<Subordinado>? Subordinados { get; set; }

    }

    public class Subordinado
    {
        public string? Nome { get; set; }

    }
}
