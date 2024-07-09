using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Core.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; } = 0;
        public DateTime Date { get; set; }
        public string PaidUserBy { get; set; }
        public bool Issettled { get; set; } = false;

        [ForeignKey("GroupId")]
        public int GroupId { get; set; }

        //Navigation Property
        public Group Group { get; set; }
        public ICollection<ExpenseSplit> Splits { get; set; }

    }
}
