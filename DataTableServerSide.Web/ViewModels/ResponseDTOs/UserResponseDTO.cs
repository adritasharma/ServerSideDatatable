using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatatableServerSide.Web.ViewModels.ResponseDTOs
{
    public class UserResponseDTO
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Roles { get; set; }

    }
}
