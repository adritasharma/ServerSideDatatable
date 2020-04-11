using System;
using System.Collections.Generic;
using System.Text;

namespace DatatableServerSide.Service.ServiceModels
{
    public class ServiceResultModel<T>
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
