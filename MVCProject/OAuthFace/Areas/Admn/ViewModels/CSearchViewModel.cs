using OAuthFace.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OAuthFace.Areas.Admn.ViewModels
{
    public class CSearchViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        [RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "EMail?")]
        public String EMailToSearch { get; set; }

        public tblUser User { get; set; }

        public IEnumerable<tblOrder> Orders { get; set; }
    }
}
