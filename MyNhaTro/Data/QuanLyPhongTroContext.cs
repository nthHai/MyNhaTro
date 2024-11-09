using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace MyNhaTro.Data;

public partial class QuanLyPhongTroContext : IdentityDbContext<ApplicationUser>
{
    public QuanLyPhongTroContext(DbContextOptions<QuanLyPhongTroContext> options)
        : base(options)
    {

    }

    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Customer>(); //Bỏ qua việc Migration cho Model Customer
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__customer__3213E83F466F1807");
        });

        OnModelCreatingPartial(modelBuilder); 
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
