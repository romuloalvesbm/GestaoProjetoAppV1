using GestaoProjeto.Presentation.Contracts.v1.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Request
{
    public class FuncionarioGridRequest : PagedQueryModelBase
    {
        public string Nome { get; set; } = "";
        public string Sort { get; set; } = "Id"; // Id do banco de dados
        public bool Order { get; set; }
    }
}
