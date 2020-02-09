using Jobs_Offers_Website.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Jobs_Offers_Website.Controllers
{
    public class MessegesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Messeges
        public ActionResult Index()
        {
            return View();
        }

        // GET: Messeges/Details/5
       
        // GET: Messeges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Messeges/Create
        [HttpPost]
        public ActionResult Create(string userId)
        {
                
               var Publisher = userId;
            return View(Publisher);
        }

        // GET: Messeges/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Messeges/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Messeges/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Messeges/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
