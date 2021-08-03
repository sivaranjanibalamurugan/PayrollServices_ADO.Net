using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollServices
{
    class EmployeeRepository
    {
        public static string connectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=Payroll_services";      
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public void GetAllData()
        {

        }
    }
}
