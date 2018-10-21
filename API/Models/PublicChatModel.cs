using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIXamarin.Models
{
    public class PublicChatModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Sender { get; set; }
    }
}