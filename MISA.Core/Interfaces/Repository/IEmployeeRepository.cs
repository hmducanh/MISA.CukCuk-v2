    using MISA.core.Interfaces.Repository;
using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Repository
{
    /// <summary>
    /// Cac thao tac them voi database cua khach hang
    /// </summary>
    /// <returns></returns>
    /// created by : hmducanh (16/05/2021)
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        ///  created by : hmducanh (16/05/2021)
        public bool CheckEmployeeCodeExist(string customerCode);
        /// <summary>
        /// lấy mã nhân viên lớn nhất + 1
        /// </summary>
        /// <returns></returns>
        ///  created by : hmducanh (16/05/2021)
        public string GetMaximumEmployeeCode();
        /// <summary>
        /// lấy list nhân viên theo filter
        /// </summary>
        /// <param name="employeeFilter"></param>
        /// <returns></returns>
        ///  created by : hmducanh (16/05/2021)
        public Pagging<Employee> GetEmployees(EmployeeFilter employeeFilter);
    }
}
