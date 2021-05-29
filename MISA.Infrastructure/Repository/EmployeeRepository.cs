using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.Infrastructure.Repository
{
    /// <summary>
    /// thao tac voi database va co nhung dich vu rieng
    /// </summary>
    /// CreatedBy : hmducanh (08/05/2021)
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// kiểm tra trùng mã nhân viên trong db
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        public bool CheckEmployeeCodeExist(string employeeCode)
        {
            // kiem tra trung ma nhan vien
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = $"Proc_CheckEmployeeCodeExist";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("EmployeeCode", employeeCode);
                var EmployeeCodeExist = dbConnection.ExecuteScalar<bool>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return EmployeeCodeExist;
            }
        }
        /// <summary>
        /// lấy max mã nhân viên trong db
        /// </summary>
        /// <returns></returns>
        public string GetMaximumEmployeeCode()
        {
            // lay ma nhan vien lon nhan
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "Proc_GetMaxEmployeeCode";
                string MaxEmployeeCode = dbConnection.QueryFirstOrDefault<string>(sqlCommand, commandType: CommandType.StoredProcedure);
                return MaxEmployeeCode;
            }
        }
        /// <summary>
        /// lấy danh sách data theo filter trong db
        /// </summary>
        /// <param name="employeeFilter"></param>
        /// <returns></returns>
        public Pagging<Employee> GetEmployees(EmployeeFilter employeeFilter)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Search", employeeFilter.Search);

                var totalRecords = dbConnection.QueryFirstOrDefault<int>("Proc_GetTotalEmployees", param: parameters, commandType: CommandType.StoredProcedure);

                var totalPages = Math.Ceiling((decimal)totalRecords / employeeFilter.PageSize);

                var employees = dbConnection.Query<Employee>("Proc_GetEmployeesFilter", param: employeeFilter, commandType: CommandType.StoredProcedure);

                // Dữ liệu pagging 
                var paging = new Pagging<Employee>()
                {
                    TotalRecords = totalRecords,
                    TotalPages = (int)totalPages,
                    Data = employees,
                    PageIndex = employeeFilter.Page,
                    PageSize = employeeFilter.PageSize
                };
                return paging;
            }
        }
    }
}
