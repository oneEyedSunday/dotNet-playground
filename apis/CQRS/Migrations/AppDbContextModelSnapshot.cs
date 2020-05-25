﻿// <auto-generated />
using System;
using CQRSDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CQRSDemo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4");

            modelBuilder.Entity("CQRSDemo.Models.Command", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Data")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CommandStore");
                });

            modelBuilder.Entity("CQRSDemo.Models.Goal", b =>
                {
                    b.Property<int>("GoalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Actionable")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CompleteDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PersonNIN")
                        .HasColumnType("TEXT");

                    b.HasKey("GoalId");

                    b.HasIndex("PersonNIN");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("CQRSDemo.Models.Person", b =>
                {
                    b.Property<string>("NIN")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasColumnType("TEXT");

                    b.HasKey("NIN");

                    b.ToTable("People");
                });

            modelBuilder.Entity("CQRSDemo.Models.Goal", b =>
                {
                    b.HasOne("CQRSDemo.Models.Person", null)
                        .WithMany("Goals")
                        .HasForeignKey("PersonNIN");
                });
#pragma warning restore 612, 618
        }
    }
}
