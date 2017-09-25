using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GmailMessagesViewer.Models
{
    public class MessagesListViewModel
    {
        public Pagination Pagination { get; set; }
        public List<MessageInfoViewModel> Messages { get; set; }
    }
}