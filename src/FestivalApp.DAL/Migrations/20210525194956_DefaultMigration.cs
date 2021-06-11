using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FestivalApp.DAL.Migrations
{
    public partial class DefaultMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Performances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Performances_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Performances_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Bands",
                columns: new[] { "Id", "CountryOfOrigin", "Genre", "LongDescription", "Name", "Photo", "ShortDescription" },
                values: new object[,]
                {
                    { new Guid("021ba853-d557-49e8-166b-08d91df9dde1"), "USA", "Rock", "Nirvana was an American rock band formed in Aberdeen, Washington in 1987. Founded by lead singer and guitarist Kurt Cobain and bassist Krist Novoselic, the band went through a succession of drummers before recruiting Dave Grohl in 1990. Nirvana's success popularized alternative rock, and they were often referenced as the figurehead band of Generation X. Their music maintains a popular following and continues to influence modern rock and roll culture.", "Nirvana", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.billboard.com%2Farticles%2Fcolumns%2Fhip-hop%2F9494318%2Fjay-z-2020-playlist&psig=AOvVaw3TF59oZPxxxsOHFUtrfJaN&ust=1621455919748000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCKDFsMiV1PACFQAAAAAdAAAAABAD", "Culture Group" },
                    { new Guid("7073c336-588a-4193-ccb1-08d91dfb2c3a"), "USA", "Pop", "Shawn Corey Carter (born December 4, 1969), known professionally as Jay-Z (stylized as JAY-Z),[a] is an American rapper, songwriter, record executive, businessman, and record producer. He is widely regarded as one of the most influential hip-hop artists in history,[5] and often cited as one of the greatest rappers of all time.[6][7]", "Jay-Z", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fclassicalbumsundays.com%2Fnirvanas-nevermind-a-memory-replay-presented-colleen-cosmo-murphy%2F&psig=AOvVaw3Y8MjLEMygFfHWH1hV0Rhv&ust=1621455736225000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCLCcv--U1PACFQAAAAAdAAAAABAD", "Best-selled rapper" },
                    { new Guid("09ab0c58-21fd-4245-316f-08d91dfb8a09"), "USA", "Pop", "Billie Eilish Pirate Baird O'Connell (/ˈaɪlɪʃ/ EYE-lish;[1] born December 18, 2001) is an American singer and songwriter. She first gained attention in 2015 when she uploaded the song 'Ocean Eyes' to SoundCloud, which was subsequently released by the Interscope Records subsidiary Darkroom. The song was written and produced by her brother Finneas O'Connell, with whom she collaborates on music and live shows. Her debut EP, Don't Smile at Me (2017), became a sleeper hit, reaching the top 15 in the US, UK, Canada, and Australia.", "Billie Eilish", "https://en.wikipedia.org/wiki/File:Billie_Eilish_2019_by_Glenn_Francis_(cropped)_2.jpg", "Young nice singer" },
                    { new Guid("0d446d91-2474-492b-0a6a-08d91dfd4a6d"), "USA", "Raggy", "Something very serious.", "Lil Grandma", "https://frontiersinblog.files.wordpress.com/2020/04/frontiers-psychology-free-from-dance-alternative-interaction-grandchildren-grandparents.jpg?w=1024", "Rely short description" },
                    { new Guid("807d16e2-a65e-446d-ba7d-2adde9b7db71"), "CZ", "Metal", "Something very serious.", "OMFG", "https://frontiersinblog.files.wordpress.com/2020/04/frontiers-psychology-free-from-dance-alternative-interaction-grandchildren-grandparents.jpg?w=1024", "Rely short description" }
                });

            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "Id", "Capacity", "Description", "Name", "Photo" },
                values: new object[,]
                {
                    { new Guid("8a4fa0b6-2b26-42ce-2ce3-08d91d019f7c"), 100, "Stage on blue hills with best view", "Blue hills", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.reddit.com%2Fr%2Fboston%2Fcomments%2F9s6o12%2Ffall_colors_at_blue_hills_reservation%2F&psig=AOvVaw0QkcOISxAXYBsWZRFs-JOB&ust=1621456641044000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCJC4kqaY1PACFQAAAAAdAAAAABAD" },
                    { new Guid("b6345eaa-ecec-42de-a3b8-e16a76bb171d"), 100, "Letnany in Prague", "Letnany", "https://www.google.com/url?sa=i&url=http%3A%2F%2Fwww.pilotinfo.cz%2Fz-letist%2Fceska-republika%2Fjak-se-dela-propaganda-proti-letisti-praha-letnany&psig=AOvVaw18otsYEpIRFlN6krPFBg95&ust=1621456840456000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCLif9_6Y1PACFQAAAAAdAAAAABAK" },
                    { new Guid("99b333d5-20b3-418f-0a5e-08d91dfb2c3b"), 100, "Stage3", "Stage3", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fvysilackymilin.cz%2Fdetail-produktu%2Fautoradia%2Freproduktory%2Fjbl%2F15595_reproduktory-jbl-stage3-427%2F&psig=AOvVaw3TH_XTLko-9dPqaErc05z3&ust=1621613902359000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCLC6pY3i2PACFQAAAAAdAAAAABAD" }
                });

            migrationBuilder.InsertData(
                table: "Performances",
                columns: new[] { "Id", "BandId", "From", "StageId", "To" },
                values: new object[,]
                {
                    { new Guid("77a6f205-992e-4bc9-e461-08d91df9dde0"), new Guid("021ba853-d557-49e8-166b-08d91df9dde1"), new DateTime(2021, 2, 1, 21, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a4fa0b6-2b26-42ce-2ce3-08d91d019f7c"), new DateTime(2021, 2, 1, 22, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3c2c1081-97e9-4b32-0d97-08d91dfb8a09"), new Guid("7073c336-588a-4193-ccb1-08d91dfb2c3a"), new DateTime(2021, 3, 1, 12, 59, 0, 0, DateTimeKind.Unspecified), new Guid("8a4fa0b6-2b26-42ce-2ce3-08d91d019f7c"), new DateTime(2021, 3, 1, 14, 21, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7e1d51a8-6840-44de-96ab-08d91dfb2c3a"), new Guid("021ba853-d557-49e8-166b-08d91df9dde1"), new DateTime(2021, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b6345eaa-ecec-42de-a3b8-e16a76bb171d"), new DateTime(2021, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e6ec7542-aad4-466e-a44a-1ba8400bc60e"), new Guid("09ab0c58-21fd-4245-316f-08d91dfb8a09"), new DateTime(2021, 3, 10, 12, 59, 0, 0, DateTimeKind.Unspecified), new Guid("b6345eaa-ecec-42de-a3b8-e16a76bb171d"), new DateTime(2021, 3, 10, 14, 21, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Performances_BandId",
                table: "Performances",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_StageId",
                table: "Performances",
                column: "StageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Performances");

            migrationBuilder.DropTable(
                name: "Bands");

            migrationBuilder.DropTable(
                name: "Stages");
        }
    }
}
