﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollService
{
    class EmployeeRepo
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=payroll_service;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);

        public void GetAllEmployee()
        {

            try
            {
                EmployeeModel model = new EmployeeModel();
                using (this.connection)
                {
                    string query = "Select * from Employee_Payroll";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            model.EmployeeID = Convert.ToInt32(dr["Id"]);
                            model.EmployeeName = dr["Name"].ToString();
                            model.BasicPay = Convert.ToDecimal(dr["Salary"]);
                            model.StartDate = dr.GetDateTime(3);
                            model.Gender = dr["Gender"].ToString();
                            model.PhoneNumber = dr["phone_number"].ToString();
                            model.Address = dr["address"].ToString();
                            model.Department = dr["department"].ToString();
                            model.Deductions = Convert.ToDecimal(dr["deduction"]);
                            model.TaxablePay = Convert.ToDecimal(dr["taxable_pay"]);
                            model.Tax = Convert.ToDecimal(dr["income_tax"]);
                            model.NetPay = Convert.ToDecimal(dr["net_pay"]);
                            Console.WriteLine(model.EmployeeName + " " + model.BasicPay + " " + model.StartDate + " " + model.Gender + " " + model.PhoneNumber + " " + model.Address + " " + model.Department + " " + model.Deductions + " " + model.TaxablePay + " " + model.Tax + " " + model.NetPay);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }

                    this.connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateSalary(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    string query = @"update employee_payroll set Salary=3000000 where name='Terisa'";
                    SqlCommand command = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine("Salary Updated Successfully ");
                    }
                    else
                    {
                        Console.WriteLine("Unsuccessfull");
                    }
                    this.connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        
        }
    }
}
