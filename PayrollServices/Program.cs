using System;

namespace PayrollServices
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Payroll ADO.Net");
            EmployeeRepository repository = new EmployeeRepository();
            repository.GetAllData();
        }
    }
}
