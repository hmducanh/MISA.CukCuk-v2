using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        #region Field
        IDepartmentService _departmentService;
        IDepartmentRepository _departmentRepository;

        #endregion

        #region Constructor
        /// <summary>
        /// khởi tạo
        /// </summary>
        /// <param name="departmentService"></param>
        /// <param name="departmentRepository"></param>
        public DepartmentController(IDepartmentService departmentService, IDepartmentRepository departmentRepository)
        {
            _departmentService = departmentService;
            _departmentRepository = departmentRepository;
        }

        #endregion

        #region Method
        /// <summary>
        /// lấy tất cả đơn vị
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var departments = _departmentService.GetAll();
            if (departments.Count() > 0)
            {
                return Ok(departments);
            }
            else
            {
                return NoContent();
            }
        }
        #endregion
    }
}
