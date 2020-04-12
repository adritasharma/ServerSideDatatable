using System;
using System.Collections.Generic;
using System.Text;

namespace DatatableServerSide.Service.ServiceModels
{
    public class DatatableResultModel<T>
    {
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public T Data { get; set; }
        public IDictionary<string, object> AdditionalData { get; set; }
    }
}
