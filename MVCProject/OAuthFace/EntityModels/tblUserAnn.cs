using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAuthFace.EntityModels
{
    [MetadataType(typeof(tblUserMD))]
    public partial class tblUser
    {
        public class tblUserMD
        {
            [Required(AllowEmptyStrings=false, ErrorMessage="Required")]
            [MaxLength(50, ErrorMessage="< 50 chars")]
            [Remote("IsEMailAvailable", "Home", "Admn",
                ErrorMessage = "User already exists",
                AdditionalFields = "hfSuppressRemote")]
            
            [RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage="EMail?")]
            public string customerEmail { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
            [MaxLength(50, ErrorMessage = "< 100 chars")]
            public string customerName { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
            [MaxLength(50, ErrorMessage = "< 15 chars")]
            [RegularExpression(@"^\d{10}$", ErrorMessage = "Valid Phone?")]
            public string customerPhone { get; set; }
        }
    }
}