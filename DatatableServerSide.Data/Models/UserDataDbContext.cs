using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatatableServerSide.Data.Models
{
    public class UserDataDbContext : DbContext
    {
        public UserDataDbContext()
        {
        }

        public UserDataDbContext(DbContextOptions<UserDataDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

    }
}
