using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.Data.Migrations
{
    public partial class modigyNameHorse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Samurais_Horses_HorseefcoreId",
                table: "Samurais");

            migrationBuilder.DropIndex(
                name: "IX_Samurais_HorseefcoreId",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "HorseefcoreId",
                table: "Samurais");

            migrationBuilder.AddColumn<int>(
                name: "HorseId",
                table: "Samurais",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Samurais_HorseId",
                table: "Samurais",
                column: "HorseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Samurais_Horses_HorseId",
                table: "Samurais",
                column: "HorseId",
                principalTable: "Horses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Samurais_Horses_HorseId",
                table: "Samurais");

            migrationBuilder.DropIndex(
                name: "IX_Samurais_HorseId",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "HorseId",
                table: "Samurais");

            migrationBuilder.AddColumn<int>(
                name: "HorseefcoreId",
                table: "Samurais",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Samurais_HorseefcoreId",
                table: "Samurais",
                column: "HorseefcoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Samurais_Horses_HorseefcoreId",
                table: "Samurais",
                column: "HorseefcoreId",
                principalTable: "Horses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
