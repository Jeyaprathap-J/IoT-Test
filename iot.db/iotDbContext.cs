using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace iot.db;

public partial class iotDbContext : DbContext
{
    private readonly string connStr = "";
    public iotDbContext(string _connStr)
    {
        connStr = _connStr;
    }

    public iotDbContext(DbContextOptions<iotDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CommunicationLog> CommunicationLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured == false) 
        { 
            optionsBuilder.UseSqlServer(connStr);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommunicationLog>(entity =>
        {
            entity.ToTable("CommunicationLog");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.InputDeviceName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OutputDeviceName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Request)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("updatedBy");
            entity.Property(e => e.UpdatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("updatedDateTime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
