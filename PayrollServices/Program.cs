using System;

namespace PayrollServices
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Payroll ADO.Net");
            EmployeeRepository repository = new EmployeeRepository();
            bool Continue = true;
            while (Continue)
            {
                Console.WriteLine("1.Retrive all data\n");
                Console.WriteLine("2.Update salary\n");
                Console.WriteLine("3.Retrieve date\n");
                Console.WriteLine("4.Aggregate Function\n");
                Console.WriteLine("5.Exit");
                Console.Write("Enter your choice:");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        repository.GetAllData();
                        break;
                    case 2:
                        repository.UpdateSalary();
                        break;
                    case 3 :
                        DateTime startdate = new DateTime(2021, 07, 31);
                        DateTime dateTime = new DateTime(2020, 07, 30);
                        repository.DisplayDataBasedOnDate(startdate, dateTime);
                        break;
                    case 4:
                        repository.Aggregate();
                        break;
                    default:
                        break;
                }
            }
            Console.Read();
        }
    }
}
