using OAuthFace.EntityModels;
using OAuthFace.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAuthFace.Areas.Admn.Controllers
{
    [MyAuth]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(tblUser t)
        {
            if (ModelState.IsValid)
            {
                using (dbOrdersEntities ctxt = new dbOrdersEntities())
                {
                    ctxt.tblUsers.Add(t);
                    ctxt.SaveChanges();
                }

                ViewBag.Message = "Record Saved";

                ModelState.Clear();
            }
            return View();
        }

        public ActionResult IsEMailAvailable(String customerEMail, String hfSuppressRemote)
        {
            bool bRet = false;

            using (dbOrdersEntities ctx = new dbOrdersEntities())
            {
                bRet = (
                    null == ctx.tblUsers.SingleOrDefault(x => x.customerEmail == customerEMail)
                    );
            }

            return Json(hfSuppressRemote == "1" ? true : bRet, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditCustomer(int? id)
        {
            tblUser t = null;

            using (dbOrdersEntities ctxt = new dbOrdersEntities())
            {
                t = ctxt.tblUsers.SingleOrDefault<tblUser>(x => x.idAutoPK == id);
            }
            return View(t);
        }

        [HttpPost]
        public ActionResult EditCustomer(int? id, tblUser t)
        {
            using (dbOrdersEntities ctxt = new dbOrdersEntities())
            {
                t.idAutoPK = id ?? 0;
                ctxt.tblUsers.Attach(t);
                ctxt.Entry(t).State = System.Data.EntityState.Modified;

                ctxt.SaveChanges();
            }
            return RedirectToAction("Index", "SearchCustomer", new {EMailToSearch = t.customerEmail});
        }

        public ActionResult SignOut()
        {
            Session.Clear();
            return Redirect("/Index.html");
        }
    }
}
