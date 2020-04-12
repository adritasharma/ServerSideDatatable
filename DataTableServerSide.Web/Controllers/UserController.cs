using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using DatatableServerSide.Service.Interface;
using DatatableServerSide.Web.AutoMapperProfiles.RequestDTOs;
using DatatableServerSide.Web.ViewModels.ResponseDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataTableServerSide.Web.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _service = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [DisableRequestSizeLimit]
        [HttpGet]
        public IActionResult Get(IDataTablesRequest request)
        {
            if (request == null)
            {
                var res = _service.GetAll();
                return Ok(res);
            }
            else
            {
                var dtOptions = _mapper.Map<DataTablesRequestDTO>(request);

                var res = _service.GetDatatableUsers(dtOptions.SearchText, dtOptions.FilterType, dtOptions.SortColumn, dtOptions.SortType, dtOptions.SearchColumns, dtOptions.Start, dtOptions.Length);

                return Ok(DataTablesResponse.Create(request, res.recordsTotal, res.recordsFiltered, _mapper.Map<List<UserResponseDTO>>(res.Data)));
            }
        }
    }
}