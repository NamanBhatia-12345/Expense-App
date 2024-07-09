using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Application.DTOs
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string PaidUserBy { get; set; }
        public int GroupId { get; set; }
        public bool IsSettled { get; set; }
    }

}
