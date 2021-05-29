using MISA.core.Interfaces.Repository;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Service;
using MISA.Core.Services;
using System;
using System.Collections.Generic;


namespace MISA.Core.Service
{
    /// <summary>
    /// Các dịch riêng cho khách hàng
    /// </summary>
    /// CreatedBy : hmducanh (29/04/2021)
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        #region Field
        private ICustomerRepository _customerRepository;
        #endregion

        #region Constructor
        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #endregion


    }
}