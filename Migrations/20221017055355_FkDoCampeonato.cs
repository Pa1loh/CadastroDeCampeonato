using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroDeCampeonato.Migrations
{
    public partial class FkDoCampeonato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_partidaTime_campeonato_campeonatoId",
                table: "partidaTime"); 

            migrationBuilder.AddForeignKey(
                name: "FK_partidaTime_campeonato_CampeonatoId",
                table: "partidaTime",
                column: "CampeonatoId",
                principalTable: "campeonato",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_partidaTime_campeonato_CampeonatoId",
                table: "partidaTime");

            migrationBuilder.AddForeignKey(
                name: "FK_partidaTime_campeonato_campeonatoId",
                table: "partidaTime",
                column: "campeonatoId",
                principalTable: "campeonato",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
