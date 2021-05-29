using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;

namespace MISA.Infrastructure.Repository
{
    /// <summary>
    /// Những thao tác chung với db
    /// </summary>
    /// CreatedBy : hmducanh (29/04/2021)
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity> where MISAEntity : class
    {

        #region Setup
        IConfiguration configuration;
        //chuỗi kết nối DB 
        protected string connectionString = "" +
                "Host = 47.241.69.179;" +
                "Port=3306;" +
                "User Id = dev;" +
                "Password = 12345678;" +
                "Database =  MF822_Import_KDLong;" +
                "ConvertZeroDateTime=True";
        // Lấy tên đối tượng MISAEntity
        string tableName = typeof(MISAEntity).Name;
        protected IDbConnection dbConnection;
        #endregion

        #region methods
        /// <summary>
        ///  lấy tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MISAEntity> GetAll()
        {
            //Kết nối DB 
            using (dbConnection = new MySqlConnection(connectionString))
            {
                //Thực thi với DB 
                var sqlCommand = $"Proc_Get{tableName}s";
                var customers = dbConnection.Query<MISAEntity>(sqlCommand, commandType: CommandType.StoredProcedure);
                return customers;
            }
        }
        /// <summary>
        /// lấy dữ liệu theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public MISAEntity GetById(Guid entityId)
        {
            // Kết nối DB 
            using (dbConnection = new MySqlConnection(connectionString))
            {
                //3. Thực thi với DB (Thêm , Sửa xóa)
                var sqlCommand = $"Proc_Get{tableName}ById";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{tableName}Id", entityId);
                var entity = dbConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return entity;
            }
        }
        /// <summary>
        ///  thêm 1 thực thể
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(MISAEntity entity)
        {
            // Kêt nối DB 
            using (dbConnection = new MySqlConnection(connectionString))
            {
                // số dòng bị thay đổi 
                var rowsAffect = dbConnection.Execute($"Proc_Insert{tableName}", param: entity, commandType: CommandType.StoredProcedure);
                return rowsAffect;
            }
        }
        /// <summary>
        ///  cập nhật 1 thực thể
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(MISAEntity entity)
        {
            // Kết nối DB
            using (dbConnection = new MySqlConnection(connectionString))
            {
                // số dòng bị thay đổi  
                var sqlCommand = $"Proc_Update{tableName}";
                var rowAffects = dbConnection.Execute(sqlCommand, param: entity, commandType: CommandType.StoredProcedure);
                return rowAffects;
            }
        }
        /// <summary>
        /// xóa một thực thể
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public int Delete(Guid entityId)
        {
            // Kết nối DB 
            using (dbConnection = new MySqlConnection(connectionString))
            {
                // 3. Thực thi hành động
                var sqlCommand = $"Proc_Delete{tableName}";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{tableName}Id", entityId);
                var rowsAffects = dbConnection.Execute(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return rowsAffects;
            }
        }
        #endregion

    }
}