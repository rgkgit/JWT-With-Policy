using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoSync.EFC.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(nullable: true),
                    AuditDetailId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(nullable: false),
                    DeviceId = table.Column<string>(nullable: true),
                    SupervisorId = table.Column<long>(nullable: false),
                    FolderFilePath = table.Column<string>(nullable: true),
                    AutoSyncTime = table.Column<string>(nullable: true),
                    AutoSyncDays = table.Column<string>(nullable: true),
                    AutoDeleteInterval = table.Column<int>(nullable: true),
                    AuditDetailId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    AuditDetailId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    AuditDetailId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AuditDetailId", "RoleName" },
                values: new object[,]
                {
                    { 1L, null, "SuperAdmin" },
                    { 2L, null, "Admin" },
                    { 3L, null, "Supervisor" },
                    { 4L, null, "Quality Checker" },
                    { 5L, null, "Technician" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AuditDetailId", "IsActive", "Password", "Username" },
                values: new object[] { 1L, null, true, "superadmin@123", "superadmin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "AuditDetailId", "RoleId", "UserId" },
                values: new object[] { 1L, null, 1L, 1L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
