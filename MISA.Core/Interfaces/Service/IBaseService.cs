using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{

    /// <summary>
    /// Ha interface lon nhat quan ly tat ca dich vu
    /// </summary>
    /// created by : hmducanh (30/4/2021)
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// dịch vụ lấy tất cả nhân viên
        /// </summary>
        /// <returns></returns>
        ///  created by : hmducanh (30/4/2021)
        public IEnumerable<T> GetAll();
        /// <summary>
        /// dịch vụ lấy nhân viên theo id
        /// </summary>
        /// <param name="EntityId"></param>
        /// <returns></returns>
        ///  created by : hmducanh (30/4/2021)
        public T GetById(Guid EntityId);
        /// <summary>
        /// dịch vụ thêm 1 nhân viên
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        ///  created by : hmducanh (30/4/2021)
        public int Insert(T Entity);
        /// <summary>
        /// dịch vụ cập nhật 1 nhân viên
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        ///  created by : hmducanh (30/4/2021)
        public int Update(T Entity);
        /// <summary>
        /// dịch vụ xóa một nhân viên
        /// </summary>
        /// <param name="EntityId"></param>
        /// <returns></returns>
        ///  created by : hmducanh (30/4/2021)
        public int Delete(Guid EntityId);
    }
}