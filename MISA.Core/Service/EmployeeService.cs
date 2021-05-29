using MISA.core.Interfaces.Repository;
using MISA.Core.Entities;
using MISA.Core.Enum;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Service;
using MISA.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MISA.Core.Service
{
    /// <summary>
    /// Các dịch vụ riêng cho nhân viên
    /// </summary>
    /// Createdby : hmducanh (15/5/2021)
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Field
        private IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// khoi tao thuc the employeeService
        /// </summary>
        /// <param name="employeeRepository"></param>
        /// Createdby : hmducanh (15/5/2021)
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Method
        /// <summary>
        /// Xu ly trung ma nhan vien
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        /// Createdby : hmducanh (15/5/2021)
        public bool CheckEmployeeCodeExist(string employeeCode)
        {
            // xu ly trung ma nhan vien
            bool res = _employeeRepository.CheckEmployeeCodeExist(employeeCode);
            return res;
        }
        /// <summary>
        /// lấy max mã nhân viên + 1
        /// </summary>
        /// <param name="result">xau ket qua</param>
        /// <param name="res">xau ban dau can chinh</param>
        /// <returns></returns>
        /// Createdby : hmducanh (15/5/2021)
        public string GetMaximumEmployeeCode()
        {
            // xu ly va tra ve ma nhan vien lon nhat +1
            string res = _employeeRepository.GetMaximumEmployeeCode();
            if(res == null)
            {
                return "NV-0001";
            }
            string result = "";
            int pos = res.Length;
            for(int i = res.Length - 1; i > 2; i--)
            {
                if (res[i] == '9')
                    result = "0" + result;
                else
                {
                    pos = i;
                    result = (res[i] - '0' + 1).ToString() + result;
                    break;
                }
            }
            if(pos == res.Length)
            {
                result = "NV-1" + result;
            }
            else
            {
                for (int i = pos - 1; i >= 0; i--)
                    result = res[i] + result;
            }
            return result;
                
        }

        /// <summary>
        /// Lấy list nhân viên theo filter
        /// </summary>
        /// <param name="employeeFilter"></param>
        /// <returns></returns>
        /// Createdby : hmducanh (15/5/2021)
        public Pagging<Employee> GetEmployees(EmployeeFilter employeeFilter)
        {
            var pagging = _employeeRepository.GetEmployees(employeeFilter);
            return pagging;
        }

        /// <summary>
        /// Hàm validate kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name="entity"></param>
        /// Createdby : hmducanh (15/5/2021)
        protected override void CustomValidate(Employee entity)
        {
            var isEmployeeCodeExist = _employeeRepository.CheckEmployeeCodeExist(entity.EmployeeCode);
            if (isEmployeeCodeExist) throw new CustomException("EmployeeCode" + " " + MISAConstant.Dev_Msg_Exist);
        }
        /// <summary>
        /// Xu ly loi trung ma employeeCode khi update 1 nhan vien
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="employeeCode">employeeCode cua thang hien tai duoc nhan tu client</param>
        /// <param name="entity.EmployeeCode">employeeCode cua thang duoc lay theo Id trong database</param>
        /// Createdby : hmducanh (19/5/2021)
        protected override void CheckPutError(Employee entity)
        {
            // lay ma nhan vien theo id
            var employee = _employeeRepository.GetById(entity.EmployeeId);
            var employeeCode = employee.EmployeeCode;
            if (entity.EmployeeCode != employeeCode)
            {
                throw new CustomException("EmployeeCode" + " " + MISAConstant.Dev_Msg_Exist);
            }
        }
        /// <summary>
        /// Ham Xu ly data va chuyen sang file excel de tra ve API
        /// </summary>
        /// <returns></returns>
        /// Created by : hmducanh (18/5/2021)
        public Stream ExportExcel()
        {
            var res = _employeeRepository.GetAll();
            var list = res.ToList();
            var stream = new MemoryStream();
            using var package = new ExcelPackage(stream);
            var workSheet = package.Workbook.Worksheets.Add("DANH SÁCH NHÂN VIÊN");

            // set độ rộng col
            workSheet.Column(1).Width = 5;
            workSheet.Column(2).Width = 15;
            workSheet.Column(3).Width = 25;
            workSheet.Column(4).Width = 12;
            workSheet.Column(5).Width = 15;
            workSheet.Column(6).Width = 20;
            workSheet.Column(7).Width = 20;
            workSheet.Column(8).Width = 25;
            workSheet.Column(9).Width = 20;
            workSheet.Column(10).Width = 15;


            using (var range = workSheet.Cells["A1:J1"])
            {
                range.Merge = true;
                range.Value = "DANH SÁCH NHÂN VIÊN";
                range.Style.Font.Bold = true;
                range.Style.Font.Size = 16;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // style cho excel.
            workSheet.Cells[3, 1].Value = "STT";
            workSheet.Cells[3, 2].Value = "Mã nhân viên";
            workSheet.Cells[3, 3].Value = "Tên nhân viên";
            workSheet.Cells[3, 4].Value = "Giới tính";
            workSheet.Cells[3, 5].Value = "Ngày sinh";
            workSheet.Cells[3, 6].Value = "Số CMND";
            workSheet.Cells[3, 7].Value = "Chức danh";
            workSheet.Cells[3, 8].Value = "Tên đơn vị";
            workSheet.Cells[3, 9].Value = "Số tài khoản";
            workSheet.Cells[3, 10].Value = "Tên ngân hàng";

            using (var range = workSheet.Cells["A3:J3"])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.Font.Bold = true;
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }


            int i = 0;
            // đổ dữ liệu từ list vào.
            foreach (var e in list)
            {
                workSheet.Cells[i + 4, 1].Value = i + 1;
                workSheet.Cells[i + 4, 2].Value = e.EmployeeCode;
                workSheet.Cells[i + 4, 3].Value = e.FullName;
                workSheet.Cells[i + 4, 4].Value = e.GenderName;
                workSheet.Cells[i + 4, 5].Value = e.DateOfBirth.ToString("dd/MM/yyyy");
                workSheet.Cells[i + 4, 6].Value = e.IdentifyNumber;
                workSheet.Cells[i + 4, 7].Value = e.PositionName;
                workSheet.Cells[i + 4, 8].Value = e.DepartmentName;
                workSheet.Cells[i + 4, 9].Value = e.BankAccount;
                workSheet.Cells[i + 4, 10].Value = e.BankName;

                using (var range = workSheet.Cells[i + 4, 1, i + 4, 10])
                {
                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }
                i++;
            }

            package.Save();
            stream.Position = 0;
            return package.Stream;
        }
        #endregion
    }
}