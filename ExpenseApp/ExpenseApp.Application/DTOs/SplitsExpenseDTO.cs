using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Application.DTOs
{
    public class SplitsExpenseDTO : ExpenseDTO
    {
        public ICollection<ExpenseSplitDTO> Splits { get; set; }
    }
}
