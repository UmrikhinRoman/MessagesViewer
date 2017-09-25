using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace GmailMessagesViewer.Services
{
    public static class AuthService
    {
        private static string credPath;
        public static UserCredential CreateCredentials(Controller controller)
        {
            //if (controller.Session == null)
            //{
            //    controller.Session["User"] = "user";
            //}
            UserCredential credential;
            var secretPath = HostingEnvironment.ApplicationPhysicalPath + "client_id.json";
            using (var stream = new FileStream(secretPath, FileMode.Open, FileAccess.Read))
            {
                credPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    "GmailCredentials/credentials.json");
                var secret = GoogleClientSecrets.Load(stream).Secrets;
                return credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    secret,
                    new []{ GmailService.Scope.GmailReadonly },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
        }
        public static void RemoveCredentials(Controller controller)
        {
            controller.Session["User"] = null;
            Directory.Delete(credPath, true);
        }
    }
}