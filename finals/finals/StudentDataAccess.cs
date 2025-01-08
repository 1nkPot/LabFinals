using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

public class StudentDataAccess
{
    // Connection string using the provided connection string
    private string connectionString = "Data Source=DESKTOP-67TDBGJ;Initial Catalog=final;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

    // Fetch students with optional filters for grade and subject
    public List<Student> GetStudents(string gradeFilter = null, string subjectFilter = null)
    {
        List<Student> students = new List<Student>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Students WHERE 1=1";

            if (!string.IsNullOrEmpty(gradeFilter))
                query += " AND Grade = @Grade";

            if (!string.IsNullOrEmpty(subjectFilter))
                query += " AND Subject = @Subject";

            SqlCommand command = new SqlCommand(query, connection);

            if (!string.IsNullOrEmpty(gradeFilter))
                command.Parameters.AddWithValue("@Grade", gradeFilter);

            if (!string.IsNullOrEmpty(subjectFilter))
                command.Parameters.AddWithValue("@Subject", subjectFilter);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                students.Add(new Student
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    Grade = reader["Grade"].ToString(),
                    Subject = reader["Subject"].ToString(),
                    Marks = (decimal)reader["Marks"],
                    Attendance = (decimal)reader["Attendance"]
                });
            }
        }
        return students;
    }

    // Add a new student
    public void AddStudent(Student student)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Students (Name, Grade, Subject, Marks, Attendance) VALUES (@Name, @Grade, @Subject, @Marks, @Attendance)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", student.Name);
            command.Parameters.AddWithValue("@Grade", student.Grade);
            command.Parameters.AddWithValue("@Subject", student.Subject);
            command.Parameters.AddWithValue("@Marks", student.Marks);
            command.Parameters.AddWithValue("@Attendance", student.Attendance);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Update an existing student's details
    public void UpdateStudent(Student student)
    {
        // Validate Marks before updating
        if (student.Marks < 0 || student.Marks > 100)
        {
            throw new ArgumentOutOfRangeException("Marks must be between 0 and 100.");
        }

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Students SET Name = @Name, Grade = @Grade, Subject = @Subject, Marks = @Marks, Attendance = @Attendance WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", student.Name);
            command.Parameters.AddWithValue("@Grade", student.Grade);
            command.Parameters.AddWithValue("@Subject", student.Subject);
            command.Parameters.AddWithValue("@Marks", student.Marks);
            command.Parameters.AddWithValue("@Attendance", student.Attendance);
            command.Parameters.AddWithValue("@Id", student.Id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Delete a student
    public void DeleteStudent(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Students WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
