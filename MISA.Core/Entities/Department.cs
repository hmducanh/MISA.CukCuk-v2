using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    public class Department
    {
        /// <summary>
        /// id của đơn vị ( phòng ban ) trong db
        /// created by : hmducanh (14/5/2021)
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        ///  Tên đơn vị ( phòng ban )
        /// created by : hmducanh (14/5/2021)
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
