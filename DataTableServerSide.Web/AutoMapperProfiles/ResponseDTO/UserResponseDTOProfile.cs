using AutoMapper;
using DatatableServerSide.Data.Models;
using DatatableServerSide.Web.ViewModels.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesCRM.Web.AutoMapperProfiles.ResponseDTO
{
    public class UserResponseDTOProfile : Profile
    {
        public UserResponseDTOProfile()
        {
            CreateMap<User, UserResponseDTO>()
               .ForMember(m => m.Roles, map => map.MapFrom(s => s.UserRoles.ToString()));
        }
    }
}
