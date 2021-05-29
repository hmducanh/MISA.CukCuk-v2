using MISA.Core.AttributeCustom;
using MISA.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    /// <summary>
    /// thong tin nhan vien
    /// </summary>
    /// CreatedBy : hmducanh (08/05/2021)
    public class Employee
    {
        /// <summary>
        /// Id nhân viên
        /// </summary>
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MISARequired]
        [MISAMaxLength(20)]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [MISARequired]
        public string FullName { get; set; }
        /// <summary>
        /// Ngày sinh nhân viên
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính nhân viên
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// tên giới tính nhân viên ( 1 - Nam && 2 - Nữ && 3 - Khác )
        /// </summary>
        public string GenderName
        {
            get
            {
                if (Gender == 0)
                    return MISAConstant.Gender_Male;
                else if (Gender == 1)
                    return MISAConstant.Gender_Female;
                else
                    return MISAConstant.Gender_Null;
            }
        }
        /// <summary>
        /// CMND nhân viên
        /// </summary>
        public string IdentifyNumber { get; set; }
        /// <summary>
        /// Ngày cấp CMND nhân viên
        /// </summary>
        public DateTime IdentifyDate { get; set; }
        /// <summary>
        /// Nơi cấp CMND nhân viên
        /// </summary>
        public string IdentifyPlace { get; set; }
        /// <summary>
        /// Chức danh nhân viên
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// Đơn vị nhân viên
        /// </summary>
        [MISARequired]
        public string DepartmentName { get; set; }
        /// <summary>
        /// Địa chỉ nhân viên
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Số đt nhân viên
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Số đt cố định nhân viên
        /// </summary>
        public string ConstantPhoneNumber { get; set; }
        /// <summary>
        /// Email nhân viên
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// số tài khoản ngân hàng nhân viên
        /// </summary>
        public string BankAccount { get; set; }
        /// <summary>
        /// Tên ngân hàng nhân viên
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// Chi nhánh ngân hàng nhân viên
        /// </summary>
        public string BankBranch { get; set; }

    }
}
