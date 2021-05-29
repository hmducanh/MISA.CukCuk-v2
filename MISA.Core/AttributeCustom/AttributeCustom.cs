using MISA.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;
namespace MISA.Core.AttributeCustom
{
    /// <summary>
    /// Tạo 1 số attribute cho việc validate
    /// created by : hmducanh (14/5/2021)
    /// </summary>
  
     
    
        /// <summary>
        /// Validate bắt buộc nhập
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class MISARequired : Attribute
        {
            public string MsgError = string.Empty;
            public MISARequired()
            {
                MsgError = MISAConstant.Dev_Msg_Require;
            }
        }
        /// <summary>
        /// Validate quá độ dài tối đa
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class MISAMaxLength : Attribute
        {
            public int MaxLength = 0;
            public string MsgError = string.Empty;
            public MISAMaxLength(int maxLength)
            {
                MaxLength = maxLength;
                var builder = new StringBuilder();
                builder.AppendFormat(MISAConstant.Dev_Msg_MaxLength, maxLength);
                MsgError = builder.ToString();
            }
        }
        /// <summary>
        /// Validate đã tồn tại
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class MISAExist : Attribute
        {
            public string MsgError = string.Empty;
            public MISAExist()
            {
                MsgError = MISAConstant.Dev_Msg_Exist;
            }
        }
        /// <summary>
        /// Validate định dạng
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class MISAFormat : Attribute
        {
            public string MsgError = string.Empty;
            public string Regex_Valid = string.Empty;
            public MISAFormat(string regex = "")
            {
                Regex_Valid = regex;
                MsgError = MISAConstant.Dev_Msg_Format;
            }
        }
}
