using GmailMessagesViewer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GmailMessagesViewer.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult ChangeUser()
        {
            AuthService.RemoveCredentials(this);
            return RedirectToAction("GetMessages", "Messages");
        }
    }
}