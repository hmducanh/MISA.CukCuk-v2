using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Service;
using MISA.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Service
{
    /// <summary>
    /// Các dịch riêng cho nhóm khách hàng
    /// </summary>
    /// CreatedBy : hmducanh (29/04/2021)
    public class DepartmentService: BaseService<Department>, IDepartmentService
    {
        #region Field
        private IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// khoi tao
        /// </summary>
        /// <param name="departmentRepository"></param>
        ///  CreatedBy : hmducanh (29/04/2021)
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        #endregion
    }
}
