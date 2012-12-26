using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QConnectSDK.Context;
using QConnectSDK;
using System.Web.Security;

namespace HiGirl360.Controllers
{
    public class MemberController : Controller
    {
        public ActionResult JoinUs()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult JoinUs(FormCollection from)
        {
            return View();
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(string username, string password)
        {
            return View();
        }


        public ActionResult LogInQQ()
        {
            var context = new QzoneContext();
            string state = Guid.NewGuid().ToString().Replace("-", "");
            Session["requeststate"] = state;
            string scope = "get_user_info,add_share,list_album,upload_pic,check_page_fans,add_t,add_pic_t,del_t,get_repost_list,get_info,get_other_info,get_fanslist,get_idolist,add_idol,del_idol,add_one_blog,add_topic,get_tenpay_addr";
            var authenticationUrl = context.GetAuthorizationUrl(state, scope);
            return new RedirectResult(authenticationUrl);
        }

        public ActionResult QQConnect()
        {
            if (Request.Params["code"] != null)
            {
                QOpenClient qzone = null;

                var verifier = Request.Params["code"];
                var state = Request.Params["state"];
                string requestState = Session["requeststate"].ToString();

                if (state == requestState)
                {
                    qzone = new QOpenClient(verifier, state);
                    var currentUser = qzone.GetCurrentUser();
                    if (this.Session["QzoneOauth"] == null)
                    {
                        this.Session["QzoneOauth"] = qzone;
                    }
                    var friendlyName = currentUser.Nickname;
                    var isPersistentCookie = true;

                    SetAuthCookie(qzone.OAuthToken.OpenId, friendlyName, isPersistentCookie);

                    return RedirectToAction("Index", "Home");
                }

            }
            return View(); 
        }

        private void SetAuthCookie(string userid,string userName, bool isPersistentCookie)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, userid, DateTime.Now, DateTime.Now.AddDays(1), true, userName);
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            FormsAuthentication.SetAuthCookie(userName, isPersistentCookie, "/");
        }

        private void SetAuth(string userName)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(userName, false, 1);
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
        }

        public void LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            RedirectToAction("Index", "Home");
        }
        
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(string password, string newpassword)
        {
            return View();
        }

        [Authorize]
        public ActionResult AddMoreInfo()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddMoreInfo(FormContext from)
        {
            return View();
        }

    }
}
