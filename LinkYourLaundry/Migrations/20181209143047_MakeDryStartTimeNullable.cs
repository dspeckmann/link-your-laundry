using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkYourLaundry.Migrations
{
    public partial class MakeDryStartTimeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DryStartTime",
                table: "ActiveLaundries",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DryStartTime",
                table: "ActiveLaundries",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
