using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerAddress.DAL.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PostCode",
                table: "Posts",
                type: "varchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(13)",
                oldMaxLength: 13);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PostCode",
                table: "Posts",
                type: "varbinary(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(13)",
                oldMaxLength: 13);
        }
    }
}
