using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BudgetMateAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
