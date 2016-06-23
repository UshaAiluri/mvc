using OAuthFace.Areas.Admn.Models;
using OAuthFace.Areas.Admn.ViewModels;
using OAuthFace.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAuthFace.Areas.Admn.Controllers
{
    public class SearchCustomerController : Controller
    {
        public ActionResult Index(String EMailToSearch)
        {
            tblUser t = null;

            List<tblOrder> ord = null;

            if (!String.IsNullOrEmpty(EMailToSearch))
            {
                using (dbOrdersEntities ctx = new dbOrdersEntities())
                {
                    t = ctx
                        .tblUsers
                        .SingleOrDefault<tblUser>(x => x.customerEmail == EMailToSearch);

                    if (null != t)
                    {
                        ord = ctx.tblOrders
                            .Include("tblProduct")
                            .Where<tblOrder>(x => x.customerFK == t.idAutoPK)
                            .ToList<tblOrder>();
                    }
                }
            }

            CSearchViewModel cs = new CSearchViewModel()
            {
                EMailToSearch = EMailToSearch,
                User = t,
                Orders = ord
            };

            return View(cs);
        }

        public ActionResult ShowTopTenCustomers()
        {
            List<CCustomerOrderReport> ret = null;

            using (dbOrdersEntities ctxt = new dbOrdersEntities())
            {
                var orders = ctxt.tblOrders;
                ret = ctxt.tblUsers
                    .GroupJoin<tblUser, tblOrder, int, CCustomerOrderReport>(orders,
                    x => x.idAutoPK, y => y.customerFK, (h, k) => new
                    CCustomerOrderReport()
                    {
                        CustomerName = h.customerEmail,
                        TotalOrderAmount = k.Sum<tblOrder>(x => x.invoiceAmount),
                        TotalNumberOfOrders = k.Count<tblOrder>()
                    })
                    .OrderByDescending(z => z.TotalOrderAmount)
                    .ToList<CCustomerOrderReport>();

                return View(ret);
            }


        }
    }

}
