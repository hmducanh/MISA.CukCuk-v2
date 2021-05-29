using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.core.Interfaces.Repository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        public bool CheckCustomerCodeExist(string customerCode);
        public bool CheckPhoneNumberExist(string phoneNumber);
    }
}