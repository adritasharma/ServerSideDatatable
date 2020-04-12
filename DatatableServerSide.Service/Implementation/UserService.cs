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
using DatatableSeverside.Utility;
using System.Reflection;

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

        public DatatableResultModel<List<User>> GetDatatableUsers(string searchText, string filterType, string sortColumn, FCSortDirection sortDirection, IDictionary<string, string> SearchColumns, int? start = null, int? length = null)
        {

            Expression<Func<User, bool>> deleg = x => true;

            //Check for search item
            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();
                deleg = x => x.FirstName.ToLower().Contains(searchText) ||
                x.EmailAddress.ToLower().Contains(searchText) ||
                x.MiddleName.ToLower().Contains(searchText) ||
                x.LastName.ToLower().Contains(searchText) ||
                x.PhoneNumber.ToLower().Contains(searchText);
            }
            foreach (KeyValuePair<string, string> searchCol in SearchColumns)
            {
                var colName = searchCol.Key.ToLower();
                var colValue = searchCol.Value;

                if (colValue != null)
                {
                    colValue = colValue.ToLower();

                    switch (colName)
                    {
                        case "fullname":
                            deleg = deleg.AndAlso(x => (x.FirstName.ToLower().Contains(colValue)) || (x.MiddleName.ToLower().Contains(colValue)) || (x.LastName.ToLower().Contains(colValue)));
                            break;
                        case "emailaddress":
                            deleg = deleg.AndAlso(x => x.EmailAddress.ToLower().Contains(colValue));
                            break;
                        case "phonenumber":
                            deleg = deleg.AndAlso(x => x.PhoneNumber.ToLower().Contains(colValue));
                            break;
                        default:
                            break;
                    }
                    // deleg = deleg.AndAlso(x => x.GetType().GetProperty(colName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(x).ToString().ToLower().Contains(colValue));
                }
            }

            //Check if active or inactive
            if (!string.IsNullOrEmpty(filterType))
            {
                filterType = filterType.ToLower();
                switch (filterType)
                {

                    case "active":
                        deleg = deleg.AndAlso(x => x.IsVerified == true);
                        break;
                    case "inactive":
                        deleg = deleg.AndAlso(x => x.IsVerified != true);
                        break;
                    default:
                        break;
                }
            }

            IEnumerable<User> query;


            string defaultOrderBy = "FirstName";
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
                recordsTotal = _userRepository.Count(),
                recordsFiltered = _userRepository.Count(deleg)
            };
        }

        IEnumerable<User> IEntityService<User>.GetAll()
        {
            return _userRepository.GetAll();
        }
    }
}
