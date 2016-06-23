using System.Web.Mvc;

namespace OAuthFace.Areas.Admn
{
    public class AdmnAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admn";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admn_default",
                "Admn/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
