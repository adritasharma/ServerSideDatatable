using System;
using System.Collections.Generic;
using System.Text;

namespace DatatableServerSide.Repository.Interface
{
    public interface IUnitOfWork
    {
        int Commit();
        void Dispose(bool disposing);
    }
}
