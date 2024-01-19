﻿// <auto-generated />
using System;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gibs.MultiBroker.Portal.Migrations
{
    [DbContext(typeof(BrokerContext))]
    [Migration("20240119101106_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gibs.Domain.Entities.Broker", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("BrokerId");

                    b.Property<string>("BrokerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BrokerName");

                    b.Property<DateTime>("CreatedUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedUtc");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FirstName");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Phone");

                    b.Property<string>("RegistrationNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("RegistrationNo");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Brokers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "OMOMOWO_RELIANCE",
                            BrokerName = "Omomowo Reliance",
                            CreatedUtc = new DateTime(2024, 1, 19, 11, 11, 5, 740, DateTimeKind.Local).AddTicks(160),
                            Email = "omo@gmail.com",
                            FirstName = "Omomowo",
                            IsActive = false,
                            LastName = "Reliance",
                            Password = "6170F04B86312D18C0BE6EB0B0E9A2D2534DC1F6B51736840FB1B4645C3A882F55A062B37F76B85F8A207DEDAA6E67AE175E45C85A74EA14B49B8DDFA25BC8CA",
                            Phone = "08095482981",
                            RegistrationNo = "12345678"
                        });
                });

            modelBuilder.Entity("Gibs.Domain.Entities.Insured", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("InsuredId");

                    b.Property<string>("BrokerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CompanyName");

                    b.Property<DateTime>("CreatedUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedUtc");

                    b.Property<DateOnly>("DateOfBirthOrReg")
                        .HasColumnType("date")
                        .HasColumnName("DateOfBirthOrReg");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FirstName");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<bool>("IsCorporate")
                        .HasColumnType("bit")
                        .HasColumnName("IsCorporate");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Phone");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.HasIndex("BrokerId");

                    b.ToTable("Insureds", (string)null);
                });

            modelBuilder.Entity("Gibs.Domain.Entities.Insurer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("InsurerId");

                    b.Property<int>("ApiAuthStyle")
                        .HasColumnType("int")
                        .HasColumnName("ApiAuthStyle");

                    b.Property<DateTime>("CreatedUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedUtc");

                    b.Property<string>("InsurerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("InsurerName");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<string>("NaicomId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NaicomId");

                    b.HasKey("Id");

                    b.ToTable("Insurers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "CORNERSTONE",
                            ApiAuthStyle = 1,
                            CreatedUtc = new DateTime(2024, 1, 19, 10, 11, 5, 740, DateTimeKind.Utc).AddTicks(6542),
                            InsurerName = "Cornerstone Insurance",
                            IsActive = true,
                            NaicomId = "naicomId"
                        });
                });

            modelBuilder.Entity("Gibs.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BrokerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedUtc");

                    b.Property<string>("InsuredId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PaymentMethod");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PaymentStatus");

                    b.Property<DateTime?>("PaymentUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("PaymentUtc");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Reference");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Remark");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("TotalAmount");

                    b.HasKey("Id");

                    b.HasIndex("BrokerId");

                    b.HasIndex("InsuredId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Gibs.Domain.Entities.Policy", b =>
                {
                    b.Property<string>("PolicyNo")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("PolicyId");

                    b.Property<string>("AgentId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("AgentId");

                    b.Property<string>("ApprovedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ApprovedBy");

                    b.Property<DateTime?>("ApprovedUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("ApprovedUtc");

                    b.Property<string>("BrokerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChannelId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ChannelId");

                    b.Property<decimal>("Commission")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Commision");

                    b.Property<DateTime>("CreatedUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDate");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("EndDate");

                    b.Property<string>("FxCurrency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FxCurrency");

                    b.Property<decimal>("FxRate")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("FxRate");

                    b.Property<decimal>("GrossPremium")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("GrossPremium");

                    b.Property<string>("InsuredId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("InsuredName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<string>("InsurerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("StartDate");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Status");

                    b.Property<string>("SubChannelId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SubChannelId");

                    b.Property<decimal>("SumInsured")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("SumInsured");

                    b.HasKey("PolicyNo");

                    b.HasIndex("BrokerId");

                    b.HasIndex("InsuredId");

                    b.HasIndex("InsurerId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("Policies", (string)null);
                });

            modelBuilder.Entity("Gibs.Domain.Entities.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ProductId");

                    b.Property<string>("BrokerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClassId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ClassId");

                    b.Property<string>("MidClassId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MidClassId");

                    b.Property<string>("NaiconTypeId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NaiconTypeId");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ShortName");

                    b.HasKey("Id");

                    b.HasIndex("BrokerId");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Gibs.Domain.Entities.Underwriter", b =>
                {
                    b.Property<string>("InsurerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BrokerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApiKey1Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ApiKey1Username");

                    b.Property<string>("ApiKey2Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ApiKey2Password");

                    b.Property<string>("MappedFields")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MappedFields");

                    b.HasKey("InsurerId", "BrokerId");

                    b.HasIndex("BrokerId");

                    b.ToTable("Underwriters", (string)null);
                });

            modelBuilder.Entity("Gibs.Domain.Entities.Broker", b =>
                {
                    b.OwnsOne("Gibs.Domain.Entities.KycInfo", "Kyc", b1 =>
                        {
                            b1.Property<string>("BrokerId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateOnly?>("ExpiryDate")
                                .HasColumnType("date")
                                .HasColumnName("ExpiryDate");

                            b1.Property<DateOnly?>("IssueDate")
                                .HasColumnType("date")
                                .HasColumnName("IssueDate");

                            b1.Property<string>("KycNumber")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("KycNumber");

                            b1.Property<string>("KycType")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("KycType");

                            b1.Property<string>("TaxNumber")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TaxNumber");

                            b1.HasKey("BrokerId");

                            b1.ToTable("Brokers");

                            b1.WithOwner()
                                .HasForeignKey("BrokerId");
                        });

                    b.Navigation("Kyc");
                });

            modelBuilder.Entity("Gibs.Domain.Entities.Insured", b =>
                {
                    b.HasOne("Gibs.Domain.Entities.Broker", null)
                        .WithMany("Insureds")
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("Gibs.Domain.Entities.KycInfo", "Kyc", b1 =>
                        {
                            b1.Property<string>("InsuredId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateOnly?>("ExpiryDate")
                                .HasColumnType("date")
                                .HasColumnName("ExpiryDate");

                            b1.Property<DateOnly?>("IssueDate")
                                .HasColumnType("date")
                                .HasColumnName("IssueDate");

                            b1.Property<string>("KycNumber")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("KycNumber");

                            b1.Property<string>("KycType")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("KycType");

                            b1.Property<string>("TaxNumber")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TaxNumber");

                            b1.HasKey("InsuredId");

                            b1.ToTable("Insureds");

                            b1.WithOwner()
                                .HasForeignKey("InsuredId");
                        });

                    b.Navigation("Kyc");
                });

            modelBuilder.Entity("Gibs.Domain.Entities.Order", b =>
                {
                    b.HasOne("Gibs.Domain.Entities.Broker", null)
                        .WithMany("Orders")
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gibs.Domain.Entities.Insured", "Insured")
                        .WithMany()
                        .HasForeignKey("InsuredId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Insured");
                });

            modelBuilder.Entity("Gibs.Domain.Entities.Policy", b =>
                {
                    b.HasOne("Gibs.Domain.Entities.Broker", null)
                        .WithMany("Policies")
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gibs.Domain.Entities.Insured", "Insured")
                        .WithMany()
                        .HasForeignKey("InsuredId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gibs.Domain.Entities.Insurer", "Insurer")
                        .WithMany()
                        .HasForeignKey("InsurerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Gibs.Domain.Entities.Order", null)
                        .WithMany("Policies")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gibs.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Insured");

                    b.Navigation("Insurer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Gibs.Domain.Entities.Product", b =>
                {
                    b.HasOne("Gibs.Domain.Entities.Broker", null)
                        .WithMany("Products")
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Gibs.Domain.Entities.Underwriter", b =>
                {
                    b.HasOne("Gibs.Domain.Entities.Broker", null)
                        .WithMany("Underwriters")
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gibs.Domain.Entities.Insurer", "Insurer")
                        .WithMany()
                        .HasForeignKey("InsurerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Insurer");
                });

            modelBuilder.Entity("Gibs.Domain.Entities.Broker", b =>
                {
                    b.Navigation("Insureds");

                    b.Navigation("Orders");

                    b.Navigation("Policies");

                    b.Navigation("Products");

                    b.Navigation("Underwriters");
                });

            modelBuilder.Entity("Gibs.Domain.Entities.Order", b =>
                {
                    b.Navigation("Policies");
                });
#pragma warning restore 612, 618
        }
    }
}