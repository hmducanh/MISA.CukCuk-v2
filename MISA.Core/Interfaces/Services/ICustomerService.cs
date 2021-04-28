using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Service
{
    // Các dịch vụ cho khách hàng
    public interface ICustomerService : IBaseService<Customer>
    {
        // moi thu co ban trong IbaseService
    }
}
