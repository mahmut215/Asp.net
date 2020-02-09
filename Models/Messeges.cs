using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Jobs_Offers_Website.Models
{
    public class Messeges
    {
        public int Id { get; set; }
        public DateTime MessegeTime { get; set; }
        public string PublisherId { get; set; }
        [Required]
        [Display(Name = "Messege")]
        public string Messege { get; set; }
        public string ResearcherId { get; set; }
        [Required]
        public Int32 IdJob { get; set; }

       // public Int32 Job_Id { get; set; }


        public virtual ApplicationUser user { get; set; }
      //  public virtual Job job { get; set; }

    }
}