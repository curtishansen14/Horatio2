using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Horatio_2._0.Models
{
    public class UserQuest
    {
        [Key]
        public int UserQuestID { get; set; }

        [ForeignKey("Quest")]
        public int QuestID { get; set; }
        public Quest Quest { get; set; }

        [ForeignKey("AspNetUser")]
        public string Id { get; set; }
        public ApplicationUser AspNetUser { get; set; }

        [Display(Name = "Active")]
        public bool isActive { get; set; }

        [Display(Name = "Complete")]
        public bool isComplete { get; set; }

        [Display(Name = "Target Date")]
        public DateTime? Target { get; set; }

        
    }
}