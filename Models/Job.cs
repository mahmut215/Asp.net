using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Jobs_Offers_Website.Models
{
    public class Job
    {
        public int Id { get; set; }

    
        [Display(Name = "Job Name")]
        public string JobTitle { get; set; }

    
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }

    
        [Display(Name = "Job Image")]
        public string JobImage { get; set; }

        //public int State { get; set; }

        //public DateTime AcceptDate { get; set; }

        [Display(Name = "Job Type")]
        public int CategoryId { get; set; }

        public string UserId { get; set; }


        public virtual Category category { get; set; }

        public virtual ApplicationUser User { get; set; }



    }
}