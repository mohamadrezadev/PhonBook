﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhonBook.infra.Data.sqlserver.Common;

#nullable disable

namespace PhonBook.infra.Data.sqlserver.Migrations
{
    [DbContext(typeof(ApplicationDbcontext))]
    [Migration("20230106124735_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ContactGrop", b =>
                {
                    b.Property<int>("Contact_id")
                        .HasColumnType("int");

                    b.Property<int>("Group_Id")
                        .HasColumnType("int");

                    b.HasKey("Contact_id", "Group_Id");

                    b.HasIndex("Group_Id");

                    b.ToTable("ContactGrop");
                });

            modelBuilder.Entity("PhonBook.Core.Domain.Contacts.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fullname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Group_Id")
                        .HasColumnType("int");

                    b.Property<string>("POST")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone_number1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone_number2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tell_phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("PhonBook.Core.Domain.Grops.Grop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code_posti")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Contact_id")
                        .HasColumnType("int");

                    b.Property<int>("Fax")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Organization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tell_phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Grops");
                });

            modelBuilder.Entity("ContactGrop", b =>
                {
                    b.HasOne("PhonBook.Core.Domain.Grops.Grop", null)
                        .WithMany()
                        .HasForeignKey("Contact_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhonBook.Core.Domain.Contacts.Contact", null)
                        .WithMany()
                        .HasForeignKey("Group_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
