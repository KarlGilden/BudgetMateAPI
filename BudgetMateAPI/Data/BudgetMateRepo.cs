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

        public User GetByEmail(string email)
        {
            User user = _context.Users.FirstOrDefault(e => e.Email == email);
            return user;
        }
    }
}
