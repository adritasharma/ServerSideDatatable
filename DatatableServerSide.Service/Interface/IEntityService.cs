using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DatatableServerSide.Service.Interface
{
    public interface IEntityService<T>
    {
            IEnumerable<T> GetAll();

        }
}
