using Jobs_Offers_Website.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        //private string UserId;
        //SessionStateAttribute["UserId"] = UserId;
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId;
            return View(job);
        }




        public ActionResult Acceptance(int Id,string UserId)
        {

            // Query the database for the row to be updated.
            var query =
                from ord in db.ApplyForJobs
                where ord.JobId == Id && ord.UserId == UserId
                select ord;

            // Execute the query, and change the column values
            // you want to change.
            foreach (ApplyForJob ord in query)
            {
                ord.State = 1;
                ord.AcceptDate = DateTime.Now;
                // Insert any additional changes to column values.
            }

            // Submit the changes to the database.
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            
            }
            
            return View(query.ToList());
        }

        public ActionResult Refusal(int Id, string UserId)
        {

            // Query the database for the row to be updated.
            var query =
                from ord in db.ApplyForJobs
                where ord.JobId == Id && ord.UserId == UserId
                select ord;

            // Execute the query, and change the column values
            // you want to change.
            foreach (ApplyForJob ord in query)
            {
                ord.State = -1;
                ord.AcceptDate = DateTime.Now;
                // Insert any additional changes to column values.
            }

            // Submit the changes to the database.
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            return View(query.ToList());
        }
    


        public ActionResult Details1(string UserId)
        {
            var user = db.Users.Find(UserId);
            if (user == null)
            {
                return HttpNotFound();
            }
            Session["UserId"] = UserId;
            return View(user);
        }

        public ActionResult Send()
        {
            var PublisherId = User.Identity.GetUserId();
            var ResearcherId = (string)Session["UserId"];
            //var ResearcherId = UserId;
            //var IdJOB = job_id;
            var messag = db.Messeges.Where(a => a.PublisherId == PublisherId
           && a.ResearcherId == ResearcherId 
           || a.PublisherId == ResearcherId && a.ResearcherId == PublisherId
           );


            // Console.WriteLine("Length: {0}");
            //Session["JobId"] = job_id;
           // Session["UserId"] = UserId;
            //UserId
            return View(messag.ToList());
            //return View();
        }
        [HttpPost]
        public ActionResult Send(string Message)
        {
            
            var PublisherId = User.Identity.GetUserId();
            var ResearcherId = (string)Session["UserId"];
           // var JobId = (int)Session["JobId"];
            var messag = db.Messeges.Where(a => a.PublisherId == PublisherId
            && a.ResearcherId == ResearcherId //&& a.job.Id == job_id
           || a.PublisherId == ResearcherId && a.ResearcherId == PublisherId //&& a.job.Id == job_id
           );

            var m = new Messeges();
            m.PublisherId = PublisherId;
            m.ResearcherId = ResearcherId;
            m.MessegeTime = DateTime.Now;
            m.Messege = Message;
            m.IdJob= 1;
            db.Messeges.Add(m);
            db.SaveChanges();


            // Console.WriteLine("Length: {0}");
            return View(messag.ToList());
           // return View();
        }

        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];

            var check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();

            if (check.Count < 1)
            {
                var job = new ApplyForJob();

                job.UserId = UserId;
                job.JobId = JobId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;

                job.State = 0;
                job.AcceptDate = DateTime.Now;

                db.ApplyForJobs.Add(job);
                db.SaveChanges();

                ViewBag.Result = "Applyed to job !";

            }
            else
            {
                ViewBag.Result = "you cant apply to this job beucuse you applyed befor :) !";

            }


            return View();
        }
        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());
        }

        [Authorize]
        public ActionResult GetUserAccept() 
        {
            var UserID = User.Identity.GetUserId();

            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserID && app.State==1
                       select app;
            var grouped = from j in Jobs
                          group j by j.job.JobTitle
                          into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr
                          };

            return View(grouped.ToList());

        }

        //[Authorize]
        //public ActionResult GetUserAccept()
        //{
        //    var UserId = User.Identity.GetUserId();
        //    var Jobs = db.Jobs.Where(a => a.UserId == UserId && a.State ==1);
        //    return View(Jobs.ToList());
        //}

        public ActionResult GetMessages()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.Messeges.Where(a => a.PublisherId == UserId ||
              a.ResearcherId == UserId);
            return View(Jobs.ToList());
           // return View();
        }
        [Authorize]
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserID = User.Identity.GetUserId();

            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserID
                       select app;
            var grouped = from j in Jobs
                          group j by j.job.JobTitle
                          into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr
                          };

            return View(grouped.ToList());
                    
        }

        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);

        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");

            }
            return View(job);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);

        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {

            // TODO: Add delete logic here
            var myJob = db.ApplyForJobs.Find(job.Id);
            db.ApplyForJobs.Remove(myJob);
            db.SaveChanges();

            return RedirectToAction("GetJobsByUser");

        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jobs.Where(a => a.JobTitle.Contains(searchName)
            || a.JobDescription.Contains(searchName)
            || a.category.categoryName.Contains(searchName)
            || a.category.categoryDescription.Contains(searchName)).ToList();

            return View(result);
        }
    }
}