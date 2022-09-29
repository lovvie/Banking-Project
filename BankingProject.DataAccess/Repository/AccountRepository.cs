using BankingProject.DataAccess.Repository.IRepository;
using BankingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingProject.DataAccess.Repository
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private ApplicationDbContext _db;

        public AccountRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Account obj)
        {
            var objFromDb = _db.Accounts.FirstOrDefault(x => x.Id == obj.Id);
            if (objFromDb == null)
            {
                //objFromDb.AccountName = obj.AccountName;
                //objFromDb.accountType = obj.accountType;
            }
            _db.Accounts.Update(obj);
        }
    }
}
