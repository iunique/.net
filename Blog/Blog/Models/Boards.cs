using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Boards
    {
        public int ID { get; set; }
        public DateTime UpDate { get; set; }

        public string Text { get; set; }
        public string Author { get; set; }
        public string BeLong { get; set; }
    }
}