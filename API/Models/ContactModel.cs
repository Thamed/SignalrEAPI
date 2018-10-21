using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ContactModel
    {
        [Key]
        public int ID { get; set; }
        public string Me { get; set; }
        public string MyContact { get; set; }
    }
}