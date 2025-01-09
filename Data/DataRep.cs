using CRUDwithADO.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace CRUDwithADO.Data
{
    public class DataRep
    {
        private SqlConnection sqlConnection;
        public DataRep()
        {
            
            string connString= "Server=NOONE\\SQLEXPRESS07;Database=CRUD_ADO;Trusted_Connection=True;MultipleActiveResultSets=true";
            sqlConnection = new SqlConnection(connString);

                if (sqlConnection != null)
                {
                    Console.WriteLine("Connection Successfull");
                }
        }

        public List<Student> RetriveData()
        {
            List<Student> stud_data = new List<Student>();

            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            using (SqlCommand sqlCommand = new SqlCommand("RetriveStudent", sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand))
                {
                    DataTable table = new DataTable();
                    sqlAdapter.Fill(table);

                    foreach (DataRow rowData in table.Rows)
                    {
                        Student student = new Student
                        {
                            Id = Convert.ToInt32(rowData["id"]),
                            Name = Convert.ToString(rowData["name"]),
                            Age = Convert.ToInt32(rowData["age"]),
                            Address = Convert.ToString(rowData["address"]),
                            Gender = Convert.ToString(rowData["gender"]),
                            Status = Convert.ToBoolean(rowData["status"]),
                            Education = Convert.ToString(rowData["education"])
                        };
                        //Console.WriteLine(student.Name);
                        stud_data.Add(student);
                    }
                }
            }

            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }

            return stud_data;
        }

        public void InsertData(Student stud_data)
        {


            //foreach (Student student in stud_data)
            //{
            //    Console.WriteLine($"Student {student.Id}");
            //    Console.WriteLine(student.Name);
            //    Console.WriteLine(student.Age);
            //    Console.WriteLine(student.Gender); 
            //    Console.WriteLine(student.Address);
            //    Console.WriteLine(student.Status);
            //    Console.WriteLine(student.Education);

            //}


            using (SqlCommand sqlCommand = new SqlCommand("CreateStudent", sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@name", stud_data.Name));
                sqlCommand.Parameters.Add(new SqlParameter("@age", stud_data.Age));
                sqlCommand.Parameters.Add(new SqlParameter("@address", stud_data.Address));
                sqlCommand.Parameters.Add(new SqlParameter("@gender", stud_data.Gender));
                sqlCommand.Parameters.Add(new SqlParameter("@status", stud_data.Status));
                sqlCommand.Parameters.Add(new SqlParameter("@education", stud_data.Education));
                sqlConnection.Open();

                sqlCommand.ExecuteReader();
            }

            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public void UpdateData(Student stud_data)
        {

            //foreach (Student student in stud_data)
            //{
            //    Console.WriteLine($"Student {student.Id}");
            //    Console.WriteLine(student.Name);
            //    Console.WriteLine(student.Age);
            //    Console.WriteLine(student.Gender);
            //    Console.WriteLine(student.Address);
            //    Console.WriteLine(student.Status);
            //    Console.WriteLine(student.Education);

            //}

            using (SqlCommand sqlCommand = new SqlCommand("UpdateStudent", sqlConnection))
            {         
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@id", stud_data.Id));
                sqlCommand.Parameters.Add(new SqlParameter("@name", stud_data.Name));
                sqlCommand.Parameters.Add(new SqlParameter("@age", stud_data.Age));
                sqlCommand.Parameters.Add(new SqlParameter("@address", stud_data.Address));
                sqlCommand.Parameters.Add(new SqlParameter("@gender", stud_data.Gender));
                sqlCommand.Parameters.Add(new SqlParameter("@status", stud_data.Status));
                sqlCommand.Parameters.Add(new SqlParameter("@education", stud_data.Education));

                sqlConnection.Open();
                sqlCommand.ExecuteReader();
            }

            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public void DeleteData(int id)
        {

            using (SqlCommand sqlCommand = new SqlCommand("DeleteStudent", sqlConnection))
            {

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@id", id.ToString()));
                sqlConnection.Open();
                sqlCommand.ExecuteReader();
            }

            if(sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        public Student? GetStudentById(int id)
        {
            try
            {
                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                using (SqlCommand sqlCommand = new SqlCommand("GetStudentById", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                Age = reader.GetInt32(reader.GetOrdinal("age")),
                                Address = reader.GetString(reader.GetOrdinal("address")),
                                Gender = reader.GetString(reader.GetOrdinal("gender")),
                                Status = reader.GetBoolean(reader.GetOrdinal("status")),
                                Education = reader.GetString(reader.GetOrdinal("education"))
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return null;
        }

    }
}
