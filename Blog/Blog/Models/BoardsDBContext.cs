using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class BoardsDBContext:DbContext
    {
        public DbSet<Boards> Boards { get; set; }
    }
}