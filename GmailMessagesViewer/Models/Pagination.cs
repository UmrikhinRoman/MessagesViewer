using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GmailMessagesViewer.Models
{
    public class Pagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItemsCount { get; set; }
        public int TotalPagesCount
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItemsCount/ PageSize);
            }
        }
    }
}