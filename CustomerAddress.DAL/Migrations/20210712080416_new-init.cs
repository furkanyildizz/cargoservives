using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerAddress.DAL.Migrations
{
    public partial class newinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ShippingFinishDate",
                table: "Posts",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippingStartDate",
                table: "Posts",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingFinishDate",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ShippingStartDate",
                table: "Posts");
        }
    }
}
