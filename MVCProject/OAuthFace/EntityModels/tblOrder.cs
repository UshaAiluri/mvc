//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OAuthFace.EntityModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblOrder
    {
        public int idAutoPK { get; set; }
        public int customerFK { get; set; }
        public string productCodeFK { get; set; }
        public short invoiceAmount { get; set; }
        public System.DateTime orderDate { get; set; }
    
        public virtual tblProduct tblProduct { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}
