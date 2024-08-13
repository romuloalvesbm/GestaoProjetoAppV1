using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Presentation.Contracts.v1.Common
{
    public class PagedList<T>
    {
        public int CurrentPage { get; set; }
        public int TotalResults { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public ICollection<T> ResultSet { get; set; }

        public static PagedList<T> Create(List<T> items, int count, int pageNumber, int pageSize)
        {
            return new PagedList<T>
            {
                ResultSet = items ?? new List<T>(),
                TotalResults = count,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
        }
    }
}
