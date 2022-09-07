using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        { 
            var context = new UserContext();
            
        List<User> users = context.Users.ToList();
            return View(users);
        }
        /**
         * To get patial User List
         */
        public ActionResult UserList()
        {
            var context = new UserContext();

            List<User> users = context.Users.ToList();
            return PartialView(users);
        }

        public ActionResult Edit(int id)
        {
            var usrCtx = new UserContext();
            User user = usrCtx.Users.Find(id);
            return PartialView(user);
        }
        /**
         * To call Edit from ajax
         */
        [HttpPost]
        public bool Edit(User user) 
        {
            if (ModelState.IsValid)
            {
                var ctx = new UserContext();
                ctx.Entry(user).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public ActionResult Delete(int id)
        {
            var usrCtx = new UserContext();
            User user = usrCtx.Users.Find(id);
            return PartialView(user);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfimed(int id)
        {
            var usrCtx = new UserContext();
            User user = usrCtx.Users.Find(id);
            usrCtx.Users.Remove(user);
            usrCtx.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public bool Create(User user)
        {
            var ctx = new UserContext();
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return true;
        }
    }
}