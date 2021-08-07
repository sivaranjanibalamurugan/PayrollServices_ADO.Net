using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollServices
{
    class DepartmentDetails
    {
        public int departmentId;
        public string department;
        public DepartmentDetails(int departmentId, string department)
        {
            this.departmentId = departmentId;
            this.department = department;
        }
    }
}
