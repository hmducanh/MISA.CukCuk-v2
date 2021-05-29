using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MySqlConnector;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.CukCuk.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    /// <summary>
    /// Xay dung API cho phep kiem tra va nhap khau du lieu
    /// </summary>
    /// CreatedBy : hmducanh (06/05/2021)
    public class UserExcel : ControllerBase
    {
        [HttpPost("import")]
        public async Task<DemoResponse<List<UserInfo>>> Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            // file dau vao khong thoa man
            if (formFile == null || formFile.Length <= 0)
            {
                return DemoResponse<List<UserInfo>>.GetFailResult(-1, "formfile is empty");
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return DemoResponse<List<UserInfo>>.GetFailResult(-1, "Not Support file extension");
            }

            var list = new List<UserInfo>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 3; row <= rowCount; row++)
                    {
                        // format ngay thang nam
                        var dateOfBirth = (worksheet.Cells[row, 6].Value == null) ? "" : ((worksheet.Cells[row, 6].Value.GetType() == typeof(double)) ? worksheet.Cells[row, 6].Value.ToString().Trim() : (string)worksheet.Cells[row, 6].Value);
                        var DOB = FormatDate(dateOfBirth);
                        // tao entity
                        UserInfo _userInfo = new UserInfo
                        {
                            CustomerCode = (string)worksheet.Cells[row, 1].Value,
                            FullName = (string)worksheet.Cells[row, 2].Value,
                            MemberCardCode = (string)worksheet.Cells[row, 3].Value,
                            CustomerGroupName = (string)worksheet.Cells[row, 4].Value,
                            PhoneNumber = (worksheet.Cells[row, 5].Value == null) ? "" : worksheet.Cells[row, 5].Value.ToString().Trim(),
                            DateOfBirth = DOB,
                            CompanyName = (string)worksheet.Cells[row, 7].Value,
                            TaxCode = (worksheet.Cells[row, 8].Value == null) ? "" : worksheet.Cells[row, 8].Value.ToString().Trim(),
                            Email = (string)worksheet.Cells[row, 9].Value,
                            Address = (string)worksheet.Cells[row, 10].Value,
                            Note = (string)worksheet.Cells[row, 11].Value,
                        };
                        // ktra du lieu
                        string error_PhoneNumberExist = $"Trung ma so dien thoai {_userInfo.PhoneNumber} trong File Excel";
                        string error_CustomerCodeExist = $"Trung ma khach hang {_userInfo.CustomerCode} trong File Excel";
                        string error_NullCustomerCode = "Ma nhan vien khong duoc de trong";
                        // ktra trung ma so dien thoai
                        if (checkPhoneNumberExistInFileExcel(_userInfo, list) == true)
                        {
                            _userInfo.Status = _userInfo.Status + error_PhoneNumberExist + " ";
                        }
                        // ktra ma nhan vien bo trong
                        if(string.IsNullOrEmpty(_userInfo.CustomerCode) == true)
                        {
                            _userInfo.Status = _userInfo.Status + error_NullCustomerCode + " ";
                        }
                        // ktra trung ma nhan vien
                        if(checkCustomerCodeExistInFileExcel(_userInfo, list) == true)
                        {
                            _userInfo.Status = _userInfo.Status + error_CustomerCodeExist + " ";
                        }
                        // them nhan vien hop le vao list
                        list.Add(_userInfo);
                    }
                }
            }

            return DemoResponse<List<UserInfo>>.GetResult(0, "OK", list);
        }
        // string connect
        private string connectionString = "" +
                "Host = 47.241.69.179;" +
                "Port = 3306;" +
                "User Id = dev;" +
                "Password = 12345678;" +
                "Database =  MF823_Import_HDANH;" +
                "ConvertZeroDateTime=True";
        private IDbConnection dbConnection;

        private bool checkPhoneNumberExistInDB(UserInfo userInfo)
        {
            // kiem tra so dien thoai co trong database hay khong
            using (dbConnection = new MySqlConnection(connectionString))
            {
                //Thực thi với DB 
                var sqlCommand = $"Proc_CheckPhoneNumberExist";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("PhoneNumber", userInfo.PhoneNumber);
                var checkPhoneNumber = dbConnection.ExecuteScalar<bool>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return checkPhoneNumber;
            }
        }

        private bool checkPhoneNumberExistInFileExcel(UserInfo userInfo, List<UserInfo> LU)
        {
            // kiem tra so dien thoai co o trong file excel vua nhap vao hay khong
            for(int i = 0; i < LU.Count; i++)
            {
                if(LU[i].CustomerCode != userInfo.CustomerCode && LU[i].PhoneNumber == userInfo.PhoneNumber)
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkCustomerCodeExistInDB(UserInfo userInfo)
        {
            // kiem tra ma khach hang co trong database hay khong
            using (dbConnection = new MySqlConnection(connectionString))
            {
                //Thực thi với DB 
                var sqlCommand = $"Proc_CheckCustomerCodeExist";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("CustomerCode", userInfo.PhoneNumber);
                var checkCustomerCode = dbConnection.ExecuteScalar<bool>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return checkCustomerCode;
            }
        }

        private bool checkCustomerCodeExistInFileExcel(UserInfo userInfo, List<UserInfo> LU)
        {
            // kiem tra ma khach hang co o trong file excel vua nhap vao hay khong
            int cnt = 0;
            for(int i = 0; i < LU.Count; i++)
            {
                if (LU[i].CustomerCode == userInfo.CustomerCode)
                    cnt++;
            }
            return (cnt > 1);
        }

        private DateTime FormatDate(string date)
        {
            // format ngay thang nam
            DateTime res = new DateTime();
            // ngay thang nam
            Regex rg1 = new Regex(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$");
            // thang nam
            Regex rg2 = new Regex(@"^(0[1-9]{1}|1[0-2]{1})/\d{4}$");
            // nam
            Regex rg3 = new Regex(@"\d{4}$");
            if (rg1.IsMatch(date))
            {
                DateTime res1 = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                return res1;
            }
            else if (rg2.IsMatch(date))
            {
                string newDate = "01/" + date;
                DateTime res1 = DateTime.ParseExact(newDate, "dd/MM/yyyy", null);
                return res1;
            }
            else if (rg3.IsMatch(date))
            {
                string newDate = "01/01/" + date;
                DateTime res1 = DateTime.ParseExact(newDate, "dd/MM/yyyy", null);
                return res1;
            }
            return res;
        }

        private bool checkCustomerGroupNameExistInDB(UserInfo userInfo)
        {
            // check xem Nhom khach hang co thoa man khong
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = $"Proc_CheckCustomerGroupName";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("CustomerGroupName", userInfo.CustomerGroupName);
                var checkCustomerGroupName = dbConnection.ExecuteScalar<bool>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return checkCustomerGroupName;
            }
        }

        private bool Validate(UserInfo userInfo)
        {
            // ktra chung cho input dau vao them 1 nhan vien  
            // trung so dien thoai
            if(checkPhoneNumberExistInDB(userInfo) == false)
            {
                throw new Exception("Trung so dien thoai da ton tai trong database");
            }
            // trung ma nhan vien
            else if (checkCustomerCodeExistInDB(userInfo) == false)
            {
                throw new Exception("Trung ma nhan vien da ton tai trong database");
            }
            // trung ma khach hang
            else if (checkCustomerGroupNameExistInDB(userInfo) == false)
            {
                throw new Exception("Trung nhom khach hang da ton tai trong database");
            }
            return false;
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserInfo userInfo)
        {
            if(Validate(userInfo))
            {
                // thong tin nhan vien thoa man
                using (dbConnection = new MySqlConnection(connectionString))
                {
                    //Thực thi với DB + them nhan vien
                    var sqlCommand = $"Proc_InsertCustomer";
                    var rowAffect = dbConnection.Execute(sqlCommand, param: userInfo, commandType: CommandType.StoredProcedure);
                    return StatusCode(201, rowAffect);
                }
            }
            else
            {
                return NoContent();
            }
        }
    }
}