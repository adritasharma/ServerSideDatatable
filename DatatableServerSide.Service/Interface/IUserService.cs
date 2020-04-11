using DatatableServerSide.Data.Models;
using DatatableServerSide.Service.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static DatatableServerSide.Data.Enum;

namespace DatatableServerSide.Service.Interface
{
    public interface IUserService : IEntityService<User>
    {
        Task<List<User>> GetAllAsync();
        DatatableResultModel<List<User>> GetDatatableUsers(string searchText, string filterType, string sortColumn, FCSortDirection sortDirection, IDictionary<string, string> SearchColumns, int? start = null, int? length = null);

    }
}
