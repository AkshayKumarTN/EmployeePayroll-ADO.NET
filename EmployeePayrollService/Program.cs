using System;
using System.Collections.Generic;

namespace EmployeePayrollService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Employee Payroll Service \n");
            EmployeeRepo repo = new EmployeeRepo();
            EmployeeModel employee = new EmployeeModel();

            employee.EmployeeID=5;
            employee.EmployeeName="Sara";
            employee.PhoneNumber="9678123459";
            employee.Address="US";
            employee.Gender = "F";
            employee.Department= "Marketing";
            employee.BasicPay= 250000;
            employee.Deductions= 18000;
            employee.TaxablePay= 240000;
            employee.Tax= 4000;
            employee.NetPay= 259000;

            // Remove Employee From DataBase By Passing EmployeeID.................
            repo.RemoveEmployee(5);

            ThreadOperations threadOperations = new ThreadOperations();
            // Created a Employee List......................
            List<EmployeeModel> listEmployee = new List<EmployeeModel>();
            // Adding Employee to Employee List..................
            listEmployee.Add(employee);

            // Method to Add List of Contacts To DB Without Thread..................
            threadOperations.AddEmployeeListToDBWithoutThread(listEmployee);
            // Method to Add List of Contacts To DB With Thread..................
            threadOperations.AddEmployeeListToDBWithThread(listEmployee);

        }
    }
}
