using BankingProject.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingProject.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Customer = new CustomerRepository(_db);
            Account = new AccountRepository(_db);
        }
        public ICustomerRepository Customer { get; private set; }

        public IAccountRepository Account { get; private set; }   

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
