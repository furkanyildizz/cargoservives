using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace CustomerAddress.DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    CityRegion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Cdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Mdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Cuser = table.Column<int>(type: "int", nullable: false),
                    Muser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TownName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Cdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Mdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Cuser = table.Column<int>(type: "int", nullable: false),
                    Muser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Towns_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Neighborhoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NeighborhoodName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TownId = table.Column<int>(type: "int", nullable: false),
                    Cdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Mdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Cuser = table.Column<int>(type: "int", nullable: false),
                    Muser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighborhoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Neighborhoods_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    StreetName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    NeighborhoodId = table.Column<int>(type: "int", nullable: false),
                    Cdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Mdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Cuser = table.Column<int>(type: "int", nullable: false),
                    Muser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Streets_Neighborhoods_NeighborhoodId",
                        column: x => x.NeighborhoodId,
                        principalTable: "Neighborhoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    StreetId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    TownId = table.Column<int>(type: "int", nullable: false),
                    NeighborhoodId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Cdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Mdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Cuser = table.Column<int>(type: "int", nullable: false),
                    Muser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Neighborhoods_NeighborhoodId",
                        column: x => x.NeighborhoodId,
                        principalTable: "Neighborhoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyBranches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    BranchName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Cdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Mdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Cuser = table.Column<int>(type: "int", nullable: false),
                    Muser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyBranches_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CustomerName = table.Column<string>(type: "varchar(155)", maxLength: 155, nullable: true),
                    CustomerSurname = table.Column<string>(type: "varchar(155)", maxLength: 155, nullable: true),
                    TelNumber = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    Mail = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CustomerType = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Cdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Mdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Cuser = table.Column<int>(type: "int", nullable: false),
                    Muser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EmployeeName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    EmployeeSurname = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    CompanyBranchId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Cdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Mdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Cuser = table.Column<int>(type: "int", nullable: false),
                    Muser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_CompanyBranches_CompanyBranchId",
                        column: x => x.CompanyBranchId,
                        principalTable: "CompanyBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PostCode = table.Column<byte[]>(type: "varbinary(13)", maxLength: 13, nullable: false),
                    PostStatus = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    CompanyBranchId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Cdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Mdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Cuser = table.Column<int>(type: "int", nullable: false),
                    Muser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_CompanyBranches_CompanyBranchId",
                        column: x => x.CompanyBranchId,
                        principalTable: "CompanyBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Customers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Customers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_NeighborhoodId",
                table: "Addresses",
                column: "NeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StreetId",
                table: "Addresses",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_TownId",
                table: "Addresses",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBranches_AddressId",
                table: "CompanyBranches",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressId",
                table: "Customers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyBranchId",
                table: "Employees",
                column: "CompanyBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhoods_TownId",
                table: "Neighborhoods",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CompanyBranchId",
                table: "Posts",
                column: "CompanyBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_EmployeeId",
                table: "Posts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ReceiverId",
                table: "Posts",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SenderId",
                table: "Posts",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_NeighborhoodId",
                table: "Streets",
                column: "NeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_CityId",
                table: "Towns",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "CompanyBranches");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropTable(
                name: "Neighborhoods");

            migrationBuilder.DropTable(
                name: "Towns");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
