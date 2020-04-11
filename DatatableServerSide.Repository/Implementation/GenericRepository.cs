using DatatableServerSide.Data;
using DatatableServerSide.Data.Models;
using DatatableServerSide.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using static DatatableServerSide.Data.Enum;
using System.Linq.Dynamic.Core;

namespace DatatableServerSide.Repository.Implementation
{
        public class GenericRepository<T> : IGenericRepository<T> where T : class
        {

            protected UserDataDbContext _context;
            protected DbSet<T> DbSet;
            public GenericRepository(UserDataDbContext context)
            {
                _context = context;
                DbSet = _context.Set<T>();
            }

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return DbSet.Count();
            else
                return DbSet.Count(predicate);
        }
        public IEnumerable<T> GetAll()
        {
            return DbSet.AsEnumerable<T>();
        }


        public List<T> GetDatatableResponse(
             Expression<Func<T, bool>> filter = null,
            string orderBy = null,
            FCSortDirection? sortDirection = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int? start = null, int? length = null, bool disableTracking = true)
            {
                IQueryable<T> query = DbSet;

                if (disableTracking)
                    query = query.AsNoTracking();

                if (filter != null)
                    query = query.Where(filter);

                if (include != null)
                    query = include(query);

                if (orderBy != null)
                {
                    if (sortDirection == FCSortDirection.Descending)
                    {
                        query = query.OrderBy(orderBy + " descending");
                    }
                    else
                    {
                        query = query.OrderBy(orderBy);
                    }
                }


                if (start.HasValue)
                {
                    var skipValue = start.Value;
                    query = query.Skip(skipValue);
                }
                if (length.HasValue)
                {
                    var takeValue = length.Value;
                    query = query.Take(takeValue);
                }
                return query.ToList();
            }

       
    }
}
