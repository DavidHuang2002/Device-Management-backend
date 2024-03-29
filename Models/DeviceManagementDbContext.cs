using System;
using System.Collections.Generic;
using Device_Management.Models.AlertManagement;
using Device_Management.Models.DeviceUpdate;
using Microsoft.EntityFrameworkCore;

namespace Device_Management.Models;

public partial class DeviceManagementDbContext : DbContext
{
    // TODO: learn why need to remove default constructor
    //public DeviceManagementDbContext()
    //{
    //}

    public DeviceManagementDbContext(DbContextOptions<DeviceManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Device> Devices { get; set; }
    public virtual DbSet<Alert> Alerts { get; set; }
    public DbSet<AlertRule> AlertRules { get; set; }
    public DbSet<AlertTemplate> AlertTemplates { get; set; }
    public virtual DbSet<RaspberryPi> RaspberryPi { get; set; }

    public DbSet<RaspberryPiUpdate> RaspberryPiUpdates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>(entity =>
        {
            entity.ToTable("Device");

            entity.Property(e => e.AddedDate).HasColumnType("date");
            entity.Property(e => e.AzureConnectionString)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.AzureDeviceId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AzureDeviceKey)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastCheckInTime).HasColumnType("datetime");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Notes)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(true);
        });

        modelBuilder.Entity<Alert>(entity => // Adding Alert model configuration
        {
            entity.ToTable("Alert");

            entity.Property(e => e.DeviceId).IsRequired();
            entity.Property(e => e.Severity).HasMaxLength(255);
            entity.Property(e => e.Description).HasColumnType("nvarchar(max)");
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.AcknowledgedBy).HasMaxLength(255);
            entity.Property(e => e.ResolvedBy).HasMaxLength(255);
            entity.Property(e => e.AdditionalInfo).HasColumnType("nvarchar(max)");
        });

        modelBuilder.Entity<RaspberryPi>(entity => 
        {
            entity.ToTable("RaspberryPi");
            entity.Property(e => e.Temperature).HasColumnType("float");
            entity.Property(e => e.Humidity).HasColumnType("float");
        });

        // This will set the table name for the RaspberryPiUpdate entity.
        modelBuilder.Entity<RaspberryPiUpdate>().ToTable("RaspberryPiUpdate");


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
