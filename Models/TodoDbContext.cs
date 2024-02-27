using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Serverside.Models;

public partial class TodoDbContext : DbContext
{
    public TodoDbContext()
    {
    }

    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cpr> Cprs { get; set; }

    public virtual DbSet<TodoList> TodoLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TodoDB;Trusted_Connection=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cpr>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cpr__3214EC07FBFE7202");

            entity.ToTable("Cpr");

            entity.Property(e => e.CprNr).HasMaxLength(500);
            entity.Property(e => e.User).HasMaxLength(500);
        });

        modelBuilder.Entity<TodoList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TodoList__3214EC07DC714127");

            entity.ToTable("TodoList");

            entity.Property(e => e.Item).HasMaxLength(500);
            entity.Property(e => e.User).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
