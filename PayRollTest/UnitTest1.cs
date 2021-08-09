using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayrollServices;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PayRollTest
{
    [TestClass]
    public class PayrollTestClass
    {
        EmployeeRepository repository;
        [TestInitialize]
        public void setup()
        {
            repository = new EmployeeRepository();
        }
        //checking whether all data is retrived
        [TestMethod]
        public void RetriveAllDetails()
        {
            //Assign
            int expected = 6;
            //Act
            List<EmployeeDetail> list = repository.GetAllData();
            int actual = list.Count();
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        //checking whether the update is passed or not
        public void UpdateSalaryTest()
        {
            //Assign
            int expected = 1;
            //Act
            int actual = repository.UpdateSalary();
            //Assert
            Assert.AreEqual(expected, actual);
        }
        //Checking the result of the retrival based on date 
        [TestMethod]
        public void DisplayDataBasedOnData()
        {
            int expected = 3;
            DateTime startdate = new DateTime(2020, 07, 20);
            DateTime dateTime = new DateTime(2021, 07, 30);
            List<EmployeeDetail> list = repository.DisplayDataBasedOnDate(startdate, dateTime);
            int actual = list.Count();
            Assert.AreEqual(expected, actual);

        }
        //implementing the parameterized test cases
        [DataRow(1, "4 2 ")] //Count of employee test
        [DataRow(2, "312142 468213 ")] //maximum salary test
        [DataRow(3, "44211 44211 ")] // minimum salary test
        [DataRow(4, "18000 18000 ")] // Avg salary test
        [DataRow(5, "31214.2 88000 ")] //sum of salary test
        [DataTestMethod]
        public void AggregatefunctionTest(int choice, string expected)
        {

            //Act
            string actual = repository.Aggregate();
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void InsertIntoTableTest()
        {
            int expected = 1;
            //Assign
            EmployeeDetail employee = new EmployeeDetail();
            employee.employeeName = "aaa";
            employee.address = "xxx";
            employee.gender = "male";
            employee.department = "HR";
            employee.startDate = "2021-03-29";
            employee.basicPay = 100000;
            employee.deduction = 2000;
            employee.taxablePay = 1326;
            employee.tax = 256;
            employee.phoneNumber = 9032165478;
            int actual = repository.InsertIntotable(employee);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void InsertIntoPayrollTable()
        {
            int expected = 1;
            //Assign
            EmployeeDetail employee = new EmployeeDetail();
            employee.basicPay = 18000;
            employee.employeeId = 11;
            TransactionManagement transaction = new TransactionManagement();
            int actual = transaction.AddingRecord(employee);
            Assert.AreEqual(expected, actual);

        }
        
        [TestMethod]
        public void InsertIntoTables()
        {
            int expected = 1;
          
            EmployeeDetail employee = new EmployeeDetail { employeeId = 10, employeeName = "Tom", companyId = 01, departmentId = 9, phoneNumber =7410258963, address = "MRNagar", city = "chennai", state = "TamilNadu", startDate = "2017-12-05", gender = "M", basicPay = 30000 };
            TransactionManagement transaction = new TransactionManagement();
            int actual = transaction.AddingRecord(employee);
            Assert.AreEqual(expected, actual);

        }
        //--------------Uc10- checking U2-6 in new tables
       
        [DataRow("4 2 ", "dbo.countEmployee")] //Count of employee test
        [DataRow("312142 468213 ", "dbo.MaximumSalary1")] //maximum salary test
        [DataRow("44211 44211", "dbo.MinimumSalary1")] // minimum salary test
        [DataRow("67093 874628 ", "dbo.AverageSalary")] // Avg salary test
        [DataRow("4571002 973020 ", "dbo.SumofSalary1")] //sum of salary test
        [DataTestMethod]
        public void AggregatefunctionTest2(string expected, string procedure)
        {

            //Act
            string actual = repository.AggregareteFunction(procedure);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void DisplayDataBasedOnData1()
        {
            int expected = 3;
            DateTime startdate = new DateTime(2020, 07, 20);
            DateTime dateTime = new DateTime(2021, 07, 30);
            List<EmployeeDetail> list = repository.DisplayDataBasedOnDate(startdate, dateTime, "dbo.RetriveBasedOnDate");
            int actual = list.Count();
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        //checking whether the update is passed or not
        public void UpdateSalaryTest1()
        {
            //Assign
            int expected = 1;
            //Act
            int actual = repository.UpdateSalary(2, 24000, "dbo.updatePayroll");
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        //UC12- Cascading delete
        public void DeleteRecord()
        {
            int expected = 1;
            int actual = new TransactionManagement().DeleteUsingCasadeDelete(4);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        //UC12- Cascading delete
        public void AddindIsActiveField()
        {
            int expected = 1;
            int actual = new TransactionManagement().AddIsActiveColumn();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void RetrivingDataBasedOnIsActiveField()
        {
            int expected = 8;
            List<EmployeeDetail> actual = new TransactionManagement().RetriveDataForAudit("dbo.RetriveAllData");
            Assert.AreEqual(expected, actual.Count);
        }
        [TestMethod]
        public void TestInsertionWithoutThread()
        {
            long actual = new TransactionManagement().InsertWithoutThread();
            Console.WriteLine("" + actual);
        }
        [TestMethod]
        public void TestInsertionWithThread()
        {
            long actual = new TransactionManagement().InsertWithThread();
            Console.WriteLine("" + actual);
        }
        [TestMethod]
        public void TestInsertionWithSynchronization()
        {
            long actual = new TransactionManagement().InsertWithThread();
            Console.WriteLine("" + actual);
        }
          //*********JSON*******
        //UC1-Read data from server
        [TestMethod]
        public void ReadAllDataFromServer()
        {
            int expected = 3;
            List<EmployeeDetailsWithSalary> employeeList = new PayRollJSONServer().ReadFromServer();
            Assert.AreEqual(expected, employeeList.Count);
        }

    }
}
