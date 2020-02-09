using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jobs_Offers_Website.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Job Type")]

        public string categoryName { get; set; }

        [Required]
        [Display(Name = "Job Description")]
        public string categoryDescription { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }


    }
}