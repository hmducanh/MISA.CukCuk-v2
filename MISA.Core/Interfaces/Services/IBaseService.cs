using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface IBaseService<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T GetById(Guid EntityId);
        public int Insert(T Entity);
        public int Update(T Entity);
        public int Delete(Guid EntityId);
    }
}
