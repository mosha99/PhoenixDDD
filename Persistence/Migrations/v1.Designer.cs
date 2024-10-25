﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(PhoenixDbContext))]
    [Migration("20241023145241_v1")]
    partial class v1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("ContractorId_Seq")
                .StartsAt(1000L);

            modelBuilder.HasSequence("TenderId_Seq")
                .StartsAt(1000L);

            modelBuilder.Entity("Contractor.Contractor", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TenancyId")
                        .HasColumnType("bigint");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Contractors");
                });

            modelBuilder.Entity("Tender.Tender", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CancelDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CloseDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("OpeDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<long>("TenancyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tenders");
                });

            modelBuilder.Entity("Tender.Tender", b =>
                {
                    b.OwnsOne("BuildingBlocks.DateTimeFrame", "DateTimeFrame", b1 =>
                        {
                            b1.Property<long>("TenderId")
                                .HasColumnType("bigint");

                            b1.Property<DateTime>("End")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("datetime2");

                            b1.HasKey("TenderId");

                            b1.ToTable("Tenders");

                            b1.WithOwner()
                                .HasForeignKey("TenderId");
                        });

                    b.OwnsOne("BuildingBlocks.PricingFrame<long>", "PricingFrame", b1 =>
                        {
                            b1.Property<long>("TenderId")
                                .HasColumnType("bigint");

                            b1.Property<long>("End")
                                .HasColumnType("bigint");

                            b1.Property<long>("Start")
                                .HasColumnType("bigint");

                            b1.HasKey("TenderId");

                            b1.ToTable("Tenders");

                            b1.WithOwner()
                                .HasForeignKey("TenderId");
                        });

                    b.OwnsMany("Tender.ContractorBid", "Bids", b1 =>
                        {
                            b1.Property<long>("TenderId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<long>("BidAmount")
                                .HasColumnType("bigint");

                            b1.Property<long>("ContractorId")
                                .HasColumnType("bigint");

                            b1.Property<DateTime>("CreateDate")
                                .HasColumnType("datetime2");

                            b1.Property<bool>("Winner")
                                .HasColumnType("bit");

                            b1.HasKey("TenderId", "Id");

                            b1.ToTable("Tenders");

                            b1.ToJson("Bids");

                            b1.WithOwner()
                                .HasForeignKey("TenderId");
                        });

                    b.Navigation("Bids");

                    b.Navigation("DateTimeFrame")
                        .IsRequired();

                    b.Navigation("PricingFrame")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
