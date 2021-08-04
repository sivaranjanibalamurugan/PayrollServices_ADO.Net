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
                Console.WriteLine("4.Exit");
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
                        repository.DisplayDataBasedOnDate();
                        break;
                    case 4:
                        Continue = false;
                        break;
                    default:
                        break;
                }
            }
            Console.Read();
        }
    }
}
