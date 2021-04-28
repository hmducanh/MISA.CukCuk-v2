using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Exceptions
{
    public class CustomerException : Exception
    {
        public CustomerException(string msg) : base(msg)
        {
            this.Data.Add("sunbae_mess", "loi do data gui len");
        }
    }
}