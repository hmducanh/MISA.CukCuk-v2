using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Enum;
using MISA.Core.Exceptions;
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
    /// <summary>
    /// Controller quan ly nhan vien
    /// </summary>
    /// CreatedBy : hmducanh (08/05/2021)
    public class EmployeeController : ControllerBase
    {
        #region Field
        IEmployeeService _employeeService;
        IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// khởi tạo
        /// </summary>
        /// <param name="employeeService"></param>
        /// <param name="employeeRepository"></param>
        public EmployeeController(IEmployeeService employeeService, IEmployeeRepository employeeRepository)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;   
        }

        #endregion

        #region Methods
        /// <summary>
        /// lấy tất cả nhân viên
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var employees = _employeeService.GetAll();
            if (employees.Count() > 0)
            {
                return Ok(employees);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// lấy mã nhân viên lớn nhất + 1
        /// </summary>
        /// <returns></returns>
        [HttpGet("maxEmployeeCode")]
        public IActionResult GetMaxEmployeeCode()
        {
            string maxEmployeeId = _employeeService.GetMaximumEmployeeCode();
            if(!string.IsNullOrEmpty(maxEmployeeId))
            {
                return Ok(maxEmployeeId);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// gửi 1 nhân viên lên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            try
            {
                var rowAffect = _employeeService.Insert(employee);
                if (rowAffect > 0)
                {
                    return StatusCode(201, rowAffect);
                }
                else
                {
                    return NoContent();
                }
            }
            // lay loi cua minh tu dinh nghia
            catch (CustomException Ex)
            {
                var mes = new
                {
                    devMsg = Ex.Message,
                    userMsg = "Du lieu khong hop le vui long thu lai",
                    data = Ex.Data
                };
                return StatusCode(400, mes);
            }
            // lay loi cua he thong
            catch (Exception Ex)
            {
                var mes = new
                {
                    devMsg = Ex.Message,
                    userMsg = "Co loi xay ra vui long lien he MISA de duoc tro giup",
                };
                return StatusCode(500, mes);
            }
        }

        /// <summary>
        ///  xóa 1 nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var rowAffect = _employeeService.Delete(id);
            if(rowAffect > 0)
            {
                return StatusCode(200, MISAConstant.Delete_Success);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        ///  chỉnh sửa một nhân viên có sẵn
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(Employee employee)
        {
            try
            {
                var rowAffect = _employeeService.Update(employee);
                if (rowAffect > 0)
                {
                    return StatusCode(200, MISAConstant.Update_Success);
                }
                else
                {
                    return NoContent();
                }
            }
            // lay loi cua minh tu dinh nghia
            catch (CustomException Ex)
            {
                var mes = new
                {
                    devMsg = Ex.Message,
                    userMsg = "Du lieu khong hop le vui long thu lai",
                    data = Ex.Data
                };
                return StatusCode(400, mes);
            }
            // lay loi cua he thong
            catch (Exception Ex)
            {
                var mes = new
                {
                    devMsg = Ex.Message,
                    userMsg = "Co loi xay ra vui long lien he MISA de duoc tro giup",
                };
                return StatusCode(500, mes);
            }
        }

        /// <summary>
        ///  kiểm tra mã nhân viên có tồn tại hay không
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <returns></returns>
        [HttpGet("{EmployeeCode}")]
        public IActionResult CheckEmployeeCodeExist(string EmployeeCode)
        {
            var check = _employeeService.CheckEmployeeCodeExist(EmployeeCode);
            if (check != null)
                return Ok(check);
            else
                return NoContent();
        }
        
        /// <summary>
        /// lấy nhân viên theo id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("GetById/{employeeId}")]
        public IActionResult GetById(Guid employeeId)
        {
            Employee employee = _employeeService.GetById(employeeId);
            if(employee != null)
            {
                return Ok(employee);
            }
            else
            {
                return NoContent();
            }
        }
        /// <summary>
        /// lấy danh sách nhân viên theo phân trang
        /// </summary>
        /// <param name="employeeFilter"></param>
        /// <returns></returns>
        [HttpGet("Filter")]
        public IActionResult GetEmployees([FromQuery] EmployeeFilter employeeFilter)
        {
            var pagging = _employeeService.GetEmployees(employeeFilter);

            // Xử lý kết quả trả về cho client.
            if (pagging.Data.Any() && employeeFilter.Page >= 0 && employeeFilter.PageSize >= 0)
            {
                return Ok(pagging);
            }

            return NoContent();
        }
        /// <summary>
        /// lay file excel nhan duoc tu service va tra ve cho client
        /// </summary>
        /// <returns></returns>
        /// created by : hmducanh (18/5/2021)
        [HttpGet("ExportFileExcel")]
        public IActionResult ExportFileExcel()
        {
            var stream = _employeeService.ExportExcel();
            string excelName = $"Danh-sach-nhan-vien.xlsx";

            //return File(stream, "application/octet-stream", excelName);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        #endregion
    }
}
