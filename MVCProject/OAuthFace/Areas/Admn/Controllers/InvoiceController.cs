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
    public class InvoiceController : Controller
    {
        private void FillProducts(dbOrdersEntities ctxt)
        {
            ViewBag.Products = ctxt.tblProducts.ToList<tblProduct>();

            ViewBag.Products.Insert(0, new tblProduct() { productCode = "", productName = "[-Select-]" });
        }

        public ActionResult Index(int? id)
        {
            tblOrder t = new tblOrder();
            using (dbOrdersEntities ctxt = new dbOrdersEntities())
            {
                FillProducts(ctxt);

                t.tblUser = ctxt.tblUsers.SingleOrDefault<tblUser>(x => x.idAutoPK == id);
            }
            return View(t);
        }

        public ActionResult EditInvoice(int? id)
        {
            tblOrder t = null;
            using (dbOrdersEntities ctx = new dbOrdersEntities())
            {
                FillProducts(ctx);

                t = ctx.tblOrders.Include("tblUser").SingleOrDefault<tblOrder>(x => x.idAutoPK == id);
            }
            return View(t);
        }

        [HttpPost]
        public ActionResult EditInvoice(int? id, tblOrder t, string redirectTo)
        {
            tblOrder tb = null;
            using (dbOrdersEntities ctx = new dbOrdersEntities())
            {
                tb = ctx.tblOrders.Include("tblUser").SingleOrDefault<tblOrder>(x => x.idAutoPK == id);

                tb.invoiceAmount = t.invoiceAmount;

                tb.productCodeFK = t.productCodeFK;

                ctx.SaveChanges();
            }

            if ("PrintInvoice" == redirectTo)
            {
                return RedirectToAction("PrintInvoice", new { id = id });
            }
            else
            {
                return RedirectToAction("Index",
                    "SearchCustomer", new { EMailToSearch = tb.tblUser.customerEmail });
            }
        }

        public ActionResult AutoComplete(String term)
        {
            using (dbOrdersEntities ctx = new dbOrdersEntities())
            {
                var arr = ctx.tblUsers
                    .Where<tblUser>(x => x.customerEmail.StartsWith(term))
                    .Select<tblUser, string>(x => x.customerEmail)
                    .ToArray<string>();

                return Json(arr, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Index(int? id, tblOrder t)
        {
            using (dbOrdersEntities ctxt = new dbOrdersEntities())
            {
                if (ModelState.IsValid)
                {
                    t.customerFK = id ?? 0;
                    t.orderDate = DateTime.Now;
                    ctxt.tblOrders.Add(t);
                    ctxt.SaveChanges();
                }
            }

            return RedirectToAction("PrintInvoice", new { id = t.idAutoPK });
        }

        public ActionResult PrintInvoice(int? id)
        {
            tblOrder t = null;

            using (dbOrdersEntities ctxt = new dbOrdersEntities())
            {
                t = ctxt.tblOrders
                    .Include("tblUser")
                    .Include("tblProduct")
                    .SingleOrDefault<tblOrder>(x => x.idAutoPK == id);
            }

            return View(t);
        }

        public ActionResult ShowLastTenInvoices()
        {
            List<tblOrder> ret = null;
            using (dbOrdersEntities ctxt = new dbOrdersEntities())
            {
                ret = ctxt.tblOrders
                    .Include("tblProduct")
                    .Include("tblUser")
                    .OrderByDescending<tblOrder, DateTime>(x => x.orderDate)
                    .Take<tblOrder>(10).ToList<tblOrder>();
            }
            return View(ret);
        }

    }
}
