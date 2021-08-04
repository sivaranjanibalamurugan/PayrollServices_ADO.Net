﻿using System;
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
                    }
                }
                else
                {
                    Console.WriteLine("No data available");
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
        public void UpdateSalary()
        {
            //assigning the details which has to be updated
            EmployeeDetail employee = new EmployeeDetail();
            employee.employeeName = "Terissa";
            employee.employeeId = 3;
            employee.basicPay = 3000000;
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
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
        }
        public void DisplayDataBasedOnDate()
        {
            EmployeeDetail employee = new EmployeeDetail();
            DateTime startdate = new DateTime(2021, 07,31 );
            DateTime dateTime = new DateTime(2020, 07, 30);

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
                    sqlConnection.Close();
                }
        }
    }
}
        
    


       
