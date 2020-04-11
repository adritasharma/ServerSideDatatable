using System;
using System.Collections.Generic;
using System.Text;

namespace DatatableServerSide.Service.ServiceModels
{
    public class DatatableResultModel<T>
    {
        public int TotalDataCount { get; set; }
        public int FilteredDataCount { get; set; }
        public T Data { get; set; }
        public IDictionary<string, object> AdditionalData { get; set; }
    }
}
