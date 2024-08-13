using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Request
{
    public class FuncionarioEdicaoRequest
    {
        public Guid FuncionarioId { get; set; }
        public string? Nome { get; set; }
        public Guid? SupervisorId { get; set; }
    }
}
