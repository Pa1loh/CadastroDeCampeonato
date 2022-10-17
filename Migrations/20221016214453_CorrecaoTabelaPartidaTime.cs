using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroDeCampeonato.Migrations
{
    public partial class CorrecaoTabelaPartidaTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_partida_time_timeId",
                table: "partida");

            migrationBuilder.DropForeignKey(
                name: "FK_partidaTime_partida_partidaId",
                table: "partidaTime");

            migrationBuilder.DropIndex(
                name: "IX_partidaTime_partidaId",
                table: "partidaTime");

            migrationBuilder.DropColumn(
                name: "partidaId",
                table: "partidaTime");

            migrationBuilder.RenameColumn(
                name: "timeId",
                table: "partida",
                newName: "partidaTimeId");

            migrationBuilder.RenameIndex(
                name: "IX_partida_timeId",
                table: "partida",
                newName: "IX_partida_partidaTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_partida_partidaTime_partidaTimeId",
                table: "partida",
                column: "partidaTimeId",
                principalTable: "partidaTime",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_partida_partidaTime_partidaTimeId",
                table: "partida");

            migrationBuilder.RenameColumn(
                name: "partidaTimeId",
                table: "partida",
                newName: "timeId");

            migrationBuilder.RenameIndex(
                name: "IX_partida_partidaTimeId",
                table: "partida",
                newName: "IX_partida_timeId");

            migrationBuilder.AddColumn<int>(
                name: "partidaId",
                table: "partidaTime",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_partidaTime_partidaId",
                table: "partidaTime",
                column: "partidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_partida_time_timeId",
                table: "partida",
                column: "timeId",
                principalTable: "time",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_partidaTime_partida_partidaId",
                table: "partidaTime",
                column: "partidaId",
                principalTable: "partida",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
