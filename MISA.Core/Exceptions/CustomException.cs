using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Exceptions
{
    public class CustomException : Exception
    {
        /// <summary>
        /// Trả về lỗi do mình định nghĩa
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        public CustomException (string msg) : base(msg)
        {
            this.Data.Add("dev_hmducanh_mess", "loi data do server gui len");
        }
    }
}
