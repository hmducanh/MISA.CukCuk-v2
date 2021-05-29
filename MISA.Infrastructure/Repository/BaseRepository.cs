using Dapper;
using MISA.core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
        public class BaseRepository<T> : IBaseRepository<T> where T : class
        {
            protected string connectionString = "" +
                "Host = 47.241.69.179;" +
                "Port=3306;" +
                "User Id = dev;" +
                "Password = 12345678;" +
                "Database =  MF0_NVManh_CukCuk02;" +
                "ConvertZeroDateTime=True";
            protected IDbConnection dbConnection;
            protected string tableName = typeof(T).Name;

            public int Delete(Guid entityId)
            {
                using (dbConnection = new MySqlConnection(connectionString))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add($"@{tableName}Id", entityId);
                    var rowsAffect = dbConnection.Execute($"Proc_Delete{tableName}ById", param: parameters, commandType: CommandType.StoredProcedure);
                    return rowsAffect;
                }
            }

            public IEnumerable<T> GetAll()
            {
                using (dbConnection = new MySqlConnection(connectionString))
                {
                    var entities = dbConnection.Query<T>($"Proc_Get{tableName}s", commandType: CommandType.StoredProcedure);
                    return entities;
                }
            }

            public T GetById(Guid entityId)
            {
                using (dbConnection = new MySqlConnection(connectionString))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add($"@{tableName}Id", entityId);
                    var entity = dbConnection.QueryFirstOrDefault<T>($"Proc_Get{tableName}ById", param: parameters, commandType: CommandType.StoredProcedure);
                    return entity;
                }
            }

            public int Insert(T entity)
            {
                using (dbConnection = new MySqlConnection(connectionString))
                {
                    var rowsAffect = dbConnection.Execute($"Proc_Insert{tableName}", param: entity, commandType: CommandType.StoredProcedure);
                    return rowsAffect;
                }
            }

            public int Update(T entity)
            {
                using (dbConnection = new MySqlConnection(connectionString))
                {
                    var rowsAffect = dbConnection.Execute($"Proc_Insert{tableName}", param: entity, commandType: CommandType.StoredProcedure);
                    return rowsAffect;
                }
            }
        }
}
