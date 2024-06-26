﻿// <auto-generated />
using System;
using FUser.CLDataAccess.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FUser.CLDataAccess.Migrations
{
    [DbContext(typeof(MFUserDbContext))]
    [Migration("20240312165237_Mig_MFUVMachine")]
    partial class Mig_MFUVMachine
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FUser.CLDomain.MJwtSTokens", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("JwtSToken")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("DbS_JSTokens");
                });

            modelBuilder.Entity("FUser.CLDomain.MProduct", b =>
                {
                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AmountAvailable")
                        .HasColumnType("int");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellerId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("DbS_Products");
                });

            modelBuilder.Entity("FUser.CLDomain.MUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Deposit")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DbS_Users");
                });
#pragma warning restore 612, 618
        }
    }
}
