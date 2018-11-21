using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkYourLaundry.Migrations
{
    public partial class AddUniqueIndices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invitations_GroupOwnerId",
                table: "Invitations");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LaundryTemplates",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LaundryTemplates_Name_UserId",
                table: "LaundryTemplates",
                columns: new[] { "Name", "UserId" },
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_GroupOwnerId_InvitedUserId",
                table: "Invitations",
                columns: new[] { "GroupOwnerId", "InvitedUserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_LaundryTemplates_Name_UserId",
                table: "LaundryTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_GroupOwnerId_InvitedUserId",
                table: "Invitations");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LaundryTemplates",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_GroupOwnerId",
                table: "Invitations",
                column: "GroupOwnerId");
        }
    }
}
