using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp.Core.Models
{
    public class GroupMembers
    {
        [ForeignKey("GroupId")]
        public int GroupId { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        //Navigation Property
        public Group Group { get; set; }
        public ApplicationUser User { get; set; }

    }
}
