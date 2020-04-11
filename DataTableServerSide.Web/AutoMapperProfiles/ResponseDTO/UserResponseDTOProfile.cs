using AutoMapper;
using SalesCRM.DB.Models;
using SalesCRM.Web.ViewModels.ResponseDTOs;
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
            CreateMap<UsertblTemp, UserResponseDTO>()
               .ForMember(m => m.RoleName, map => map.MapFrom(s => s.Userrole.FirstOrDefault().Role.RoleName));
        }
    }
}
