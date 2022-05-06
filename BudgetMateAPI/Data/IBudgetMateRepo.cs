using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetMateAPI.Models;

namespace BudgetMateAPI.Data
{
    public interface IBudgetMateRepo
    {
        User Create(User user);
        User GetByEmail(string email);
        User GetById(int id);

        Transaction Create(Transaction transaction);
        void Delete(Transaction transaction);
        IEnumerable<Transaction> GetAllForUser(int userId);
        Transaction GetTransactionById(int id);




    }
}
