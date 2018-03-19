using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Horatio_2._0.Models
{
    public class Topic
    {
        [Key]
        public int TopicID { get; set; }

        public string Theme { get; set; }

    }
}