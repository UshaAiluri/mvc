using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAuthFace.EntityModels
{
    [MetadataType(typeof(tblOrderMD))]
    public partial class tblOrder
    {
        public class tblOrderMD
        {
            //[Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
            //[MaxLength(50, ErrorMessage = "< 50 chars")]
            //[RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "EMail?")]
            //public string customerFK { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
            [Range(1000, 100000, ErrorMessage = "Order Amount should be between 1000 and 1-lakh")]
            [RegularExpression(@"[0-9]*$", ErrorMessage = "Please enter a valid number")]
            public short invoiceAmount { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
            public string productCodeFK { get; set; }

            [ScaffoldColumn(false)]
            public int idAutoPK { get; set; }

            [DisplayFormat(DataFormatString="{0:dd-MMM-yyyy}")]
            public System.DateTime orderDate { get; set; }
        }

        
    }
}