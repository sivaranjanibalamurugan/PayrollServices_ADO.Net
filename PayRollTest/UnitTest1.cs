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
            employee.startDate = new DateTime(2020-03-30);
            employee.basicPay = 100000;
            employee.deduction = 2000;
            employee.taxablePay = 1326;
            employee.tax = 256;
            employee.phoneNumber = 9032165478;
            int actual = repository.InsertIntotable(employee);
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
