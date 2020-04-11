using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DatatableServerSide.Data.Enum;

namespace DatatableServerSide.Web.AutoMapperProfiles.RequestDTOs
{
    public class DataTablesRequestDTO
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public IDictionary<string, object> AdditionalParameters { get; set; }
        public IDictionary<string, string> SearchColumns { get; set; }
        public string SearchText { get; set; }
        public string SortColumn { get; set; }
        public FCSortDirection SortType { get; set; }
        public string FilterType { get; set; }
    }
}
