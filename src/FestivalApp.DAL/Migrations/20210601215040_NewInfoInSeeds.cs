using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FestivalApp.DAL.Migrations
{
    public partial class NewInfoInSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: new Guid("021ba853-d557-49e8-166b-08d91df9dde1"),
                column: "Photo",
                value: "https://happymag.tv/wp-content/uploads/2018/01/earlynirvanahappy.jpg");

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: new Guid("09ab0c58-21fd-4245-316f-08d91dfb8a09"),
                column: "Photo",
                value: "https://cdn.myshoptet.com/usr/www.esuvenyry.cz/user/documents/upload/nirvana.jpg");

            migrationBuilder.UpdateData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: new Guid("8a4fa0b6-2b26-42ce-2ce3-08d91d019f7c"),
                column: "Photo",
                value: "https://img1.10bestmedia.com/Images/Photos/366598/GettyImages-1093700094_55_660x440.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: new Guid("021ba853-d557-49e8-166b-08d91df9dde1"),
                column: "Photo",
                value: "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.billboard.com%2Farticles%2Fcolumns%2Fhip-hop%2F9494318%2Fjay-z-2020-playlist&psig=AOvVaw3TF59oZPxxxsOHFUtrfJaN&ust=1621455919748000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCKDFsMiV1PACFQAAAAAdAAAAABAD");

            migrationBuilder.UpdateData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: new Guid("09ab0c58-21fd-4245-316f-08d91dfb8a09"),
                column: "Photo",
                value: "https://en.wikipedia.org/wiki/File:Billie_Eilish_2019_by_Glenn_Francis_(cropped)_2.jpg");

            migrationBuilder.UpdateData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: new Guid("8a4fa0b6-2b26-42ce-2ce3-08d91d019f7c"),
                column: "Photo",
                value: "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.reddit.com%2Fr%2Fboston%2Fcomments%2F9s6o12%2Ffall_colors_at_blue_hills_reservation%2F&psig=AOvVaw0QkcOISxAXYBsWZRFs-JOB&ust=1621456641044000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCJC4kqaY1PACFQAAAAAdAAAAABAD");
        }
    }
}
