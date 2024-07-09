using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Application.DTOs
{
    public class ExpenseSplitDTO
    {
        public int ExpenseId { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public decimal Balance { get; set; }
    }
}
