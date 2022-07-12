using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ContosoUniversity.DAL
{
    public class ContosoUniversityContext : DbContext
    {
        public ContosoUniversityContext() : base("CUniContext")
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /* Evita que las tablas se creen en plural. */
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}