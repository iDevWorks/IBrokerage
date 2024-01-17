﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gibs.MultiBroker.Portal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brokers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BrokerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KycType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KycNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brokers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Insureds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsCorporate = table.Column<bool>(type: "bit", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirthOrReg = table.Column<DateOnly>(type: "date", nullable: false),
                    KycType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KycNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insureds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Insurers",
                columns: table => new
                {
                    InsurerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InsurerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApiAuthStyle = table.Column<int>(type: "int", nullable: false),
                    NaicomId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurers", x => x.InsurerId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClassId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MidClassId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NaiconTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Underwriters",
                columns: table => new
                {
                    MappedFields = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApiKey1Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiKey2Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrokerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InsurerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Underwriters", x => x.MappedFields);
                    table.ForeignKey(
                        name: "FK_Underwriters_Brokers_BrokerId",
                        column: x => x.BrokerId,
                        principalTable: "Brokers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Underwriters_Insurers_InsurerId",
                        column: x => x.InsurerId,
                        principalTable: "Insurers",
                        principalColumn: "InsurerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsuredId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Insureds_InsuredId",
                        column: x => x.InsuredId,
                        principalTable: "Insureds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    PolicyNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChannelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubChannelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FxCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SumInsured = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Commision = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InsuredId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnderwriterMappedFields = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.PolicyNo);
                    table.ForeignKey(
                        name: "FK_Policies_Insureds_InsuredId",
                        column: x => x.InsuredId,
                        principalTable: "Insureds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Policies_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Policies_Underwriters_UnderwriterMappedFields",
                        column: x => x.UnderwriterMappedFields,
                        principalTable: "Underwriters",
                        principalColumn: "MappedFields");
                });

            migrationBuilder.InsertData(
                table: "Brokers",
                columns: new[] { "Id", "BrokerName", "CreatedUtc", "Email", "FirstName", "IsActive", "LastName", "Password", "Phone", "RegistrationNo", "Title" },
                values: new object[] { "OMOMOWO_RELIANCE", "Omomowo Reliance", new DateTime(2024, 1, 15, 15, 47, 23, 923, DateTimeKind.Local).AddTicks(2633), "omomowosymeon45@gmail.com", "Omomowo", false, "Reliance", "6170F04B86312D18C0BE6EB0B0E9A2D2534DC1F6B51736840FB1B4645C3A882F55A062B37F76B85F8A207DEDAA6E67AE175E45C85A74EA14B49B8DDFA25BC8CA", "08095482981", "12345678", null });

            migrationBuilder.InsertData(
                table: "Insurers",
                columns: new[] { "InsurerId", "ApiAuthStyle", "CreatedUtc", "InsurerName", "IsActive", "NaicomId" },
                values: new object[] { "001", 2, new DateTime(2024, 1, 15, 14, 47, 23, 924, DateTimeKind.Utc).AddTicks(2497), "cornerstone", true, "naicomId" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ClassId", "MidClassId", "NaiconTypeId", "Name", "ShortName" },
                values: new object[,]
                {
                    { "1001", "classId", "", null, "third-party-insurance", "shortname" },
                    { "1002", "classId1", "", null, "third-party-insurance-2", "shortname2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_InsuredId",
                table: "Orders",
                column: "InsuredId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_InsuredId",
                table: "Policies",
                column: "InsuredId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_ProductId",
                table: "Policies",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_UnderwriterMappedFields",
                table: "Policies",
                column: "UnderwriterMappedFields");

            migrationBuilder.CreateIndex(
                name: "IX_Underwriters_BrokerId",
                table: "Underwriters",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_Underwriters_InsurerId",
                table: "Underwriters",
                column: "InsurerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "Insureds");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Underwriters");

            migrationBuilder.DropTable(
                name: "Brokers");

            migrationBuilder.DropTable(
                name: "Insurers");
        }
    }
}