using Microsoft.EntityFrameworkCore.Migrations;

namespace FestivalApp.DAL.Migrations
{
    public partial class DefaultMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Performances_Bands_BandId",
                table: "Performances");

            migrationBuilder.DropForeignKey(
                name: "FK_Performances_Stages_StageId",
                table: "Performances");

            migrationBuilder.AddForeignKey(
                name: "FK_Performances_Bands_BandId",
                table: "Performances",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Performances_Stages_StageId",
                table: "Performances",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Performances_Bands_BandId",
                table: "Performances");

            migrationBuilder.DropForeignKey(
                name: "FK_Performances_Stages_StageId",
                table: "Performances");

            migrationBuilder.AddForeignKey(
                name: "FK_Performances_Bands_BandId",
                table: "Performances",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Performances_Stages_StageId",
                table: "Performances",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
