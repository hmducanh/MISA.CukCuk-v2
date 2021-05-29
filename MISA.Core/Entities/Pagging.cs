using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Dữ liệu phân trang
    /// </summary>
    /// Created by Hmducanh ( 07/05/2021 )
    public class Pagging<MISAEntity>
    {
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalRecords { get; set; }
        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Dữ liệu
        /// </summary>
        public IEnumerable<MISAEntity> Data { get; set; }
        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// Số bản ghi trong 1 trang
        /// </summary>
        public int PageSize { get; set; }
    }
}