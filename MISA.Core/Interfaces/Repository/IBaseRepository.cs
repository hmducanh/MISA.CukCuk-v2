using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.core.Interfaces.Repository
{
    /// <summary>
    /// Base của Repository - các thao tác với database chung 
    /// </summary>
    /// CreatedBy : hmducanh (29/04/2021)
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Phương thức ket noi voi database de get tất cả data
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        public IEnumerable<T> GetAll();
        /// <summary>
        /// Phương thức ket noi voi database de get data bằng id
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        public T GetById(Guid EntityId);
        /// <summary>
        /// Phương thức ket noi voi database de thêm 1 entity
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        public int Insert(T Entity);
        /// <summary>
        /// Phương thức ket noi voi database de cập nhật 1 entity
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        public int Update(T Entity);
        /// <summary>
        /// Phương thức ket noi voi database de xóa entity
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        public int Delete(Guid EntityId);
    }
}