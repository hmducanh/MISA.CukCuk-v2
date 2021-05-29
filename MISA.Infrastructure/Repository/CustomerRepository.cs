using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySqlConnector;
using Dapper;
using MISA.core.Interfaces.Repository;

namespace MISA.Infrastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        public bool CheckPhoneNumberExist(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public int Delete(Guid customerId)
        {
            string connectionString = "" +
                "Host = 47.241.69.179;" +
                "Port=3306;" +
                "User Id = dev;" +
                "Password = 12345678;" +
                "Database =  MF0_NVManh_CukCuk02;" +
                "ConvertZeroDateTime=True";
            // khoi tao ket noi
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            // Thao tac voi db
            DynamicParameters Param = new DynamicParameters();
            Param.Add("@CustomerId", customerId);
            var rowAffect = dbConnection.Execute("Proc_DeleteCustomerById", param: Param, commandType: CommandType.StoredProcedure);
            return rowAffect;
        }

        public IEnumerable<Customer> GetAll()

        {
            // khai bao thong tin ket noi toi db
            string connectionString = "" +
                "Host = 47.241.69.179;" +
                "Port=3306;" +
                "User Id = dev;" +
                "Password = 12345678;" +
                "Database =  MF0_NVManh_CukCuk02;" +
                "ConvertZeroDateTime=True";
            // khoi tao ket noi
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            // tuong tac toi db
            var customers = dbConnection.Query<Customer>("SELECT * FROM Customer;");
            return customers;
        }

        public Customer GetById(Guid customerId)
        {
            string connectionString = "" +
                "Host = 47.241.69.179;" +
                "Port=3306;" +
                "User Id = dev;" +
                "Password = 12345678;" +
                "Database =  MF0_NVManh_CukCuk02;" +
                "ConvertZeroDateTime=True";
            // khoi tao ket noi
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            // Thao tac voi db
            DynamicParameters Param = new DynamicParameters();
            Param.Add("@CustomerId", customerId);
            var customer = dbConnection.QueryFirstOrDefault<Customer>("Proc_GetCustomerById", param: Param, commandType: CommandType.StoredProcedure);
            return customer;
        }

        public int Insert(Customer customer)
        {
            string connectionString = "" +
                "Host = 47.241.69.179;" +
                "Port=3306;" +
                "User Id = dev;" +
                "Password = 12345678;" +
                "Database =  MF0_NVManh_CukCuk02;" +
                "ConvertZeroDateTime=True";
            // khoi tao ket noi
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            // Thao tac voi db
            var rowAffect = dbConnection.Execute("Proc_InsertCustomer", param: customer, commandType: CommandType.StoredProcedure);
            return rowAffect;
        }

        public int Update(Customer customer)
        {
            string connectionString = "" +
                "Host = 47.241.69.179;" +
                "Port=3306;" +
                "User Id = dev;" +
                "Password = 12345678;" +
                "Database =  MF0_NVManh_CukCuk02;" +
                "ConvertZeroDateTime=True";
            // khoi tao ket noi
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            // Thao tac voi db
            var rowAffect = dbConnection.Execute("Proc_UpdateCustomer", param: customer, commandType: CommandType.StoredProcedure);
            return rowAffect;
        }

        public bool CheckCustomerCodeExist(string customerCode)
        {
            string connectionString = "" +
                "Host = 47.241.69.179;" +
                "Port=3306;" +
                "User Id = dev;" +
                "Password = 12345678;" +
                "Database =  MF0_NVManh_CukCuk02;" +
                "ConvertZeroDateTime=True";
            // khoi tao ket noi
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            // Thao tac voi db
            DynamicParameters Param = new DynamicParameters();
            Param.Add("@m_CustomerCode", customerCode);
            var res = dbConnection.ExecuteScalar<bool>($"Proc_CheckCustomerCodeExists", param: Param, commandType: CommandType.StoredProcedure);
            return res;
        }
    }
}
