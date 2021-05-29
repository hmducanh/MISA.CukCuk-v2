using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    public class UserInfo
    {
        public string CustomerCode { get; set; }
        public string FullName { get; set; }
        public string MemberCardCode { get; set; }
        public string CustomerGroupName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CompanyName { get; set; }
        public string TaxCode  { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
    }

    public class DemoResponse<T>
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }

        public static DemoResponse<T> GetResult(int code, string msg, T data = default(T))
        {
            return new DemoResponse<T>
            {
                Code = code,
                Msg = msg,
                Data = data
            };
        }

        public static DemoResponse<T> GetFailResult(int code, string msg)
        {
            return new DemoResponse<T>
            {
                Code = code,
                Msg = msg
            };
        }
    }
}
