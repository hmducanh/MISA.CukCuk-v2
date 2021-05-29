using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Endpoint filter
    /// </summary>
    /// CreatedBy hmducanh ( 07/05/2021 )
    public class EmployeeFilter
    {
        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int Page { get; set; } = 1;
        /// <summary>
        /// Tổng số bản ghi trên 1 trang
        /// </summary>
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// Dữ liệu dùng để filter
        /// </summary>
        public string Search { get; set; } = "";
    }
}