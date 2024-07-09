using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Core.Models
{
    public class ExpenseSplit
    {
        [ForeignKey("ExpenseId")]
        public int ExpenseId { get; set; }

        //Navigation Property
        public Expense Expense { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public string FullName { get; set; }    

        //Navigation Property
        public ApplicationUser User { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; } = 0;
    }
}
