using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using static DatatableServerSide.Data.Enum;

namespace DatatableServerSide.Repository.Interface
{
    public interface IGenericRepository<T>
    {
        int Count(Expression<Func<T, bool>> predicate = null);

        IEnumerable<T> GetAll();
        List<T> GetDatatableResponse(
        Expression<Func<T, bool>> filter = null,
        string orderBy = null,
        FCSortDirection? sortDirection = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                        int? start = null, int? length = null, bool disableTracking = true);
    }
}
