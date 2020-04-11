using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SalesCRM.DB.Models;
using SalesCRM.Web.ViewModels.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SalesCRM.Web.AutoMapperProfiles.ResponseDTO
{
    public class LoginResponseDTOProfile : Profile
    {
        public LoginResponseDTOProfile()
        {
            CreateMap<UsertblTemp, LoginResponseDTO>()
               .ForMember(m => m.RoleName, map => map.MapFrom(s => s.Userrole.FirstOrDefault().Role.RoleName))
               .ForMember(m => m.Token, map => map.MapFrom(s => GenerateToken(s.Id, s.Email)));
        }
        private string GenerateToken(int userId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("thisiscustomSecretkeyforauthentication");


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                     new Claim(ClaimTypes.Name,email)
                }),
                Expires = System.DateTime.Now.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }
    }
}