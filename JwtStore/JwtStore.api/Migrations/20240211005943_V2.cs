using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtStore.api.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailVerificationVerificationAt",
                table: "User",
                newName: "EmailVerificationVerifiedAt");

            migrationBuilder.RenameColumn(
                name: "EmailVerificationExpiredAt",
                table: "User",
                newName: "EmailVerificationExpiresAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailVerificationVerifiedAt",
                table: "User",
                newName: "EmailVerificationVerificationAt");

            migrationBuilder.RenameColumn(
                name: "EmailVerificationExpiresAt",
                table: "User",
                newName: "EmailVerificationExpiredAt");
        }
    }
}
