using DatatableServerSide.Data.Models;
using DatatableServerSide.Repository.Implementation;
using DatatableServerSide.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatatableServerSide.Repository.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(UserDataDbContext context) : base(context)
        {

        }
    }
}
