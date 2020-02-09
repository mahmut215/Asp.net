using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jobs_Offers_Website.Models
{
    public class GetMessages
    {
        public int Id { get; set; }
        public string Message { get; set; }

        //public DateTime ApplyDate { get; set; }

       // public int State { get; set; }

        public DateTime sendtDate { get; set; }

        public string ResearcherId { get; set; }

        public int JobId { get; set; }
        public string UserId { get; set; }


        //public virtual Job job { get; set; }
       // public virtual ApplicationUser user { get; set; }
    }
}