using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.Models;

public partial class coredb2Context : DbContext
{
    public coredb2Context(DbContextOptions<coredb2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<MainTestType> MainTestType { get; set; }

    public virtual DbSet<MainType> MainType { get; set; }

    public virtual DbSet<SubTestType> SubTestType { get; set; }

    public virtual DbSet<SubTypes> SubTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MainTestType>(entity =>
        {
            entity.HasIndex(e => e.MainTypeId, "IX_MainTestType_MainTypeId");

            entity.HasOne(d => d.MainType).WithMany(p => p.MainTestType)
                .HasForeignKey(d => d.MainTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SubTestType>(entity =>
        {
            entity.Property(e => e.AddColumn)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<SubTypes>(entity =>
        {
            entity.HasIndex(e => e.MainTypeId, "IX_SubTypes_MainTypeId");

            entity.HasOne(d => d.MainType).WithMany(p => p.SubTypes).HasForeignKey(d => d.MainTypeId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
