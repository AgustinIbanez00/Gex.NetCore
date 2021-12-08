using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gex.Migrations
{
    public partial class ModifyAutoryUpdatedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "last_update_at",
                table: "comisiones",
                newName: "updated_at");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "comisiones",
                newName: "last_update_at");
        }
    }
}
