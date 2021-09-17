using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace CustomerAddress.DAL.Migrations
{
    public partial class anew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Cdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Mdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Cuser = table.Column<int>(type: "int", nullable: false),
                    Muser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RouteMaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    CompanyBranchId = table.Column<int>(type: "int", nullable: false),
                    Cdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Mdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Cuser = table.Column<int>(type: "int", nullable: false),
                    Muser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteMaps_CompanyBranches_CompanyBranchId",
                        column: x => x.CompanyBranchId,
                        principalTable: "CompanyBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteMaps_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteMaps_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteMaps_CompanyBranchId",
                table: "RouteMaps",
                column: "CompanyBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteMaps_PostId",
                table: "RouteMaps",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteMaps_StatusId",
                table: "RouteMaps",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteMaps");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}
