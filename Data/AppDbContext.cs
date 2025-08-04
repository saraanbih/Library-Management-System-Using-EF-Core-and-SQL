using System;
using System.Collections.Generic;
using Library_Management_System_Task.Data;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_Task.Entities;

public partial class AppDbContext : DbContext
{
    public AppDbContext(){}

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options){}

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BorrowedBook> BorrowedBooks { get; set; }

    public virtual DbSet<Borrower> Borrowers { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.;Database=Library_Management_System;Integrated Security = SSPI ;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__authors__3213E83F0D7FAAFD");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__books__3213E83F9951CC06");

            entity.HasOne(d => d.Author).WithMany(p => p.Books).HasConstraintName("FK__books__author_id__398D8EEE");
        });

        modelBuilder.Entity<BorrowedBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__borrowed__3213E83F59B4BDEA");

            entity.HasOne(d => d.Book).WithMany(p => p.BorrowedBooks).HasConstraintName("FK__borrowed___book___3F466844");

            entity.HasOne(d => d.Borrower).WithMany(p => p.BorrowedBooks).HasConstraintName("FK__borrowed___borro__403A8C7D");
        });

        modelBuilder.Entity<Borrower>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__borrower__3213E83F36518D87");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__reviews__3213E83F60F6A710");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Book).WithMany(p => p.Reviews).HasConstraintName("FK__reviews__book_id__44FF419A");

            entity.HasOne(d => d.Borrower).WithMany(p => p.Reviews).HasConstraintName("FK__reviews__borrowe__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
