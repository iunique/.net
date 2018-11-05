﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Passage
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Sort { get; set; }
        public string Text { get; set; }
        public DateTime OutDate { get; set; }
        public string Author { get; set; }
    }
}