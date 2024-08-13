using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoProjeto.Presentation.Contracts.v1.Common;

namespace GestaoProjeto.Presentation.Contracts.Extensions
{
    public static class ColletionExtensions
    {
        public static PagedList<T> ToPagedList<T>(this ICollection<T> source, int pageIndex, int pageSize)
        {
            var count = source?.Count();
            var items = source?
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();

            return PagedList<T>.Create(items!, count.GetValueOrDefault(), pageIndex, pageSize);
        }
    }

}
