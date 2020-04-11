using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using DatatableServerSide.Repository.Interface;
using DatatableServerSide.Service.ServiceModels;
using DatatableServerSide.Data.Models;
using static DatatableServerSide.Data.Enum;
using DatatableServerSide.Service.Interface;

namespace DatatableServerSide.Service.Implementations
{
    public class UserService : EntityService<User>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository repository) : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public DatatableResultModel<List<User>> GetDatatableUsers(string searchText, string filterType, string sortColumn, FCSortDirection sortDirection, int? start = null, int? length = null)
        {
            Expression<Func<User, bool>> deleg = x => true;


            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();
                deleg = x => x.FullName().ToLower().Contains(searchText);
            }

            //deleg = x => !string.IsNullOrEmpty(searchText) ? (x.FullName().ToLower().Contains(searchText)) : true &&
            //!string.IsNullOrEmpty(filterType) ? 
            //(filterType == "active" ? x.IsActive == true : x.IsActive != true) : true;

            //Check if active or inactive
            if (!string.IsNullOrEmpty(filterType))
            {
                filterType = filterType.ToLower();
                switch (filterType)
                {

                    case "active":
                        deleg = x => x.IsVerified == true;
                        break;
                    case "inactive":
                        //deleg = deleg.AndAlso(x => x.Status == Status.InActive);

                        //  deleg = (deleg != null) ? deleg.AndAlso(x => x.Status == Status.InActive) : x => x.Status == Status.InActive;
                        break;
                    default:
                        break;
                }
            }

            IEnumerable<User> query;


            string defaultOrderBy = "FullName";
            if (!string.IsNullOrEmpty(sortColumn) && sortColumn.ToLower() != "Id")
            {
                query = _userRepository.GetDatatableResponse(deleg, sortColumn, sortDirection, null, start, length);
            }
            else
            {
                query = _userRepository.GetDatatableResponse(deleg, defaultOrderBy, null, null, start, length);
            }
            return new DatatableResultModel<List<User>>
            {
                Data = query.ToList(),
                TotalDataCount = _userRepository.Count(),
                FilteredDataCount = _userRepository.Count(deleg)
            };
        }

        IEnumerable<User> IEntityService<User>.GetAll()
        {
            return _userRepository.GetAll();
        }
    }
}
