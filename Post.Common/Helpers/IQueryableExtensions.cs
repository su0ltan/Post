using Post.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Common.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> source, QueryParameters qp)
        {
            int skip = (qp.PageNumber - 1) * qp.PageSize;
            return source.Skip(skip).Take(qp.PageSize);
        }
    }

}
