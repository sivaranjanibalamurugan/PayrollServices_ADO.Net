using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PayrollServices
{
    public class TransactionManagement
    {
        public static string connectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=Payroll_services";
        //creating the object for sql connection class
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public int AddingRecord(EmployeeDetail employee)
        {
            EmployeeDetail details = employee;
            PayRollDetails payRoll = new PayRollDetails(employee.basicPay);
            using (sqlConnection)
                
                {
                    sqlConnection.Open();
                SqlTransaction transaction = sqlConnection.BeginTransaction();
                try
                {
                    string employeInsertion = "insert into Employee(emp_name,company_id,phoneNumber,address,city,state,startDate,gender) values ('" + employee.employeeName + "'," + employee.companyId + "," + employee.phoneNumber + ",'" + employee.address + "','" + employee.city + "','" + employee.state + "','" + employee.startDate + "','" + employee.gender + "')";
                    string payRollInsertion = "insert into PayRoll(Emp_id,BasicPay,Deduction,TaxablePay,Tax,NetPay) values(" + employee.employeeId + "," + payRoll.basicPay + "," + payRoll.deduction + "," + payRoll.taxablePay + "," + payRoll.tax + "," + payRoll.netPay + ")";
                    string employeeDepartmentInsertion = "insert into Employee_Department values (" + employee.employeeId + "," + employee.departmentId + ")";
                    new SqlCommand(employeInsertion, sqlConnection, transaction).ExecuteNonQuery();
                    new SqlCommand(payRollInsertion, sqlConnection, transaction).ExecuteNonQuery();
                    new SqlCommand(employeeDepartmentInsertion, sqlConnection, transaction).ExecuteNonQuery();
                    //if all query is successfull commit
                    transaction.Commit();
                    return 1;
                }
                catch (Exception )
                {
                    //else roll back
                    transaction.Rollback();
                    return 0;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public int DeleteUsingCasadeDelete(int id)
        {

            using (sqlConnection)
            {
                sqlConnection.Open();
                //Begin SQL transaction
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                try
                {
                    string delete1 = "update Employee set isActive=0 where emp_Id=" + id + "";
                    new SqlCommand(delete1, sqlConnection, sqlTransaction).ExecuteNonQuery();
                    sqlTransaction.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback to the point before exception
                    sqlTransaction.Rollback();
                    return 0;
                }
            }
        }
        public int AddIsActiveColumn()
        {
            using (sqlConnection)
            {
                sqlConnection.Open();
                //Begin SQL transaction
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                try
                {
                    string updatateQuery = "alter table Employee add isActive BIT NOT NULL default(1)";
                    new SqlCommand(updatateQuery, sqlConnection, sqlTransaction).ExecuteNonQuery();
                    sqlTransaction.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback to the point before exception
                    sqlTransaction.Rollback();
                    return 0;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public List<EmployeeDetail> RetriveDataForAudit(string procedureName)
        {
            using (sqlConnection)
            {
                sqlConnection.Open();
                EmployeeDetail employee = new EmployeeDetail();
                List<EmployeeDetail> employeeList = new List<EmployeeDetail>();
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection);
                    //passing command type as stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    //create data reader 
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    //if it has data
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //store each data in the employee details properties 
                            employee.employeeId = Convert.ToInt32(reader["emp_Id"] == DBNull.Value ? default : reader["emp_Id"]);
                            employee.employeeName = reader["emp_name"] == DBNull.Value ? default : reader["emp_name"].ToString();
                            employee.gender = reader["gender"] == DBNull.Value ? default : reader["gender"].ToString();
                            employee.startDate = reader["startDate"] == DBNull.Value ? default : reader["startDate"].ToString(); ;
                            employee.phoneNumber = Convert.ToDouble(reader["phoneNumber"] == DBNull.Value ? default : reader["phoneNumber"]);
                            employee.address = reader["address"] == DBNull.Value ? default : reader["address"].ToString();
                            employee.NetPay = Convert.ToDouble(reader["netPay"] == DBNull.Value ? default : reader["netPay"]);
                            employee.department = reader["dept_name"] == DBNull.Value ? default : reader["dept_name"].ToString();
                            //display the result
                            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} ", employee.employeeId, employee.employeeName, employee.gender, employee.startDate, employee.phoneNumber, employee.address, employee.NetPay);
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
        }
        public long InsertWithoutThread()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            AddDetails();
            stopwatch.Stop();
            return (stopwatch.ElapsedMilliseconds);

        }
        void AddDetails()
        {
            AddingRecord(new EmployeeDetail { employeeName = "aaa", address = "MR road", companyId = 1, city = "salem", state = "TamilNadu", startDate = "2019-11-20", gender = "M", phoneNumber = 8963025471, departmentId = 5, basicPay = 45500 });
            AddingRecord(new EmployeeDetail { employeeName = "bbbb", address = "RR road", companyId = 2, city = "Trichy", state = "TamilNadu", startDate = "2004-05-13", gender = "F", phoneNumber = 7896541230, departmentId = 4, basicPay = 35000 });
            AddingRecord(new EmployeeDetail { employeeName = "ccc", address = "Psk Street", companyId = 1, city = "YYY", state = "Kerala", startDate = "2017-7-30", gender = "M", phoneNumber = 8520147963, departmentId = 3, basicPay = 35126 });
        }
    }

}


    

        
    

    

