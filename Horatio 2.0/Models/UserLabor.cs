using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Horatio_2._0.Models
{
    public class UserLabor
    {
        [Key]
        public int UserLaborID { get; set; }

        [ForeignKey("Labor")]
        public int LaborID { get; set; }
        public Labor Labor { get; set; }

        [ForeignKey("AspNetUser")]
        public string Id { get; set; }
        public ApplicationUser AspNetUser { get; set; }

        [ForeignKey("UserQuest")]
        public int UserQuestID { get; set; }
        public UserQuest UserQuest { get; set; }

        public bool isComplete { get; set; }

    }
}