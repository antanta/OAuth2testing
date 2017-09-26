using OAuth2;
using OAuth2.Client;
using OAuth2.Client.Impl;
using OAuth2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAuth2testing.Controllers
{
    /*
        https://github.com/login/oauth/authorize 
        https://github.com/login/oauth/access_token

        //https://cloud.digitalocean.com/v1/oauth/authorize?response_type=token&client_id=CLIENT_ID&redirect_uri=CALLBACK_URL&scope=read
        https://github.com/login/oauth/authorize?response_type=code&client_id=9d04a896d531746e3dcf&redirect_uri=http://localhost:62179/Home/Auth&scope=user:email
    */

    public class HomeController : Controller
    {
        public HomeController(AuthorizationRoot authorizationRoot)
        {
            var t = new AuthorizationRoot();
            this.authorizationRoot = authorizationRoot;
        }

        public HomeController() : this(new AuthorizationRoot())
        {
        }

        public ActionResult Index()
        {
            string uri = authorizationRoot.Clients.ElementAt(0).GetLoginLinkUri();
            ViewBag.Uri = uri;
            return View();
        }

        public ActionResult Auth()
        {
            IClient currentClient = authorizationRoot.Clients.ElementAt(0);
            UserInfo info = currentClient.GetUserInfo(Request.QueryString);
            //TODO manually create a session, add token to database
            return View("Index", info);
        }

        #region Private members
        private AuthorizationRoot authorizationRoot;
        #endregion
    }
}