using MISA.core.Interfaces.Repository;
using MISA.Core.Entities;
using MISA.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Service
{
    /// <summary>
    /// Cac dich vu them voi nhom khach hang
    /// </summary>
    /// CreatedBy : hmducanh (29/04/2021)
    public class CustomerGroupService : BaseService<CustomerGroup>, ICustomerGroupService
    {
        #region Field
        /// <summary>
        /// 
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        ICustomerGroupRepository _customerGroupRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerGroupRepository"></param>
        ///  CreatedBy : hmducanh (29/04/2021)
        public CustomerGroupService(ICustomerGroupRepository customerGroupRepository) : base(customerGroupRepository)
        {
            _customerGroupRepository = customerGroupRepository;
        }
        #endregion

    }
}