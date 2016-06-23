using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuthFace.Areas.Admn.Models
{
    public class CCustomerOrderReport
    {
        public String CustomerName { get; set; }
        public int? TotalOrderAmount { get; set; }
        public int? TotalNumberOfOrders { get; set; }
    }
}