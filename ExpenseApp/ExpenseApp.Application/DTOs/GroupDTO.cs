using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Application.DTOs
{
    public class GroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<GroupMemberDTO> Members { get; set; } = new List<GroupMemberDTO>();
        public ICollection<ExpenseDTO> Expenses { get; set; } = new List<ExpenseDTO>();
    }

}
