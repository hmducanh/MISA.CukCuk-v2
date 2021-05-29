using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.core.Interfaces.Repository
{
    /// <summary>
    /// cac thao tac them voi database cua khach hang
    /// </summary>
    /// CreatedBy : hmducanh (29/04/2021)
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
         /// <summary>
         /// thêm 1 số (2) thao tác với database
         /// </summary>
         /// CreatedBy : hmducanh (29/04/2021)
  
        // kiểm tra trùng mã khách hàng
        public bool CheckCustomerCodeExist(string customerCode);
        // kiểm tra trùng số điện thoại
        public bool CheckPhoneNumberExist(string phoneNumber);
    }
}