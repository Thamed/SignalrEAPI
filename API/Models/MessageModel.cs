using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Sender { get; set; }
        public string Reciver { get; set; }
        public string Time { get; set; }
    }
}