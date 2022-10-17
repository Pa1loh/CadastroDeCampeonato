using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroDeCampeonato.Migrations
{
    public partial class CorrecaoTabelaPartida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_partida_partidaTime_partidaTimeId",
                table: "partida");

            migrationBuilder.DropIndex(
                name: "IX_partida_partidaTimeId",
                table: "partida");

            migrationBuilder.DropColumn(
                name: "partidaTimeId",
                table: "partida");

            migrationBuilder.AddColumn<int>(
                name: "PartidaId",
                table: "partidaTime",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_partidaTime_PartidaId",
                table: "partidaTime",
                column: "PartidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_partidaTime_partida_PartidaId",
                table: "partidaTime",
                column: "PartidaId",
                principalTable: "partida",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_partidaTime_partida_PartidaId",
                table: "partidaTime");

            migrationBuilder.DropIndex(
                name: "IX_partidaTime_PartidaId",
                table: "partidaTime");

            migrationBuilder.DropColumn(
                name: "PartidaId",
                table: "partidaTime");

            migrationBuilder.AddColumn<int>(
                name: "partidaTimeId",
                table: "partida",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_partida_partidaTimeId",
                table: "partida",
                column: "partidaTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_partida_partidaTime_partidaTimeId",
                table: "partida",
                column: "partidaTimeId",
                principalTable: "partidaTime",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
