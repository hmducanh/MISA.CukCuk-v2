using MISA.core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        IBaseRepository<T> _baseRepository;
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public int Delete(Guid EntityID)
        {
            var rowAffect = _baseRepository.Delete(EntityID);
            return rowAffect;
        }

        public IEnumerable<T> GetAll()
        {
            var customers = _baseRepository.GetAll();
            return customers;
        }

        public T GetById(Guid EntityID)
        {
            var customer = _baseRepository.GetById(EntityID);
            return customer;
        }

        public int Insert(T Entity)
        {
            validate(Entity);
            var rowAffect = _baseRepository.Insert(Entity);
            return rowAffect;
        }
        protected virtual void validate(T Entity) { }
        public int Update(T Entity)
        {
            var rowAffect = _baseRepository.Update(Entity);
            return rowAffect;
        }
    }
}
