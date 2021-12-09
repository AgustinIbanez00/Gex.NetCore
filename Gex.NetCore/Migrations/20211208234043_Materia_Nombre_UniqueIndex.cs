using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gex.Migrations
{
    public partial class Materia_Nombre_UniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 346628690L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 360029765L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 415371795L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 651374082L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 665841109L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 703902824L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 924128811L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 1153585286L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 1241054937L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 1373808374L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 1588826795L);

            migrationBuilder.InsertData(
                table: "materias",
                columns: new[] { "id", "created_at", "estado", "nombre", "tipo", "updated_at" },
                values: new object[,]
                {
                    { 27571943L, new DateTime(2021, 12, 8, 20, 40, 43, 355, DateTimeKind.Local).AddTicks(705), 1, "claudia_strosin", 2, new DateTime(2021, 12, 8, 20, 40, 43, 355, DateTimeKind.Local).AddTicks(703) },
                    { 85214850L, new DateTime(2021, 12, 8, 20, 40, 43, 352, DateTimeKind.Local).AddTicks(9456), 1, "deangelo.kirlin", 2, new DateTime(2021, 12, 8, 20, 40, 43, 352, DateTimeKind.Local).AddTicks(9450) },
                    { 213094214L, new DateTime(2021, 12, 8, 20, 40, 43, 355, DateTimeKind.Local).AddTicks(8518), 1, "sadye", 1, new DateTime(2021, 12, 8, 20, 40, 43, 355, DateTimeKind.Local).AddTicks(8515) },
                    { 239772803L, new DateTime(2021, 12, 8, 20, 40, 43, 354, DateTimeKind.Local).AddTicks(8064), 1, "della", 0, new DateTime(2021, 12, 8, 20, 40, 43, 354, DateTimeKind.Local).AddTicks(8060) },
                    { 301407982L, new DateTime(2021, 12, 8, 20, 40, 43, 352, DateTimeKind.Local).AddTicks(3900), 1, "henri", 2, new DateTime(2021, 12, 8, 20, 40, 43, 352, DateTimeKind.Local).AddTicks(3891) },
                    { 535118122L, new DateTime(2021, 12, 8, 20, 40, 43, 350, DateTimeKind.Local).AddTicks(4099), 1, "laurine_brekke", 0, new DateTime(2021, 12, 8, 20, 40, 43, 350, DateTimeKind.Local).AddTicks(4086) },
                    { 972487296L, new DateTime(2021, 12, 8, 20, 40, 43, 352, DateTimeKind.Local).AddTicks(6989), 1, "bethel", 2, new DateTime(2021, 12, 8, 20, 40, 43, 352, DateTimeKind.Local).AddTicks(6982) },
                    { 998819174L, new DateTime(2021, 12, 8, 20, 40, 43, 350, DateTimeKind.Local).AddTicks(6547), 1, "amelia", 2, new DateTime(2021, 12, 8, 20, 40, 43, 350, DateTimeKind.Local).AddTicks(6545) },
                    { 1054938514L, new DateTime(2021, 12, 8, 20, 40, 43, 351, DateTimeKind.Local).AddTicks(3810), 1, "lydia_hamill", 0, new DateTime(2021, 12, 8, 20, 40, 43, 351, DateTimeKind.Local).AddTicks(3809) },
                    { 1086299584L, new DateTime(2021, 12, 8, 20, 40, 43, 354, DateTimeKind.Local).AddTicks(5621), 1, "jean", 0, new DateTime(2021, 12, 8, 20, 40, 43, 354, DateTimeKind.Local).AddTicks(5617) },
                    { 1160699221L, new DateTime(2021, 12, 8, 20, 40, 43, 351, DateTimeKind.Local).AddTicks(12), 1, "colton.kling", 2, new DateTime(2021, 12, 8, 20, 40, 43, 351, DateTimeKind.Local).AddTicks(9) },
                    { 1271665876L, new DateTime(2021, 12, 8, 20, 40, 43, 350, DateTimeKind.Local).AddTicks(8226), 1, "ramiro", 2, new DateTime(2021, 12, 8, 20, 40, 43, 350, DateTimeKind.Local).AddTicks(8224) },
                    { 1310171986L, new DateTime(2021, 12, 8, 20, 40, 43, 354, DateTimeKind.Local).AddTicks(581), 1, "arch.gusikowski", 2, new DateTime(2021, 12, 8, 20, 40, 43, 354, DateTimeKind.Local).AddTicks(570) },
                    { 1332610653L, new DateTime(2021, 12, 8, 20, 40, 43, 351, DateTimeKind.Local).AddTicks(5777), 1, "lynn", 1, new DateTime(2021, 12, 8, 20, 40, 43, 351, DateTimeKind.Local).AddTicks(5776) },
                    { 1372973313L, new DateTime(2021, 12, 8, 20, 40, 43, 351, DateTimeKind.Local).AddTicks(9468), 1, "dave.lindgren", 0, new DateTime(2021, 12, 8, 20, 40, 43, 351, DateTimeKind.Local).AddTicks(9468) },
                    { 1548696203L, new DateTime(2021, 12, 8, 20, 40, 43, 351, DateTimeKind.Local).AddTicks(7553), 1, "katrina_swift", 0, new DateTime(2021, 12, 8, 20, 40, 43, 351, DateTimeKind.Local).AddTicks(7551) },
                    { 1584935721L, new DateTime(2021, 12, 8, 20, 40, 43, 355, DateTimeKind.Local).AddTicks(6121), 1, "brad", 0, new DateTime(2021, 12, 8, 20, 40, 43, 355, DateTimeKind.Local).AddTicks(6117) },
                    { 1636131673L, new DateTime(2021, 12, 8, 20, 40, 43, 351, DateTimeKind.Local).AddTicks(2031), 1, "hester", 0, new DateTime(2021, 12, 8, 20, 40, 43, 351, DateTimeKind.Local).AddTicks(2026) },
                    { 1956020206L, new DateTime(2021, 12, 8, 20, 40, 43, 355, DateTimeKind.Local).AddTicks(3639), 1, "magali", 1, new DateTime(2021, 12, 8, 20, 40, 43, 355, DateTimeKind.Local).AddTicks(3634) },
                    { 2114238936L, new DateTime(2021, 12, 8, 20, 40, 43, 354, DateTimeKind.Local).AddTicks(3265), 1, "shanna", 0, new DateTime(2021, 12, 8, 20, 40, 43, 354, DateTimeKind.Local).AddTicks(3263) }
                });

            migrationBuilder.CreateIndex(
                name: "ix_materias_nombre",
                table: "materias",
                column: "nombre",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_materias_nombre",
                table: "materias");

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 27571943L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 85214850L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 213094214L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 239772803L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 301407982L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 535118122L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 972487296L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 998819174L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 1054938514L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 1086299584L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 1160699221L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 1271665876L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 1310171986L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 1332610653L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 1372973313L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 1548696203L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 1584935721L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 1636131673L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 1956020206L);

            migrationBuilder.DeleteData(
                table: "materias",
                keyColumn: "id",
                keyValue: 2114238936L);

            migrationBuilder.InsertData(
                table: "materias",
                columns: new[] { "id", "created_at", "estado", "nombre", "tipo", "updated_at" },
                values: new object[,]
                {
                    { 346628690L, new DateTime(2021, 12, 8, 18, 10, 12, 593, DateTimeKind.Local).AddTicks(1695), 1, "wilson_haley", 0, new DateTime(2021, 12, 8, 18, 10, 12, 593, DateTimeKind.Local).AddTicks(1694) },
                    { 360029765L, new DateTime(2021, 12, 8, 18, 10, 12, 593, DateTimeKind.Local).AddTicks(4517), 1, "lorenzo", 2, new DateTime(2021, 12, 8, 18, 10, 12, 593, DateTimeKind.Local).AddTicks(4513) },
                    { 415371795L, new DateTime(2021, 12, 8, 18, 10, 12, 592, DateTimeKind.Local).AddTicks(6570), 1, "valentina_ward", 0, new DateTime(2021, 12, 8, 18, 10, 12, 592, DateTimeKind.Local).AddTicks(6569) },
                    { 651374082L, new DateTime(2021, 12, 8, 18, 10, 12, 594, DateTimeKind.Local).AddTicks(272), 1, "roosevelt_ruecker", 1, new DateTime(2021, 12, 8, 18, 10, 12, 594, DateTimeKind.Local).AddTicks(271) },
                    { 665841109L, new DateTime(2021, 12, 8, 18, 10, 12, 592, DateTimeKind.Local).AddTicks(8446), 1, "hildegard", 1, new DateTime(2021, 12, 8, 18, 10, 12, 592, DateTimeKind.Local).AddTicks(8445) },
                    { 703902824L, new DateTime(2021, 12, 8, 18, 10, 12, 592, DateTimeKind.Local).AddTicks(4882), 1, "tyler", 0, new DateTime(2021, 12, 8, 18, 10, 12, 592, DateTimeKind.Local).AddTicks(4880) },
                    { 924128811L, new DateTime(2021, 12, 8, 18, 10, 12, 593, DateTimeKind.Local).AddTicks(104), 1, "anderson", 2, new DateTime(2021, 12, 8, 18, 10, 12, 593, DateTimeKind.Local).AddTicks(103) },
                    { 1153585286L, new DateTime(2021, 12, 8, 18, 10, 12, 593, DateTimeKind.Local).AddTicks(8326), 1, "mekhi.gaylord", 2, new DateTime(2021, 12, 8, 18, 10, 12, 593, DateTimeKind.Local).AddTicks(8324) },
                    { 1241054937L, new DateTime(2021, 12, 8, 18, 10, 12, 592, DateTimeKind.Local).AddTicks(2248), 1, "dario.mcglynn", 1, new DateTime(2021, 12, 8, 18, 10, 12, 592, DateTimeKind.Local).AddTicks(2236) },
                    { 1373808374L, new DateTime(2021, 12, 8, 18, 10, 12, 593, DateTimeKind.Local).AddTicks(6643), 1, "gerald", 2, new DateTime(2021, 12, 8, 18, 10, 12, 593, DateTimeKind.Local).AddTicks(6640) },
                    { 1588826795L, new DateTime(2021, 12, 8, 18, 10, 12, 594, DateTimeKind.Local).AddTicks(2181), 1, "easton.johns", 2, new DateTime(2021, 12, 8, 18, 10, 12, 594, DateTimeKind.Local).AddTicks(2180) }
                });
        }
    }
}
