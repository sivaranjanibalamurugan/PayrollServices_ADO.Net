using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollServices
{
   public class EmployeeRepository
    {
        List<EmployeeDetail> employeeList = new List<EmployeeDetail>();
        /// Creating the connection
        public static string connectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=Payroll_services";
        //creating the object for sql connection class
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public List<EmployeeDetail> GetAllData()
        {
            //opening the sql connection
            sqlConnection.Open();
            //create the query to display data
            string query = @"select * from employee_payroll";
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
                        employeeList.Add(employee);
                    }
                    reader.Close();
                    return employeeList;
                }
                else
                {
                    reader.Close();
                    return employeeList;
                }

            }
            //if any exception occurs catch and display exception message
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            //finally close the connection
            finally
            {
                sqlConnection.Close();
            }
        }
        public int UpdateSalary()
        {
            //assigning the details which has to be updated
            EmployeeDetail employee = new EmployeeDetail();
            employee.employeeName = "Terissa";
            employee.employeeId = 3;
            employee.basicPay = 3000012;
            using (sqlConnection)
                try
                {
                    //passing query in terms of stored procedure
                    SqlCommand sqlCommand = new SqlCommand("dbo.UpadateSalaryPayrollemployee_payroll", sqlConnection);
                    //passing command type as stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    //adding the parameter to the strored procedure
                    sqlCommand.Parameters.AddWithValue("@id", employee.employeeId);
                    sqlCommand.Parameters.AddWithValue("@name", employee.employeeName);
                    sqlCommand.Parameters.AddWithValue("@basicPay", employee.basicPay);
                    //checking the result 
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        Console.WriteLine("Salary is updated");
                    else
                        Console.WriteLine("Updation failed");
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
        }
        public List<EmployeeDetail> DisplayDataBasedOnDate(DateTime startdate, DateTime dateTime)
        {
            EmployeeDetail employee = new EmployeeDetail();


            using (sqlConnection)
                try
                {
                    //passing query in terms of stored procedure
                    SqlCommand sqlCommand = new SqlCommand("dbo.RetriveData", sqlConnection);
                    //passing command type as stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    //adding the parameter to the strored procedure
                    sqlCommand.Parameters.AddWithValue("@startDate", startdate);
                    sqlCommand.Parameters.AddWithValue("@endDate", dateTime);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
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
                            employeeList.Add(employee);
                        }
                        reader.Close();
                        return employeeList;
                    }
                    else
                    {
                        reader.Close();
                        return employeeList;
                    }

                }
                //if any exception occurs catch and display exception message
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                //finally close the connection
                finally
                {
                    sqlConnection.Close();
                }
        }

        public int Aggregate()
        {

            //create object for employee detail class
            EmployeeDetail employee = new EmployeeDetail();
            using (sqlConnection)
                try
                {
                    //passing query in terms of stored procedure
                    SqlCommand sqlCommand = new SqlCommand("dbo.Aggregate", sqlConnection);
                    //passing command type as stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    //adding the parameter to the strored procedure
                    sqlCommand.Parameters.AddWithValue("@NetPay", employee.NetPay);
                    sqlCommand.Parameters.AddWithValue("@gender", employee.gender);

                    //checking the result 
                    int result = sqlCommand.ExecuteNonQuery();
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
        }
        public int InsertIntotable(EmployeeDetail employee)
        {
            using (sqlConnection)
                try
                {
                    //passing query in terms of stored procedure
                    SqlCommand sqlCommand = new SqlCommand("dbo.InsertIntoTable1", sqlConnection);
                    //passing command type as stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    //adding the parameter to the strored procedure
                    sqlCommand.Parameters.AddWithValue("@employeeName", employee.employeeName);
                    sqlCommand.Parameters.AddWithValue("@startDate", employee.startDate);
                    sqlCommand.Parameters.AddWithValue("@gender", employee.gender);
                    sqlCommand.Parameters.AddWithValue("@phonenumber", employee.phoneNumber);
                    sqlCommand.Parameters.AddWithValue("@address", employee.address);
                    sqlCommand.Parameters.AddWithValue("@department", employee.department);
                    sqlCommand.Parameters.AddWithValue("@Deduction", employee.deduction);
                    sqlCommand.Parameters.AddWithValue("@Tax", employee.tax);
                    sqlCommand.Parameters.AddWithValue("@TaxablePay", employee.taxablePay);
                    sqlCommand.Parameters.AddWithValue("@basicPay", employee.basicPay);
                    //checking the result 
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        return 1;
                    else
                        return 0;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
        }
   }
}
    

