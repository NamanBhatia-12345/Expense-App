using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Application.DTOs
{
    public class CreateExpenseDTO
    {
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, 1000000, ErrorMessage = "Amount must be between 0.01 and 1,000,000.")]
        public decimal Amount { get; set; } = 0;
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "PaidUserBy is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "User can only contain alphabets and spaces.")]
        public string PaidUserBy { get; set; }
    }
}
