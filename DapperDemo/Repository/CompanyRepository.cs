using Dapper;
using DapperDemo.Data;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperDemo.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private IDbConnection db;

        public CompanyRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public Company Add(Company company)
        {
            var sql = "INSERT INTO Companies (Name,Address,City,State,PostalCode) VALUES(@Name,@Address,@City,@State,@PostalCode);" +
                "SELECT CAST(SCOPE_IDENTITY() as int); ";
            //int id =db.Query<int>(sql,new 
            //{   company.Name,
            //    company.Address,
            //    company.City,
            //    company.State,
            //    company.PostalCode
            //}).Single();
            int id = db.Query<int>(sql,company).Single();
            company.ComapnyID = id;
            return company;
        }

        public void Delete(int id)
        {
            var sql = "DELETE FROM Companies WHERE ComapnyID=@Id";
            db.Execute(sql, new { id });
        }

        public Company Find(int id)
        {
            var sql = "SELECT * FROM Companies WHERE ComapnyID=@ComapnyID";
            return db.Query<Company>(sql,new { @ComapnyID = id}).Single();
        }

        public List<Company> GetAll()
        {
            var sql = "SELECT * FROM Companies";
            return db.Query<Company>(sql).ToList();
        }

        public Company Update(Company company)
        {
            var sql = "UPDATE Companies SET Name=@Name,Address=@Address,City=@City,State=@State," +
                "PostalCode=@PostalCode WHERE ComapnyID=@ComapnyID";
            db.Execute(sql,company);
            return company;
        }
    }
}
