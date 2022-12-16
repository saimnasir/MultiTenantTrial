using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiTenantApp.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Passes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tenant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Passes",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Kind", "Name", "Tenant", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("5afc5d78-a7c3-89e7-629a-3a08275db903"), DateTime.Now, false, "Dog", "Samson", "TK1", DateTime.Now },
                    { new Guid("6498e94b-8e27-09df-76eb-3a08275db903"), DateTime.Now, false, "Cat", "Mr. Bigglesworth", "TK1", DateTime.Now },
                    { new Guid("ee6b7029-03d7-517a-1e97-3a08275db903"), DateTime.Now, false, "Dog", "Guinness", "TK2", DateTime.Now },
                    { new Guid("fa77153d-11b8-e783-ee41-3a08275db903"), DateTime.Now, false, "Cat", "Grumpy Cat", "TK2", DateTime.Now }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passes");
        }
    }
}
