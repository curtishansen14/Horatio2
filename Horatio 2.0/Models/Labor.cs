using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Horatio_2._0.Models
{
    public class Labor
    {
        [Key]
        public int LaborID { get; set; }

        [ForeignKey("Quest")]
        [ScriptIgnore]
        public int QuestID { get; set; }
        [ScriptIgnore]
        public Quest Quest { get; set; }

        [Required]
        public string Title { get; set;}

        [Required]
        [MaxLength(509)]
        public string Description { get; set; }

        public string Location { get; set; }
    }
}