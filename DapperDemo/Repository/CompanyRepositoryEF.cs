using DapperDemo.Data;
using DapperDemo.Models;

namespace DapperDemo.Repository
{
    public class CompanyRepositoryEF : ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepositoryEF(ApplicationDbContext db)
        {
            _db = db;
        }
        public Company Add(Company company)
        {
            _db.companies.Add(company);
            _db.SaveChanges();
            return company;
        }

        public void Delete(int id)
        {
            Company company = _db.companies.Find(id);
            _db.companies.Remove(company);  
            _db.SaveChanges();
            return;
        }

        public Company Find(int id)
        {
            return _db.companies.Find(id);
        }

        public List<Company> GetAll()
        {
            return _db.companies.ToList();
        }

        public Company Update(Company company)
        {
            _db.companies.Update(company);
            _db.SaveChanges();
            return company;
        }
    }
}
