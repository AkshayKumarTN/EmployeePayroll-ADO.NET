using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace EmployeePayrollService
{
    public class ThreadOperations
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=payroll_service;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);

        // Method to Add New Employee To DataBase .................
        public bool AddEmployee(EmployeeModel employee)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spAddEmployee", this.connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", employee.EmployeeID);
                    sqlCommand.Parameters.AddWithValue("@Name", employee.EmployeeName);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Address", employee.Address);
                    sqlCommand.Parameters.AddWithValue("@Gender", employee.Gender);
                    sqlCommand.Parameters.AddWithValue("@Department", employee.Department);
                    sqlCommand.Parameters.AddWithValue("@Salary", employee.BasicPay);
                    sqlCommand.Parameters.AddWithValue("@Deduction", employee.Deductions);
                    sqlCommand.Parameters.AddWithValue("@TaxablePay", employee.TaxablePay);
                    sqlCommand.Parameters.AddWithValue("@IncomeTax", employee.Tax);
                    sqlCommand.Parameters.AddWithValue("@NetPay", employee.NetPay);
                    sqlCommand.Parameters.AddWithValue("@StartDate", DateTime.Now);


                    this.connection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                this.connection.Close();
            }
            return false;
        }
        // Method to Add List of Employee To DB Without Thread..................
        public void AddEmployeeListToDBWithoutThread(List<EmployeeModel> listEmployee)
        {

            listEmployee.ForEach(employee =>
            {
                Console.WriteLine("Contact being added: " + employee.EmployeeID);
                this.AddEmployee(employee);
                Console.WriteLine("Contact added: " + employee.EmployeeID);
            });
        }

        // Method to Add List of Employee To DB With Thread......................
        public void AddEmployeeListToDBWithThread(List<EmployeeModel> contactList)
        {
            contactList.ForEach(employee =>
            {
                Thread thread = new Thread(() =>
                {
                    Console.WriteLine("Contact Being added" + employee.EmployeeID);
                    this.AddEmployee(employee);
                    Console.WriteLine("Contact added: " + employee.EmployeeID);
                });
                thread.Start();
                thread.Join();
            });
        }
    }
}
