using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class MovieDBContext : DbContext
    {        public DbSet<Movies> Movies { get; set; }
    }
}