using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HelpDeskDAL
{
    public partial class HelpdeskContext : DbContext
    {
        public HelpdeskContext()
            : base("name=HelpdeskContext")
        {
        }

        public virtual DbSet<Call> Calls { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Problem> Problems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Call>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<Call>()
                .Property(e => e.Timer)
                .IsFixedLength();

            modelBuilder.Entity<Department>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Timer)
                .IsFixedLength();

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PhoneNo)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Timer)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Calls)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Calls1)
                .WithRequired(e => e.Employee1)
                .HasForeignKey(e => e.TechId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Problem>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Problem>()
                .Property(e => e.Timer)
                .IsFixedLength();

            modelBuilder.Entity<Problem>()
                .HasMany(e => e.Calls)
                .WithRequired(e => e.Problem)
                .WillCascadeOnDelete(false);
        }
    }
}
