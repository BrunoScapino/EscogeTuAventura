using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AjustarRelacionesHistorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Historias_HistoriaId",
                table: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_HistoriaId",
                table: "Categorias");

            migrationBuilder.AlterColumn<int>(
                name: "CapituloInicialId",
                table: "Historias",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "HistoriaCategoria",
                columns: table => new
                {
                    CategoriasId = table.Column<int>(type: "int", nullable: false),
                    HistoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriaCategoria", x => new { x.CategoriasId, x.HistoriaId });
                    table.ForeignKey(
                        name: "FK_HistoriaCategoria_Categorias_CategoriasId",
                        column: x => x.CategoriasId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoriaCategoria_Historias_HistoriaId",
                        column: x => x.HistoriaId,
                        principalTable: "Historias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoriaCategoria_HistoriaId",
                table: "HistoriaCategoria",
                column: "HistoriaId");

            // Conservamos las asociaciones existentes en la nueva tabla.
            migrationBuilder.Sql(@"
                INSERT INTO HistoriaCategoria (CategoriasId, HistoriaId)
                SELECT Id, HistoriaId
                FROM Categorias
                WHERE HistoriaId IS NOT NULL;
            ");

            // Eliminamos la columna antigua después de copiar los datos.
            migrationBuilder.DropColumn(
                name: "HistoriaId",
                table: "Categorias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoriaCategoria");

            migrationBuilder.AlterColumn<int>(
                name: "CapituloInicialId",
                table: "Historias",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HistoriaId",
                table: "Categorias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_HistoriaId",
                table: "Categorias",
                column: "HistoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Historias_HistoriaId",
                table: "Categorias",
                column: "HistoriaId",
                principalTable: "Historias",
                principalColumn: "Id");
        }
    }
}
