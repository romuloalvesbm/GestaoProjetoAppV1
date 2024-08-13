using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Domain.Common
{
    public class EntityBase : ISoftDelete
    {
        public Guid Id { get; set; } //TODO Verificar se não tem q ser private set
        public DateTime? DeletedAt { get; set; }
    }
}
