using DatatableServerSide.Data.Models;
using DatatableServerSide.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatatableServerSide.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private UserDataDbContext _context;

        public UnitOfWork(UserDataDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_context == null) return;
            _context.Dispose();
            _context = null;
        }

    }
}
