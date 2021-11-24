using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gex.NetCore.Migrations
{
    public partial class IndexUniqueComision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Comisiones_Nombre",
                table: "Comisiones",
                column: "Nombre",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comisiones_Nombre",
                table: "Comisiones");
        }
    }
}
