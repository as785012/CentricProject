using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CentricProject.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CentricProject.Models.DAL
{
    public class centricContext : DbContext
    {
        public centricContext() : base("name=DefaultConnection")
        {

        }

        public DbSet<userDetails> userDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

    }
}