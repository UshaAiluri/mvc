using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.WebPages.OAuth;
using OAuthFace.Utility;
using System.Net;
using DotNetOpenAuth.OAuth2;


namespace OAuthFace.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/index.html");
        }

        private static readonly FacebookClient client = new FacebookClient
        {
            ClientIdentifier = CConstants.APPID,

            ClientCredentialApplicator =
                ClientCredentialApplicator.PostParameter(CConstants.APPSECRETKEY)
        };

        public ActionResult Login()
        {
            // COMMENT THESE TWO LINES AND MODIFY CConstants
            // with your Facebook AppId and AppSecret Keys
            Session[CConstants.SESSION_ADMNLOGGED] = true;

            return RedirectToAction("Index", "Home", new { Area = "Admn" });

            IAuthorizationState authorization = client.ProcessUserAuthorization();
            if (authorization == null)
            {
                // start authorization request
                // request goes to facebook to request accesstoken
                client.RequestUserAuthorization(new String[] { "email" });
            }
            else
            {
                if (null != authorization.AccessToken)
                {
                    // use that accesstoken to request information
                    // from the user's profile
                    var request = WebRequest.Create(
                        String.Format(
                            "https://graph.facebook.com/me?access_token={0}",
                            Uri.EscapeDataString(authorization.AccessToken))
                            );

                    using (var response = request.GetResponse())
                    {
                        using (var responseStream = response.GetResponseStream())
                        {
                            var graph = FacebookGraph.Deserialize(responseStream);

                            string myUserName = graph.Name;
                            string myEmail = graph.Email;

                            // use the data in the graph object to authorise the user
                            if (
                                !String.IsNullOrEmpty(myEmail)

                                &&

                                myEmail.Equals(
                                    CConstants.ADMINISTRATOR, StringComparison.OrdinalIgnoreCase))
                            {
                                Session[CConstants.SESSION_ADMNLOGGED] = true;

                                return RedirectToAction("Index", "Home", new { Area = "Admn" });

                            }
                        }
                    }
                }
            }

            return Content("Authorization Failed");
        }

        

        
    }
}
