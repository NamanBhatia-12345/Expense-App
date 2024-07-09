using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Application.DTOs
{
    public class CreateGroupDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name can only contain alphabets and spaces.")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
