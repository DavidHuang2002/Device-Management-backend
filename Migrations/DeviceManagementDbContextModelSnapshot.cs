﻿// <auto-generated />
using System;
using Device_Management.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Device_Management.Migrations
{
    [DbContext(typeof(DeviceManagementDbContext))]
    partial class DeviceManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Device_Management.Models.AlertManagement.Alert", b =>
                {
                    b.Property<int>("AlertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlertId"));

                    b.Property<string>("AcknowledgedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("AcknowledgedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlertName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<string>("ResolvedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ResolvedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Severity")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("AlertId");

                    b.ToTable("Alert", (string)null);
                });

            modelBuilder.Entity("Device_Management.Models.AlertManagement.AlertRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlertTemplateId")
                        .HasColumnType("int");

                    b.Property<string>("Attribute")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AttributeDataType")
                        .HasColumnType("int");

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Operator")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AlertTemplateId");

                    b.HasIndex("DeviceId");

                    b.ToTable("AlertRules");
                });

            modelBuilder.Entity("Device_Management.Models.AlertManagement.AlertTemplate", b =>
                {
                    b.Property<int>("AlertTemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlertTemplateId"));

                    b.Property<string>("AlertName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlertTemplateName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Severity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AlertTemplateId");

                    b.ToTable("AlertTemplates");
                });

            modelBuilder.Entity("Device_Management.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("date");

                    b.Property<string>("AzureConnectionString")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("AzureDeviceId")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("AzureDeviceKey")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("LastCheckInTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Location")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Device", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Device_Management.Models.DeviceUpdate.RaspberryPiUpdate", b =>
                {
                    b.Property<int>("UpdateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UpdateId"));

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<float?>("Humidity")
                        .HasColumnType("real");

                    b.Property<float?>("Temperature")
                        .HasColumnType("real");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("UpdateId");

                    b.ToTable("RaspberryPiUpdate", (string)null);
                });

            modelBuilder.Entity("Device_Management.Models.RaspberryPi", b =>
                {
                    b.HasBaseType("Device_Management.Models.Device");

                    b.Property<double?>("Humidity")
                        .HasColumnType("float");

                    b.Property<double?>("Temperature")
                        .HasColumnType("float");

                    b.ToTable("RaspberryPi", (string)null);
                });

            modelBuilder.Entity("Device_Management.Models.AlertManagement.AlertRule", b =>
                {
                    b.HasOne("Device_Management.Models.AlertManagement.AlertTemplate", "AlertTemplate")
                        .WithMany()
                        .HasForeignKey("AlertTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Device_Management.Models.Device", null)
                        .WithMany("AlertRules")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AlertTemplate");
                });

            modelBuilder.Entity("Device_Management.Models.RaspberryPi", b =>
                {
                    b.HasOne("Device_Management.Models.Device", null)
                        .WithOne()
                        .HasForeignKey("Device_Management.Models.RaspberryPi", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Device_Management.Models.Device", b =>
                {
                    b.Navigation("AlertRules");
                });
#pragma warning restore 612, 618
        }
    }
}
