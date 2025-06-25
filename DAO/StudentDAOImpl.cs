using CSharpWebAppRazorDB.Models;
using Microsoft.Data.SqlClient;
using CSharpWebAppRazorDB.Services.DBHelper;

namespace CSharpWebAppRazorDB.DAO
{
    public class StudentDAOImpl : IStudentDAO
    {
        /*
         * A method that takes a Student object and returns a newly inserted
         * Student from the database — or null if something goes wrong.
         */
        public Student? Insert(Student student)
        {
            Student? studentToReturn = null; //will hold the result you return at the end.
            int insertedId = 0; //will store the auto-generated ID of the new student.

            string sql1 = "INSERT INTO Students (Firstname, Lastname) VALUES (@firstname, @lastname); " +
                "SELECT SCOPE_IDENTITY()"; // an INSERT to add the student
                                           // and SELECT SCOPE_IDENTITY() to get the ID of the inserted row.

            using SqlConnection connection = DBUtil.GetConnection();
            connection.Open();

            using SqlCommand command1 = new (sql1, connection); //Create a SQL command with the insert statement and connection.

            //Use parameterized queries to prevent SQL injection.
            command1.Parameters.AddWithValue("@firstname", student.Firstname);
            command1.Parameters.AddWithValue("@lastname", student.Lastname);

            object insertObject = command1.ExecuteScalar(); //executes the command and returns the first column of the first row — in this case, the ID returned by SCOPE_IDENTITY()

            if (insertObject != null)
            {
                if (!int.TryParse(insertObject.ToString(), out insertedId))
                {
                    throw new Exception("Error in inserted id.");
                }
            }

            string sql2 = "SELECT * FROM Students WHERE id = @studentId"; //SQL command to fetch the student with the inserted ID.
            using SqlCommand command2 = new(sql2, connection);
            command2.Parameters.AddWithValue("@studentId", insertedId);

            using SqlDataReader reader = command2.ExecuteReader();
            if (reader.Read())
            {
                studentToReturn = new Student()
                {
                    Id = (int)reader["Id"],
                    Firstname = (string)reader["Firstname"],
                    Lastname = (string)reader["Lastname"]
                };

            }
            return studentToReturn;
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM Students WHERE Id = @id";
            using SqlConnection connection = DBUtil.GetConnection();
            connection.Open();

            using SqlCommand command = new(sql, connection); //Create a SQL command with the insert statement and connection.

            //Use parameterized queries to prevent SQL injection.
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }

        public List<Student> GetAll()
        {
            string sql = "SELECT * FROM Students";
            List<Student> students = [];

            using SqlConnection connection = DBUtil.GetConnection();
            connection.Open();

            using SqlCommand command = new(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                students.Add(new Student()
                {
                    Id = (int)reader["Id"],
                    Firstname = (string)reader["Firstname"],
                    Lastname = (string)reader["Lastname"]
                });
            }
            return students;
        }

        public Student? GetById(int id)
        {
            Student? studentToReturn = null;
            string sql = "SELECT * FROM Students WHERE Id = @id";

            using SqlConnection connection = DBUtil.GetConnection();
            connection.Open();

            using SqlCommand command = new(sql, connection); //Create a SQL command with the insert statement and connection.

            //Use parameterized queries to prevent SQL injection.
            command.Parameters.AddWithValue("@id", id);

            using SqlDataReader  reader = command.ExecuteReader();

            if (reader.Read())
            {
                studentToReturn = new Student()
                {
                    Id = (int)reader["Id"],
                    Firstname = (string)reader["Firstname"],
                    Lastname = (string)reader["Lastname"]
                };

            }
            return studentToReturn;
        }

        public void Update(Student student)
        {
            string sql = "UPDATE Students SET Firstname = @firstname, Lastname = @lastname WHERE id = @id";
            using SqlConnection connection = DBUtil.GetConnection();
            connection.Open();

            using SqlCommand command = new(sql, connection); //Create a SQL command with the insert statement and connection.

            //Use parameterized queries to prevent SQL injection.
            command.Parameters.AddWithValue("@id", student.Id);
            command.Parameters.AddWithValue("@firstname", student.Firstname);
            command.Parameters.AddWithValue("@lastname", student.Lastname);

            command.ExecuteNonQuery();
            
        }
    }
}
