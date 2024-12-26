using Dapper;
using DomainObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessLayer
{
    public class DapperRepository : IRepository<Student>
    {
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                AttachDbFilename=C:\Users\SadCy\source\repos\DataAccessLayer\Database.mdf;
                                Integrated Security=True";

        public DapperRepository()
        {
        }

        public void Create(Student item)
        {
            string script = "INSERT INTO Students ([Name], [Speciality], [Group]) " +
                           "VALUES(@Name, @Speciality, @Group);" +
                           "SELECT CAST(SCOPE_IDENTITY() AS INT)";

            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                int id = dbConnection.Query<int>(script, item).FirstOrDefault();
                item.Id = id;
            }
        }

        public void Delete(int id)
        {
            string script = $"DELETE FROM Students WHERE [Id] = {id}";
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Execute(script);
            }
        }

        public IEnumerable<Student> GetAll()
        {
            string script = @"SELECT * FROM Students";
            IEnumerable<Student> values;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                values = db.Query<Student>(script).ToList();
            }
            return values;
        }

        public Student GetById(int id)
        {
            string script = $@"SELECT * FROM Students WHERE Id = {id}";
            Student value;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                value = db.Query<Student>(script).FirstOrDefault();
            }
            return value;
        }

        public void Update(int _studentFromId, Student _studentTo)
        {
            IDomainObject studentTo = _studentTo as IDomainObject;

            string script = $"UPDATE Students SET [Name] = @Name," +
                $" [Speciality] = @Speciality, [Group] = @Group WHERE [Id] = {_studentFromId}";
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Query<int>(script, _studentTo);
            }
        }

        private void UseScript(string script)
        {
            if (!string.IsNullOrEmpty(script))
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Execute(script);
                }
            }
        }
    }
}
