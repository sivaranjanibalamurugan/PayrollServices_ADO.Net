﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollServices
{
    class TransactionManagement
    {
        public static string connectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=Payroll_services";
        //creating the object for sql connection class
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public int AddingRecord(EmployeeDetail employee)
        {
            EmployeeDetail details = employee;
            PayRollDetails payRoll = new PayRollDetails(employee.basicPay);
            using (sqlConnection)
                try
                {
                    sqlConnection.Open();
                    string query = "insert into Employee values('xxx',4, 9632587410, 'RR Nagar' , 'TamilNadu' , 'madurai','2018-06-13','M')";
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    //create data reader 
                    int result = command.ExecuteNonQuery();
                    //if last query executed next query takes place
                    if (result > 0)
                    {
                        query = "insert into PayRoll(Emp_id,BasicPay,Deduction,TaxablePay,Tax,NetPay) values(" + employee.employeeId + "," + payRoll.basicPay + "," + payRoll.deduction + "," + payRoll.taxablePay + "," + payRoll.tax + "," + payRoll.netPay + ")";
                        command = new SqlCommand(query, sqlConnection);
                        result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            return 1;
                        }
                    }
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
    

