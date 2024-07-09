using System.ComponentModel.DataAnnotations;

namespace ExpenseApp.Application.DTOs
{
    public class UpdateUserDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Full Name can only contain alphabets and spaces.")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }
    }
}
