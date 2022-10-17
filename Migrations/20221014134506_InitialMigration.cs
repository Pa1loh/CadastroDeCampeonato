using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroDeCampeonato.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "campeonato",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    inicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fim = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campeonato", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "time",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_time", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "campeonatoTime",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    campeonatoId = table.Column<int>(type: "INTEGER", nullable: false),
                    timeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campeonatoTime", x => x.id);
                    table.ForeignKey(
                        name: "FK_campeonatoTime_campeonato_campeonatoId",
                        column: x => x.campeonatoId,
                        principalTable: "campeonato",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_campeonatoTime_time_timeId",
                        column: x => x.timeId,
                        principalTable: "time",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "partida",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    campeonatoId = table.Column<int>(type: "INTEGER", nullable: false),
                    timeId = table.Column<int>(type: "INTEGER", nullable: false),
                    fasePartida = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partida", x => x.id);
                    table.ForeignKey(
                        name: "FK_partida_campeonato_campeonatoId",
                        column: x => x.campeonatoId,
                        principalTable: "campeonato",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_partida_time_timeId",
                        column: x => x.timeId,
                        principalTable: "time",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "partidaTime",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    partidaId = table.Column<int>(type: "INTEGER", nullable: false),
                    timeId = table.Column<int>(type: "INTEGER", nullable: false),
                    golsFeitos = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partidaTime", x => x.id);
                    table.ForeignKey(
                        name: "FK_partidaTime_partida_partidaId",
                        column: x => x.partidaId,
                        principalTable: "partida",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_partidaTime_time_timeId",
                        column: x => x.timeId,
                        principalTable: "time",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_campeonatoTime_campeonatoId",
                table: "campeonatoTime",
                column: "campeonatoId");

            migrationBuilder.CreateIndex(
                name: "IX_campeonatoTime_timeId",
                table: "campeonatoTime",
                column: "timeId");

            migrationBuilder.CreateIndex(
                name: "IX_partida_campeonatoId",
                table: "partida",
                column: "campeonatoId");

            migrationBuilder.CreateIndex(
                name: "IX_partida_timeId",
                table: "partida",
                column: "timeId");

            migrationBuilder.CreateIndex(
                name: "IX_partidaTime_partidaId",
                table: "partidaTime",
                column: "partidaId");

            migrationBuilder.CreateIndex(
                name: "IX_partidaTime_timeId",
                table: "partidaTime",
                column: "timeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "campeonatoTime");

            migrationBuilder.DropTable(
                name: "partidaTime");

            migrationBuilder.DropTable(
                name: "partida");

            migrationBuilder.DropTable(
                name: "campeonato");

            migrationBuilder.DropTable(
                name: "time");
        }
    }
}
