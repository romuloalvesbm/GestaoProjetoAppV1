using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Domain.Common
{
    public interface ISoftDelete
    {
        public DateTime? DeletedAt { get; set; }
    }
}
