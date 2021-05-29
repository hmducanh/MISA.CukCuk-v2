using MISA.Core.AttributeCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Thông tin nhóm khách hàng
    /// </summary>
    /// CreatedBy : hmducanh (29/04/2021)
    public class CustomerGroup
    {
        /// Id nhóm khách hàng
        public Guid CustomerGroupId { get; set; }
        [MISARequired]
        /// Tên nhóm khách hàng
        public string CustomerGroupName { get; set; }
        /*/// Mô tả
        public String Description { get; set; }
        /// Ngày chỉnh sửa gần nhất
        public DateTime? ModifiedDate { get; set; }
        /// Người chỉnh sửa
        public string ModifiedBy { get; set; }*/
    }
}