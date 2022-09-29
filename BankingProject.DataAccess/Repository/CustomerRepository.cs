﻿using BankingProject.DataAccess.Repository.IRepository;
using BankingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingProject.DataAccess.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db) : base(db)   
        {
            _db = db;
        }


        public void Update(Customer obj)
        {
            _db.Customers.Update(obj);
        }
    }
}
