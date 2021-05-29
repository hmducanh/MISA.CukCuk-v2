using MISA.core.Interfaces.Repository;
using MISA.Core.AttributeCustom;
using MISA.Core.Exceptions;
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
        #region Field
        IBaseRepository<T> _baseRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// chi tiết các dịch vụ được cung cấp
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        #endregion

        #region Method
        /// <summary>
        /// xóa 1 thực thể
        /// </summary>
        /// <param name="EntityID"></param>
        /// <returns></returns>
        /// CreatedBy : hmducanh (29/04/2021)
        public int Delete(Guid EntityID)
        {
            var rowAffect = _baseRepository.Delete(EntityID);
            return rowAffect;
        }
        /// <summary>
        /// lấy tất cả thực thể
        /// </summary>
        /// <returns></returns>
        /// CreatedBy : hmducanh (29/04/2021)
        public IEnumerable<T> GetAll()
        {
            var customers = _baseRepository.GetAll();
            return customers;
        }
        /// <summary>
        /// lấy thực thể theo id
        /// </summary>
        /// <param name="EntityID"></param>
        /// <returns></returns>
        /// CreatedBy : hmducanh (29/04/2021)
        public T GetById(Guid EntityID)
        {
            var customer = _baseRepository.GetById(EntityID);
            return customer;
        }
        /// <summary>
        /// kiểm tra dữ liệu
        /// </summary>
        /// <param name="Entity">Thuc the can kiem tra</param>
        /// <param name="properties">Cac tag duoc gan attributes</param>
        /// <param name="property">1 trong cac tag can kiem tra</param>
        /// <param name="propertyValue">Gia tri cua tag do</param>
        /// <param name="msgError">Noi dung loi</param>
        /// CreatedBy : hmducanh (29/04/2021)
        private void Validate(T Entity)
        {
            // lay ra tat ca cac property cua class Entity
                var properties = typeof(T).GetProperties();
            // tim property can thuc hien ktra not null or empty
            foreach (var property in properties)
            {
                // check trong
                var requiredProperties = property.GetCustomAttributes(typeof(MISARequired), true);
                // neu co 
                if (requiredProperties.Length > 0)
                {
                    var propertyValue = property.GetValue(Entity);
                    // kiem tra rong
                    if (string.IsNullOrEmpty(propertyValue.ToString()))
                    {
                        // lay ten loi
                        var msgError = (requiredProperties[0] as MISARequired).MsgError;
                        // kiem tra gia tri
                        if(!string.IsNullOrEmpty(msgError))
                        {
                            throw new CustomException(property.Name + " " + msgError);
                        }
                    }
                }
                //Check max length
                var maxLengthProperties = property.GetCustomAttributes(typeof(MISAMaxLength), true);
                if (maxLengthProperties.Length > 0)
                {
                    //Lấy giá trị
                    var propertyValue = property.GetValue(Entity);
                    var maxLength = (maxLengthProperties[0] as MISAMaxLength).MaxLength;
                    //Kiểm tra giá trị
                    if (propertyValue.ToString().Length > maxLength)
                    {
                        var msgError = (maxLengthProperties[0] as MISAMaxLength).MsgError;
                        throw new CustomException(property.Name + " " + msgError);
                    }
                }

            }
        }
        /// <summary>
        /// thêm 1 thực thể
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        /// CreatedBy : hmducanh (29/04/2021)
        public int Insert(T Entity)
        {
            Validate(Entity);
            CustomValidate(Entity);
            var rowAffect = _baseRepository.Insert(Entity);
            return rowAffect;
        }

        /// <summary>
        /// cập nhật dữ liệu ( thực thể )
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        /// CreatedBy : hmducanh (29/04/2021)
        public int Update(T Entity)
        {
            CheckPutError(Entity);
            var rowAffect = _baseRepository.Update(Entity);
            return rowAffect;
        }
        /// <summary>
        /// Hàm validate tự do thằng con thêm
        /// </summary>
        /// <param name="Entity"></param>
        /// Createdby : hmducanh (15/5/2021)
        protected virtual void CustomValidate(T Entity)
        {

        }
        /// <summary>
        /// Hàm validate trong trường hợp sửa dữ liệu nhưng lại sửa mã thành bị trùng
        /// </summary>
        /// <param name="Entity"></param>
        /// Createdby : hmducanh (15/5/2021)
        protected virtual void CheckPutError(T Entity)
        {

        }
        #endregion
    }
}