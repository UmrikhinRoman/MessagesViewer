using GmailMessagesViewer.Filters;
using GmailMessagesViewer.Models;
using GmailMessagesViewer.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GmailMessagesViewer.Controllers
{
    [UserSession]
    public class MessagesController : Controller
    {
        private GmailService service;
        private static UserCredential credential;
        private static string ApplicationName = "GetMessagesApp";

        public MessagesController()
        {
            credential = AuthService.CreateCredentials(this);
            service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        // GET: Messages
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMessages(int pageNumber=1)
        {
            int pageSize = 10;
            var messages = MessageService.GetMessagesList(service, "me");
            var totalMessagesCount = messages.Count;

            messages = messages
                .Skip((pageSize - 1) * pageNumber)
                .Take(pageSize)
                .ToList();

            var viewModel = new MessagesListViewModel()
            {
                Messages = new List<MessageInfoViewModel>(),
                Pagination = new Pagination()
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    TotalItemsCount = totalMessagesCount
                }
            };

            foreach (var mes in messages)
            {
                Message message = MessageService.GetMessage(service, "me", mes.Id);
                viewModel.Messages.Add(new MessageInfoViewModel
                {
                    Id = message.Id,
                    Title = message.Snippet
                });
            }

            return View(viewModel);
        }
        public ActionResult ChangeUser()
        {
            AuthService.RemoveCredentials(this);
            return RedirectToAction("GetMessages");
        }
    }
}