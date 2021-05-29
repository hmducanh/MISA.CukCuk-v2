using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.core.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T GetById(Guid EntityId);
        public int Insert(T Entity);
        public int Update(T Entity);
        public int Delete(Guid EntityId);
    }
}