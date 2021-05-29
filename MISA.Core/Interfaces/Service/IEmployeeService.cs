using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MISA.Core.Interfaces.Service
{
    /// <summary>
    /// cac dich vu cho nhan vien
    /// </summary>
    /// CreatedBy : hmducanh (08/05/2021)
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// dich vu kiem tra trung ma nhan vien
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        ///  CreatedBy : hmducanh (08/05/2021)
        public bool CheckEmployeeCodeExist(string employeeCode);
        /// <summary>
        /// dich vu lay ma nhan vien max + 1
        /// </summary>
        /// <returns></returns>
        ///  CreatedBy : hmducanh (08/05/2021)
        public string GetMaximumEmployeeCode();
        /// <summary>
        /// dich vu phan trang
        /// </summary>
        /// <param name="employeeFilter"></param>
        /// <returns></returns>
        ///  CreatedBy : hmducanh (08/05/2021)
        public Pagging<Employee> GetEmployees(EmployeeFilter employeeFilter);
        /// <summary>
        /// dich vu tao ra file excel va truyen data vao
        /// </summary>
        /// <returns></returns>
        /// created by : hmducanh (18/5/2021)
        public Stream ExportExcel();
    }
}
