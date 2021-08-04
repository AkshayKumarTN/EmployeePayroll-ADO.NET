using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollService
{
    public class EmployeeRepo
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
                            model.PhoneNumber = dr["PhoneNumber"].ToString();
                            model.Address = dr["Address"].ToString();
                            model.Department = dr["Department"].ToString();
                            model.Deductions = Convert.ToDecimal(dr["Deduction"]);
                            model.TaxablePay = Convert.ToDecimal(dr["TaxablePay"]);
                            model.Tax = Convert.ToDecimal(dr["IncomeTax"]);
                            model.NetPay = Convert.ToDecimal(dr["NetPay"]);
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

        public int UpdateSalary(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    string query = @"update employee_payroll set Salary=3000000 where Name='Terisa'";
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
                    return result;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }

        }
        public int UpdateSalaryUsingPreparedStatement(EmployeeModel model)
        {
            int result;
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("dbo.UpdateEmployeeDetails", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Id", model.EmployeeID);
                    command.Parameters.AddWithValue("Name", model.EmployeeName);
                    command.Parameters.AddWithValue("Salary", model.BasicPay);
                    connection.Open();
                    result = command.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine("Updated Successfully using Prepared Statement ");

                    }
                    else
                    {
                        Console.WriteLine("Not Updated!!!");
                        return default;
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
            finally
            {
                this.connection.Close();
            }
        }

        public void RetrieveDataBasedOnDateRange()
        {
            try
            {
                EmployeeModel model = new EmployeeModel();
                using (this.connection)
                {
                    // Retrieving all employee Data who have joined in a particular data range from the payroll service database..........
                    string query = @"select * from employee_payroll where StartDate between('2017-01-01') and getdate()";
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
                            model.PhoneNumber = dr["PhoneNumber"].ToString();
                            model.Address = dr["Address"].ToString();
                            model.Department = dr["Department"].ToString();
                            model.Deductions = Convert.ToDecimal(dr["Deduction"]);
                            model.TaxablePay = Convert.ToDecimal(dr["TaxablePay"]);
                            model.Tax = Convert.ToDecimal(dr["IncomeTax"]);
                            model.NetPay = Convert.ToDecimal(dr["NetPay"]);
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

        public void PerformAggregateFunctions()
        {
            string result = null;
            try
            {
                // find sum, average, min, max and number of male and female employees.......
                string query = @"select sum(Salary) as TotalSalary,min(Salary) as MinSalary,max(Salary) as MaxSalary,Round(avg(Salary),0) as AvgSalary,Gender,Count(*) from employee_payroll group by Gender";
                SqlCommand sqlCommand = new SqlCommand(query, this.connection);
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                Console.WriteLine("-----------------------------------------------------");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Total Salary : {0}", reader[0]);
                        Console.WriteLine("Min Salary : {0}", reader[1]);
                        Console.WriteLine("Max Salary : {0}", reader[2]);
                        Console.WriteLine("Average Salary : {0}", reader[3]);
                        Console.WriteLine("Grouped By Gender : {0}", reader[4]);
                        Console.WriteLine("No of employess : {0}", reader[5]);
                        result += reader[4] + " " + reader[0] + " " + reader[1] + " " + reader[2] + " " + reader[3] + " " + reader[5];
                        Console.WriteLine("-----------------------------------------------------");

                    }
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}
