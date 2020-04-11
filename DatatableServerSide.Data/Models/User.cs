using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatatableServerSide.Data.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public virtual List<UserRole> UserRoles { get; set; }
        public bool IsVerified { get; set; }
        public string FullName() => (!string.IsNullOrEmpty(MiddleName)) ? $"{FirstName} {MiddleName} {LastName}"?.Trim() : $"{FirstName} {LastName}";

    }
}
