using DatatableServerSide.Repository.Interface;
using DatatableServerSide.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatatableServerSide.Service.Implementations
{
    public class EntityService<T> : IEntityService<T> where T : class
    {
        protected IUnitOfWork UnitOfWork;
        protected IGenericRepository<T> Repository;

        public EntityService(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            UnitOfWork = unitOfWork;
            Repository = repository;
        }

        public IEnumerable<T> GetAll()
        {
            return Repository.GetAll();
        }

    }
}
