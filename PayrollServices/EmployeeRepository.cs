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
            //opening SQL connection
            this.sqlConnection.Open();
            //create the query to display data
            string query = @"select * from dbo.employee_payroll";
            //create object for employee detail class
            EmployeeDetail employee = new EmployeeDetail();
            try
            {
                //create the sql command object nd pass the querry and connection
                SqlCommand command = new SqlCommand(query, sqlConnection);
                //create data reader 
                SqlDataReader reader = command.ExecuteReader();
                //if it has data
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //store each data in the employee details properties 
                        employee.employeeId = Convert.ToInt32(reader["id"]);
                        employee.employeeName = reader["name"].ToString();
                        employee.gender = reader["gender"].ToString();
                        employee.startDate = reader.GetDateTime(2);
                        employee.phoneNumber = Convert.ToDouble(reader["phoneNumber"]);
                        employee.address = reader.GetString(5);
                        employee.department = reader.GetString(6);
                        //display the result
                        Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} ", employee.employeeId, employee.employeeName, employee.gender, employee.startDate, employee.phoneNumber, employee.address, employee.department);
                    }
                }
                else
                {
                    Console.WriteLine("No data vailable");
                }
                reader.Close();
            }
            //if any exception occurs catch and display exception message
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //finally close the connection
            finally
            {
                this.sqlConnection.Close();
            }
        }

    }
    
}
