using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetMateAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetMateAPI.Data
{
    public class BudgetMateContext : DbContext
    {
        public BudgetMateContext(DbContextOptions<BudgetMateContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }


}
