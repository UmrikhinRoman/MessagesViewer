using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmailMessagesViewer.Services
{
    class MessageService
    {
        public static Message GetMessage(GmailService service, string userId, string messageId)
        {
            try
            {
                return service.Users.Messages.Get(userId, messageId).Execute();
            }
            catch (Exception)
            {
                return null;
            }

            
        }
        public static List<Message> GetMessagesList(GmailService service, string userId)
        {
            List<Message> result = new List<Message>();
            UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List(userId);

            do
            {
                 ListMessagesResponse response = request.Execute();
                 result.AddRange(response.Messages);
                 request.PageToken = response.NextPageToken;
            } while (!string.IsNullOrEmpty(request.PageToken));

            return result;
        }

    }
}
