﻿// <auto-generated />
using System;
using DataAccess.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.EFCore.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20241113064841_addedProductsProductCategback")]
    partial class addedProductsProductCategback
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("CustomerType")
                        .HasColumnType("int");

                    b.Property<int?>("DeletedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("DeletionTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsModified")
                        .HasColumnType("bit");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Middlename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("DeletedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("DeletionTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsModified")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Entities.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("DeletedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("DeletionTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsModified")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("Domain.Entities.SalesDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("DeletedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("DeletionTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsModified")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("SalesHeaderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SalesHeaderId");

                    b.ToTable("SalesDetails");
                });

            modelBuilder.Entity("Domain.Entities.SalesHeader", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("DeletedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("DeletionTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsModified")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TransNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SalesHeaders");
                });

            modelBuilder.Entity("Domain.Entities.SalesReturn", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("DeletedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("DeletionTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsModified")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("SalesHeaderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SalesHeaderId");

                    b.ToTable("SalesReturns");
                });

            modelBuilder.Entity("Domain.Entities.StockDamageDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("DeletedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("DeletionTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsModified")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("StockDamageHeaderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("StockDamageHeaderId");

                    b.ToTable("StockDamageDetails");
                });

            modelBuilder.Entity("Domain.Entities.StockDamageHeader", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("DeletedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("DeletionTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsModified")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StockDamageHeaders");
                });

            modelBuilder.Entity("Domain.Entities.StocksReceiving", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("DeletedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("DeletionTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsModified")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TransNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("StocksReceivings");
                });

            modelBuilder.Entity("ProductProductCategory", b =>
                {
                    b.Property<int>("ProductCategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.HasKey("ProductCategoriesId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("ProductProductCategory");
                });

            modelBuilder.Entity("Domain.Entities.SalesDetail", b =>
                {
                    b.HasOne("Domain.Entities.Customer", "CustomerFk")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Product", "ProductFk")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.SalesHeader", "SalesHeaderFk")
                        .WithMany("SalesDetails")
                        .HasForeignKey("SalesHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerFk");

                    b.Navigation("ProductFk");

                    b.Navigation("SalesHeaderFk");
                });

            modelBuilder.Entity("Domain.Entities.SalesReturn", b =>
                {
                    b.HasOne("Domain.Entities.SalesHeader", "SalesHeaderFk")
                        .WithMany()
                        .HasForeignKey("SalesHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SalesHeaderFk");
                });

            modelBuilder.Entity("Domain.Entities.StockDamageDetail", b =>
                {
                    b.HasOne("Domain.Entities.Product", "ProductFK")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.StockDamageHeader", "StockDamageHeaderFk")
                        .WithMany("StockDamageDetails")
                        .HasForeignKey("StockDamageHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductFK");

                    b.Navigation("StockDamageHeaderFk");
                });

            modelBuilder.Entity("Domain.Entities.StocksReceiving", b =>
                {
                    b.HasOne("Domain.Entities.Product", "ProductFK")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductFK");
                });

            modelBuilder.Entity("ProductProductCategory", b =>
                {
                    b.HasOne("Domain.Entities.ProductCategory", null)
                        .WithMany()
                        .HasForeignKey("ProductCategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.SalesHeader", b =>
                {
                    b.Navigation("SalesDetails");
                });

            modelBuilder.Entity("Domain.Entities.StockDamageHeader", b =>
                {
                    b.Navigation("StockDamageDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
