using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoSync.EFC.Migrations
{
    public partial class AddTechnician : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "AuditDetailId", "AutoDeleteInterval", "AutoSyncDays", "AutoSyncTime", "DeviceId", "FolderFilePath", "SupervisorId", "UserId" },
                values: new object[,]
                {
                    { 1L, null, 15, "Sunday,Monday,Tuesday", "17:30", null, "D:\\Kishore\\PrimeMover", 2L, 3L },
                    { 2L, null, 15, "Sunday,Monday,Tuesday", "17:30", null, "D:\\Satheesh\\PrimeMover", 2L, 4L }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AuditDetailId", "IsActive", "Password", "Username" },
                values: new object[,]
                {
                    { 2L, null, true, "supervisor@123", "supervisor" },
                    { 3L, null, true, "kishore@123", "Kishore" },
                    { 4L, null, true, "satheesh@123", "Satheesh" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "AuditDetailId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 2L, null, 3L, 2L },
                    { 3L, null, 5L, 3L },
                    { 4L, null, 5L, 4L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4L);
        }
    }
}
