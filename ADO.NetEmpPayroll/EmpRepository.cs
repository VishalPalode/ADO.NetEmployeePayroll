﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NetEmpPayroll
{
    public class EmpRepository
    {
        public static string connectionstring = @"Data Source=(localdb)\ProjectModels;Initial Catalog=payroll_service;Integrated Security=True";
        SqlConnection connection = null;

        public void GetAllEmployees()
        {
            try
            {
                using (connection = new SqlConnection(connectionstring))
                {
                    EmployeeModel model = new EmployeeModel();
                    string query = "select * from Employee_Payroll";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    connection.Open();
                    var reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.Id = Convert.ToInt32(reader["Id"] == DBNull.Value ? default : reader["Id"]);
                            model.Name = Convert.ToString(reader["Name"] == DBNull.Value ? default : reader["Name"]);
                            model.Salary = Convert.ToDouble(reader["Salary"] == DBNull.Value ? default : reader["Salary"]);
                            model.Startdate = (DateTime)(reader["Startdate"] == DBNull.Value ? default : reader["Startdate"]);
                            model.Gender = Convert.ToChar(reader["Gender"] == DBNull.Value ? default : reader["Gender"]);
                            model.Phone = Convert.ToInt32(reader["Phone"] == DBNull.Value ? default : reader["Phone"]);
                            model.Address = Convert.ToString(reader["Address"] == DBNull.Value ? default : reader["Address"]);
                            model.Department = Convert.ToString(reader["Department"] == DBNull.Value ? default : reader["Department"]);
                            model.Basic_Pay = Convert.ToDouble(reader["Basic_Pay"] == DBNull.Value ? default : reader["Basic_Pay"]);
                            model.Deduction = Convert.ToDouble(reader["Deduction"] == DBNull.Value ? default : reader["Deduction"]);
                            model.Taxable_Pay = Convert.ToDouble(reader["Taxable_Pay"] == DBNull.Value ? default : reader["Taxable_Pay"]);
                            model.Income_Tax = Convert.ToDouble(reader["Income_Tax"] == DBNull.Value ? default : reader["Income_Tax"]);
                            model.Net_Pay = Convert.ToDouble(reader["Net_Pay"] == DBNull.Value ? default : reader["Net_Pay"]);
                            Console.WriteLine($"{model.Id}, {model.Name}, {model.Salary}, {model.Startdate}, {model.Gender}, {model.Phone}, {model.Address}, {model.Department}, {model.Basic_Pay}, {model.Deduction}, {model.Taxable_Pay}, {model.Income_Tax}, {model.Net_Pay} \n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows present");
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            //finally
            //{
            //    connection.Close();
            //}
        }
        public void GetAllEmployeesWithDataAdapter()
        {
            try
            {
                DataSet dataSet = new DataSet();
                using (connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from Employee_Payroll", connection);
                    adapter.Fill(dataSet);
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        Console.WriteLine(dataRow["Id"] + ", " + dataRow["Name"] + ", " + dataRow["Salary"] + ", " + dataRow["StartDate"] + ", " + dataRow["Gender"] + ", " + dataRow["Phone"] + ", " + dataRow["Address"] + ", " + dataRow["Department"] + ", " + dataRow["Basic_Pay"] + ", " + dataRow["Deductions"] + ", " + dataRow["Taxable_Pay"] + ", " + dataRow["Income_Tax"] + ", " + dataRow["Net_Pay"]);
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //To Add Employee details    
        public void AddEmployee(EmployeeModel obj)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                SqlCommand command = new SqlCommand("spAddEmployee", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Name", obj.Name);
                command.Parameters.AddWithValue("@Salary", obj.Salary);
                command.Parameters.AddWithValue("@Gender", obj.Gender);
                command.Parameters.AddWithValue("@Startdate", obj.Startdate);
                command.Parameters.AddWithValue("@Phone", obj.Phone);
                command.Parameters.AddWithValue("@Department", obj.Department);
                command.Parameters.AddWithValue("@Address", obj.Address);
                command.Parameters.AddWithValue("@Basic_Pay", obj.Basic_Pay);
                command.Parameters.AddWithValue("@Deduction", obj.Deduction);
                command.Parameters.AddWithValue("@Taxable_Pay", obj.Taxable_Pay);
                command.Parameters.AddWithValue("@Income_Tax", obj.Income_Tax);
                command.Parameters.AddWithValue("@Net_Pay", obj.Net_Pay);
                command.Parameters.AddWithValue("@Dept_Id", obj.Dept_Id);
                connection.Open();
                var result = command.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Employee details added successfully");
                }
                else
                {
                    Console.WriteLine("Failed to add employee details");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

