using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enum
{
    /// <summary>
    /// Hằng số lấy trong resource
    /// </summary>
    /// CreatedBy: hmducanh ( 05/05/2021 )
    public class MISAConstant
    {
        // không được phép để trống!
        public static string Dev_Msg_Require = Properties.Resources.Dev_Msg_Require;
        // sai định dạng!
        public static string Dev_Msg_Format = Properties.Resources.Dev_Msg_Format;
        // vượt quá độ dài!
        public static string Dev_Msg_MaxLength = Properties.Resources.Dev_Msg_MaxLength;
        // đã tồn tại trong hệ thống
        public static string Dev_Msg_Exist = Properties.Resources.Dev_Msg_Exist;
        // Có lỗi sảy ra vui lòng liên hệ MISA!
        public static string User_Msg_Error = Properties.Resources.User_Msg_Error;
        // Sửa thành công!
        public static string Update_Success = Properties.Resources.Update_Success;
        // Sửa thất bại!
        public static string Update_Fail = Properties.Resources.Update_Fail;
        // Xóa thành công!
        public static string Delete_Success = Properties.Resources.Delete_Success;
        // Xóa thất bại!
        public static string Delete_Fail = Properties.Resources.Delete_Fail;
        // Nam
        public static string Gender_Male = Properties.Resources.Gender_Male;
        // Nữ
        public static string Gender_Female = Properties.Resources.Gender_Female;
        // Khác
        public static string Gender_Other = Properties.Resources.Gender_Other;
        // Không xác định
        public static string Gender_Null = Properties.Resources.Gender_Null;
    }
}