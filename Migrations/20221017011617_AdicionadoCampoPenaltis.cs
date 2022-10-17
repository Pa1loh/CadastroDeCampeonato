using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroDeCampeonato.Migrations
{
    public partial class AdicionadoCampoPenaltis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "penaltisFeitos",
                table: "partidaTime",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "penaltisFeitos",
                table: "partidaTime");
        }
    }
}
