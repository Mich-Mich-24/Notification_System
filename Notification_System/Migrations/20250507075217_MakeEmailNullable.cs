using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notification_System.Migrations
{
    /// <inheritdoc />
    public partial class MakeEmailNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the index on PhoneNumber
            migrationBuilder.DropIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users");

            // Drop the index on Email
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            // Alter the PhoneNumber column to make it nullable
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            // Alter the PushId column to make it nullable
            migrationBuilder.AlterColumn<string>(
                name: "PushId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            // Alter the Email column to make it nullable
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            // Recreate the index on PhoneNumber with a filter for non-NULL values
            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            // Recreate the index on Email with a filter for non-NULL values
            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            // Recreate the index on PushId with a filter for non-NULL values
            migrationBuilder.CreateIndex(
                name: "IX_Users_PushId",
                table: "Users",
                column: "PushId",
                unique: true,
                filter: "[PushId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the index on PhoneNumber
            migrationBuilder.DropIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users");

            // Drop the index on Email
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            // Drop the index on PushId
            migrationBuilder.DropIndex(
                name: "IX_Users_PushId",
                table: "Users");

            // Revert the PhoneNumber column to NOT NULL
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            // Revert the PushId column to NOT NULL
            migrationBuilder.AlterColumn<string>(
                name: "PushId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            // Revert the Email column to NOT NULL
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            // Recreate the index on PhoneNumber
            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);

            // Recreate the index on Email
            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            // Recreate the index on PushId
            migrationBuilder.CreateIndex(
                name: "IX_Users_PushId",
                table: "Users",
                column: "PushId",
                unique: true);
        }
    }
}