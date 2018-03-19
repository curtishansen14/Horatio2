using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Horatio_2._0.Models
{
    public class Quest
    {
        [Key]
        public int QuestID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Descritpion { get; set; }

        [Required]
        [ForeignKey("Topic")]
        public int TopicID { get; set; }

        public Topic Topic { get; set; }

        public virtual IEnumerable<SelectListItem>Topics { get; set; }

        public List<Labor> Labors { get; set; }
        


    }
}