using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetMateAPI.Models;

namespace BudgetMateAPI.Data
{
    public class BudgetMateRepo : IBudgetMateRepo
    {
        private readonly BudgetMateContext _context;

        public BudgetMateRepo(BudgetMateContext context)
        {
            _context = context;
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            user.Id = _context.SaveChanges();
            return user;
        }

        public Transaction Create(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            transaction.Id = _context.SaveChanges();
            return transaction;
        }

        public void Delete(Transaction transaction)
        {
             _context.Transactions.Remove(transaction);
            _context.SaveChanges();
        }

        public IEnumerable<Transaction> GetAllForUser(int userId)
        {
            IEnumerable<Transaction> ts = _context.Transactions.Where(t => t.UserId == userId);
            return ts;
        }

        public User GetByEmail(string email)
        {
            User user = _context.Users.FirstOrDefault(e => e.Email == email);
            return user;
        }

        public User GetById(int id)
        {
            User user = _context.Users.FirstOrDefault(e => e.Id == id);
            return user;
        }
    }
}
