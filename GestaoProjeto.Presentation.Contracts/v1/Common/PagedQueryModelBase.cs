using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Presentation.Contracts.v1.Common
{
    public abstract class PagedQueryModelBase
    {
        public int CurrentPage { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
